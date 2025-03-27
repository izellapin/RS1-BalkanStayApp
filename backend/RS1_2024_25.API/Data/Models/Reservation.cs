using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RS1_2024_25.API.Data
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        [ForeignKey(nameof(Account))]
        public int AccountID { get; set; } // Koristi se AccountID, jer User nasleđuje Account


        [JsonIgnore]
        public Account Account { get; set; }

        [ForeignKey(nameof(Apartment))]
        public int ApartmentId { get; set; }

        [JsonIgnore]
        public Apartment? Apartment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }
}
