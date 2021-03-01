/// <summary>
/// test struct
/// </summary>
public struct DisplacementVector
{
  public int X;
  public int Y;
  /// <summary>
  /// constructor for struct DisplacementVector
  /// </summary>
  /// <param name="initialX"></param>
  /// <param name="initialY"></param>
  public DisplacementVector(int initialX, int initialY)
  {
  X = initialX;
  Y = initialY;
  }
  public static DisplacementVector operator +(
    DisplacementVector vector1,
    DisplacementVector vector2)
  {
    return new DisplacementVector(
    vector1.X + vector2.X,
    vector1.Y + vector2.Y);
  }
}