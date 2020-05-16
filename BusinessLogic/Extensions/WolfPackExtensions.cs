using Database.Entities;
using Services.Models;

namespace Services.Extensions
{
    public static class WolfPackExtensions
    {
        public static WolfPackModel ToWolfPackModel(this WolfPack wolfPack)
        {
            return new WolfPackModel
            {
                WolfId = wolfPack.WolfId,
                PackId = wolfPack.PackId
            };
        }
    }
}