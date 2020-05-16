using Database.Entities;
using FluentValidation;
using Services.Models;

namespace Services.Validators
{
    public class PackForModificationValidator : AbstractValidator<PackForModificationModel>
    {
        public PackForModificationValidator()
        {
            RuleFor(model => model.Name)
                .MaximumLength(Pack.NameMaxLength);
            
            RuleFor(model => model.Description)
                .MaximumLength(Pack.DescriptionMaxLength);
        }
    }
}