using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Goose_in_a_Cap;

public class Drawer
{
    private int _currentTime;
    private Texture2D _background;
    
    private int _backgroundSpeed = 10;
    private Point _currentFrame = new Point(0, 0);
    private int _runPeriod = 80;
    private Texture2D _currentCharacterSprite;
    private Point _currentCharacterFrameSize;
    private Point _currentCharacterSpriteSize;
    private int _currentPosition;

    private bool _canRun;
    private bool _canJump = true;
    private bool _canLand;
    
    private SpriteBatch _spriteBatch;
    public Drawer(SpriteBatch spriteBatch)
    {
        _spriteBatch = spriteBatch;
    }

    public Texture2D Background
    {
        get => _background; 
        set => _background = value; 
    }
    
    public Point CurrentFrame
    {
        get => _currentFrame;
    }

    public Texture2D CurrentCharacterSprite
    {
        get => _currentCharacterSprite;
    }
    
    public Point CurrentCharacterFrameSize
    {
        get => _currentCharacterFrameSize;
    }

    public int CurrentTime
    {
        get => _currentTime;
        set => _currentTime = value;
    }

    public bool CanRun
    {
        get => _canRun;
        set => _canRun = value;
    }
    
    public bool CanJump
    {
        get => _canJump;
        set => _canJump = value;
    }
    
    public bool CanLand
    {
        get => _canLand;
        set => _canLand = value;
    }
    
    public void Draw(Race race)
    {
        _spriteBatch.Begin();
        
        DrawBackground();
        if (_canRun) DrawRun(race);
        if (!_canJump) DrawJump(race);
        if (_canLand) DrawLand(race);
        
        _spriteBatch.End();
    }
    
    public void DrawCharacter(Goose goose)
    {
        _currentCharacterSprite = goose.RunSprite;
        _currentCharacterFrameSize = new Point(goose.FrameRunWidth, goose.FrameRunHeight);
        _currentCharacterSpriteSize = goose.SpriteSizeRun;
    }

    private void DrawRun(Race race)
    {
        if (_currentTime > _runPeriod)
        {
            _currentTime -= _runPeriod;
            ++_currentFrame.X;
            if (_currentFrame.X >= _currentCharacterSpriteSize.X)
            {
                _currentFrame.X = 0;
            }
        }
        
        _spriteBatch.Draw(_currentCharacterSprite, race.Position, 
            new Rectangle(_currentFrame.X * _currentCharacterFrameSize.X, 
                _currentFrame.Y * _currentCharacterFrameSize.Y, 
                _currentCharacterFrameSize.X,  _currentCharacterFrameSize.Y), Color.White);
    }

    private void DrawJump(Race race)
    {
        _currentCharacterSprite = race.Character.JumpSprite;
        _currentCharacterFrameSize = new Point(race.Character.FrameJumpWidth, race.Character.FrameJumpHeight);
        _currentCharacterSpriteSize = race.Character.SpriteSizeJump;
    }
    
    private void DrawLand(Race race)
    {
        _currentCharacterSprite = race.Character.RunSprite;
        _currentCharacterFrameSize = new Point(race.Character.FrameRunWidth, race.Character.FrameRunHeight);
        _currentCharacterSpriteSize = race.Character.SpriteSizeRun;
        _canLand = false;
    }
    
    private void DrawBackground()
    {
        _spriteBatch.Draw(_background, Vector2.Zero,
            new Rectangle(_currentPosition, 0, 1920, 1080),
            Color.White);
        _currentPosition += _backgroundSpeed;
        if (_currentPosition >= 1920) _currentPosition = 0;
    }
}