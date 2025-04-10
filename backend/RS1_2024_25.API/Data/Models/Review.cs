using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RS1_2024_25.API.Data
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int ApartmentId { get; set; }

        [JsonIgnore]
        public Apartment Apartment { get; set; }

        [ForeignKey(nameof(Account))]
        public int AccountID { get; set; } // Koristi se AccountID, jer User nasleđuje Account
        
        public Account Account { get; set; }

        public string Rating { get; set; }
        public string Comment { get; set; }
    }
}
