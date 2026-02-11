using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DbManager.Security
{
  public class Manager
  {
    public List<Profile> Profiles { get; private set; } = new List<Profile>();

    private string m_username;
    public Manager(string username)
    {
      m_username = username;
    }

    public bool IsUserAdmin()
    {
      //TODO DEADLINE 5: Return true if the user logged-in (m_username) is the admin, false otherwise
      if (m_username == Profile.AdminProfileName) return true;
      return false;
    }

    public bool IsPasswordCorrect(string username, string password)
    {
      //TODO DEADLINE 5: Return true if the user's password is correct. The given password should be encrypted before comparing with the saved one
      var encryptedPassword = Encryption.Encrypt(password);
      foreach (var profile in Profiles)
      {
        foreach (var user in profile.Users)
        {
          if (user.Username == username && user.EncryptedPassword == encryptedPassword) return true;
        }
      }
      return false;
    }

    public void GrantPrivilege(string profileName, string table, Privilege privilege)
    {
      //TODO DEADLINE 5: Add this privilege on this table to the profile with this name
      //If the profile or the table don't exist, do nothing 
      foreach(var profile in Profiles)
      {
        if (profile.Name == profileName)
        {
          profile.GrantPrivilege(table, privilege);
        }
      }
    }

    public void RevokePrivilege(string profileName, string table, Privilege privilege)
    {
      //TODO DEADLINE 5: Remove this privilege on this table to the profile with this name
      //If the profile or the table don't exist, do nothing
      foreach (var profile in Profiles)
      {
        if (profile.Name == profileName)
        {
          profile.RevokePrivilege(table, privilege);
        }
      }
    }

    public bool IsGrantedPrivilege(string username, string table, Privilege privilege)
    {
      //TODO DEADLINE 5: Return true if the username has this privilege on this table. False otherwise (also in case of error)
      try
      {
        foreach (var profile in Profiles)
        {
          foreach (var user in profile.Users)
          {
            if (user.Username == username)
            {
              return profile.IsGrantedPrivilege(table, privilege);
            }
          }
        }
        return false;
      }
      catch
      {
        return false;
      }
    }

    public void AddProfile(Profile profile)
    {
      //TODO DEADLINE 5: Add this profile
      if (!Profiles.Contains(profile)) {  Profiles.Add(profile); }
    }

    public User UserByName(string username)
    {
      //TODO DEADLINE 5: Return the user by name. If it doesn't exist, return null
      foreach (var profile in Profiles)
      {
        foreach (var user in profile.Users)
        {
          if (user.Username == username)
          {
            return user;
          }
        }
      }
      return null;
    }

    public Profile ProfileByName(string profileName)
    {
      //TODO DEADLINE 5: Return the profile by name. If it doesn't exist, return null
      foreach (var profile in Profiles)
      {
        if (profile.Name == profileName)
        {
          return profile;
        }
      }
      return null;
    }

    public Profile ProfileByUser(string username)
    {
      //TODO DEADLINE 5: Return the profile by user. If the user doesn't exist, return null
      foreach (var profile in Profiles)
      {
        foreach (var user in profile.Users)
        {
          if (user.Username == username)
          {
            return profile;
          }
        }
      }
      return null;
    }

    public bool RemoveProfile(string profileName)
    {
      //TODO DEADLINE 5: Remove this profile
      if (ProfileByName(profileName) != null)
      {
        Profiles.Remove(ProfileByName(profileName));
        return true;
      }
      return false;
    }

    public static Manager Load(StreamReader reader, string username)
    {
      //TODO DEADLINE 5: Load all the profiles and users saved for this database. The Manager instance should be created with the given username
      var manager = new Manager(username);
      var line = reader.ReadLine();
      while (line != null)
      {
        var profile = new Profile();
        profile.Name = line;
        line = reader.ReadLine();
        var usernames = line.Split(";");
        line = reader.ReadLine();
        var passwords = line.Split(";");
        for (int i = 0; i < usernames.Length; i++)
        {
          var user = new User(usernames[i], passwords[i]);
          profile.Users.Add(user);
        }
        line = reader.ReadLine();
        var tables = line.Split(",");
        line = reader.ReadLine();
        var privilegesGroup = line.Split(";");
        for (int i=0; i < tables.Length; i++)
        {
          var privilegeArray = privilegesGroup[i].Split(",");
          var privilegeList = new List<Privilege>();
          foreach (var privilege in privilegeArray)
          {
            if (privilege == "Select") privilegeList.Add(Privilege.Select);
            else if (privilege == "Update") privilegeList.Add(Privilege.Update);
            else if (privilege == "Insert") privilegeList.Add(Privilege.Insert);
            else if (privilege == "Delete") privilegeList.Add(Privilege.Delete);
          }
          profile.PrivilegesOn.Add(tables[i], privilegeList);
        }
        manager.AddProfile(profile);
        line = reader.ReadLine();
      }
      return manager;
    }

    public void Save(StreamWriter writer)
    {
      //TODO DEADLINE 5: Save all the profiles and users/passwords created for this database.
      foreach(var profile in Profiles)
      {
        //Profile-name
        writer.WriteLine(profile.Name);
        //Users
        var usernameLine = "";
        var passwordLine = "";
        for (int i = 0; i < profile.Users.Count; i++)
        {
          if (i == profile.Users.Count - 1) usernameLine += profile.Users[i].Username;
          else usernameLine += profile.Users[i].Username + ",";
        }
        for (int i = 0; i < profile.Users.Count; i++)
        {
          if (i == profile.Users.Count - 1) passwordLine += profile.Users[i].EncryptedPassword;
          else passwordLine += profile.Users[i].EncryptedPassword + ",";
        }
        writer.WriteLine(usernameLine);
        writer.WriteLine(passwordLine);
        //Dictionary
        var dictionary = profile.PrivilegesToString();
        writer.WriteLine(dictionary[0]);
        writer.WriteLine(dictionary[1]);
      }
      writer.Close();
    }
  }
}
