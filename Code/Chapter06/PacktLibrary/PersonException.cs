using System;
namespace Packt.Shared
{
  /// <summary>
  /// PersonException, extening from Exception
  /// </summary>
  // Unlike ordinary methods, constructors are not inherited, so we must explicitly declare
  // and explicitly call the
  // constructor implementations in
  // to make
  // base
  // System.Exception
  // them available to programmers who might want to use those constructors with our
  // custom exception.
public class PersonException : Exception
  {
    /// <summary>
    /// Constructor is not inherited
    /// </summary>
    public PersonException() : base() { }
    /// <summary>
    /// Constructor is not inherited
    /// </summary>
    public PersonException(string message) : base(message) { }
    /// <summary>
    /// Constructor is not inherited
    /// </summary>
    public PersonException(
      string message, Exception innerException)
      : base(message, innerException) { }
    }
}