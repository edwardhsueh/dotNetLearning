using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace Packt.Shared
{
  public class Person
  {

    public Person(decimal initialSalary)
    {
      Salary = initialSalary;
    }
    public void WriteToConsole() {
      Console.WriteLine("FirstName:{0}, LastName is {1}", FirstName, LastName);
    }
    public Person(){

    }
    [XmlAttribute("fname")]
    public string FirstName { get; set; }
    [XmlAttribute("name")]
    public string LastName { get; set; }
    [XmlAttribute("dob")]
    public DateTime DateOfBirth { get; set; }
    public HashSet<Person> Children { get; set; }
    protected decimal Salary { get; set; }
    }
}