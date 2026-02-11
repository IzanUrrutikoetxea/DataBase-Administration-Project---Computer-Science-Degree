using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DbManager;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DbManager.Network
{
  public static class XmlDeserializer
  {
    public static bool ParseOpen(string command, out string database, out string username, out string password)
    {
      //TODO DEADLINE 6: Try to parse the xml command using the specified xml format (eGela)
      //Return true if 'command' is an Open statement, false otherwise. If true, set the value of database, username and password
      database = null;
      username = null;
      password = null;

      var openMatch = new Regex(@"^<Open Database=""(?<database>[a-z]+)"" User=""(?<username>[a-z]+)"" Password=""(?<password>[a-z]+)""/>$", RegexOptions.None).Match(command);

      if (openMatch.Success)
      {
        database = openMatch.Groups["database"].Value;
        username = openMatch.Groups["username"].Value;
        password = openMatch.Groups["password"].Value;
        return true;
      }
           
      return false;
    }

    public static bool ParseOpenCreateAnswer(string answer, out string error)
    {
      //TODO DEADLINE 6: Try to parse the answer to an Open/Create command.
      //Return true if 'command' is equal to XmlSerializer.OpenCreateSuccess
      //If it is an error (<Error>...</Error>), return false and set 'error' with the error message

      error = null;

      if (answer.Equals(XmlSerializer.OpenCreateSuccess)) return true;

      var errorMatch = new Regex(@"^<Error>(?<error>.+)</Error>$", RegexOptions.None).Match(answer);

      if (errorMatch.Success)
      { 
        error = errorMatch.Groups["error"].Value;
        return false;
      }

      return false;
    }

    public static bool ParseCreate(string command, out string database, out string username, out string password)
    {
      //TODO DEADLINE 6: Try to parse a Create xml command using the specified xml format (eGela)
      //Return true if 'command' is a Create statement, false otherwise. If true, set the value of database, username and password

      database = null;
      username = null;
      password = null;

      var createMatch = new Regex(@"^<Create Database=""(?<database>[a-z]+)"" User=""(?<username>[a-z]+)"" Password=""(?<password>[a-z]+)""/>$", RegexOptions.None).Match(command);

      if (createMatch.Success)
      {
        database = createMatch.Groups["database"].Value;
        username = createMatch.Groups["username"].Value;
        password = createMatch.Groups["password"].Value;
        return true;
      }

      return false;
    }



    public static bool ParseQuery(string command, out string query)
    {
      //TODO DEADLINE 6: Try to parse a Query xml command using the specified xml format (eGela)
      //Return true if 'command' is a Query statement, false otherwise. If true, set the value of query with the content of the command

      query = null;

      var queryMatch = new Regex(@"^<QUERY>(?<query>[a-zA-Z/,.]+)</Query>$", RegexOptions.None).Match(command);

      if (queryMatch.Success)
      {
        query = queryMatch.Groups["query"].Value;
        return true;
      }

      return false;
    }

    public static bool ParseQueryAnswer(string answer, out string answerContent)
    {
      //TODO DEADLINE 6: Try to parse the answer to a Query command.
      //Return true if 'command' does not contain an error inside (<Error>...</Error>)
      //If it is an error (<Error>...</Error>), return false and set 'answerContent' with the error message

      answerContent = null;

      var errorMatch = new Regex(@"^<Error>(?<answerContent>.+)</Error>$", RegexOptions.None).Match(answer);

      if (errorMatch.Success)
      {
        answerContent = errorMatch.Groups["answerContent"].Value;
        return false;
      }

      return true;
    }

    public static bool IsCloseCommand(string command)
    {
      return command == XmlSerializer.CloseConnection;
    }
  }
}
