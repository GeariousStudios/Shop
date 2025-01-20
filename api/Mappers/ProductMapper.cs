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
                Price = productModel.Price,
                Stock = productModel.Stock,
                ImageIds = productModel.ProductImages.Select(pi => pi.ImageId).ToArray(),
                CategoryIds = productModel.ProductCategories.Select(pc => pc.CategoryId).ToArray(),
                AppUserId = productModel.AppUserId,
            };
        }

        public static Product ToProductFromCreateDto(this CreateProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                ProductImages = productDto
                    .ImageIds.Select(i => new ProductImage { ImageId = i })
                    .ToList(),
                ProductCategories = productDto
                    .CategoryIds.Select(i => new ProductCategory { CategoryId = i })
                    .ToList(),
                AppUserId = productDto.AppUserId,
            };
        }

        public static Product ToProductFromUpdateDto(this UpdateProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                ProductImages = productDto
                    .ImageIds.Select(i => new ProductImage { ImageId = i })
                    .ToList(),
                ProductCategories = productDto
                    .CategoryIds.Select(i => new ProductCategory { CategoryId = i })
                    .ToList(),
                AppUserId = productDto.AppUserId,
            };
        }
    }
}
