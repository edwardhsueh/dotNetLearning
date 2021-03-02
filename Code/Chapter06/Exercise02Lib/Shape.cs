using System;

namespace Exercise02Lib
{
    public class Shape
    {
        public double Height {get;set;}
        public double Width {get;set;}
        public virtual double Area
        {
            get {
                return Width*Height;
            }
        }
        public Shape() {
            Height = 1.0;
            Width = 1.0;
        }
        public Shape(double width, double height){
            Height = height;
            Width = width;
        }
    }
}
