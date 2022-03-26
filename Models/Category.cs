using System.ComponentModel.DataAnnotations;

namespace HPlusSportApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<Product> Products { get; set; } 
    }
}
