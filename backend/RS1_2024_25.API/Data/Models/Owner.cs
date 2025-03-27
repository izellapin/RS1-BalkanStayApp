using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;
using System.Text.Json.Serialization;

namespace RS1_2024_25.API.Data
{
    public class Owner : Account
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
        public List<OwnerReview> OwnerReviews { get; set; }

        [JsonIgnore]
        public List<Apartment> Apartments { get; set; }

        public List<OwnerImage> OwnerImages { get; set; }
    }
}
