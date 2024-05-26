using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GooseInCap;

public class GameController
{
    private Race _race; 
    private GameDrawer _gameDrawer;
    private ContentLoad _loader;
    private Player _player;
    
    private bool _isJump;
    private bool _isCollision;

    public GameController(GameDrawer gameDrawer, ContentLoad loader, Player player)
    {
        _gameDrawer = gameDrawer;
        _loader = loader;
        _player = player;
        _race = new Race(gameDrawer.ContentManager);
    }

    public Race Race  => _race;
    public bool IsRunning { get; set; }

    public void Update()
    {
        Run();
        Jump();
        CheckCollision();
        CheckGetCoin();
        if (IsRunning) _race.Score += _race.PointsConst;
        else if (_race.Score > _player.Record) _player.Record = _race.Score;
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
            _isJump = true;
            _gameDrawer.CanJump = false;
        }
    
        if (_isJump)
        {
            if (_race.CharacterPosition.Y > _race.FlightLevel) _race.CharacterPosition.Y -= _race.JumpSpeed;
            else _isJump = false;
        }
    
        if (!_isJump)
        {
            if (_race.CharacterPosition.Y < _race.RunningLevel) _race.CharacterPosition.Y += _race.JumpSpeed;
            else
            {
                _gameDrawer.CanLand = true;
                if (IsRunning) _gameDrawer.CanJump = true;
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
        if (_gameDrawer.Let != null && _race.CharacterPosition.Y >= _gameDrawer.Let.Height &&
            (_race.CharacterPosition.X + _race.Character.FrameRunWidth - 10 >= _gameDrawer.Let.CurrentPosition && 
             _race.CharacterPosition.X <= _gameDrawer.Let.CurrentPosition + _gameDrawer.Let.Width))
        {
            _gameDrawer.IsCollision = true;
            IsRunning = false;
            _gameDrawer.CanJump = false;
        }
    }

    private void CheckGetCoin()
    {
        if (_race.Coin.CurrentPosition <= _race.CharacterPosition.X + _race.Character.FrameRunWidth / 2 
            && _race.Coin.CurrentPosition >= _race.CharacterPosition.X
            && _race.Coin.Level <= _race.CharacterPosition.Y + _race.Character.FrameRunHeight)
        {
            _player.CountCoins += 1;
            _race.CountCoins += 1;
            _race.Coin.CurrentPosition = _race.Coin.CountOvercomeLets(_gameDrawer.Let);
        }
    }
}