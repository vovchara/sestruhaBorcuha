using Scene.Model;

namespace Scene.Storage
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
        
        public UserModel User { get; set; }
    }
}