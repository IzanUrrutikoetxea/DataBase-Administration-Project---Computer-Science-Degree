using DbManager.Parser;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace DbManager
{
  public class MiniSQLParser
  {
    public static MiniSqlQuery Parse(string miniSQLQuery)
    {
      //TODO DEADLINE 2
      //SELECT ColumnName[,ColumnName…] FROM Table [WHERE Condition]
      const string selectPattern = null;
      //INSERT INTO TableName VALUES (LiteralValue[,LiteralValue, …])
      const string insertPattern = null;
      //DROP TABLE TableName
      const string dropTablePattern = @"^DROP\s+TABLE\s+(?<table>\w+)$";
      var dropTable = new Regex(dropTablePattern, RegexOptions.IgnoreCase);

      //Note: The parsing of CREATE TABLE should accept empty columns "()"
      //And then, an execution error should be given if a CreateTable without columns is executed
      //CREATE TABLE TableName (ColumnName DataType[,ColumnName DataType... ])
      const string createTablePattern = null;
      //UPDATE TableName SET ColumnName=LiteralValue[,ColumnName=LiteralValue,…] WHERE Condition  
      const string updateTablePattern = null;
      //DELETE FROM TableName WHERE Condition
      const string deletePattern = @"^DELETE\s+FROM\s+(?<table>\w+)\s+WHERE\s+(?<column>\w+)\s?(?<operator>[<=>])\s?(?<value>.+)$";
      var deleteFrom = new Regex(deletePattern, RegexOptions.IgnoreCase);
            

      //TODO DEADLINE 4
      const string createSecurityProfilePattern = null;
            
      const string dropSecurityProfilePattern = null;
            
      const string grantPattern = null;
            
      const string revokePattern = null;
            
      const string addUserPattern = null;
            
      const string deleteUserPattern = null;


      //TODO DEADLINE 2
      //Parse query using the regular expressions above one by one. If there is a match, create an instance of the query with the parsed parameters
      //For example, if the query is a "SELECT ...", there should be a match with selectPattern. We would create and return an instance of Select
      //initialized with the table name, the columns, and (possibly) an instance of Condition.
      //If there is no match, it means there is a syntax error. We will return null.
      var trimmedQuery = miniSQLQuery.Trim();

      var dropTableMatch = dropTable.Match(trimmedQuery);
      var deleteFromMatch = deleteFrom.Match(trimmedQuery);

      if (dropTableMatch.Success)
      {
        var table = dropTableMatch.Groups["table"].Value;
        return new DropTable(table);
      }
      if (deleteFromMatch.Success)
      {
        var table = deleteFromMatch.Groups["table"].Value;
        var column = deleteFromMatch.Groups["column"].Value;
        var op = deleteFromMatch.Groups["operator"].Value;
        var value = deleteFromMatch.Groups["value"].Value;
        var condition = new Condition(column, op, value);
        return new Delete(table, condition);
      }

        //TODO DEADLINE 4
        //Do the same for the security queries (CREATE SECURITY PROFILE, ...)

        return null;
           
    }

        static List<string> CommaSeparatedNames(string text)
        {
            string[] textParts = text.Split(",", System.StringSplitOptions.RemoveEmptyEntries);
            List<string> commaSeparator = new List<string>();
            for(int i=0; i < textParts.Length; i++)
            {
                commaSeparator.Add(textParts[i]);
            }
            return commaSeparator;
        }
        
    }
}
