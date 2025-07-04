using LightNap.Core.Articles.Interfaces;
using LightNap.Core.Articles.Dto.Request;

namespace LightNap.WebApi.Configuration
{
    /// <summary>
    /// Seeds the database with sample bicycle component articles.
    /// </summary>
    public static class ArticleSeeder
    {
        /// <summary>
        /// Seeds sample articles into the database.
        /// </summary>
        /// <param name="articleService">The article service.</param>
        public static async Task SeedArticlesAsync(IArticleService articleService)
        {
            var sampleArticles = new[]
            {
                new CreateArticleRequest
                {
                    ArticleNumber = "HUB-001",
                    Name = "Shimano Deore XT Hub",
                    ArticleCategory = "Hub",
                    BicycleCategory = "Mountain",
                    Material = "Aluminum",
                    LengthMm = 135,
                    WidthMm = 32,
                    HeightMm = 32,
                    NetWeightG = 450
                },
                new CreateArticleRequest
                {
                    ArticleNumber = "HUB-002",
                    Name = "Campagnolo Record Hub",
                    ArticleCategory = "Hub",
                    BicycleCategory = "Road",
                    Material = "Carbon Fiber",
                    LengthMm = 130,
                    WidthMm = 30,
                    HeightMm = 30,
                    NetWeightG = 280
                },
                new CreateArticleRequest
                {
                    ArticleNumber = "CRANK-001",
                    Name = "SRAM GX Eagle Crankset",
                    ArticleCategory = "Crank",
                    BicycleCategory = "Mountain",
                    Material = "Aluminum",
                    LengthMm = 175,
                    WidthMm = 24,
                    HeightMm = 24,
                    NetWeightG = 650
                },
                new CreateArticleRequest
                {
                    ArticleNumber = "CRANK-002",
                    Name = "Shimano Ultegra Crankset",
                    ArticleCategory = "Crank",
                    BicycleCategory = "Road",
                    Material = "Aluminum",
                    LengthMm = 172.5m,
                    WidthMm = 24,
                    HeightMm = 24,
                    NetWeightG = 580
                },
                new CreateArticleRequest
                {
                    ArticleNumber = "WHEEL-001",
                    Name = "Mavic Cosmic Pro Carbon",
                    ArticleCategory = "Wheel",
                    BicycleCategory = "Road",
                    Material = "Carbon Fiber",
                    LengthMm = 622,
                    WidthMm = 25,
                    HeightMm = 25,
                    NetWeightG = 1450
                },
                new CreateArticleRequest
                {
                    ArticleNumber = "WHEEL-002",
                    Name = "DT Swiss XM 1700",
                    ArticleCategory = "Wheel",
                    BicycleCategory = "Mountain",
                    Material = "Aluminum",
                    LengthMm = 622,
                    WidthMm = 30,
                    HeightMm = 30,
                    NetWeightG = 1850
                },
                new CreateArticleRequest
                {
                    ArticleNumber = "FORK-001",
                    Name = "RockShox Pike Ultimate",
                    ArticleCategory = "Fork",
                    BicycleCategory = "Mountain",
                    Material = "Aluminum",
                    LengthMm = 160,
                    WidthMm = 51,
                    HeightMm = 51,
                    NetWeightG = 1650
                },
                new CreateArticleRequest
                {
                    ArticleNumber = "FORK-002",
                    Name = "Fox 32 Step-Cast",
                    ArticleCategory = "Fork",
                    BicycleCategory = "Road",
                    Material = "Carbon Fiber",
                    LengthMm = 100,
                    WidthMm = 32,
                    HeightMm = 32,
                    NetWeightG = 1250
                },
                new CreateArticleRequest
                {
                    ArticleNumber = "BRAKE-001",
                    Name = "Shimano XT M8100",
                    ArticleCategory = "Brake",
                    BicycleCategory = "Mountain",
                    Material = "Aluminum",
                    LengthMm = 85,
                    WidthMm = 45,
                    HeightMm = 25,
                    NetWeightG = 320
                },
                new CreateArticleRequest
                {
                    ArticleNumber = "BRAKE-002",
                    Name = "SRAM Red eTap AXS",
                    ArticleCategory = "Brake",
                    BicycleCategory = "Road",
                    Material = "Carbon Fiber",
                    LengthMm = 75,
                    WidthMm = 40,
                    HeightMm = 20,
                    NetWeightG = 280
                }
            };

            foreach (var article in sampleArticles)
            {
                try
                {
                    await articleService.CreateArticleAsync(article);
                }
                catch
                {
                    // Article might already exist, continue with next
                }
            }
        }
    }
} 