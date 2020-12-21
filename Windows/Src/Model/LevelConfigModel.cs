namespace Scene.Src.Model
{
    public class LevelConfigModel
    {
        public int LevelId { get; }
        public int LevelTimeSec { get; }
        public float ButtonsActiveTimeSec { get; }
        public long MinScores { get; }
        public int ButtonsAmount { get; }

        public LevelConfigModel(int levelId, int levelTimeSec, float buttonsActiveTimeSec, long minScores, int buttonsAmount)
        {
            LevelId = levelId;
            LevelTimeSec = levelTimeSec;
            ButtonsActiveTimeSec = buttonsActiveTimeSec;
            MinScores = minScores;
            ButtonsAmount = buttonsAmount;
        }
    }
}
