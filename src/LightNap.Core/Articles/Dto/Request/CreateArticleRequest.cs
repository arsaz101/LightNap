using System.ComponentModel.DataAnnotations;

namespace LightNap.Core.Articles.Dto.Request
{
    /// <summary>
    /// Request DTO for creating a new article.
    /// </summary>
    public class CreateArticleRequest
    {
        /// <summary>
        /// The article number/code.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ArticleNumber { get; set; } = string.Empty;

        /// <summary>
        /// The name of the article.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The category of the article (e.g., Hub, Crank, etc.).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ArticleCategory { get; set; } = string.Empty;

        /// <summary>
        /// The bicycle category this article is compatible with (e.g., Road, Mountain, etc.).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string BicycleCategory { get; set; } = string.Empty;

        /// <summary>
        /// The material used in the article.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Material { get; set; } = string.Empty;

        /// <summary>
        /// The length of the article in millimeters.
        /// </summary>
        [Range(0, 10000)]
        public decimal? LengthMm { get; set; }

        /// <summary>
        /// The width of the article in millimeters.
        /// </summary>
        [Range(0, 10000)]
        public decimal? WidthMm { get; set; }

        /// <summary>
        /// The height of the article in millimeters.
        /// </summary>
        [Range(0, 10000)]
        public decimal? HeightMm { get; set; }

        /// <summary>
        /// The net weight of the article in grams.
        /// </summary>
        [Range(0, 100000)]
        public decimal? NetWeightG { get; set; }
    }
} 