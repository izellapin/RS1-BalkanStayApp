using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RS1_2024_25.API.Data.Models.Auth;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.ViewModel;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data.Models;

namespace RS1_2024_25.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _DbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            var users = _DbContext.Users
                          .Include(x => x.City)
                            .ThenInclude(y => y.Country)
                          .Include(x => x.Gender)
                          .Include(x => x.Favorites)
                          .Include(x => x.Reservations)
                          .Include(x => x.Reviews)
                          .Include(x => x.OwnerReviews)
                          .Include(x => x.UserImages)
                            .ThenInclude(ui => ui.Image)
                          .ToList();

            if (users == null)
                return BadRequest();

            return Ok(users);
        }



        [HttpGet("{AccountId}")]
        public ActionResult<User> GetById(int AccountId)
        {
            var user = _DbContext.Users
                          .Include(x => x.City)
                            .ThenInclude(y => y.Country)
                          .Include(x => x.Gender)
                          .Include(x => x.Favorites)
                          .Include(x => x.Reservations)
                          .Include(x => x.Reviews)
                          .Include(x => x.OwnerReviews)
                          .Include(x => x.UserImages)
                            .ThenInclude(ui => ui.Image)
                          .FirstOrDefault(x => x.AccountID == AccountId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }



        [HttpDelete("{AccountId}")]
        public ActionResult Delete(int AccountId)
        {
            var user = _DbContext.Users.Find(AccountId);

            if (user == null)
                return BadRequest();

            _DbContext.Users.Remove(user);
            _DbContext.SaveChanges();

            return Ok();
        }



        [HttpPost]
        public ActionResult Insert(UserInsertVM userVM)
        {
            var newUser = new User
            {
                Username = userVM.Username,
                Email = userVM.Email,
                Password = userVM.Password,
                FirstName = userVM.FirstName,
                LastName = userVM.LastName,
                Phone = userVM.Phone,
                GenderID = userVM.GenderID,
                CityID = userVM.CityID,
                CreatedAt = DateTime.Now
            };

            _DbContext.Users.Add(newUser);
            _DbContext.SaveChanges();

            if (!string.IsNullOrEmpty(userVM.ImagePath))
            {
                var image = new Image { ImagePath = userVM.ImagePath };
                _DbContext.Images.Add(image);
                _DbContext.SaveChanges();

                var userImage = new UserImage
                {
                    AccountID = newUser.AccountID,
                    ImageID = image.ImageID
                };
                _DbContext.UserImages.Add(userImage);
                _DbContext.SaveChanges();
            }

            return Ok(newUser);
        }




        [HttpPut]
        public ActionResult Update(UserUpdateVM userVM)
        {
            var user = _DbContext.Users.Find(userVM.AccountID);

            if (user == null)
                return BadRequest();

            user.Username = userVM.Username;
            user.Email = userVM.Email;
            user.FirstName = userVM.FirstName;
            user.LastName = userVM.LastName;
            user.Phone = userVM.Phone;
            user.GenderID = userVM.GenderID;
            user.CityID = userVM.CityID;

            // Handle image update
            if (!string.IsNullOrEmpty(userVM.ImagePath))
            {
                // Create new image
                var image = new Image { ImagePath = userVM.ImagePath };
                _DbContext.Images.Add(image);
                _DbContext.SaveChanges();

                // Remove old image if exists
                var oldUserImage = _DbContext.UserImages
                    .FirstOrDefault(ui => ui.AccountID == user.AccountID);
                if (oldUserImage != null)
                {
                    _DbContext.UserImages.Remove(oldUserImage);
                }

                // Add new image
                var userImage = new UserImage
                {
                    AccountID = user.AccountID,
                    ImageID = image.ImageID
                };
                _DbContext.UserImages.Add(userImage);
            }

            _DbContext.Users.Update(user);
            _DbContext.SaveChanges();

            return Ok(user);
        }
    }
}
