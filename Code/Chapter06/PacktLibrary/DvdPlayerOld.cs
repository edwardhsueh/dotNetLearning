using static System.Console;
namespace Packt.Shared
{
  public class DvdPlayerOld : IPlayable
  {
    public void Pause()
    {
      WriteLine("Old DVD player is pausing.");
    }
    public void Play()
    {
      WriteLine("Old DVD player is playing.");
    }
    public void Error()
    {
      WriteLine("Old DVD player has error.");
    }
    public void Stop()
    {
      WriteLine("Old DVD player stop.");
    }
  }
}