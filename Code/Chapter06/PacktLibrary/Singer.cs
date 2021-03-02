using static System.Console;
namespace Packt.Shared
{
  /// <summary>
  /// test virtual method
  /// </summary>
  public class Singer
  {
  // virtual allows this method to be overridden
    public virtual void Sing()
    {
      WriteLine("Singing...");
    }
  }
  /// <summary>
  /// test sealed method
  /// </summary>
  public class LadyGaga : Singer
  {
    // sealed prevents overriding the method in subclasses
    public sealed override void Sing()
    {
      WriteLine("Singing with LadyGaga...");
    }
  }


  /// <summary>
  /// test sealed class: class inheritance is disallowed
  /// </summary>
  public sealed class Jackson : Singer
  {

    public override void Sing(){
       WriteLine("Singing with Jackson...");
    }
  }

  // The follow will be error
  // public class Miller : Jackson
  // {

  // }
}