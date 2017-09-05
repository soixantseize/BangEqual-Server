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

        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.ArticleInfo.Any())
                {
                  
                }                   
            }
        }
    }
}
