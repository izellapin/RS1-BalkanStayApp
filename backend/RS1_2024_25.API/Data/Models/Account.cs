using RS1_2024_25.API.Data.Models.Auth;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RS1_2024_25.API.Data.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public TwoFactorAuth? TwoFactorAuth { get; set; } 

        [JsonIgnore]
        public User? User => this as User;

        public bool isUser => User != null;

        [JsonIgnore]
        public Administrator? Administrator => this as Administrator;

        public bool isAdministrator => Administrator != null;

        [JsonIgnore]
        public Owner? Owner => this as Owner;

        public bool isOwner => Owner != null;

        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }
    }
}

