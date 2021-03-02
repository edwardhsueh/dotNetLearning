using System;
using static System.Console;
namespace Exercise02Lib
{
    public class Circle : Shape
    {
      public Circle(double width) : base(width: width, height:width){ WriteLine("base constructor used"); }
      public override double Area
      {
        get {
            return Math.PI*Width*Width;
        }
      }
    }
}