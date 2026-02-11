using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DbManager.Parser;
using DbManager.Security;

namespace DbManager
{
 
  public class AddUser : MiniSqlQuery
  {
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string ProfileName { get; private set; }


    public AddUser(string username, string password, string profileName)
    {
      //TODO DEADLINE 4: Initialize member variables
      Username = username;
      Password = password;
      ProfileName = profileName;
    }
    public string Execute(Database database)
    {
      //TODO DEADLINE 5: Run the query and return the appropriate message
      //UsersProfileIsNotGrantedRequiredPrivilege, SecurityProfileDoesNotExistError, AddUserSuccess
      var profile = database.SecurityManager.ProfileByName(ProfileName);
      if (profile != null)
      {
        //ASK TEACHER HOW CAN I CHECK HERE IF UsersProfileIsNotGrantedRequiredPrivilege WHEN I ONLY HAVE THE DATABASE AND NOT THE TABLE
        if (database.SecurityManager.IsUserAdmin())
        {
          var user = new User(Username, Password);
          profile.Users.Add(user);
          database.SecurityManager.AddProfile(profile);
          return Constants.AddUserSuccess;
        }
        return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
      }
      return Constants.SecurityProfileDoesNotExistError;
    }
  }
}
