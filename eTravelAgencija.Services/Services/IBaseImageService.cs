using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eTravelAgencija.Services.Services
{
    public interface IBaseImageService<TResponse>
    {
        Task<List<TResponse>> GetImagesAsync(int referenceId, bool isMain);
        Task<int> AddImageAsync(int referenceId, string imageUrl);
        Task<bool> DeleteImageByIdAsync(int imageId);
    }
}