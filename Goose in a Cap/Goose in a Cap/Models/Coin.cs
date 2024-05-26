using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GooseInCap;

public class Coin
{
    private int _level = 580;
    private int _currentPosition = 2500;
    private Texture2D _sprite;
    private Random _rnd = new Random();
    private int _distanceToLet = 100;

    public Coin(ContentManager content)
    {
        _sprite = content.Load<Texture2D>("coin");
    }

    public int CurrentPosition
    {
        get => _currentPosition;
        set => _currentPosition = value;
    }

    public int StartPosition => 2500;

    public int Level => _level;

    public Texture2D Sprite => _sprite;

    public int CountOvercomeLets(int letPosition)
    {
        var result = _rnd.Next(1920, 10000);
        while (result > letPosition - _distanceToLet && result < letPosition + _distanceToLet)
            result = _rnd.Next(1920, 6000);
        return result;
    }
}