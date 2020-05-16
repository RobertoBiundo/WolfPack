using Database.Entities;
using Services.Models;

namespace Services.Extensions
{
    public static class PackExtensions
    {
        public static PackModel ToPackModel(this Pack pack)
        {
            return new PackModel()
            {
                Id = pack.Id,
                Name = pack.Name,
                Description = pack.Description
            };
        }
    }
}