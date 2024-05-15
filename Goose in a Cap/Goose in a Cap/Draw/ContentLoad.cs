using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GooseInCap;

public class ContentLoad
{
    private Dictionary<string, (Texture2D, Point)> _sprites = new Dictionary<string, (Texture2D, Point)>();

    public Dictionary<string, (Texture2D, Point)> Sprites
    {
        get => _sprites;
    }

    public void LoadSprite(Texture2D sprite, Point spriteSize, string nameSprite)
    {
        _sprites[nameSprite] = (sprite, spriteSize);
    }

    public (Texture2D, Point) GetSprite(string nameSprite)
    {
        return _sprites[nameSprite];
    }
    
    // public void InitializeContent(Texture2D sprite, Point spriteSize, 
    //     int frameWidth, int frameHeight, string nameSprite)
    // {
    //     LoadSprite(sprite, spriteSize,frameWidth, frameHeight, nameSprite);
    // }
}