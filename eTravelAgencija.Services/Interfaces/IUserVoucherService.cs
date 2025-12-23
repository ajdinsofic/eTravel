using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IUserVoucherService : IService<Model.model.UserVoucher, UserVoucherSearchObject>
    {
        Task BuyVoucherAsync(BuyVoucherRequest request);
        Task<bool> MarkAsUsed(MarkVoucherUsedRequest request);
    }
}
