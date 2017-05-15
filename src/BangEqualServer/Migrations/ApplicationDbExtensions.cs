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

        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.SiteContent.Any())
                {
                  
                }                   
            }
        }
    }
}
