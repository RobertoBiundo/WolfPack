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
    public class PackController : ControllerBase
    {
        private readonly IPackService _packService;
        private readonly IWolfPackService _wolfPackService;
        
        public PackController(IPackService packService, IWolfPackService wolfPackService)
        {
            _packService = packService;
            _wolfPackService = wolfPackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPacks()
        {
            return Ok(await _packService.GetAll());
            
        }
        
        [HttpGet("{packId}/wolfs")]
        public async Task<IActionResult> GetWolfsForPack([FromRoute] Guid packId)
        {
            return Ok(await _wolfPackService.GetWolfsForPack(packId));
            
        }

        [HttpPost]
        public async Task<IActionResult> CreatePack([FromBody] PackForModificationModel packForModificationModel)
        {
            var (pack, validationResult) = await _packService.Create(packForModificationModel);

            if (validationResult != null)
                return BadRequest(validationResult);
            return Ok(pack);
        }
        
        [HttpPut ("{id}")]
        public async Task<IActionResult> EditPack([FromRoute] Guid id, [FromBody] PackForModificationModel packForModificationModel)
        {
            var (pack, validationResult, serviceResult) = await _packService.Edit(id, packForModificationModel);

            switch (serviceResult)
            {
                case Enums.ServiceResult.NotFound:
                    return NotFound();
                case Enums.ServiceResult.Ok:
                default:
                    if (validationResult != null)
                        return BadRequest(validationResult);
                    return Ok(pack);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePack([FromRoute] Guid id)
        {
            var serviceResult = await _packService.Delete(id);
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