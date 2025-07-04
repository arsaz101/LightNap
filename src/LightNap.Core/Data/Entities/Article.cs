using System.ComponentModel.DataAnnotations;

namespace LightNap.Core.Data.Entities
{
    /// <summary>
    /// Represents a bicycle component article submitted by suppliers.
    /// </summary>
    public class Article
    {
        /// <summary>
        /// The unique identifier for the article.
        /// </summary>
        public int Id { get; set; }

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
        public decimal? LengthMm { get; set; }

        /// <summary>
        /// The width of the article in millimeters.
        /// </summary>
        public decimal? WidthMm { get; set; }

        /// <summary>
        /// The height of the article in millimeters.
        /// </summary>
        public decimal? HeightMm { get; set; }

        /// <summary>
        /// The net weight of the article in grams.
        /// </summary>
        public decimal? NetWeightG { get; set; }

        /// <summary>
        /// The date when the article was created.
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The date when the article was last modified.
        /// </summary>
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    }
} 