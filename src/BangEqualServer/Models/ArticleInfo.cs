using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangEqualServer.Models
{
    public class ArticleInfo
    {
        public int Id { get; set; }
        public int ArticleInfoId { get; set; }
        public int ArticleIdFK { get;set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ArticleDateWrit { get;set; }
        public DateTime ArticleDateMod { get;set; }
        public string ArticleTitle { get;set; }
        public int ArticleAuthor { get;set; }
        public string ArticleTags { get;set; }
        public string ArticleCaption = "";
        public string ArticleText = "";
        public int ArticleViews { get;set; }
        public int ArticleShares { get;set; }
        public string ArticleHeaderImageUrl { get;set; }
        public bool ArticleActive { get; set; }       
    }  
}