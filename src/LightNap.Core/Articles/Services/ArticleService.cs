using LightNap.Core.Articles.Dto.Request;
using LightNap.Core.Articles.Dto.Response;
using LightNap.Core.Articles.Interfaces;
using LightNap.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using LightNap.Core.Data;

namespace LightNap.Core.Articles.Services
{
    /// <summary>
    /// Service implementation for managing bicycle component articles.
    /// </summary>
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request)
        {
            var query = _context.Articles.AsQueryable();

            // Apply filters that can be translated
            if (!string.IsNullOrWhiteSpace(request.ArticleCategory))
            {
                query = query.Where(a => a.ArticleCategory == request.ArticleCategory);
            }
            if (!string.IsNullOrWhiteSpace(request.Material))
            {
                query = query.Where(a => a.Material == request.Material);
            }
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(a =>
                    a.ArticleNumber.ToLower().Contains(searchTerm) ||
                    a.Name.ToLower().Contains(searchTerm));
            }

            // Fetch data from DB
            var allArticles = await query.ToListAsync();

            // Apply in-memory filtering for bicycle categories
            if (!string.IsNullOrWhiteSpace(request.BicycleCategory))
            {
                var categories = request.BicycleCategory.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Trim())
                    .ToList();
                allArticles = allArticles.Where(a =>
                    a.BicycleCategories != null &&
                    a.BicycleCategories.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(cat => cat.Trim())
                        .Any(cat => categories.Contains(cat))
                ).ToList();
            }

            // Apply sorting in memory
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                var desc = request.SortDirection?.ToLower() == "desc";
                allArticles = request.SortBy.ToLower() switch
                {
                    "articlenumber" => desc ? allArticles.OrderByDescending(a => a.ArticleNumber).ToList() : allArticles.OrderBy(a => a.ArticleNumber).ToList(),
                    "name" => desc ? allArticles.OrderByDescending(a => a.Name).ToList() : allArticles.OrderBy(a => a.Name).ToList(),
                    "articlecategory" => desc ? allArticles.OrderByDescending(a => a.ArticleCategory).ToList() : allArticles.OrderBy(a => a.ArticleCategory).ToList(),
                    "bicyclecategory" => desc ? allArticles.OrderByDescending(a => a.BicycleCategories).ToList() : allArticles.OrderBy(a => a.BicycleCategories).ToList(),
                    "material" => desc ? allArticles.OrderByDescending(a => a.Material).ToList() : allArticles.OrderBy(a => a.Material).ToList(),
                    "netweightg" => desc ? allArticles.OrderByDescending(a => a.NetWeightG).ToList() : allArticles.OrderBy(a => a.NetWeightG).ToList(),
                    _ => allArticles.OrderBy(a => a.Id).ToList()
                };
            }
            else
            {
                allArticles = allArticles.OrderBy(a => a.Id).ToList();
            }

            // Get total count
            var totalCount = allArticles.Count;

            // Apply pagination
            var skip = (request.Page - 1) * request.PageSize;
            var pagedArticles = allArticles.Skip(skip).Take(request.PageSize).ToList();

            // Map to DTOs
            var articles = pagedArticles.Select(a => new ArticleDto
            {
                Id = a.Id,
                ArticleNumber = a.ArticleNumber,
                Name = a.Name,
                ArticleCategory = a.ArticleCategory,
                BicycleCategories = a.BicycleCategories != null ? a.BicycleCategories.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()).ToList() : new List<string>(),
                Material = a.Material,
                LengthMm = a.LengthMm,
                WidthMm = a.WidthMm,
                HeightMm = a.HeightMm,
                NetWeightG = a.NetWeightG,
                CreatedDate = a.CreatedDate,
                LastModifiedDate = a.LastModifiedDate
            }).ToList();

            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            return new GetArticlesResponse
            {
                Articles = articles,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                HasNextPage = request.Page < totalPages,
                HasPreviousPage = request.Page > 1
            };
        }

        /// <inheritdoc/>
        public async Task<ArticleDto?> GetArticleByIdAsync(int id)
        {
            var article = await _context.Articles
                .Where(a => a.Id == id)
                .Select(a => new ArticleDto
                {
                    Id = a.Id,
                    ArticleNumber = a.ArticleNumber,
                    Name = a.Name,
                    ArticleCategory = a.ArticleCategory,
                    BicycleCategories = a.BicycleCategories != null ? a.BicycleCategories.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()).ToList() : new List<string>(),
                    Material = a.Material,
                    LengthMm = a.LengthMm,
                    WidthMm = a.WidthMm,
                    HeightMm = a.HeightMm,
                    NetWeightG = a.NetWeightG,
                    CreatedDate = a.CreatedDate,
                    LastModifiedDate = a.LastModifiedDate
                })
                .FirstOrDefaultAsync();

            return article;
        }

        /// <inheritdoc/>
        public async Task<ArticleDto> CreateArticleAsync(CreateArticleRequest request)
        {
            var article = new Article
            {
                ArticleNumber = request.ArticleNumber,
                Name = request.Name,
                ArticleCategory = request.ArticleCategory,
                BicycleCategories = request.BicycleCategories != null ? string.Join(",", request.BicycleCategories) : string.Empty,
                Material = request.Material,
                LengthMm = request.LengthMm,
                WidthMm = request.WidthMm,
                HeightMm = request.HeightMm,
                NetWeightG = request.NetWeightG,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow
            };

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return new ArticleDto
            {
                Id = article.Id,
                ArticleNumber = article.ArticleNumber,
                Name = article.Name,
                ArticleCategory = article.ArticleCategory,
                BicycleCategories = article.BicycleCategories != null ? article.BicycleCategories.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()).ToList() : new List<string>(),
                Material = article.Material,
                LengthMm = article.LengthMm,
                WidthMm = article.WidthMm,
                HeightMm = article.HeightMm,
                NetWeightG = article.NetWeightG,
                CreatedDate = article.CreatedDate,
                LastModifiedDate = article.LastModifiedDate
            };
        }

        /// <inheritdoc/>
        public async Task<ArticleDto> UpdateArticleAsync(int id, UpdateArticleRequest request)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                throw new ArgumentException($"Article with ID {id} not found.");
            }

            article.ArticleNumber = request.ArticleNumber;
            article.Name = request.Name;
            article.ArticleCategory = request.ArticleCategory;
            article.BicycleCategories = request.BicycleCategories != null ? string.Join(",", request.BicycleCategories) : string.Empty;
            article.Material = request.Material;
            article.LengthMm = request.LengthMm;
            article.WidthMm = request.WidthMm;
            article.HeightMm = request.HeightMm;
            article.NetWeightG = request.NetWeightG;
            article.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ArticleDto
            {
                Id = article.Id,
                ArticleNumber = article.ArticleNumber,
                Name = article.Name,
                ArticleCategory = article.ArticleCategory,
                BicycleCategories = article.BicycleCategories != null ? article.BicycleCategories.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()).ToList() : new List<string>(),
                Material = article.Material,
                LengthMm = article.LengthMm,
                WidthMm = article.WidthMm,
                HeightMm = article.HeightMm,
                NetWeightG = article.NetWeightG,
                CreatedDate = article.CreatedDate,
                LastModifiedDate = article.LastModifiedDate
            };
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteArticleAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return false;
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetArticleCategoriesAsync()
        {
            return await _context.Articles
                .Select(a => a.ArticleCategory)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetBicycleCategoriesAsync()
        {
            var allArticles = await _context.Articles.ToListAsync();
            return allArticles
                .SelectMany(a => a.BicycleCategories != null ? a.BicycleCategories.Split(',', StringSplitOptions.RemoveEmptyEntries) : new string[0])
                .Select(c => c.Trim())
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetMaterialsAsync()
        {
            return await _context.Articles
                .Select(a => a.Material)
                .Distinct()
                .OrderBy(m => m)
                .ToListAsync();
        }
    }
} 