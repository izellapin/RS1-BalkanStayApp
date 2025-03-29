using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models.Auth;
using RS1_2024_25.API.Helpers;

namespace RS1_2024_25.API.Services
{
    public class TwoFactorAuthService
    {
        private readonly ApplicationDbContext _context; 
        private readonly EmailService _emailService;


        public TwoFactorAuthService(ApplicationDbContext context, EmailService emailService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _emailService = emailService;
        }

        public async Task<string> Generate2FAToken(int accountId)
        {
            var user = await _context.Accounts
                .Include(a => a.TwoFactorAuth)
                .FirstOrDefaultAsync(a => a.AccountID == accountId);

            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {accountId} not found.");
            }

            var token = GenerateRandomToken();
            var tokenHash = TokenHasher.HashToken(token);
            var expiration = DateTime.UtcNow.AddMinutes(5);

            if (user.TwoFactorAuth == null)
            {
                var twoFactorAuth = new TwoFactorAuth
                {
                    AccountId = user.AccountID,
                    AuthTokenHash = tokenHash,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = expiration
                };
                _context.TwoFactorAuths.Add(twoFactorAuth);
            }
            else
            {
                user.TwoFactorAuth.AuthTokenHash = tokenHash;
                user.TwoFactorAuth.CreatedAt = DateTime.UtcNow;
                user.TwoFactorAuth.ExpiresAt = expiration;
            }

            await _context.SaveChangesAsync();

            await _emailService.SendResetEmailAsync(user.Email, $"Your 2FA Code: {token}");

            return token;
        }



        public async Task<bool> Verify2FAToken(int accountId, string token)
        {
            var twoFactorAuth = await _context.TwoFactorAuths
                .Where(tfa => tfa.AccountId == accountId)
                .OrderByDescending(tfa => tfa.CreatedAt)
                .FirstOrDefaultAsync();

            if (twoFactorAuth == null)
            {
                return false;
            }

            if (DateTime.UtcNow > twoFactorAuth.ExpiresAt)
            {
                return false;
            }

            if (!TokenHasher.VerifyToken(token, twoFactorAuth.AuthTokenHash))
            {
                return false;
            }

            _context.TwoFactorAuths.Remove(twoFactorAuth);
            await _context.SaveChangesAsync();

            return true;
        }


        private string GenerateRandomToken()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
