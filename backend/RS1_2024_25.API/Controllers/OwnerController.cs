using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;
using RS1_2024_25.API.ViewModel;
using System.Numerics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RS1_2024_25.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OwnerController : ControllerBase
    {
        private readonly ApplicationDbContext _DbContext;


        public OwnerController(ApplicationDbContext _DbContext)
        {
            this._DbContext = _DbContext;
        }



        [HttpGet]

        public ActionResult<List<Owner>> Get()
        {
            var owners = _DbContext.Owners
                            .Include(x => x.City) 
                                .ThenInclude(y=>y.Country)
                            .Include(x => x.Gender) 
                            .Include(x => x.OwnerReviews) 
                            .Include(x => x.Apartments)
                            .Include(x => x.OwnerImages)
                                .ThenInclude(oi => oi.Image)
                            .ToList();

            if (owners == null)
            {
                return BadRequest();
            }


            return Ok(owners);
        }

        [HttpGet("{OwnerID}")]

        public ActionResult<Owner> GetById(int OwnerID)
        {
            var owners = _DbContext.Owners.Find(OwnerID);

            if (owners == null)
            {
                return BadRequest();
            }

            return Ok(owners);
        }




        [HttpDelete("{OwnerID}")]

        public ActionResult Delete(int OwnerID)
        {
            var owner = _DbContext.Owners.Find(OwnerID);

            if (owner == null)
            {
                return BadRequest();
            }

            _DbContext.Owners.Remove(owner);
            _DbContext.SaveChanges();

            return Ok();
        }




        [HttpPost] /// INSERT

        public ActionResult Insert(OwnerInsertVM x)
        {
            var newOwner = new Owner()
            {
                Username = x.Username,
                Email = x.Email,
                Password = x.Password,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone = x.Phone,
                GenderID = x.GenderID,
                CityID = x.CityID,
                CreatedAt = DateTime.Now
            };

            _DbContext.Owners.Add(newOwner);
            _DbContext.SaveChanges();

            // Add the image if provided
            if (!string.IsNullOrEmpty(x.ImagePath))
            {
                var image = new Image { ImagePath = x.ImagePath };
                _DbContext.Images.Add(image);
                _DbContext.SaveChanges();

                var ownerImage = new OwnerImage
                {
                    AccountID = newOwner.AccountID,
                    ImageID = image.ImageID
                };
                _DbContext.OwnerImages.Add(ownerImage);
                _DbContext.SaveChanges();
            }

            return Ok(newOwner);
        }




        [HttpPut] ///UPDATE

        public ActionResult Update(OwnerUpdateVM x)
        {
            var updatedOwner = _DbContext.Owners.Find(x.AccountID);

            if (updatedOwner == null)
            {
                return BadRequest();
            }

            updatedOwner.FirstName = x.FirstName;
            updatedOwner.LastName = x.LastName;
            updatedOwner.Email = x.Email;
            updatedOwner.Phone = x.Phone;

            _DbContext.Owners.Update(updatedOwner);
            _DbContext.SaveChanges();

            return Ok(updatedOwner);
        }
    }
}

