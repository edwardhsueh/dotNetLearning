using System;
using System.Threading;
namespace Packt.Shared
{
  /// <summary>
  /// Static class: Working with generic methods
  /// </summary>
  public static class Squarer
  {
    /// <summary>
    /// Generic method:
    /// </summary>
    /// <param name="input"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    // The Squarer class is non-generic.
    // • The Square method is generic, and its type parameter T must implement
    // IConvertible, so the compiler will make sure that it has a ToDouble method.
    // • T is used as the type for the input parameter.
    // • ToDouble requires a parameter that implements IFormatProvider to
    // understand the format of numbers for a language and region. We can pass
    // the CurrentCulture property of the current thread to specify the language
    // and region used by your computer. You will learn about cultures in Chapter 8,
    // Working with Common .NET Types.
    // • The return value is the input parameter multiplied by itself, that is, squared
    public static double Square<T>(T input)
    where T : IConvertible
    {
      // convert using the current culture
      double d = input.ToDouble(
        Thread.CurrentThread.CurrentCulture);
      return d * d;
    }
  }
}