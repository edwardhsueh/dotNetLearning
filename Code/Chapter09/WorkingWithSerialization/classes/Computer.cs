using System;
using System.Collections.Generic;
namespace Packt.Shared
{
  public class Computer
  {

    public void WriteToConsole() {
      Console.WriteLine("CPU:{0}, Memory is {1}", Cpu, Memory);
    }
    public string Cpu { get; set; }
    public string Memory { get; set; }
  }
}