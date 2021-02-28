using static System.Console;
namespace Packt.Shared
{
  public class DvdPlayer : IPlayable
  {
    public void Pause()
    {
      WriteLine("DVD player is pausing.");
    }
    public void Play()
    {
      WriteLine("DVD player is playing.");
    }
    public void Error()
    {
      WriteLine("DVD player has error.");
    }
    public void Stop()
    {
      WriteLine("DVD player stop.");
    }
    public void Error2()
    {
      WriteLine("DVD player has 2nd error.");
    }

  }
}