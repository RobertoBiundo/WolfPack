using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using FluentValidation.Results;
using Services.Models;

namespace Services.Interfaces
{
    public interface IPackService
    {
        Task<List<PackModel>> GetAll();
        Task<(PackModel, ValidationResult)> Create(PackForModificationModel wolfForModification);

        Task<(PackModel, ValidationResult, Enums.ServiceResult)> Edit(Guid id, PackForModificationModel wolfForModification);
        
        Task<Enums.ServiceResult> Delete(Guid id);
    }
}