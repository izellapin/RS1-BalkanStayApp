using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RS1_2024_25.API.Data.Models.Auth;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.ViewModel;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data.Models;
using System.IO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace RS1_2024_25.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _DbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        [HttpGet("Get")]
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

        [HttpGet("Get/{AccountId}")]
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

        [HttpPost("Insert")]
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

        [HttpPut("Update")]
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

            if (!string.IsNullOrEmpty(userVM.ImagePath))
            {
                var image = new Image { ImagePath = userVM.ImagePath };
                _DbContext.Images.Add(image);
                _DbContext.SaveChanges();

                var oldUserImage = _DbContext.UserImages
                    .FirstOrDefault(ui => ui.AccountID == user.AccountID);
                if (oldUserImage != null)
                {
                    _DbContext.UserImages.Remove(oldUserImage);
                }

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

        [HttpPost("UploadProfileImage")]
        [Consumes("multipart/form-data")]  // Specify content type
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadProfileImage(
            [FromForm] IFormFile file,
            [FromHeader(Name = "my-auth-token")] string token)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded");

                var userId = GetUserIdFromToken(token); 
                if (userId == null)
                    return BadRequest("Invalid token");

                var user = await _DbContext.Users
                    .Include(u => u.UserImages)
                    .FirstOrDefaultAsync(u => u.AccountID == userId);

                if (user == null)
                    return NotFound($"User not found");

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"profile_{userId}_{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

         
                var image = new Image
                {
                    ImagePath = $"/images/{fileName}"
                };
                _DbContext.Images.Add(image);
                await _DbContext.SaveChangesAsync();

                var oldUserImage = user.UserImages.FirstOrDefault();
                if (oldUserImage != null)
                {
                    _DbContext.UserImages.Remove(oldUserImage);
                }

                var userImage = new UserImage
                {
                    AccountID = userId.Value,
                    ImageID = image.ImageID
                };
                _DbContext.UserImages.Add(userImage);
                await _DbContext.SaveChangesAsync();

                return Ok(new { imagePath = image.ImagePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private int? GetUserIdFromToken(string token)
        {
            
            try
            {
              
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "userId");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
