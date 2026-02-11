using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DbManager.Parser;
using DbManager.Security;

namespace DbManager
{
  public class Database
  {
    private List<Table> Tables = new List<Table>();
    private string m_username;

    public string LastErrorMessage { get; private set; }

    public Manager SecurityManager { get; private set; }

    //This constructor should only be used from Load (without needing to set a password for the user). It cannot be used from any other class
    private Database()
    {
    }

    public Database(string adminUsername, string adminPassword)
    {
      //DEADLINE 1.B: Initalize the member variables
      //ASK TO THE TEACHER HOW IS THIS SUPOSSED TO WORK
      m_username = adminUsername;
      SecurityManager = new Manager(m_username);
      //var profile = new Profile();
      //var user = new User(adminUsername, adminPassword);
      //profile.Users.Add(user);
      //SecurityManager.AddProfile(profile);
    }

    public bool AddTable(Table table)
    {
      //DEADLINE 1.B: Add a new table to the database
      Tables.Add(table);
      return false;
    }

    public Table TableByName(string tableName)
    {
      //DEADLINE 1.B: Find and return the table with the given name
      foreach (Table table in Tables)
      {
        if (table.Name == tableName) return table;
      }
      return null;   
    }

    public bool CreateTable(string tableName, List<ColumnDefinition> ColumnDefinition)
    {
      //DEADLINE 1.B: Create and new table with the given name and columns. If there is already a table with that name,
      //return false and set LastErrorMessage with the appropriate error (Check Constants.cs)
      //Do the same if no column is provided
      //If everything goes ok, set LastErrorMessage with the appropriate success message (Check Constants.cs)
      foreach(Table table in Tables)
      {
        if (table.Name == tableName) 
        {
          LastErrorMessage = Constants.TableAlreadyExistsError;
          return false;
        }
      }
      if (ColumnDefinition.Count == 0)
      {
        LastErrorMessage = Constants.DatabaseCreatedWithoutColumnsError;
        return false;
      }
      Tables.Add(new Table(tableName, ColumnDefinition));
      LastErrorMessage = Constants.CreateTableSuccess;
      return true;   
    }

    public bool DropTable(string tableName)
    {
      //DEADLINE 1.B: Delete the table with the given name. If the table doesn't exist, return false and set LastErrorMessage
      //If everything goes ok, return true and set LastErrorMessage with the appropriate success message (Check Constants.cs)
      foreach (Table table in Tables)
      {
        if (table.Name == tableName)
        {
          Tables.Remove(table);
          LastErrorMessage = Constants.DropTableSuccess;
          return true;
        }
      }
      LastErrorMessage = Constants.TableDoesNotExistError;
      return false;
    }

    public bool Insert(string tableName, List<string> values)
    {
      //DEADLINE 1.B: Insert a new row to the table. If it doesn't exist return false and set LastErrorMessage appropriately
      //If everything goes ok, set LastErrorMessage with the appropriate success message (Check Constants.cs)
      foreach (Table table in Tables)
      {
        if (table.Name == tableName)
        {
          table.Insert(values);
          LastErrorMessage = Constants.InsertSuccess;
          return true;
        }
      }
      LastErrorMessage = Constants.TableDoesNotExistError;
      return false;
    }

    public Table Select(string tableName, List<string> columns, Condition condition)
    {
      //DEADLINE 1.B: Return the result of the select. If the table doesn't exist return null and set LastErrorMessage appropriately (Check Constants.cs)
      //If any of the requested columns doesn't exist, return null and set LastErrorMessage (Check Constants.cs)
      //If everything goes ok, return the table
      foreach (Table table in Tables)
      {
        if (table.Name == tableName)
        {
          foreach(string column in columns)
          {
            bool exists = false;
            for (int i=0; i<table.NumColumns(); i++)
            {
              if (table.GetColumn(i).Name == column) exists = true;
            }
            if (exists == false)
            {
              LastErrorMessage = Constants.ColumnDoesNotExistError;
              return null;
            }
          }
          return table.Select(columns, condition);
        }
      }
      LastErrorMessage = Constants.TableDoesNotExistError;
      return null;
    }

    public bool DeleteWhere(string tableName, Condition columnCondition)
    {
      //DEADLINE 1.B: Delete all the rows where the condition is true. 
      //If the table or the column in the condition don't exist, return null and set LastErrorMessage (Check Constants.cs)
      //If everything goes ok, return true
      foreach (Table table in Tables)
      {
        if (table.Name == tableName)
        {
          for (int i = 0; i < table.NumColumns(); i++)
          {
            if (table.GetColumn(i).Name == columnCondition.ColumnName)
            {
              table.DeleteWhere(columnCondition);
              LastErrorMessage = Constants.DeleteSuccess;
              return true;
            }
          }
          LastErrorMessage = Constants.ColumnDoesNotExistError;
          return false;
        }
      }
      LastErrorMessage = Constants.TableDoesNotExistError;
      return false;
    }

    public bool Update(string tableName, List<SetValue> columnNames, Condition columnCondition)
    {
      //DEADLINE 1.B: Update in the given table all the rows where the condition is true using the SetValues
      //If the table or the column in the condition don't exist, return null and set LastErrorMessage (Check Constants.cs)
      //If everything goes ok, return true
      foreach (Table table in Tables)
      {
        if (table.Name == tableName)
        {
          for (int i = 0; i < table.NumColumns(); i++)
          {
            if (table.GetColumn(i).Name == columnCondition.ColumnName)
            {
              table.Update(columnNames, columnCondition);
              LastErrorMessage = Constants.UpdateSuccess;
              return true;
            }
          }
          LastErrorMessage = Constants.ColumnDoesNotExistError;
          return false;
        }
      }
      LastErrorMessage = Constants.TableDoesNotExistError;
      return false;
    }
        
    public bool Save(string databaseName)
    {
      //DEADLINE 1.C: Save this database to disk with the given name
      //If everything goes ok, return true, false otherwise.
      //DEADLINE 5: Save the SecurityManager so that it can be loaded with the database in Load()
      try
      {
        using (var writer = File.CreateText(databaseName))
        {
          foreach (var table in Tables)
          {
            writer.WriteLine(table.Name);
            writer.WriteLine(table.ColumnTypesToString());
            writer.WriteLine(table.ToString());
          }
          writer.WriteLine("MANAGER");
          SecurityManager.Save(writer);
        }
        return true;
      }
      catch (Exception ex)
      {
        throw;
        return false;
      }
    }

    public static Database Load(string databaseName, string username, string password)
    {
      //DEADLINE 1.C: Load the (previously saved) database of name databaseName
      //If everything goes ok, return the loaded database (a new instance), null otherwise.
      //DEADLINE 5: When the Database object is created, set the username (create a new method if you must)
      //After loading the database, load the SecurityManager and check the password is correct. If it's not, return null. If it is return the database
      var reader = System.IO.File.OpenText(databaseName);
      string line = reader.ReadLine();
      var database = new Database();
      while (line != "MANAGER")
      {
        string name = line;
        line = reader.ReadLine();
        var types = line.Split(',');
        var columnTypes = new List<DbManager.ColumnDefinition.DataType>();
        foreach (var column in types)
        {
          if (column == "String") columnTypes.Add(DbManager.ColumnDefinition.DataType.String);
          else if (column == "Double") columnTypes.Add(DbManager.ColumnDefinition.DataType.Double);
          else if (column == "Int") columnTypes.Add(DbManager.ColumnDefinition.DataType.Int);
        }

        line = reader.ReadLine();
        int startCols = line.IndexOf('[');
        int endCols = line.IndexOf(']');

        string columns = line.Substring(startCols + 1, endCols - startCols - 1);

        var columnNames = columns
          .Split(',', StringSplitOptions.RemoveEmptyEntries)
          .Select(c => c.Trim('\''))
          .ToList();

        var columnDefinitions = new List<ColumnDefinition>();

        for (int i=0; i<columnNames.Count; i++)
        {
          columnDefinitions.Add(new ColumnDefinition(columnTypes[i], columnNames[i]));
        }

        var table = new Table(name, columnDefinitions);

        string rows = line.Substring(endCols + 1);

        var rowValues = rows
          .Split("}{", StringSplitOptions.RemoveEmptyEntries)
          .Select(r => r.Trim('{', '}'));

        foreach (var rowValue in rowValues)
        {
          var values = rowValue
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(v => v.Trim('\''))
            .ToList();
          
          var row = new Row(columnDefinitions, values);

          table.AddRow(row);
        }
        database.AddTable(table);
        line = reader.ReadLine();
      }
      return database;
    }

    public string ExecuteMiniSQLQuery(string query)
    {
    //Parse the query
    MiniSqlQuery miniSQLQuery = MiniSQLParser.Parse(query);

    //If the parser returns null, there must be a syntax error (or the parser is failing)
    if (miniSQLQuery == null)
    return Constants.SyntaxError;

    //Once the query is parsed, we run it on this database
    return miniSQLQuery.Execute(this);
    }


    public bool IsUserAdmin()
    {
    return SecurityManager.IsUserAdmin();
    }





    //All these methods are ONLY FOR TESTING. Use them to simplify creating unit tests:
    public const string AdminUsername = "admin";
    public const string AdminPassword = "adminPassword";
    public static Database CreateTestDatabase()
    {
    Database database = new Database(AdminUsername, AdminPassword);

    database.Tables.Add(Table.CreateTestTable());

    return database;
    }

    public void AddTuplesForTesting(string tableName, List<List<string>> rows)
    {
    Table table = TableByName(tableName);
    foreach (List<string> row in rows)
    {
    table.Insert(row);
    }
    }

    public void CheckForTesting(string tableName, List<List<string>> rows)
    {
    Table table = TableByName(tableName);

    table.CheckForTesting(rows);
    }
  }
}





