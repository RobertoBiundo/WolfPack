using System;
using System.Data;
using Database.Entities;
using FluentValidation;
using Services.Models;

namespace Services.Validators
{
    public class WolfForModificationValidator : AbstractValidator<WolfForModificationModel>
    {
        public WolfForModificationValidator()
        {
            // Validate that the first name is not empty of correct length
            RuleFor(model => model.FirstName)
                .NotEmpty()
                .MaximumLength(Wolf.FirstNameMaxLength);

            // Validate that the insertion name is of correct length
            RuleFor(model => model.InsertionName)
                .MaximumLength(Wolf.InsertionNameMaxLength);

            // Validate that the last name is not empty of correct length
            RuleFor(model => model.LastName)
                .NotEmpty()
                .MaximumLength(Wolf.LastNameMaxLength);

            // Validate that the gender is a valid enum value
            RuleFor(model => model.Gender)
                .NotNull()
                .IsInEnum();

            // If a birthdate is provided it cannot be in the future
            When(model => model.BirthDate != null, () =>
            {
                RuleFor(model => model.BirthDate)
                    .Must(birthdate => birthdate <= DateTimeOffset.UtcNow)
                    .WithMessage("BirthDate cannot be set in the future");
            });

            // If lat or long are provided both of them should be provided
            When(model => model.Latitude != null, () =>
            {
                RuleFor(model => model.Longitude)
                    .Must(longitude => longitude != null)
                    .WithMessage("If you provide latitude you must also provide longitude");
            });
            
            When(model => model.Longitude != null, () =>
            {
                RuleFor(model => model.Latitude)
                    .Must(latitude => latitude != null)
                    .WithMessage("If you provide longitude you must also provide a latitude");
            });

            // If lat and long are provided they must comply to the precision values
            // And be between valid values for lat and long
            When(model => model.Latitude != null && model.Longitude != null, () =>
            {
                RuleFor(model => model.Latitude)
                    .ScalePrecision(Wolf.LatitudeScale, Wolf.LatitudePrecision)
                    .LessThanOrEqualTo(90)
                    .GreaterThanOrEqualTo(-90);
                
                RuleFor(model => model.Longitude)
                    .ScalePrecision(Wolf.LongitudeScale, Wolf.LongitudePrecision)
                    .LessThanOrEqualTo(180)
                    .GreaterThanOrEqualTo(-180);
            });



        }
    }
}