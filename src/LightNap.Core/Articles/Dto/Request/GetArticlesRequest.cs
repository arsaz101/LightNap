namespace LightNap.Core.Articles.Dto.Request
{
    /// <summary>
    /// Request DTO for getting articles with filtering and sorting options.
    /// </summary>
    public class GetArticlesRequest
    {
        /// <summary>
        /// Filter by article category.
        /// </summary>
        public string? ArticleCategory { get; set; }

        /// <summary>
        /// Filter by bicycle category (comma-separated for multiple values).
        /// </summary>
        public string? BicycleCategory { get; set; }

        /// <summary>
        /// Filter by material.
        /// </summary>
        public string? Material { get; set; }

        /// <summary>
        /// Search term for article number or name.
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Sort field (ArticleNumber, Name, ArticleCategory, BicycleCategory, Material, NetWeightG).
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Sort direction (asc or desc).
        /// </summary>
        public string? SortDirection { get; set; } = "asc";

        /// <summary>
        /// Page number for pagination (1-based).
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Page size for pagination.
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
} 