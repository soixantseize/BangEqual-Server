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

        public static void EnsureSeedData(this ApplicationDbContext context, string jsonData)
        {
            List<Content> sitecontent = JsonConvert.DeserializeObject<List<Content>>(jsonData);

            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.SiteContent.Any())
                {
                    foreach(Content c in sitecontent)
                    {
                        context.SiteContent.AddRange(
                            new Content {
                            ContentId = c.ContentId, 
                            Type = c.Type,
                            Title = c.Title, 
                            Author = c.Author, 
                            Topic = c.Topic, 
                            Tags = c.Tags, 
                            Views = c.Views, 
                            Shares = c.Shares,
                            Caption = c.Caption,
                            Active = c.Active, 
                            RenderString = c.RenderString
                            }); 
                        context.SaveChanges();
                    }    
                }                   
            }
        }
    }
}