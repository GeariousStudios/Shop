using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.api.Dtos;
using Shop.api.Models;

namespace Shop.api.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                ImageUrl = productModel.ImageUrl,
                Price = productModel.Price,
                Stock = productModel.Stock,
                CategoryId = productModel.CategoryId,
            };
        }

        public static Product ToProductFromCreateDto(this CreateProductRequestDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                ImageUrl = productDto.ImageUrl,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId,
            };
        }
    }
}
