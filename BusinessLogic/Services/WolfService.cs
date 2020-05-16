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
    public class WolfService : IWolfService
    {
        private readonly AppDatabaseContext _dbContext;
        
        public WolfService(AppDatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<WolfModel>> GetAll()
        {
            return await _dbContext.Wolfs.Select(s => s.ToWolfModel()).ToListAsync();
        }
        
        public async Task<(WolfModel, ValidationResult)> Create(WolfForModificationModel wolfForModification)
        {
            // We need to validate that the model is valid
            var validator = new WolfForModificationValidator();
            var validatorResult = await validator.ValidateAsync(wolfForModification);

            if (!validatorResult.IsValid)
                return (null, validatorResult);
            
            // All is ok we can add the object
            var wolf = new Wolf
            {
                FirstName = wolfForModification.FirstName,
                InsertionName = wolfForModification.InsertionName,
                LastName = wolfForModification.LastName,
                Gender = wolfForModification.Gender,
                BirthDate = wolfForModification.BirthDate,
                Latitude = wolfForModification.Latitude,
                Longitude = wolfForModification.Longitude,
                CreatedOnDateTimeOffsetUtc = DateTimeOffset.UtcNow,
                LastModifiedDateTimeOffsetUtc = DateTimeOffset.UtcNow
            };

            _dbContext.Add(wolf);
            await _dbContext.SaveChangesAsync();

            return (wolf.ToWolfModel(), null);
        }

        public async Task<(WolfModel, ValidationResult, Enums.ServiceResult)> Edit(Guid id, WolfForModificationModel wolfForModification)
        {
            // Try to get the wolf
            var wolf = await _dbContext.Wolfs.FirstOrDefaultAsync(w => w.Id == id);

            if (wolf == null)
                return (null, null, Enums.ServiceResult.NotFound);
            
            // We need to validate that the model is valid
            var validator = new WolfForModificationValidator();
            var validatorResult = await validator.ValidateAsync(wolfForModification);

            if (!validatorResult.IsValid)
                return (null, validatorResult,Enums.ServiceResult.Ok);

            // All is ok we can update the object
            wolf.FirstName = wolfForModification.FirstName;
            wolf.InsertionName = wolfForModification.InsertionName;
            wolf.LastName = wolfForModification.LastName;
            wolf.Gender = wolfForModification.Gender;
            wolf.BirthDate = wolfForModification.BirthDate;
            wolf.Latitude = wolfForModification.Latitude;
            wolf.Longitude = wolfForModification.Longitude;

            // Update the last modify only if the object has changed
            if (_dbContext.ChangeTracker.HasChanges())
                wolf.LastModifiedDateTimeOffsetUtc = DateTimeOffset.UtcNow;
            
            await _dbContext.SaveChangesAsync();

            return (wolf.ToWolfModel(), null, Enums.ServiceResult.Ok);
        }
        
        public async Task<Enums.ServiceResult> Delete(Guid id)
        {
            // Try to get the wolf
            var wolf = await _dbContext.Wolfs.FirstOrDefaultAsync(w => w.Id == id);

            if (wolf == null)
                return Enums.ServiceResult.NotFound;

            _dbContext.Remove(wolf);
            await _dbContext.SaveChangesAsync();
            return Enums.ServiceResult.Ok;
        }
    }
}