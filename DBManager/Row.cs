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
          if (columnDefinitions.Count != values.Count) { throw new ArgumentException("Column definitions count must match values count."); }
          if (columnDefinitions.Count == 0 || values.Count == 0) { throw new ArgumentException("Row needs at least 1 ColumnDefinition."); }
          ColumnDefinitions = columnDefinitions;
          Values = values;
          //PREGUNTAR AL PROFESOR SI DEBERIA TENER EN CUENTA AQUI EL HECHO DE QUE UNA FILA TENGO 2 COLUMNAS CON EL MISMO NOMBRE
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
          throw new ArgumentException("There is none column with the given name: " + columnName);
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
          throw new ArgumentException("There is none column with the given name: " + columnName);
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

            
            return null;
            
        }

        private static string Decode(string value)
        {
            //TODO DEADLINE 1.C: Decode the value doing the opposite of Encode()
            
            return null;
            
        }

        public string AsText()
        {
            //TODO DEADLINE 1.C: Return the row as string with all values separated by the delimiter
            
            return null;
            
        }

        public static Row Parse(List<ColumnDefinition> columns, string value)
        {
            //TODO DEADLINE 1.C: Parse a rowReturn the row as string with all values separated by the delimiter
            
            return null;
            
        }
    }
}
