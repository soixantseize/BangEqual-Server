using System.Linq;
using Microsoft.EntityFrameworkCore;
using BareMetalApi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BareMetalApi.Migrations
{
    public static class ApplicationDbExtensions
    {

        public static void EnsureSeedData(this ApplicationDbContext context, string jsonData)
        {
            List<BlogArticle> articles = JsonConvert.DeserializeObject<List<BlogArticle>>(jsonData);

            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.BlogArticles.Any())
                {
                    foreach(BlogArticle ba in articles)
                    {
                        context.BlogArticles.AddRange(
                            new BlogArticle {
                            ArticleId = ba.ArticleId, 
                            ArticleTitle = ba.ArticleTitle, 
                            ArticleAuthor = ba.ArticleAuthor, 
                            ArticleTopic = ba.ArticleTopic, 
                            ArticleTags = ba.ArticleTags, 
                            ArticleLikes = ba.ArticleLikes, 
                            ArticleContent = ba.ArticleContent, 
                            ArticleContentMarkdown = ba.ArticleContentMarkdown 
                            }); 
                        context.SaveChanges();
                    }                       
                }
            }
        }
    }
}