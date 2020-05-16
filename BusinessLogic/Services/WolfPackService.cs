using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Extensions;
using Services.Interfaces;
using Services.Models;

namespace Services.Services
{
    public class WolfPackService : IWolfPackService
    {
        private readonly AppDatabaseContext _dbContext;
        
        public WolfPackService(AppDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WolfPackModel>> GetPacksForWolf(Guid wolfId)
        {
            // Try to get the wolf and pack
            var wolfPacks = await _dbContext.WolfPack.Where(w => w.WolfId == wolfId).Select(s => s.ToWolfPackModel()).ToListAsync();
            return wolfPacks;
        }
        
        public async Task<List<WolfPackModel>> GetWolfsForPack(Guid packId)
        {
            // Try to get the wolf and pack
            var wolfPacks = await _dbContext.WolfPack.Where(w => w.PackId == packId).Select(s => s.ToWolfPackModel()).ToListAsync();
            return wolfPacks;
        }
        
        public async Task<Enums.ServiceResult> AddWolfToPack(WolfPackForModificationModel wolfPackForModification)
        {
            // Try to get the wolf and pack
            var wolf = await _dbContext.Wolfs.FirstOrDefaultAsync(w => w.Id == wolfPackForModification.WolfId);
            var pack = await _dbContext.Packs.FirstOrDefaultAsync(w => w.Id == wolfPackForModification.PackId);

            if (wolf == null || pack == null)
                return Enums.ServiceResult.NotFound;

            // Check if relationship already exists
            // If it does its ok to just say it was created
            var existingWolfPack = await _dbContext.WolfPack.FirstOrDefaultAsync(w => w.WolfId == wolfPackForModification.WolfId && w.PackId == wolfPackForModification.PackId);
            if (existingWolfPack != null)
                return Enums.ServiceResult.Ok;
            
            var wolfPack = new WolfPack
            {
                WolfId = wolfPackForModification.WolfId,
                PackId = wolfPackForModification.PackId
            };

            _dbContext.Add(wolfPack);
            await _dbContext.SaveChangesAsync();
            
            return Enums.ServiceResult.Ok;
        }

        public async Task<Enums.ServiceResult> RemoveWolfFromPack(WolfPackForModificationModel wolfPackForModification)
        {
            //Try to get the WolfPack
            var wolfPack = await _dbContext.WolfPack.FirstOrDefaultAsync(w => w.WolfId == wolfPackForModification.WolfId && w.PackId == wolfPackForModification.PackId);

            if (wolfPack == null)
                return Enums.ServiceResult.NotFound;

            _dbContext.Remove(wolfPack);
            await _dbContext.SaveChangesAsync();
            return Enums.ServiceResult.Ok;
        }
    }
}