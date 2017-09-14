using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BangEqualServer.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BangEqualServer.Migrations
{
    public static class ApplicationDbExtensions
    {

        public static void EnsureSeedData(this ApplicationDbContext context, string ArticleInfoData, string ArticleData)
        {
            List<ArticleInfo> articleinfo = JsonConvert.DeserializeObject<List<ArticleInfo>>(ArticleInfoData);
            List<Article> articles = JsonConvert.DeserializeObject<List<Article>>(ArticleData);

            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.ArticleInfo.Any())
                {
                    foreach(ArticleInfo ai in articleinfo)
                    {
                        context.ArticleInfo.AddRange(
                            new ArticleInfo {
                            ArticleInfoId = ai.ArticleInfoId, 
                            ArticleIdFK = ai.ArticleIdFK,
                            ArticleDateWrit = ai.ArticleDateWrit, 
                            ArticleDateMod = ai.ArticleDateMod, 
                            ArticleTitle = ai.ArticleTitle, 
                            ArticleAuthor = ai.ArticleAuthor, 
                            ArticleTags = ai.ArticleTags,  
                            ArticleViews = ai.ArticleViews,
                            ArticleShares = ai.ArticleShares,
                            ArticleHeaderImageUrl = ai.ArticleHeaderImageUrl,
                            ArticleActive = ai.ArticleActive 
                            }); 
                        context.SaveChanges();
                  
                    } 
                } 

                if (!context.Article.Any())
                {
                    foreach(Article a in articles)
                    {
                        context.Article.AddRange(
                            new Article {
                            ArticleId = a.ArticleId, 
                            ArticleCaption = a.ArticleCaption,
                            ArticleText = a.ArticleText
                            }); 
                        context.SaveChanges();
                  
                    }
                }                    
            }
        }
    }
}
