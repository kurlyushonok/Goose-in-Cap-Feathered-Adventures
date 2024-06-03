using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;

namespace GooseInCap;

public class Race
{
    public Vector2 CharacterPosition;
    private int _runningRunningLevel;
    private int _flightLevel;
    private int _jumpSpeed = 22;
    private int _currentCountCoins = 0;
    private int _pointsConst = 1;
    private Coin _coin;

    public Race(ContentManager content, int runningLevel, int flightLevel)
    {
        _coin = new Coin(content);
        CharacterPosition = new Vector2(250, runningLevel);
        _runningRunningLevel = runningLevel;
        _flightLevel = flightLevel;
    }
    
    public void SetCharacter(Goose goose)
    {
        Character = goose;
    }

    public int CountCoins
    {
        get => _currentCountCoins;
        set => _currentCountCoins = value;
    }

    public Goose Character { get; set; }
    public int Score { get; set; }
    public int NumberOfLetsPassed { get; set; }

    public int RunningRunningLevel => _runningRunningLevel;
    public int FlightLevel => _flightLevel;
    public int JumpSpeed => _jumpSpeed;
    public int PointsConst => _pointsConst;
    public Coin Coin => _coin;

}

// public class LevelConfiguration
// {
//     public Vector2 Position { get; set; }
//     public int RunningLevel { get; set; }
//     public int FlightLevel { get; set; }
//     public int JumpSpeed { get; set; }
//     
//     public Dictionary<string, (string, Vector2)> Sprites { get; set; } //foreach чтобы загрузить спрайт
// }
//
// public class LevelManager
// {
//     private readonly ContentManager _content;
//     public LevelManager(ContentManager content)
//     {
//         _content = content;
//     }
//     public void LoadLevel(string levelConfPath)
//     {
//         var json = File.ReadAllText(levelConfPath);
//         var levelConf = JsonSerializer.Deserialize<LevelConfiguration>(json);
//     }
// }