using System;
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

        public static void EnsureSeedData(this ApplicationDbContext context, string jsonData, string shopData)
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
                            Title = ba.Title, 
                            Author = ba.Author, 
                            Topic = ba.Topic, 
                            Tags = ba.Tags, 
                            Views = ba.Views, 
                            Shares = ba.Shares,
                            Active = ba.Active, 
                            Content = ba.Content
                            }); 
                        context.SaveChanges();
                    }                       
                }

                List<ShopDesign> shopdesigns = JsonConvert.DeserializeObject<List<ShopDesign>>(shopData);
                DateTime dt = DateTime.Now;

                if (!context.ShopDesigns.Any())
                {
                    foreach(ShopDesign sd in shopdesigns)
                    {
                        context.ShopDesigns.AddRange(
                            new ShopDesign {
                            DesignId = sd.DesignId, 
                            Title = sd.Title, 
                            Author = sd.Author, 
                            Tags = sd.Tags, 
                            Views = sd.Views,
                            Shares = sd.Shares,
                            Active = sd.Active,
                            Content = sd.Content
                            }); 
                        context.SaveChanges();
                    }                       
                }
            }
        }
    }
}