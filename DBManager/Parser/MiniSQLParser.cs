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
      const string selectPattern = @"^SELECT\s+(?<columns>[a-zA-Z,]+)\s+FROM\s+(?<table>[a-zA-Z]+)\s+WHERE\s+(?<column>\w+)\s?(?<operator>[<=>])\s?(?<value>.+)$";
      var select = new Regex(selectPattern, RegexOptions.None);
      //INSERT INTO TableName VALUES (LiteralValue[,LiteralValue, …])
      const string insertPattern = @"^INSERT\s+INTO\s+(?<table>[a-zA-Z]+)\s+VALUES\s+(?<values>[a-zA-Z0-9,.]+)$"; ;
      var insert = new Regex(insertPattern, RegexOptions.None);
      //DROP TABLE TableName
      const string dropTablePattern = @"^DROP\s+TABLE\s+(?<table>[a-zA-Z]+)$";
      var dropTable = new Regex(dropTablePattern, RegexOptions.None);

      //Note: The parsing of CREATE TABLE should accept empty columns "()"
      //And then, an execution error should be given if a CreateTable without columns is executed
      //CREATE TABLE TableName (ColumnName DataType[,ColumnName DataType... ])
      const string createTablePattern = @"^CREATE\s+TABLE\s+(?<table>\w+)\s+(?<columns>\w+)$";
      var createTable = new Regex(createTablePattern, RegexOptions.None);
      //UPDATE TableName SET ColumnName=LiteralValue[,ColumnName=LiteralValue,…] WHERE Condition  
      const string updateTablePattern = @"^UPDATE\s+(?<table>\w+)\s+SET\s+(?<columns>\w+)\s+WHERE\s+(?<column>\w+)\s?(?<operator>[<=>])\s?(?<value>.+)$";
      var updateTable = new Regex(updateTablePattern, RegexOptions.None);
      //DELETE FROM TableName WHERE Condition
      const string deletePattern = @"^DELETE\s+FROM\s+(?<table>\w+)\s+WHERE\s+(?<column>\w+)\s?(?<operator>[<=>])\s?(?<value>.+)$";
      var deleteFrom = new Regex(deletePattern, RegexOptions.None);
            

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

      var selectMatch = select.Match(trimmedQuery);
      if (selectMatch.Success)
      {
        var table = selectMatch.Groups["table"].Value;

        var unsplittedColumns = selectMatch.Groups["columns"].Value;
        var columns = CommaSeparatedNames(unsplittedColumns);

        var columnName = selectMatch.Groups["column"].Value;
        var op = selectMatch.Groups["operator"].Value;
        var value = selectMatch.Groups["value"].Value;
        var condition = new Condition(columnName, op, value);

        return new Select(table, columns, condition);
      }

      var insertMatch = insert.Match(trimmedQuery);
      if (insertMatch.Success)
      {
        var table = insertMatch.Groups["table"].Value;

        var unsplittedValues = insertMatch.Groups["values"].Value;
        var values = CommaSeparatedNames(unsplittedValues);

        return new Insert(table, values);
      }

      var dropTableMatch = dropTable.Match(trimmedQuery);
      if (dropTableMatch.Success)
      {
        var table = dropTableMatch.Groups["table"].Value;

        return new DropTable(table);
      }

      var createTableMatch = createTable.Match(trimmedQuery);
      if (createTableMatch.Success)
      {
        var table = createTableMatch.Groups["table"].Value;

        var unsplittedColumns = createTableMatch.Groups["columns"].Value;
        var splittedColumns = CommaSeparatedNames(unsplittedColumns);
        var columns = new List<ColumnDefinition>();
        for (int i=0; i < splittedColumns.Count; i ++)
        {
          var splittedSplitedColumn = splittedColumns[i].Split(" ");
          if (splittedSplitedColumn[1] == "String") columns.Add(new ColumnDefinition(ColumnDefinition.DataType.String, splittedSplitedColumn[0]));
          else if (splittedSplitedColumn[1] == "Int") columns.Add(new ColumnDefinition(ColumnDefinition.DataType.Int, splittedSplitedColumn[0]));
          else if (splittedSplitedColumn[1] == "Double") columns.Add(new ColumnDefinition(ColumnDefinition.DataType.Double, splittedSplitedColumn[0]));
        }

        return new CreateTable(table, columns);
      }

      var updateTableMatch = updateTable.Match(trimmedQuery);
      if (updateTableMatch.Success)
      {
        var table = updateTableMatch.Groups["table"].Value;

        var unsplittedColumns = updateTableMatch.Groups["columns"].Value;
        var splittedColumns = CommaSeparatedNames(unsplittedColumns);
        var setValues = new List<SetValue>();
        for (int i = 0; i < splittedColumns.Count; i++)
        {
          var splittedSplitedColumn = splittedColumns[i].Split("=");
          setValues.Add(new SetValue(splittedSplitedColumn[0], splittedSplitedColumn[1]));
        }

        var column = updateTableMatch.Groups["column"].Value;
        var op = updateTableMatch.Groups["operator"].Value;
        var value = updateTableMatch.Groups["value"].Value;
        var condition = new Condition(column, op, value);

        return new Update(table, setValues, condition);
      }

      var deleteFromMatch = deleteFrom.Match(trimmedQuery);
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
