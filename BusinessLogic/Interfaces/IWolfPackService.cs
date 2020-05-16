using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Database.Entities;
using Services.Models;

namespace Services.Interfaces
{
    public interface IWolfPackService
    {
        Task<List<WolfPackModel>> GetPacksForWolf(Guid wolfId);
        Task<List<WolfPackModel>> GetWolfsForPack(Guid packId);
        Task<Enums.ServiceResult> AddWolfToPack(WolfPackForModificationModel wolfPackForModification);
        Task<Enums.ServiceResult> RemoveWolfFromPack(WolfPackForModificationModel wolfPackForModification);
    }
}