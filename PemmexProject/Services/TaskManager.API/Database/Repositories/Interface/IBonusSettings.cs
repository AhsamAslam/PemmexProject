using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Database.Repositories.Interface
{
    public interface IBonusSettings
    {
        Task<BonusSettings> GetBonusSettingsByBusinessIdentifier(string businessIdentifier);
        Task AddBonusSettings(BonusSettings BonusSettings);
        Task UpdateBonusSettings(BonusSettings BonusSettings);
        Task DeleteBonusSettings(BonusSettings BonusSettings);
    }
}
