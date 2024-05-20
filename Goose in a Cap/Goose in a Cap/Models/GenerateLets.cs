using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GooseInCap;

public class GenerateLets
{
    private static List<LetGrandmother> _lets = new List<LetGrandmother>();
    private Random _rnd = new Random();
    private static ContentManager _content;

    public GenerateLets(ContentManager content)
    {
        _content = content;
    }

    public void LoadLet()
    {
        _lets.Add(new LetGrandmother(_content));
    }

    public LetGrandmother GenerateLet()
    {
        var index = _rnd.Next(0, _lets.Count - 1);
        return _lets[index];
    }
}