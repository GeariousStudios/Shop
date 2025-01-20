using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.api.Models
{
    public class ProductImage
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
