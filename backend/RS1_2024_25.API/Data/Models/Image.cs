using System.Text.Json.Serialization;
using RS1_2024_25.API.Data.Models;


namespace RS1_2024_25.API.Data
{

    public class Image
    {
        public int ImageID { get; set; }
        public string ?ImagePath { get; set; } // ✅ Store the image file path instead of Base64

        [JsonIgnore]
        public List<ApartmentImage> ApartmentImages { get; set; }
        
        [JsonIgnore]
        public List<UserImage> UserImages { get; set; }
        
        [JsonIgnore]
        public List<OwnerImage> OwnerImages { get; set; }
    }


}
