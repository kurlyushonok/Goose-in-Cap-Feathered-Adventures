using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GooseInCap;

public class Goose
{
    protected Texture2D _runSprite;
    private Point _spriteSizeRun = new Point(2, 1);

    protected Texture2D _jumpSprite;
    private Point _spriteSizeJump = new Point(1, 1);

    private bool _isSelected = true;
    private bool _isPayed = true;

    protected int _runningLevel = 520;
    protected int _fligthLevel = 150;
    protected int _padding = 10;

    public Goose(ContentManager content)
    {
        _runSprite = content.Load<Texture2D>("goose_run");
        _jumpSprite = content.Load<Texture2D>("goose_jump");
    }

    public int RunningLevel => _runningLevel;
    public int FlightLevel => _fligthLevel;
    public int Padding => _padding;

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

public class GooseFrog : Goose
{
    public GooseFrog(ContentManager content) : base(content)
    {
        _runSprite = content.Load<Texture2D>("goose_run_frog");
        _jumpSprite = content.Load<Texture2D>("goose_jump_frog");
        _runningLevel = 490;
        _padding = 15;
    }
}

public class GooseFlower : Goose
{
    public GooseFlower(ContentManager content) : base(content)
    {
        _runSprite = content.Load<Texture2D>("goose_run_flower");
        _jumpSprite = content.Load<Texture2D>("goose_jump_flower");
        _runningLevel = 485;
        _padding = 25;
        _fligthLevel = 130;
    }
}