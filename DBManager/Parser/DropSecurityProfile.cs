using System;
using System.Collections.Generic;
using System.Text;
using DbManager.Parser;

namespace DbManager
{

  public class DropSecurityProfile : MiniSqlQuery
  {
    public string ProfileName { get; set; }

    public DropSecurityProfile(string profileName)
    {
      //TODO DEADLINE 4: Initialize member variables
      ProfileName = profileName;
    }
    public string Execute(Database database)
    {
      //TODO DEADLINE 5: Run the query and return the appropriate message
      //UsersProfileIsNotGrantedRequiredPrivilege, SecurityProfileDoesNotExistError, DropSecurityProfileSuccess
      var profile = database.SecurityManager.ProfileByName(ProfileName);

      if (profile != null)
      {
        if (database.SecurityManager.IsUserAdmin())
        {
          foreach (var profileDatabase in database.SecurityManager.Profiles)
          {
            if (profileDatabase.Name == ProfileName)
            {
              database.SecurityManager.Profiles.Remove(profileDatabase);
              return Constants.DropSecurityProfileSuccess;
            }
          }
        }
        return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
      }
      return Constants.SecurityProfileDoesNotExistError;
    }
  }
}
