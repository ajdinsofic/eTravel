using System.Threading.Tasks;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IService<T, TSearch> where T : class where TSearch : BaseSearchObject
    {
        Task<PagedResult<T>> GetAsync(TSearch search);
        Task<T?> GetByIdAsync(int id);
    }
}
