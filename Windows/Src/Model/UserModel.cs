namespace Scene.Model
{
    public class UserModel
    {
        public string Name { get; }
        public long Score { get; }

        public UserModel(string name, long score)
        {
            Name = name;
            Score = score;
        }
    }
}