using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObject;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IWorkApplicationService : ICRUDService<Model.model.WorkApplication, WorkApplicationSearchObject, WorkApplicationUpsertRequest, WorkApplicationUpsertRequest>
    {
        Task<CVDownloadResponse> DownloadCVAsync(int id);
        Task InsertCV(WorkApplicationUpsertRequest request);
    }
}