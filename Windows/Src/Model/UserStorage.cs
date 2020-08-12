namespace Scene.Model
{
    public class UserStorage
    {
        private static UserStorage instance;
 
        private UserStorage()
        {}
 
        public static UserStorage getInstance()
        {
            if (instance == null)
            {
                instance = new UserStorage();
            }

            return instance;
        }
        
        public string UserName { get; set; }
        public long UserScore { get; set; }
    }
}