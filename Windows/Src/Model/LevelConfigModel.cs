using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.Model
{
    public class LevelConfigModel
    {
        public int LevelId { get; }
        public int LevelTimeSec { get; }
        public float ButtonsActiveTimeSec { get; }

        public LevelConfigModel(int levelId, int levelTimeSec, float buttonsActiveTimeSec)
        {
            LevelId = levelId;
            LevelTimeSec = levelTimeSec;
            ButtonsActiveTimeSec = buttonsActiveTimeSec;
        }
    }
}
