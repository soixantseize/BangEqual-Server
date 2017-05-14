namespace BareMetalApi.Models
{
    public class Content
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Topic { get; set; }
        public string Tags { get; set; }
        public int Views { get; set; }
        public int Shares { get; set; }
        public string Caption { get; set; }
        public bool Active { get; set; }
        public string RenderString { get; set; }
        
    }  
}