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
            _levels = new []
            {
                new LevelConfigModel(1, 30, 0.5f, 0),
                new LevelConfigModel(2, 25, 1, 1),
                new LevelConfigModel(3, 15, 0.8f, 2),
                new LevelConfigModel(4, 10, 0.7f, 3)
            };
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
