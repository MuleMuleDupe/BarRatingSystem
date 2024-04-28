using System.ComponentModel.DataAnnotations;

namespace BarRatingSystem.Models
{
    public class BarReview
    {
        public int Id { get; set; }
        public int BarId { get; set; }
        public Bar Bar { get; set; }

        public string UserId { get; set; }

        [Required]
        public string Review { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
