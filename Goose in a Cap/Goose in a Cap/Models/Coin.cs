﻿using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GooseInCap;

public class Coin
{
    private int _level = 580;
    private int _currentPosition = 2500;
    private Texture2D _sprite;
    private Random _rnd = new Random();
    private int _distanceToLet = 250;

    public Coin(ContentManager content)
    {
        _sprite = content.Load<Texture2D>("coin");
    }

    public int CurrentPosition
    {
        get => _currentPosition;
        set => _currentPosition = value;
    }

    public int Level => _level;

    public Texture2D Sprite => _sprite;

    public int CountOvercomeLets(Let let)
    {
        var between = _rnd.Next(_distanceToLet, 1920 - _distanceToLet);
        var countLets = _rnd.Next(1, 3) * 1920;
        return between + countLets;
    }
}