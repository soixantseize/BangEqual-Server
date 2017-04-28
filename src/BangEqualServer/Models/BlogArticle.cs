namespace BareMetalApi.Models
{
    public class BlogArticle
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public int ArticleAuthor { get; set; }
        public int ArticleTopic { get; set; }
        public string ArticleTags { get; set; }
        public int ArticleLikes { get; set; }
        public string ArticleContent { get; set; }
        public string ArticleContentMarkdown { get; set; }
    }  
}