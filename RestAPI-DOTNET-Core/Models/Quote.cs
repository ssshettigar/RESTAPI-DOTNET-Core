using System;
namespace RestAPI_DOTNET_Core.Models
{
    public class Quote
    {
        public Quote()
        {
        }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author  { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
