using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Goose_in_a_Cap;

public class Goose
{
    private Texture2D _runSprite;
    private Point _spriteSizeRun;

    private Texture2D _jumpSprite;
    private Point _spriteSizeJump;

    private Texture2D _waitSprite;
    private Point _spriteSizeWait;

    public Goose(string nameRunSprite, string nameJumpSprite, string nameWaitSprite, 
        Dictionary<string, (Texture2D, Point)> sprites)
    {
        _runSprite = sprites[nameRunSprite].Item1;
        _spriteSizeRun = sprites[nameRunSprite].Item2;

        _jumpSprite = sprites[nameJumpSprite].Item1;
        _spriteSizeJump = sprites[nameJumpSprite].Item2;

        _waitSprite = sprites[nameWaitSprite].Item1;
        _spriteSizeWait = sprites[nameWaitSprite].Item2;

    }

    public Texture2D RunSprite
    {
        get => _runSprite;
    }
    
    public Texture2D JumpSprite
    {
        get => _jumpSprite;
    }
    
    public Texture2D WaitSprite
    {
        get => _waitSprite;
    }

    public int FrameRunWidth
    {
        get => _runSprite.Width / _spriteSizeRun.X;
    }
    
    public int FrameRunHeight
    {
        get => _runSprite.Height;
    }
    
    public int FrameJumpWidth
    {
        get => _jumpSprite.Width / _spriteSizeJump.X;
    }
    
    public int FrameJumpHeight
    {
        get => _jumpSprite.Height;
    }
    
    public int FrameWaitWidth
    {
        get => _waitSprite.Width / _spriteSizeWait.X;
    }
    
    public int FrameWaitHeight
    {
        get => _waitSprite.Height;
    }
    
    public Point SpriteSizeRun
    {
        get => _spriteSizeRun;
    }
    
    public Point SpriteSizeJump
    {
        get => _spriteSizeJump;
    }
    
    public Point SpriteSizeWait
    {
        get => _spriteSizeWait;
    }
}