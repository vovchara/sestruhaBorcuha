

namespace Scene.Src.Model
{
    public class UserModel
    {
        public string UserName { get; set; }
        public long UserScore { get; set; }

        public UserModel(string name, long score)
        {
            UserName = name;
            UserScore = score;
        }
    }
}
