using LightNap.Core.Articles.Dto.Request;
using LightNap.Core.Articles.Dto.Response;
using LightNap.Core.Articles.Interfaces;
using LightNap.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

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

            // Apply filters
            if (!string.IsNullOrWhiteSpace(request.ArticleCategory))
            {
                query = query.Where(a => a.ArticleCategory == request.ArticleCategory);
            }

            if (!string.IsNullOrWhiteSpace(request.BicycleCategory))
            {
                var categories = request.BicycleCategory.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Trim())
                    .ToList();
                query = query.Where(a => categories.Contains(a.BicycleCategory));
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

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                query = request.SortBy.ToLower() switch
                {
                    "articlenumber" => request.SortDirection?.ToLower() == "desc" 
                        ? query.OrderByDescending(a => a.ArticleNumber)
                        : query.OrderBy(a => a.ArticleNumber),
                    "name" => request.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(a => a.Name)
                        : query.OrderBy(a => a.Name),
                    "articlecategory" => request.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(a => a.ArticleCategory)
                        : query.OrderBy(a => a.ArticleCategory),
                    "bicyclecategory" => request.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(a => a.BicycleCategory)
                        : query.OrderBy(a => a.BicycleCategory),
                    "material" => request.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(a => a.Material)
                        : query.OrderBy(a => a.Material),
                    "netweightg" => request.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(a => a.NetWeightG)
                        : query.OrderBy(a => a.NetWeightG),
                    _ => query.OrderBy(a => a.Id)
                };
            }
            else
            {
                query = query.OrderBy(a => a.Id);
            }

            // Get total count
            var totalCount = await query.CountAsync();

            // Apply pagination
            var skip = (request.Page - 1) * request.PageSize;
            var articles = await query
                .Skip(skip)
                .Take(request.PageSize)
                .Select(a => new ArticleDto
                {
                    Id = a.Id,
                    ArticleNumber = a.ArticleNumber,
                    Name = a.Name,
                    ArticleCategory = a.ArticleCategory,
                    BicycleCategory = a.BicycleCategory,
                    Material = a.Material,
                    LengthMm = a.LengthMm,
                    WidthMm = a.WidthMm,
                    HeightMm = a.HeightMm,
                    NetWeightG = a.NetWeightG,
                    CreatedDate = a.CreatedDate,
                    LastModifiedDate = a.LastModifiedDate
                })
                .ToListAsync();

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
                    BicycleCategory = a.BicycleCategory,
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
                BicycleCategory = request.BicycleCategory,
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
                BicycleCategory = article.BicycleCategory,
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
            article.BicycleCategory = request.BicycleCategory;
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
                BicycleCategory = article.BicycleCategory,
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
            return await _context.Articles
                .Select(a => a.BicycleCategory)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
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