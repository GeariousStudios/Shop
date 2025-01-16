using Shop.api.Dtos;
using Shop.api.Models;

namespace Shop.api.Mappers
{
    public static class ImageMapper
    {
        public static ImageDto ToImageDto(this Image imageModel)
        {
            return new ImageDto
            {
                Id = imageModel.Id,
                Name = imageModel.Name,
                ImageUrl = imageModel.ImageUrl,
            };
        }

        public static Image ToImageFromCreateDto(this CreateImageRequestDto imageDto)
        {
            return new Image { Name = imageDto.Name, ImageUrl = imageDto.ImageUrl };
        }
    }
}
