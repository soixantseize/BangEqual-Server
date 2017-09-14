using System.Collections;
using System.Collections.Generic;
using BangEqualServer.Repositories.Interfaces;

namespace BangEqualServer.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string ArticleCaption { get; set; } = "";
        public string ArticleText { get; set; } = "";
    }  
}