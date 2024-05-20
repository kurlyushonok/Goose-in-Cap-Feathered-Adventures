using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GooseInCap;

public class GenerateLets
{
    private static List<Let> _lets = new List<Let>();
    private Random _rnd = new Random();
    private static ContentManager _content;

    public GenerateLets(ContentManager content)
    {
        _content = content;
    }

    public void LoadLet()
    {
        _lets.Add(new LetButchFlower(_content));
        _lets.Add(new LetBuches(_content));
        _lets.Add(new LetBurdock(_content));
        _lets.Add(new LetCabbage(_content));
        _lets.Add(new LetCarrot(_content));
        _lets.Add(new LetFence(_content));
        _lets.Add(new LetFlowersBig(_content));
        _lets.Add(new LetFlowersSmall(_content));
        _lets.Add(new Let(_content));
        _lets.Add(new LetNechaev(_content));
        _lets.Add(new LetStone(_content));
    }

    public Let GenerateLet()
    {
        var index = _rnd.Next(0, _lets.Count - 1);
        return _lets[index];
    }
}