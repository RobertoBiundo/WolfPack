using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Database;
using Database.Entities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Services.Extensions;
using Services.Interfaces;
using Services.Models;
using Services.Validators;

namespace Services.Services
{
    public class PackService : IPackService
    {
        private readonly AppDatabaseContext _dbContext;
        
        public PackService(AppDatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<PackModel>> GetAll()
        {
            return await _dbContext.Packs.Select(s => s.ToPackModel()).ToListAsync();
        }
        
        public async Task<(PackModel, ValidationResult)> Create(PackForModificationModel packForModificationModel)
        {
            // We need to validate that the model is valid
            var validator = new PackForModificationValidator();
            var validatorResult = await validator.ValidateAsync(packForModificationModel);

            if (!validatorResult.IsValid)
                return (null, validatorResult);
            
            // All is ok we can add the object
            var pack = new Pack
            {
                Name = packForModificationModel.Name,
                Description = packForModificationModel.Description,
                CreatedOnDateTimeOffsetUtc = DateTimeOffset.UtcNow,
                LastModifiedDateTimeOffsetUtc = DateTimeOffset.UtcNow
            };

            _dbContext.Add(pack);
            await _dbContext.SaveChangesAsync();

            return (pack.ToPackModel(), null);
        }

        public async Task<(PackModel, ValidationResult, Enums.ServiceResult)> Edit(Guid id, PackForModificationModel packForModification)
        {
            // Try to get the pack
            var pack = await _dbContext.Packs.FirstOrDefaultAsync(w => w.Id == id);

            if (pack == null)
                return (null, null, Enums.ServiceResult.NotFound);
            
            // We need to validate that the model is valid
            var validator = new PackForModificationValidator();
            var validatorResult = await validator.ValidateAsync(packForModification);

            if (!validatorResult.IsValid)
                return (null, validatorResult,Enums.ServiceResult.Ok);

            // All is ok we can update the object
            pack.Name = packForModification.Name;
            pack.Description = packForModification.Description;

            // Update the last modify only if the object has changed
            if (_dbContext.ChangeTracker.HasChanges())
                pack.LastModifiedDateTimeOffsetUtc = DateTimeOffset.UtcNow;
            
            await _dbContext.SaveChangesAsync();

            return (pack.ToPackModel(), null, Enums.ServiceResult.Ok);
        }
        
        public async Task<Enums.ServiceResult> Delete(Guid id)
        {
            // Try to get the pack
            var pack = await _dbContext.Packs.FirstOrDefaultAsync(w => w.Id == id);

            if (pack == null)
                return Enums.ServiceResult.NotFound;

            _dbContext.Remove(pack);
            await _dbContext.SaveChangesAsync();
            return Enums.ServiceResult.Ok;
        }
    }
}