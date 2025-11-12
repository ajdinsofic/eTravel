using System.Threading.Tasks;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Services
{
    public interface ICRUDService<TResponse, TSearch, TInsert, TUpdate> 
        : IService<TResponse, TSearch>
        where TResponse : class
        where TSearch : BaseSearchObject
        where TInsert : class
        where TUpdate : class
    {
        Task<TResponse> CreateAsync(TInsert request);
        Task<TResponse?> UpdateAsync(int id, TUpdate request);
        Task<bool> DeleteAsync(int id);
    }
}
