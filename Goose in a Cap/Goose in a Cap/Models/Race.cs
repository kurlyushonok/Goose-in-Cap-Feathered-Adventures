using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GooseInCap;

public class Race
{
    public Vector2 Position = new Vector2(150, 520);
    private int _runningLevel = 520;
    private int _flightLevel = 200;
    private int _jumpSpeed = 20;

    public Goose Character { get; set; }

    public int RunningLevel => _runningLevel;
    public int FlightLevel => _flightLevel;
    public int JumpSpeed => _jumpSpeed;
}