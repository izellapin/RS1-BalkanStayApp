using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RS1_2024_25.API.Data.Models
{
    public class OwnerImage
    {
        [Key]
        public int OwnerImageID { get; set; }
        public int AccountID { get; set; }
        public int ImageID { get; set; }

        [ForeignKey(nameof(AccountID))]
        [JsonIgnore]
        public Owner Owner { get; set; }

        [ForeignKey(nameof(ImageID))]
        public Image Image { get; set; }
    }
} 