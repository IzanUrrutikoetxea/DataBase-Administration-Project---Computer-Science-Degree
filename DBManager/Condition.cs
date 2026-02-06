using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using DbManager;

namespace DbManager
{
  public class Condition
  {
    public string ColumnName { get; private set; }
    public string Operator { get; private set; }
    public string LiteralValue { get; private set; }

    public Condition(string column, string op, string literalValue)
    {
      //TODO DEADLINE 1A: Initialize member variables
      if (string.IsNullOrEmpty(column)) { throw new ArgumentException("Column name cannot be null or empty.", nameof(column)); }
      if (string.IsNullOrEmpty(op)) { throw new ArgumentException("Operator cannot be null or empty.", nameof(column)); }
      if (string.IsNullOrEmpty(literalValue)) { throw new ArgumentException("Value cannot be null or empty.", nameof(column)); }
        ColumnName = column;
        Operator = op;
        LiteralValue = literalValue;
    }


    public bool IsTrue(string value, ColumnDefinition.DataType type)
    {
      //TODO DEADLINE 1A: return true if the condition is true for this value
      //Depending on the type of the column, the comparison should be different:
      //"ab" < "cd
      //"9" > "10"
      //9 < 10
      //Convert first the strings to the appropriate type and then compare (depending on the operator of the condition)
      switch (type)
      {
        case ColumnDefinition.DataType.String:
          if (Operator == "=") return value == LiteralValue;
          if (Operator == "!=") return value != LiteralValue;
          if (Operator == "<") return string.Compare(value, LiteralValue) < 0;
          if (Operator == ">") return string.Compare(value, LiteralValue) > 0;
        break;

        case ColumnDefinition.DataType.Int:
          int intValue = int.Parse(value);
          int intLiteralValue = int.Parse(LiteralValue);
          if (Operator == "=") return intValue == intLiteralValue;
          if (Operator == "!=") return intValue != intLiteralValue;
          if (Operator == "<") return intValue < intLiteralValue;
          if (Operator == ">") return intValue > intLiteralValue;
        break;

        case ColumnDefinition.DataType.Double:
          double doubleValue = double.Parse(value);
          double doubleLiteralValue = double.Parse(LiteralValue);
          if (Operator == "=") return doubleValue == doubleLiteralValue;
          if (Operator == "!=") return doubleValue != doubleLiteralValue;
          if (Operator == "<") return doubleValue < doubleLiteralValue;
          if (Operator == ">") return doubleValue > doubleLiteralValue;
          break;
      }
      return false;
            
    }
  }
}