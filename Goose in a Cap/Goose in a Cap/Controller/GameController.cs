using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GooseInCap;

public class GameController
{
    private bool _flag;
    private Race _race = new Race();
    private GameDrawer _gameDrawer;
    private ContentLoad _loader;
    private bool _isRunning = true; //игра запущена
    private bool _isCollision;
    private Player _player = new Player();

    public GameController(GameDrawer gameDrawer, ContentLoad loader)
    {
        _gameDrawer = gameDrawer;
        _loader = loader;
    }

    public Race Race  => _race;
    public bool IsRunning => _isRunning;
    
    public void Update()
    {
        Run();
        Jump();
        CheckCollision();
    }

    public void InitializeCharacter()
    {
        _race.Character = ChooseGoose("run_base", "jump_base",
            "jump_base", _loader.Sprites); //эта строчка заменится на отслеживание интерфейса
        //TODO сделать дефолтную инициализацию гуся
        
        _gameDrawer.DrawCharacter(_race.Character);
    }
    
    private Goose ChooseGoose(string nameRunSprite, string nameJumpSprite, 
        string nameWaitSprite, Dictionary<string, (Texture2D, Point)> sprites)
    {
        return new Goose(nameRunSprite, nameJumpSprite, nameWaitSprite, sprites);
    }

    private void Jump()
    {
        if ((Keyboard.GetState().IsKeyDown(Keys.Up) || 
             Keyboard.GetState().IsKeyDown(Keys.Space) ||
             Keyboard.GetState().IsKeyDown(Keys.W)) && _gameDrawer.CanJump)
        {
            _flag = true;
            _gameDrawer.CanJump = false;
        }
    
        if (_flag)
        {
            if (_race.Position.Y > _race.FlightLevel) _race.Position.Y -= _race.JumpSpeed;
            else _flag = false;
        }
    
        if (!_flag)
        {
            if (_race.Position.Y < _race.RunningLevel) _race.Position.Y += _race.JumpSpeed;
            else
            {
                _gameDrawer.CanLand = true;
                if (_isRunning) _gameDrawer.CanJump = true;
            }
        }
    }

    private void Run()
    {
        //бежать, когда началась игра
        _gameDrawer.CanRun = true;
    }

    private void CheckCollision()
    {
        if (_gameDrawer.Let != null && _race.Position.Y >= _gameDrawer.Let.Height &&
            (_race.Position.X + _race.Character.FrameRunWidth - 10 >= _gameDrawer.Let.CurrentPosition && 
             _race.Position.X <= _gameDrawer.Let.CurrentPosition + _gameDrawer.Let.Width))
        {
            _gameDrawer.IsCollision = true;
            _isRunning = false;
            _gameDrawer.CanJump = false;
        }
    }
}