using System;
using static System.Console;
namespace Packt.Shared
{
  /// <summary>
  /// Inheriting from classes
  /// </summary>
  public class Employee : Person
  {
    public string EmployeeCode { get; set; }
    public DateTime HireDate { get; set; }
    public new void WriteToConsole()
    {
      WriteLine(format:
      "Employee {0} was born on {1:dd/MM/yy} and hired on {2:dd/MM/yy}", Name, DateOfBirth, HireDate);
    }
    public override string ToString()
    {
      return $"{Name}'s code is {EmployeeCode}";
    }
  }
}