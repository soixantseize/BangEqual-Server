using System.Linq;
using Microsoft.EntityFrameworkCore;
using BareMetalApi.Models;

namespace BareMetalApi.Migrations
{
    public static class ApplicationDbExtensions
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.BlogArticles.Any())
                {
                    context.BlogArticles.AddRange(
                        new BlogArticle { ArticleId = 1, ArticleTitle = "How to Dab", ArticleAuthor = 1, ArticleTopic = 1, ArticleTags = "dancing", ArticleLikes = 4, ArticleContent = "First tuck you head down" },
                        new BlogArticle { ArticleId = 2, ArticleTitle = "How to Whip", ArticleAuthor = 2, ArticleTopic = 1, ArticleTags = "dancing", ArticleLikes = 8, ArticleContent = "Rock back and forth"  },
                        new BlogArticle { ArticleId = 3, ArticleTitle = "How to Nae Nae", ArticleAuthor = 3, ArticleTopic = 1, ArticleTags = "dancing", ArticleLikes = 2, ArticleContent = "Add a connecting move"  },
                        new BlogArticle { ArticleId = 4, ArticleTitle = "How to Dougie", ArticleAuthor = 4, ArticleTopic = 1, ArticleTags = "dancing", ArticleLikes = 1, ArticleContent = "Pass your hand through"  },
                        new BlogArticle { ArticleId = 5, ArticleTitle = "How to Wop", ArticleAuthor = 1, ArticleTopic = 1, ArticleTags = "dancing", ArticleLikes = 2, ArticleContent = "Worm your upper body"  });

                    context.SaveChanges();
                }
            }
        }
    }
}