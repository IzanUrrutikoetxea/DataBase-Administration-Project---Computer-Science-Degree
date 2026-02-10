using System;
using System.Collections.Generic;
using System.Text;
using DbManager.Parser;

namespace DbManager
{
 
  public class CreateSecurityProfile : MiniSqlQuery
  {
    public string ProfileName { get; set; }

    public CreateSecurityProfile(string profileName)
    {
      //TODO DEADLINE 4: Initialize member variables
      ProfileName = profileName;
    }
    public string Execute(Database database)
    {
      //TODO DEADLINE 5: Run the query and return the appropriate message
      //UsersProfileIsNotGrantedRequiredPrivilege, CreateSecurityProfileSuccess

      //ASK TEACHER HOW CAN I CHECK HERE IF UsersProfileIsNotGrantedRequiredPrivilege WHEN I ONLY HAVE THE DATABASE AND NOT THE TABLE
      //if (profile.IsGrantedPrivilege(table?, Security.Privilege.Insert))
      //{
      //  var profile = new Profile();
      //  profile.Name = profileName;
      //  database.SecurityManager.AddProfile(profile);
      //  return Constants.CreateSecurityProfileSuccess;
      //}
      return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
    }
  }
}
