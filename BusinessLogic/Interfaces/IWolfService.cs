using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Database;
using FluentValidation.Results;
using Services.Models;

namespace Services.Interfaces
{
    public interface IWolfService
    {
        Task<List<WolfModel>> GetAll();
        Task<(WolfModel, ValidationResult)> Create(WolfForModificationModel wolfForModification);

        Task<(WolfModel, ValidationResult, Enums.ServiceResult)> Edit(Guid id,
            WolfForModificationModel wolfForModification);
        
        Task<Enums.ServiceResult> Delete(Guid id);
    }
}