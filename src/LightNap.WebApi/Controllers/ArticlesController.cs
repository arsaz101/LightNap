using LightNap.Core.Api;
using LightNap.Core.Articles.Dto.Request;
using LightNap.Core.Articles.Dto.Response;
using LightNap.Core.Articles.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LightNap.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing bicycle component articles.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        /// <summary>
        /// Gets a paginated list of articles with filtering and sorting options.
        /// </summary>
        /// <param name="request">The request containing filter and pagination parameters.</param>
        /// <returns>A response containing the articles and pagination metadata.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<GetArticlesResponse>>> GetArticles([FromQuery] GetArticlesRequest request)
        {
            try
            {
                var response = await _articleService.GetArticlesAsync(request);
                return Ok(new ApiResponseDto<GetArticlesResponse>
                {
                    Result = response,
                    Type = ApiResponseType.Success
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<GetArticlesResponse>
                {
                    Type = ApiResponseType.Error,
                    ErrorMessages = new[] { $"Failed to get articles: {ex.Message}" }
                });
            }
        }

        /// <summary>
        /// Gets an article by its ID.
        /// </summary>
        /// <param name="id">The article ID.</param>
        /// <returns>The article if found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto<ArticleDto>>> GetArticle(int id)
        {
            try
            {
                var article = await _articleService.GetArticleByIdAsync(id);
                if (article == null)
                {
                    return NotFound(new ApiResponseDto<ArticleDto>
                    {
                        Type = ApiResponseType.Error,
                        ErrorMessages = new[] { "Article not found" }
                    });
                }

                return Ok(new ApiResponseDto<ArticleDto>
                {
                    Result = article,
                    Type = ApiResponseType.Success
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<ArticleDto>
                {
                    Type = ApiResponseType.Error,
                    ErrorMessages = new[] { $"Failed to get article: {ex.Message}" }
                });
            }
        }

        /// <summary>
        /// Creates a new article.
        /// </summary>
        /// <param name="request">The article creation request.</param>
        /// <returns>The created article.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<ArticleDto>>> CreateArticle([FromBody] CreateArticleRequest request)
        {
            try
            {
                var article = await _articleService.CreateArticleAsync(request);
                return CreatedAtAction(nameof(GetArticle), new { id = article.Id },
                    new ApiResponseDto<ArticleDto>
                    {
                        Result = article,
                        Type = ApiResponseType.Success
                    });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<ArticleDto>
                {
                    Type = ApiResponseType.Error,
                    ErrorMessages = new[] { $"Failed to create article: {ex.Message}" }
                });
            }
        }

        /// <summary>
        /// Updates an existing article.
        /// </summary>
        /// <param name="id">The article ID.</param>
        /// <param name="request">The article update request.</param>
        /// <returns>The updated article.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<ArticleDto>>> UpdateArticle(int id, [FromBody] UpdateArticleRequest request)
        {
            try
            {
                var article = await _articleService.UpdateArticleAsync(id, request);
                return Ok(new ApiResponseDto<ArticleDto>
                {
                    Result = article,
                    Type = ApiResponseType.Success
                });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new ApiResponseDto<ArticleDto>
                {
                    Type = ApiResponseType.Error,
                    ErrorMessages = new[] { ex.Message }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<ArticleDto>
                {
                    Type = ApiResponseType.Error,
                    ErrorMessages = new[] { $"Failed to update article: {ex.Message}" }
                });
            }
        }

        /// <summary>
        /// Deletes an article.
        /// </summary>
        /// <param name="id">The article ID.</param>
        /// <returns>Success response if deleted.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteArticle(int id)
        {
            try
            {
                var deleted = await _articleService.DeleteArticleAsync(id);
                if (!deleted)
                {
                    return NotFound(new ApiResponseDto<bool>
                    {
                        Type = ApiResponseType.Error,
                        ErrorMessages = new[] { "Article not found" }
                    });
                }

                return Ok(new ApiResponseDto<bool>
                {
                    Result = true,
                    Type = ApiResponseType.Success
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<bool>
                {
                    Type = ApiResponseType.Error,
                    ErrorMessages = new[] { $"Failed to delete article: {ex.Message}" }
                });
            }
        }

        /// <summary>
        /// Gets all available article categories.
        /// </summary>
        /// <returns>A list of unique article categories.</returns>
        [HttpGet("categories")]
        public async Task<ActionResult<ApiResponseDto<List<string>>>> GetArticleCategories()
        {
            try
            {
                var categories = await _articleService.GetArticleCategoriesAsync();
                return Ok(new ApiResponseDto<List<string>>
                {
                    Result = categories,
                    Type = ApiResponseType.Success
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<List<string>>
                {
                    Type = ApiResponseType.Error,
                    ErrorMessages = new[] { $"Failed to get article categories: {ex.Message}" }
                });
            }
        }

        /// <summary>
        /// Gets all available bicycle categories.
        /// </summary>
        /// <returns>A list of unique bicycle categories.</returns>
        [HttpGet("bicycle-categories")]
        public async Task<ActionResult<ApiResponseDto<List<string>>>> GetBicycleCategories()
        {
            try
            {
                var categories = await _articleService.GetBicycleCategoriesAsync();
                return Ok(new ApiResponseDto<List<string>>
                {
                    Result = categories,
                    Type = ApiResponseType.Success
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<List<string>>
                {
                    Type = ApiResponseType.Error,
                    ErrorMessages = new[] { $"Failed to get bicycle categories: {ex.Message}" }
                });
            }
        }

        /// <summary>
        /// Gets all available materials.
        /// </summary>
        /// <returns>A list of unique materials.</returns>
        [HttpGet("materials")]
        public async Task<ActionResult<ApiResponseDto<List<string>>>> GetMaterials()
        {
            try
            {
                var materials = await _articleService.GetMaterialsAsync();
                return Ok(new ApiResponseDto<List<string>>
                {
                    Result = materials,
                    Type = ApiResponseType.Success
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<List<string>>
                {
                    Type = ApiResponseType.Error,
                    ErrorMessages = new[] { $"Failed to get materials: {ex.Message}" }
                });
            }
        }
    }
} 