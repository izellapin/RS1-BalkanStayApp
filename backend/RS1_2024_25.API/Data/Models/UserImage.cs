using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RS1_2024_25.API.Data.Models
{
    public class UserImage
    {
        [Key]
        public int UserImageID { get; set; }
        public int AccountID { get; set; }
        public int ImageID { get; set; }

        [ForeignKey(nameof(AccountID))]
        [JsonIgnore]
        public User User { get; set; }

        [ForeignKey(nameof(ImageID))]
        public Image Image { get; set; }
    }
} 