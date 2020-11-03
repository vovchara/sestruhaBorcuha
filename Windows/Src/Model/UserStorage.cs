using Scene.Src.Model;
using System.Collections.Generic;

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

        public List<UserModel> AllUsers { get; set; }
        public UserModel MyUserModel { get; set; }

        public bool CheckExistingUsers(string userName)
        {
              foreach (var oneUser in AllUsers)
              {
                  if (oneUser.UserName == userName)
                  {
                      return true;                 
                  }
              }
            return false;
        }

        public void AddMyUser(UserModel userModel)
        {
            getInstance().MyUserModel = userModel;
            getInstance().AllUsers.Add(userModel);
        }
    }
}