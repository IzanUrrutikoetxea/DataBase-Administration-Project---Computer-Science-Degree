using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManager.Security
{
  public class Profile
  {
    public const string AdminProfileName = "Admin";
    public string Name { get; set; }
    public List<User> Users { get; set; } = new List<User>();
    //ASK TEACHER IF THE STRING OF THE DICTIONARY ARE TABLES OR DATABASES
    public Dictionary<string, List<Privilege>> PrivilegesOn { get; private set; } = new Dictionary<string, List<Privilege>>();

    public bool GrantPrivilege(string table, Privilege privilege)
    {
      //TODO DEADLINE 5: Grant this privilege on this table. Return false if there is an error, true otherwise
      if (string.IsNullOrEmpty(table)) return false;

      if (!PrivilegesOn.ContainsKey(table)) return false;

      if (PrivilegesOn[table].Contains(privilege)) return false;

      PrivilegesOn[table].Add(privilege);
      return true;
    }

    public bool RevokePrivilege(string table, Privilege privilege)
    {
      //TODO DEADLINE 5: Revoke this privilege on this table. Return false if there is an error, true otherwise
      if (string.IsNullOrEmpty(table)) return false;

      if (!PrivilegesOn.ContainsKey(table)) return false;

      if (!PrivilegesOn[table].Contains(privilege)) return false;

      PrivilegesOn[table].Remove(privilege);
      return true;
    }

    public bool IsGrantedPrivilege(string table, Privilege privilege)
    {
      //TODO DEADLINE 5: Return whether this profile is granted this privilege on this table

      if (PrivilegesOn[table].Contains(privilege)) return true;
      return false;
    }
    public List<string> PrivilegesToString()
    {
      var result = new List<string>();
      var tablesToString = "";
      var privilegesToString = "";

      foreach (var value in PrivilegesOn)
      {
        tablesToString += value.Key + ",";

        foreach (var privilege in value.Value)
        {
          privilegesToString += privilege.ToString() + ",";
        }

        if (privilegesToString.EndsWith(","))
          privilegesToString = privilegesToString.Substring(0, privilegesToString.Length - 1);

        privilegesToString += ";";
      }

      if (tablesToString.EndsWith(","))
        tablesToString = tablesToString.Substring(0, tablesToString.Length - 1);

      if (privilegesToString.EndsWith(";"))
        privilegesToString = privilegesToString.Substring(0, privilegesToString.Length - 1);

      result.Add(tablesToString);
      result.Add(privilegesToString);
      return result;
    }
  }
}
