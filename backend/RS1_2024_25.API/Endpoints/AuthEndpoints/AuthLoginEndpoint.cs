using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;
using RS1_2024_25.API.Services;
using System.Threading;
using System.Threading.Tasks;

namespace RS1_2024_25.API.Endpoints.Auth
{
    [Route("auth")]
    [ApiController]
    public class AuthLoginEndpoint : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly MyAuthService _authService;
        private readonly PasswordHasherService _passwordHasherService;
        private readonly TwoFactorAuthService _twoFactorAuthService;

        public AuthLoginEndpoint(ApplicationDbContext db, MyAuthService authService, PasswordHasherService passwordHasherService, TwoFactorAuthService twoFactorAuthService)
        {
            _db = db;
            _authService = authService;
            _passwordHasherService = passwordHasherService;
            _twoFactorAuthService = twoFactorAuthService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> HandleAsync([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
        {
            var loggedInUser = await _db.Accounts
                .Include(a => a.TwoFactorAuth)
                .FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email, cancellationToken);

            if (loggedInUser == null)
            {
                return Unauthorized(new LoginResponse { Message = "Incorrect username or email." });
            }
            var passwordHasher = new PasswordHasher<Account>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(loggedInUser, loggedInUser.Password, request.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new LoginResponse { Message = "Incorrect password." });
            }

            // Enable 2FA for existing users if not set up
            if (loggedInUser.TwoFactorAuth == null)
            {
                var twoFactorAuth = new TwoFactorAuth
                {
                    AccountId = loggedInUser.AccountID,
                    AuthTokenHash = "",
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(5)
                };
                _db.TwoFactorAuths.Add(twoFactorAuth);
                await _db.SaveChangesAsync(cancellationToken);
                loggedInUser.TwoFactorAuth = twoFactorAuth;
            }

            Console.WriteLine($"Generating 2FA token for UserId: {loggedInUser.AccountID}");
            await _twoFactorAuthService.Generate2FAToken(loggedInUser.AccountID);
            return Ok(new LoginResponse
            {
                Message = "A verification code has been sent to your email.",
                AccountID = loggedInUser.AccountID,
            });
        }


        public class LoginRequest
        {
            public string? Username { get; set; }
            public string? Email { get; set; }
            public required string Password { get; set; }
        }

        public class LoginResponse
        {
            public string Message { get; set; }
            public int AccountID { get; set; }
        }
    }
}
