using System;
using System.Collections.Generic;
using SharpDX.Direct3D11;

namespace Goose_in_a_Cap;

public class GenerateLets
{
    private List<Texture2D> _lets = new List<Texture2D>();
    private Random _rnd = new Random();

    public void LoadLet(Texture2D let)
    {
        _lets.Add(let);
    }

    public Texture2D GenerateLet()
    {
        var index = _rnd.Next(0, _lets.Count);
        return _lets[index];
    }
}