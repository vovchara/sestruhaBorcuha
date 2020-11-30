using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.Model
{
    public class LevelConfigStorage
    {
        private static LevelConfigStorage instance;
 
        public static LevelConfigStorage getInstance()
        {
            if (instance == null)
            {
                instance = new LevelConfigStorage();
            }

            return instance;
        }

        private readonly LevelConfigModel[] _levels;

        private LevelConfigStorage()
        {
            var isLevelConfigExists = System.IO.File.Exists(ConfigConstants.LevelConfigPath);
            if (isLevelConfigExists)
            {
                var levelConfigs = System.IO.File.ReadAllText(ConfigConstants.LevelConfigPath);
                var configsArray = JsonConvert.DeserializeObject<LevelConfigModel[]>(levelConfigs);
                _levels = configsArray;
            }
            else
            {
                return;
            }
        }

        public LevelConfigModel[] GetAllLevelConfigs()
        {
            return _levels;
        }

        public LevelConfigModel GetConfig(int levelId)
        {
            for (var i=0; i<_levels.Length; i++)
            {
                var singleLvl = _levels[i];
                if (singleLvl.LevelId == levelId)
                {
                    return singleLvl;
                }
            }

            Debug.WriteLine($"No config for level:{levelId}");
            return null;
        }
    }
}
