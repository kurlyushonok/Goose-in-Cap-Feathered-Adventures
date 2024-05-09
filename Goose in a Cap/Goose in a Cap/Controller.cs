using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Goose_in_a_Cap;

public class Controller
{
    private bool _flag;
    private bool _canJump;
    private int _runPeriod = 80;
    private int _currentTime;
    private Point _currentFrame = new Point(0, 0); //в drawer убрать
    private Race _race = new Race();
    private Goose _goose;
    private Texture2D _currentCharacterSprite;
    private Point _currentCharacterFrameSize;
    private Point _currentCharacterSpriteSize;

    public int CurrentTime
    {
        get => _currentTime;
        set => _currentTime = value;
    }

    public Goose Character
    {
        get => _goose;
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

    public void Update()
    {
        Run();
        Jump();
    }

    public void InitializeCharacter(ContentLoad loader)
    {
        _goose = ChooseGoose("run_base", "jump_base",
            "jump_base", loader.Sprites, _race.Position);
        _currentCharacterSprite = _goose.RunSprite;
        _currentCharacterFrameSize = new Point(_goose.FrameRunWidth, _goose.FrameRunHeight);
        _currentCharacterSpriteSize = _goose.SpriteSizeRun;
    }
    
    private Goose ChooseGoose(string nameRunSprite, string nameJumpSprite, 
        string nameWaitSprite, Dictionary<string, (Texture2D, Point)> sprites, Vector2 position)
    {
        return new Goose(nameRunSprite, nameJumpSprite, nameWaitSprite, sprites, position);
    }

    private void Jump()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Up) && _canJump)
        {
            _flag = true;
            _canJump = false;
            _currentCharacterSprite = _goose.JumpSprite;
            _currentCharacterFrameSize = new Point(_goose.FrameJumpWidth, _goose.FrameJumpHeight);
            _currentCharacterSpriteSize = _goose.SpriteSizeJump;
        }
    
        if (_flag)
        {
            if (_goose._position.Y > 270) _goose._position.Y -= 20;
            else _flag = false;
        }
    
        if (!_flag)
        {
            if (_goose._position.Y < 510) _goose._position.Y += 20;
            else
            {
                _currentCharacterSprite = _goose.RunSprite;
                _currentCharacterFrameSize = new Point(_goose.FrameRunWidth, _goose.FrameRunHeight);
                _currentCharacterSpriteSize = _goose.SpriteSizeRun;
                _canJump = true;
            }
        }
    }

    private void Run()
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
    }
}