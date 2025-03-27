using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;

namespace RS1_2024_25.API.Data.Models
{
    public class User : Account
    {
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public int GenderID { get; set; }
        public int CityID { get; set; }

        [ForeignKey(nameof(CityID))]
        [JsonIgnore]
        public City City { get; set; }

        [ForeignKey(nameof(GenderID))]
        [JsonIgnore]
        public Gender Gender { get; set; }

        [JsonIgnore]
        public ICollection<Favorite> Favorites { get; set; }
        [JsonIgnore]
        public ICollection<Reservation> Reservations { get; set; }
        [JsonIgnore]
        public ICollection<Review> Reviews { get; set; }
        [JsonIgnore]
        public ICollection<OwnerReview> OwnerReviews { get; set; }

        public List<UserImage> UserImages { get; set; }
    }
}
