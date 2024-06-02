using System.Collections.Generic;
using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GooseInCap;

public class GameController
{
    private Race _race;
    private MainMenu _menu;
    private Final _final;
    private Pause _pause;
    private Shop _shop;
        
    private GameDrawer _gameDrawer;
    private Player _player;

    private bool _isJump;

    public GameController(GameDrawer gameDrawer, Player player)
    {
        _gameDrawer = gameDrawer;
        _player = player;
        _race = new Race(gameDrawer.ContentManager, player.Character.RunningLevel, player.Character.FlightLevel);
        _menu = new MainMenu(gameDrawer.ContentManager);
        _final = new Final(_gameDrawer.ContentManager);
        _pause = new Pause(_gameDrawer.ContentManager);
        _shop = new Shop(_gameDrawer.ContentManager);
    }

    public Race Race  => _race;
    public MainMenu Menu => _menu;
    public Final Final => _final;
    public Pause Pause => _pause;
    public Shop Shop => _shop;
    public bool IsRunning { get; set; }

    public void MenuUpdate()
    {
        _menu.PlayButton.ExecuteOnClick();
        _menu.StoreButton.ExecuteOnClick();
    }

    public void GameUpdate()
    {
        _final.ReplayButton.IsClick = false;
        _final.BackButton.IsClick = false;
        Run();
        Jump();
        CheckCollision();
        CheckGetCoin();
        CheckPause();
        if (IsRunning) _race.Score += _race.PointsConst;
        else if (_race.Score > _player.Record) _player.Record = _race.Score;
    }
    
    public void FinalUpdate()
    {
        _final.ReplayButton.ExecuteOnClick();
        _final.BackButton.ExecuteOnClick();
        if (_final.ReplayButton.IsClick || _final.BackButton.IsClick)
        {
            if (_gameDrawer.Let != null) _gameDrawer.Let.CurrentPosition = 1920;
            _gameDrawer.Let = null;
            _gameDrawer.IsCollision = false;
            _gameDrawer.CanJump = true;
            _gameDrawer.CanRun= true;
            _gameDrawer.BackgroundEarthPosition = 0;
            _gameDrawer.BackgroundSkyPosition = 0;
            _gameDrawer.CurrentCorralPosition = 400;

            IsRunning = true;
            
            _player.CountCoins += _race.CountCoins;
            _race.CountCoins = 0;
            _race.Score = 0;
            _race.Coin.CurrentPosition = 2500;
        }
    }

    public void PauseUpdate()
    {
        _pause.PauseButton.ExecuteOnClick();
        if (_pause.PauseButton.IsClick) IsRunning = true;
    }

    public void ShopUpdate()
    {
        
    }

    public void InitializeCharacter()
    {
        _race.Character = _player.Character;
        
        _gameDrawer.DrawCharacter(_race.Character);
    }
    
    private Goose ChooseGoose()
    {
        return new Goose(_gameDrawer.ContentManager);
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
            if (_race.CharacterPosition.Y < _race.RunningRunningLevel) _race.CharacterPosition.Y += _race.JumpSpeed;
            else
            {
                _gameDrawer.CanLand = true;
                if (IsRunning) _gameDrawer.CanJump = true;
            }
        }
    }

    private void Run()
    {
        _gameDrawer.CanRun = true;
    }

    private void CheckCollision()
    {
        if (_gameDrawer.Let != null && _race.CharacterPosition.Y >= _gameDrawer.Let.Height &&
            (_race.CharacterPosition.X + _race.Character.FrameRunWidth - _race.Character.Padding >= _gameDrawer.Let.CurrentPosition && 
             _race.CharacterPosition.X <= _gameDrawer.Let.CurrentPosition + _gameDrawer.Let.Width))
        {
            _gameDrawer.IsCollision = true;
            IsRunning = false;
            _gameDrawer.CanJump = false;
            Game1.State = State.Final;
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

    private void CheckPause()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Game1.State = State.Pause;
            IsRunning = false;
        }
    }
}