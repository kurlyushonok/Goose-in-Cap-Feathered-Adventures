﻿using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;

namespace GooseInCap;

public class Race
{
    public Vector2 Position = new Vector2(150, 520);
    private int _runningLevel = 520;
    private int _flightLevel = 200;
    private int _jumpSpeed = 22;

    public Goose Character { get; set; }

    public int RunningLevel => _runningLevel;
    public int FlightLevel => _flightLevel;
    public int JumpSpeed => _jumpSpeed;
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