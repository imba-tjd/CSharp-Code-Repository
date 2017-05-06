﻿using System;
using System.Collections.Generic;

namespace Shape
{
    class Program
    {
        static void Main(string[] args)
        {
            double sum = 0;
            Rectangle r = new Rectangle(1, 2);
            Cube cu = new Cube(2);
            Circle c = new Circle(2);
            Triangle t = new Triangle(2);
            List<dynamic> o = new List<dynamic>() { r, cu, c, t };
            foreach (var e in o)
                sum += e.Area();
            Console.WriteLine("Rectangle:{0:F2}, Cube:{1:F2}, Circle:{2:F2}, Triangle:{3:F2}, Sum:{4:F2}", r.Area(), cu.Area(), c.Area(), t.Area(), sum);
            Console.ReadKey();
        }
    }
    abstract class Shape
    {
        public abstract double Area();
    }
    class Rectangle : Shape
    {
        protected double width, length;
        public override double Area()
        {
            return width * length;
        }
        public Rectangle(double w, double l)
        {
            width = w;
            length = l;
        }
    }
    class Cube : Rectangle
    {
        public Cube(double l) : base(l, l) { }
    }
    class Circle : Shape
    {
        private double radius;
        public override double Area()
        {
            return Math.PI * radius * radius;
        }
        public Circle(double r)
        {
            radius = r;
        }
    }

    class Triangle : Shape
    {
        private double s1, s2, s3, p;
        public override double Area()
        {
            return Math.Sqrt(p * (p - s1) * (p - s2) * (p - s3));
        }
        public Triangle(Double s) : this(s, s, s) { }
        public Triangle(double s1, double s2, double s3)
        {
            this.s1 = s1;
            this.s2 = s2;
            this.s3 = s3;
            p = (s1 + s2 + s3) / 2;
        }
    }
}
