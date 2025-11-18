using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.model;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.helper;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class WorkApplicationController : BaseCRUDController<Model.model.WorkApplication, WorkApplicationSearchObject, WorkApplicationUpsertRequest, WorkApplicationUpsertRequest>
    {
        public WorkApplicationController(ILogger<BaseCRUDController<Model.model.WorkApplication, WorkApplicationSearchObject, WorkApplicationUpsertRequest, WorkApplicationUpsertRequest>> logger, IWorkApplicationService workApplicationService) : base(logger, workApplicationService)
        {
            
        }

        public override Task<ActionResult<WorkApplication>> Create([FromForm] WorkApplicationUpsertRequest insert)
        {
            return base.Create(insert);
        }

        public override Task<ActionResult<WorkApplication>> Update(int id, [FromForm] WorkApplicationUpsertRequest update)
        {
            return base.Update(id, update);
        }

        [HttpGet("{id}/download-cv")]
        public async Task<IActionResult> DownloadCV(int id)
        {
            var result = await (_service as IWorkApplicationService).DownloadCVAsync(id);
        
            return File(
                result.fileBytes,
                FileHelper.GetMimeType(result.CvFileName),
                result.CvFileName
            );
        }

    }
}