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
                ImageUrlId = productModel.ImageUrlId,
                Price = productModel.Price,
                Stock = productModel.Stock,
                CategoryId = productModel.CategoryId,
            };
        }

        public static Product ToProductFromCreate(this CreateProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                ImageUrlId = productDto.ImageUrlId,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId,
            };
        }
    }
}
