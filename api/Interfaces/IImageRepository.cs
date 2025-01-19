using Shop.api.Dtos;
using Shop.api.Helpers;
using Shop.api.Models;

namespace Shop.api.Interfaces
{
    public interface IImageRepository
    {
        Task<List<Image>> GetAllAsync(QueryObject query);
        Task<Image?> GetByIdAsync(int id);
        Task<Image> CreateAsync(Image imageModel);
        Task<Image?> UpdateAsync(int id, UpdateImageDto imageDto);
        Task<Image?> DeleteAsync(int id);
        Task<bool> ImageExists(int id);
    }
}
