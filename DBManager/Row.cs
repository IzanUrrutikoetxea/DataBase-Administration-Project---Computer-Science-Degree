using DbManager.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DbManager
{
  public class Row
  {
    private List<ColumnDefinition> ColumnDefinitions = new List<ColumnDefinition>();
    public List<string> Values { get; set; }

    public Row(List<ColumnDefinition> columnDefinitions, List<string> values)
    {
      if (columnDefinitions.Count != values.Count) { return; }
      ColumnDefinitions = columnDefinitions;
      Values = values;
    }

    public void SetValue(string columnName, string value)
    {
      foreach (ColumnDefinition colDef in ColumnDefinitions)
      {
        if (colDef.Name == columnName)
        {
          int index = ColumnDefinitions.IndexOf(colDef);
          Values[index] = value;
          return;
        }
      }
    }

    public string GetValue(string columnName)
    {
      foreach (ColumnDefinition colDef in ColumnDefinitions)
        {
        if (colDef.Name == columnName)
        {
          int index = ColumnDefinitions.IndexOf(colDef);
          return Values[index];
        }
      }
      return null;
    }

    public List<ColumnDefinition> GetColumnDefinition()
    {
      return ColumnDefinitions;
    }

    public bool IsTrue(Condition condition)
    {
      //TODO DEADLINE 1.A: Given a condition (column name, operator and literal value, return whether it is true or not
      //for this row. Check Condition.IsTrue method
      foreach (ColumnDefinition column in ColumnDefinitions)
      {
        if (column.Name == condition.ColumnName)
        {
          int index = ColumnDefinitions.IndexOf(column);
          string value = Values[index];
          return condition.IsTrue(value, column.Type);
        }
      }
      return false;    
    }

    private const string Delimiter = ":";
    private const string DelimiterEncoded = "[SEPARATOR]";

    private static string Encode(string value)
    {
      //TODO DEADLINE 1.C: Encode the delimiter in value
      string[] values = value.Split(Delimiter);
      string result = "";
      for (int i = 0; i < values.Length; i++)
      {
        if (i == values.Length - 1)
        {
          result += values[i];
        }
        else
        {
          result += values[i] + DelimiterEncoded;
        }
      }
      return result;
    }

    private static string Decode(string value)
    {
      //TODO DEADLINE 1.C: Decode the value doing the opposite of Encode()
      string[] values = value.Split(DelimiterEncoded);
      string result = "";
      for (int i = 0; i < values.Length; i++)
      {
        if (i == values.Length - 1)
        {
          result += values[i];
        }
        else
        {
          result += values[i] + Delimiter;
        }
      }
      return result;
    }

    public string AsText()
    {
      //TODO DEADLINE 1.C: Return the row as string with all values separated by the delimiter
      string result = "";
      for (int i = 0; i < Values.Count; i++)
      {
        if (i == Values.Count - 1)
        {
          result += Values[i];
        }
        else
        {
          result += Values[i] + Delimiter;
        }
      }
      return result;
    }

    public static Row Parse(List<ColumnDefinition> columns, string value)
    {
      //TODO DEADLINE 1.C: Parse a rowReturn the row as string with all values separated by the delimiter
      string[] values = value.Split(Delimiter);
      List<string> strings = new List<string>();
      foreach (string rowValue in values)
      {
        strings.Add(rowValue);
      }
      return new Row(columns, strings);
            
    }
  }
}
