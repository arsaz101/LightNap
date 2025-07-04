namespace LightNap.Core.Articles.Dto.Response
{
    /// <summary>
    /// Response DTO for article data.
    /// </summary>
    public class ArticleDto
    {
        /// <summary>
        /// The unique identifier for the article.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The article number/code.
        /// </summary>
        public string ArticleNumber { get; set; } = string.Empty;

        /// <summary>
        /// The name of the article.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The category of the article (e.g., Hub, Crank, etc.).
        /// </summary>
        public string ArticleCategory { get; set; } = string.Empty;

        /// <summary>
        /// The bicycle category this article is compatible with (e.g., Road, Mountain, etc.).
        /// </summary>
        public string BicycleCategory { get; set; } = string.Empty;

        /// <summary>
        /// The material used in the article.
        /// </summary>
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
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The date when the article was last modified.
        /// </summary>
        public DateTime LastModifiedDate { get; set; }
    }
} 