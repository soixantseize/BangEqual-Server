using System;

namespace BareMetalApi.Models
{
    public class ShopDesign
    {
        public int Id { get; set; }
        public int DesignId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public int Views { get; set; }
        public int Shares { get; set; }
        public bool Active { get; set; }
        public string Content { get; set; }
        
        
    }  
}