using System;

namespace Exercise02Lib
{
    public class Circle : Shape
    {
      public Circle(double width) : base(width: width, height:width){}
      public override double Area
      {
        get {
            return Math.PI*Width*Width;
        }
      }
    }
}