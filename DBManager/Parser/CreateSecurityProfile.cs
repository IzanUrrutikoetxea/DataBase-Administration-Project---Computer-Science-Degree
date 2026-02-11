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

      if (database.SecurityManager.IsUserAdmin())
      {
        var profile = new DbManager.Security.Profile();
        profile.Name = ProfileName;
        database.SecurityManager.AddProfile(profile);
        return Constants.CreateSecurityProfileSuccess;
      }
      return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
    }
  }
}
