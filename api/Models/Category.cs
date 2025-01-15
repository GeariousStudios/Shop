using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.api.Models
{
    public class Category
    {
        public int Id { get; set; }

        // [Required]
        // [MaxLength(50, ErrorMessage = "Category name cannot exceed 50 characters")]
        public string Name { get; set; } = string.Empty;
        public String? Description { get; set; }
    }
}
