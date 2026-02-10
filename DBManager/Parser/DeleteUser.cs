using System;
using System.Collections.Generic;
using System.Text;
using DbManager.Parser;

namespace DbManager
{
 
  public class DeleteUser : MiniSqlQuery
  {
    public string Username { get; private set; }

    public DeleteUser(string username)
    {
      //TODO DEADLINE 4: Initialize member variables
      Username = username;
    }
    public string Execute(Database database)
    {
      //TODO DEADLINE 5: Run the query and return the appropriate message
      //UsersProfileIsNotGrantedRequiredPrivilege, UserDoesNotExistError, DeleteUserSuccess

      var profile = database.SecurityManager.ProfileByUser(Username);

      if (profile != null) {
        //ASK TEACHER HOW CAN I CHECK HERE IF UsersProfileIsNotGrantedRequiredPrivilege WHEN I ONLY HAVE THE DATABASE AND NOT THE TABLE
        //if (profile.IsGrantedPrivilege(table ?, Security.Privilege.Insert))
        //{
        //  foreach (var user in profile.Users)
        //  {
        //    if (user.Username == Username)
        //    {
        //      profile.Users.Remove(user);
        //      return Constants.DeleteUserSuccess;
        //    }
        //  }
        //}
        //return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
      }
      return Constants.UserDoesNotExistError;
    }

  }
}
