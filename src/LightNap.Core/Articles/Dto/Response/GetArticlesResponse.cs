namespace LightNap.Core.Articles.Dto.Response
{
    /// <summary>
    /// Response DTO for getting articles with pagination metadata.
    /// </summary>
    public class GetArticlesResponse
    {
        /// <summary>
        /// The list of articles.
        /// </summary>
        public List<ArticleDto> Articles { get; set; } = new();

        /// <summary>
        /// The total number of articles matching the filters.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The current page number.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of pages.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Whether there are more pages available.
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// Whether there are previous pages available.
        /// </summary>
        public bool HasPreviousPage { get; set; }
    }
} 