using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml.Linq;
using DbManager.Parser;

namespace DbManager
{
    public class Table
    {
        private List<ColumnDefinition> ColumnDefinitions = new List<ColumnDefinition>();
        private List<Row> Rows = new List<Row>();
        
        public string Name { get; private set; } = null;

        public Table(string name, List<ColumnDefinition> columns)
        {
          if (string.IsNullOrEmpty(name)) { return; }
          if (HasDuplicatedColumnNames(columns)) { return; }
          Name = name;
          ColumnDefinitions = columns;
        }
        
        public bool HasDuplicatedColumnNames(List<ColumnDefinition> columns)
        {
          foreach (var colDef in columns)
          {
            int count = 0;
            foreach (var colDef2 in columns)
            {
              if (colDef.Name == colDef2.Name)
              {
                count++;
              }
            }
            if (count > 1)
            {
              return true;
            }
          }
          return false;
        }
        public Row GetRow(int i)
        {
          if (i < 0 || i >= Rows.Count) { throw new ArgumentException("You can't get a row that is out of the limits of the table"); }
          return Rows[i];
        }

        public void AddRow(Row row)
        {
          if (row.Values.Count != ColumnDefinitions.Count) { return; }
          if (row.GetColumnDefinition() != ColumnDefinitions) { return; }
          Rows.Add(row);
        }

        public int NumRows()
        {
            return Rows.Count; 
        }

        public ColumnDefinition GetColumn(int i)
        {
          if (i < 0 || i >= ColumnDefinitions.Count) { throw new ArgumentException("You can't get a column that is out of the limits of the table"); }
          return ColumnDefinitions[i];
        }

        public int NumColumns()
        {
            return ColumnDefinitions.Count;
        }
        
        public ColumnDefinition ColumnByName(string column)
        {
            if (string.IsNullOrEmpty(column)) { throw new ArgumentException("The column name can't be empty or null"); }
            if (!ColumnDefinitions.Exists(colDef => colDef.Name == column)) { throw new ArgumentException("The column name does not exist in the table"); }
            return ColumnDefinitions.Find(colDef => colDef.Name == column);
        }
        public int ColumnIndexByName(string columnName)
        {
          if (string.IsNullOrEmpty(columnName)) { throw new ArgumentException("The column name can't be empty or null"); }
          if (!ColumnDefinitions.Exists(colDef => colDef.Name == columnName)) { throw new ArgumentException("The column name does not exist in the table"); }
          return ColumnDefinitions.FindIndex(colDef => colDef.Name == columnName);
        }


        public override string ToString()
        {
          //TODO DEADLINE 1.A: Return the table as a string. The format is specified in the documentation
          //Valid examples:
          //"['Name']{'Adolfo'}{'Jacinto'}" <- one column, two rows
          //"['Name','Age']{'Adolfo','23'}{'Jacinto','24'}" <- two columns, two rows
          //"" <- no columns, no rows
          //"['Name']" <- one column, no rows

          string result = "";
          if (ColumnDefinitions.Count == 0)
          {
             return result;
          }
          result += "[";
          for (int i = 0; i < ColumnDefinitions.Count; i++)
          {
            result += $"'{ColumnDefinitions[i].Name}'";
            if (i != ColumnDefinitions.Count - 1)
            {
              result += ",";
            }
          }
          result += "]";
          if (Rows.Count == 0)
          {
            return result;
          }
          for (int i = 0; i < Rows.Count; i++)
          {
            result += "{";
            for (int j = 0; j < Rows[i].Values.Count; j++)
            {
              result += $"'{Rows[i].Values[j]}'";
              if (j != Rows[i].Values.Count - 1)
              {
                result += ",";
              }
            }
            result += "}";
          }            
          return result;
        }

        public void DeleteIthRow(int rowIndex)
        {
          //TODO DEADLINE 1.A: Delete the i-th row. If there is no i-th row, do nothing
          if (Rows.Count < rowIndex + 1 || rowIndex < 0) { return; }
          Row row = GetRow(rowIndex);
          List<Row> newRows = new List<Row>();
          foreach (Row r in Rows)
          {
            if (r != row)
            {
              newRows.Add(r);
            }
          }
          Rows = newRows;
        }

      public List<int> RowIndicesWhereConditionIsTrue(Condition condition)
      {
        //TODO DEADLINE 1.A: Returns the indices of all the rows where the condition is true. Check Row.IsTrue()
        List<int> indexes = new List<int>();
        foreach (Row row in Rows)
        {
          if (row.IsTrue(condition))
          {
            indexes.Add(Rows.IndexOf(row));
          }
        }
        return indexes;
      }

        public void DeleteWhere(Condition condition)
        {
            //TODO DEADLINE 1.A: Delete all rows where the condition is true. Check RowIndicesWhereConditionIsTrue()
            foreach (int index in RowIndicesWhereConditionIsTrue(condition))
            {
              DeleteIthRow(index);
          }
        }

    public Table Select(List<string> columnNames, Condition condition)
    {
      //TODO DEADLINE 1.A: Return a new table (with name 'Result') that contains the result of the select. The condition
      //may be null (if no condition, all rows should be returned). This is the most difficult method in this class
      Table newTable;
      List<int> columnIndexes = new List<int>();
      List<Row> newRows = new List<Row>();
      List<ColumnDefinition> newColumnDefinitions = new List<ColumnDefinition>();

      foreach (string columnName in columnNames)
      {
        newColumnDefinitions.Add(ColumnByName(columnName));
        columnIndexes.Add(ColumnIndexByName(columnName));
      }
      if (condition == null)
      {
        foreach (Row row in Rows)
        {
          List<string> newValues = new List<string>();
          foreach (int columnIndex in columnIndexes)
          {
            newValues.Add(row.Values[columnIndex]);
          }
          newRows.Add(new Row(newColumnDefinitions, newValues));
        }
        newTable = new Table("Result", newColumnDefinitions);
        foreach (Row row in newRows)
        {
          newTable.AddRow(row);
        }
        return newTable;
      }

      foreach (Row row in Rows)
      {
        if (row.IsTrue(condition))
        {
          List<string> newValues = new List<string>();
          foreach (int columnIndex in columnIndexes)
          {
            newValues.Add(row.Values[columnIndex]);
          }
          newRows.Add(new Row(newColumnDefinitions, newValues));
        }
      }
      newTable = new Table("Result", newColumnDefinitions);
      foreach (Row row in newRows)
      {
        newTable.AddRow(row);
      }
      return newTable;
    }

    public bool Insert(List<string> values)
    {
      //TODO DEADLINE 1.A: Insert a new row with the values given. If the number of values is not correct, return false. True otherwise
      if (values.Count != ColumnDefinitions.Count) return false;
      Row newRow = new Row(ColumnDefinitions, values);
      AddRow(newRow);
      return true;
    }

    public bool Update(List<SetValue> setValues, Condition condition)
    {
      //TODO DEADLINE 1.A: Update all the rows where the condition is true using all the SetValues (ColumnName-Value). If condition is null,
      //return false, otherwise return true
      
      if (condition == null) return false;
      List<int> rowIndexes = RowIndicesWhereConditionIsTrue(condition);
      foreach (int rowIndex in rowIndexes)
      {
        Row row = GetRow(rowIndex);
        foreach (SetValue setValue in setValues)
        {
          int columnIndex = ColumnIndexByName(setValue.ColumnName);
          row.Values[columnIndex] = setValue.Value;
        }
      }
      return true;
    }



        //Only for testing purposes
        public const string TestTableName = "TestTable";
        public const string TestColumn1Name = "Name";
        public const string TestColumn2Name = "Height";
        public const string TestColumn3Name = "Age";
        public const string TestColumn1Row1 = "Rodolfo";
        public const string TestColumn1Row2 = "Maider";
        public const string TestColumn1Row3 = "Pepe";
        public const string TestColumn2Row1 = "1.62";
        public const string TestColumn2Row2 = "1.67";
        public const string TestColumn2Row3 = "1.55";
        public const string TestColumn3Row1 = "25";
        public const string TestColumn3Row2 = "67";
        public const string TestColumn3Row3 = "51";
        public const ColumnDefinition.DataType TestColumn1Type = ColumnDefinition.DataType.String;
        public const ColumnDefinition.DataType TestColumn2Type = ColumnDefinition.DataType.Double;
        public const ColumnDefinition.DataType TestColumn3Type = ColumnDefinition.DataType.Int;
        public static Table CreateTestTable(string tableName = TestTableName)
        {
            Table table = new Table(tableName, new List<ColumnDefinition>()
            {
                new ColumnDefinition(TestColumn1Type, TestColumn1Name),
                new ColumnDefinition(TestColumn2Type, TestColumn2Name),
                new ColumnDefinition(TestColumn3Type, TestColumn3Name)
            });
            table.Insert(new List<string>() { TestColumn1Row1, TestColumn2Row1, TestColumn3Row1 });
            table.Insert(new List<string>() { TestColumn1Row2, TestColumn2Row2, TestColumn3Row2 });
            table.Insert(new List<string>() { TestColumn1Row3, TestColumn2Row3, TestColumn3Row3 });
            return table;
        }

        public void CheckForTesting(List<List<string>> rows)
        {
            if (rows.Count != NumRows())
                throw new Exception($"The table has {NumRows()} rows and {rows.Count} were expected");
            int rowIndex = 0;
            foreach (List<string> row in rows)
            {
                if (GetRow(rowIndex).Values.Count != row.Count)
                    if (rows.Count != NumRows())
                        throw new Exception($"The {rowIndex}-th row has {GetRow(rowIndex).Values.Count} values and {row.Count} were expected");

                for (int columnIndex = 0; columnIndex < row.Count; columnIndex++)
                {
                    if (GetRow(rowIndex).Values[columnIndex] != row[columnIndex])
                        if (rows.Count != NumRows())
                            throw new Exception($"The [{rowIndex},{columnIndex}] element is {GetRow(rowIndex).Values[columnIndex]} instead of {row[columnIndex]}");
                }

                rowIndex++;
            }
        }
    }
}