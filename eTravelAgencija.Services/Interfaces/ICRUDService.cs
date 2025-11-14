using System.Threading.Tasks;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Interfaces
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
        Task<TResponse?> GetByCompositeKeysAsync(params object[] keyValues);
        Task<TResponse?> UpdateCompositeAsync(object[] keyValues, TUpdate request);
        Task<bool> DeleteCompositeAsync(params object[] keyValues);
        Task<bool> DeleteAsync(int id);
    }
}
