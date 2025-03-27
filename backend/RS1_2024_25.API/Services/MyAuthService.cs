using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;
using RS1_2024_25.API.Helper;

namespace RS1_2024_25.API.Services
{
    public class MyAuthService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly MyTokenGenerator myTokenGenerator;
        private readonly PasswordHasher<Account> _passwordHasher = new();

        public MyAuthService(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, MyTokenGenerator tokenGenerator)
        {
            applicationDbContext = dbContext;
            httpContextAccessor = contextAccessor;
            myTokenGenerator = tokenGenerator;
        }

        // ✅ Hashiranje lozinke prije spremanja u bazu
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        // ✅ Provjera lozinke pri loginu
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword) == PasswordVerificationResult.Success;
        }

        // Generate a new token for the user
        public async Task<MyAuthenticationToken> GenerateAuthToken(Account account, CancellationToken cancellationToken = default)
        {
            string randomToken = myTokenGenerator.Generate(10);

            var authToken = new MyAuthenticationToken
            {
                IpAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
                Value = randomToken,
                Account = account, // Assign the Account object directly
                RecordedAt = DateTime.Now
            };


            applicationDbContext.MyAuthenticationTokens.Add(authToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return authToken;
        }

        // Revoke an authentication token
        public async Task<bool> RevokeAuthToken(string tokenValue, CancellationToken cancellationToken = default)
        {
            var authToken = await applicationDbContext.MyAuthenticationTokens
                .FirstOrDefaultAsync(t => t.Value == tokenValue, cancellationToken);

            if (authToken == null)
                return false;

            applicationDbContext.MyAuthenticationTokens.Remove(authToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        // Get authentication
        // 
        public MyAuthInfo GetAuthInfo()
        {
            string? authToken = httpContextAccessor.HttpContext?.Request.Headers["my-auth-token"];
            if (string.IsNullOrEmpty(authToken))
            {
                return GetAuthInfo(null);
            }

            var myAuthToken = applicationDbContext.MyAuthenticationTokens
                .Include(x => x.Account)
                .SingleOrDefault(x => x.Value == authToken);

            // Ako je account tipa User, castuj ga
            if (myAuthToken?.Account is User userAccount)
            {
                myAuthToken.Account = userAccount;
            }

            return GetAuthInfo(myAuthToken);
        }

        public MyAuthInfo GetAuthInfo(MyAuthenticationToken? myAuthToken)
        {
            if (myAuthToken == null || myAuthToken.Account == null)
            {
                return new MyAuthInfo
                {
                    IsLoggedIn = false
                };
            }

            var account = myAuthToken.Account;
            var user = account.User;

            return new MyAuthInfo
            {
                UserId = account.AccountID,
                Username = account.Username,
                FirstName = account.FirstName ?? account.Email.Split('@')[0], // Derive FirstName if null
                LastName = account.LastName ?? "N/A", // Provide fallback if LastName is null
                IsAdmin = account.isAdministrator, // Admin check
                isOwner = account.isOwner, // Manager logic based on Owner
                IsLoggedIn = true,
                SlikaPath = user?.UserImages?.FirstOrDefault()?.Image?.ImagePath
            };
        }
    }

    // DTO to hold authentication information
    public class MyAuthInfo
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; } // Derived from Email
        public string LastName { get; set; } // Placeholder
        public bool IsAdmin { get; set; } // Defaulted to false
        public bool isOwner { get; set; } // Defaulted to false
        public bool IsLoggedIn { get; set; }
        public string SlikaPath { get; set; } // Changed from byte[] to string
    }
}
