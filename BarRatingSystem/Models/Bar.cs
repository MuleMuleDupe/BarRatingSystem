using System.ComponentModel.DataAnnotations;

namespace BarRatingSystem.Models
{
    public class Bar
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public List<BarReview> BarReviews { get; set; }
    }
}
