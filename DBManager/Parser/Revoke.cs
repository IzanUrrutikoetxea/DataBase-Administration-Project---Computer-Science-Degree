using System;
using System.Collections.Generic;
using System.Text;
using DbManager.Parser;

namespace DbManager
{
 
  public class Revoke : MiniSqlQuery
  {
    public string PrivilegeName { get; set; }
    public string TableName { get; set; }
    public string ProfileName { get; set; }

    public Revoke(string privilegeName, string tableName, string profileName)
    {
      //TODO DEADLINE 4: Initialize member variables
      PrivilegeName = privilegeName;
      TableName = tableName;
      ProfileName = profileName;
    }
    public string Execute(Database database)
    {
      //TODO DEADLINE 5: Run the query and return the appropriate message
      //UsersProfileIsNotGrantedRequiredPrivilege, SecurityProfileDoesNotExistError, RevokePrivilegeSuccess, 
      var profile = database.SecurityManager.ProfileByName(ProfileName);
      var privilege = new Security.Privilege();

      if (PrivilegeName == "INSERT") privilege = Security.Privilege.Insert;
      else if (PrivilegeName == "UPDATE") privilege = Security.Privilege.Update;
      else if (PrivilegeName == "SELECT") privilege = Security.Privilege.Select;
      else if (PrivilegeName == "DELETE") privilege = Security.Privilege.Delete;
      else return Constants.PrivilegeDoesNotExistError;

      if (profile != null)
      {
        //ASK TEACHER HOW CAN I CHECK HERE IF UsersProfileIsNotGrantedRequiredPrivilege WHEN I ONLY HAVE THE DATABASE AND NOT THE TABLE
        //if (profile.IsGrantedPrivilege(table ?, Security.Privilege.Insert))
        //{
        profile.RevokePrivilege(TableName, privilege);
        return Constants.RevokePrivilegeSuccess;
        //}
        //return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
      }
      return Constants.SecurityProfileDoesNotExistError;

    }

  }
}
