using System.Text.RegularExpressions;
namespace Packt.Shared
{
  /// <summary>
  /// Extending types when you can't inherit
  /// Using static methods to reuse functionality
  /// </summary>
  public class StringExtensions
  {
    /// <summary>
    /// Using static methods to reuse functionality
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsValidEmail(string input)
    {
      // use simple regular expression to check
      // that the input string is a valid email
      return Regex.IsMatch(input,
      @"[a-zA-Z0-9\.-_]+@[a-zA-Z0-9\.-_]+");
    }
  }
  public class StringMethodsExtensions
  {
    /// <summary>
    /// Extending types when you can't inherit
    /// Using extension methods to reuse functionality
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsValidEmail(this string input)
    {
      {
        // use simple regular expression to check
        // that the input string is a valid email
        return Regex.IsMatch(input,
        @"[a-zA-Z0-9\.-_]+@[a-zA-Z0-9\.-_]+");
      }
    }
  }
}