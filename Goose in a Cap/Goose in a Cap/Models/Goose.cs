using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GooseInCap;

public class Goose
{
    private Texture2D _runSprite;
    private Point _spriteSizeRun;

    private Texture2D _jumpSprite;
    private Point _spriteSizeJump;

    public Goose(ContentManager content)
    {
        _runSprite = content.Load<Texture2D>("goose_run_sprite");
        _spriteSizeRun = new Point(2, 1);

        _jumpSprite = content.Load<Texture2D>("goose_jump");
        _spriteSizeJump = new Point(1, 1);
    }

    public Texture2D RunSprite
    {
        get => _runSprite;
    }
    
    public Texture2D JumpSprite
    {
        get => _jumpSprite;
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

    public Point SpriteSizeRun
    {
        get => _spriteSizeRun;
    }
    
    public Point SpriteSizeJump
    {
        get => _spriteSizeJump;
    }
}