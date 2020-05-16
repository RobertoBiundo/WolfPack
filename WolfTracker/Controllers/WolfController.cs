using System;
using System.Threading.Tasks;
using Common;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Services.Extensions;
using Services.Interfaces;
using Services.Models;
using Services.Services;

namespace WolfTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WolfController : ControllerBase
    {
        private readonly IWolfService _wolfService;
        private readonly IWolfPackService _wolfPackService;
        
        public WolfController(IWolfService wolfService, IWolfPackService wolfPackService)
        {
            _wolfService = wolfService;
            _wolfPackService = wolfPackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWolfs()
        {
            return Ok(await _wolfService.GetAll());
        }
        
        [HttpGet("{wolfId}/packs")]
        public async Task<IActionResult> GetPacksForWolf([FromRoute] Guid wolfId)
        {
            return Ok(await _wolfPackService.GetPacksForWolf(wolfId));
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateWolf([FromBody] WolfForModificationModel wolfForModificationModel)
        {
            var (wolf, validationResult) = await _wolfService.Create(wolfForModificationModel);

            if (validationResult != null)
                return BadRequest(validationResult);
            return Ok(wolf);
        }
        
        [HttpPut ("{id}")]
        public async Task<IActionResult> EditWolf([FromRoute] Guid id, [FromBody] WolfForModificationModel wolfForModificationModel)
        {
            var (wolf, validationResult, serviceResult) = await _wolfService.Edit(id, wolfForModificationModel);

            switch (serviceResult)
            {
                case Enums.ServiceResult.NotFound:
                    return NotFound();
                case Enums.ServiceResult.Ok:
                default:
                    if (validationResult != null)
                        return BadRequest(validationResult);
                    return Ok(wolf);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWolf([FromRoute] Guid id)
        {
            var serviceResult = await _wolfService.Delete(id);
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