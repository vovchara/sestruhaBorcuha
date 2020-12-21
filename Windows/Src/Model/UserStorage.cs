using Scene.Src.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Scene.Model
{
    public class UserStorage
    {
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
            if (CheckExistingUsers(userModel.UserName))
            {
                Debug.WriteLine($"User already exists");
                return;
            }
            MyUserModel = userModel;
            AllUsers.Add(userModel);
        }
        public UserModel []  AllUsersSortedByScore()
        {
            var sortedUsers = AllUsers.OrderByDescending(user => user.UserScore).ToArray();
            return sortedUsers;
        }
    }
}