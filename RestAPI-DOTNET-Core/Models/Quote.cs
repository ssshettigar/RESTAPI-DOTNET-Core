using System;
using System.ComponentModel.DataAnnotations;

namespace RestAPI_DOTNET_Core.Models
{
    public class Quote
    {
        public Quote()
        {
        }
        public int ID { get; set; }
        [Required]
        [StringLength(40)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        public string Author  { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(25)]
        public string Type { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
}
