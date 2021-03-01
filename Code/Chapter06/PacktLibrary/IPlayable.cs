using static System.Console;
namespace Packt.Shared
{
  /// <summary>
  /// interface type
  /// </summary>
  public interface IPlayable
  {
    void Play();
    void Pause();
    void Stop() // default interface implementation
    {
      WriteLine("Default implementation of Stop.");
    }
    void Error();
    void Error2() {
      WriteLine("Default implementation of Error2.");
    }
  }
}