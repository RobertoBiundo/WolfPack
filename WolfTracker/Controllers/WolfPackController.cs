using System;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;
using Services.Extensions;
using Services.Interfaces;
using Services.Models;

namespace WolfTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WolfPackController : ControllerBase
    {
        private readonly IWolfPackService _wolfPackService;

        public WolfPackController(IWolfPackService wolfPackService)
        {
            _wolfPackService = wolfPackService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWolfPack(
            [FromBody] WolfPackForModificationModel wolfPackForModificationModel)
        {
            var serviceResult = await _wolfPackService.AddWolfToPack(wolfPackForModificationModel);

            switch (serviceResult)
            {
                case Enums.ServiceResult.NotFound:
                    return NotFound();
                case Enums.ServiceResult.Ok:
                default:
                    return Ok();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWolf(
            [FromBody] WolfPackForModificationModel wolfPackForModificationModel)
        {
            var serviceResult = await _wolfPackService.RemoveWolfFromPack(wolfPackForModificationModel);
            switch (serviceResult)
            {
                case Enums.ServiceResult.NotFound:
                    return NotFound();
                case Enums.ServiceResult.Ok:
                default:
                    return Ok();
            }
        }
    }
}
