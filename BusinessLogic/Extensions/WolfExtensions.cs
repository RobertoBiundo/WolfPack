using Database.Entities;
using Services.Models;

namespace Services.Extensions
{
    public static class WolfExtensions
    {
        public static WolfModel ToWolfModel(this Wolf wolf)
        {
            return new WolfModel
            {
                Id = wolf.Id,
                FirstName = wolf.FirstName,
                InsertionName = wolf.InsertionName,
                LastName = wolf.LastName,
                Gender = wolf.Gender,
                BirthDate = wolf.BirthDate,
                Latitude = wolf.Latitude,
                Longitude = wolf.Longitude
            };
        }
    }
}