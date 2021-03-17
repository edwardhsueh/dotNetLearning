using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace Packt.Shared
{
    public class Person {
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public int Savings {get;set;}
        public int? Salary {get;set;}
    }
}
