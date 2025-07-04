using LightNap.Core.Articles.Dto.Request;
using LightNap.Core.Articles.Dto.Response;

namespace LightNap.Core.Articles.Interfaces
{
    /// <summary>
    /// Service interface for managing bicycle component articles.
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// Gets a paginated list of articles with filtering and sorting options.
        /// </summary>
        /// <param name="request">The request containing filter and pagination parameters.</param>
        /// <returns>A response containing the articles and pagination metadata.</returns>
        Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request);

        /// <summary>
        /// Gets an article by its ID.
        /// </summary>
        /// <param name="id">The article ID.</param>
        /// <returns>The article if found, null otherwise.</returns>
        Task<ArticleDto?> GetArticleByIdAsync(int id);

        /// <summary>
        /// Creates a new article.
        /// </summary>
        /// <param name="request">The article creation request.</param>
        /// <returns>The created article.</returns>
        Task<ArticleDto> CreateArticleAsync(CreateArticleRequest request);

        /// <summary>
        /// Updates an existing article.
        /// </summary>
        /// <param name="id">The article ID.</param>
        /// <param name="request">The article update request.</param>
        /// <returns>The updated article.</returns>
        Task<ArticleDto> UpdateArticleAsync(int id, UpdateArticleRequest request);

        /// <summary>
        /// Deletes an article.
        /// </summary>
        /// <param name="id">The article ID.</param>
        /// <returns>True if the article was deleted, false otherwise.</returns>
        Task<bool> DeleteArticleAsync(int id);

        /// <summary>
        /// Gets all available article categories.
        /// </summary>
        /// <returns>A list of unique article categories.</returns>
        Task<List<string>> GetArticleCategoriesAsync();

        /// <summary>
        /// Gets all available bicycle categories.
        /// </summary>
        /// <returns>A list of unique bicycle categories.</returns>
        Task<List<string>> GetBicycleCategoriesAsync();

        /// <summary>
        /// Gets all available materials.
        /// </summary>
        /// <returns>A list of unique materials.</returns>
        Task<List<string>> GetMaterialsAsync();
    }
} 