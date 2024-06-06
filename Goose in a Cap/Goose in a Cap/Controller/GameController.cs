using System.Collections.Generic;
using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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
        _menu = new MainMenu(gameDrawer.ContentManager);
        _final = new Final(_gameDrawer.ContentManager);
        _pause = new Pause(_gameDrawer.ContentManager);
        _shop = new Shop(_gameDrawer.ContentManager);
        _player = player;
        _race = new Race(gameDrawer.ContentManager, _shop.BaseCard.Character.RunningLevel, 
            _shop.BaseCard.Character.FlightLevel);
        _race.SetCharacter(_shop.BaseCard.Character);
    }

    public Race Race  => _race;
    public MainMenu Menu => _menu;
    public Final Final => _final;
    public Pause Pause => _pause;
    public Shop Shop => _shop;
    public Player Player => _player;
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
            _gameDrawer.CurrentGrandmotherPosition = 1000;

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
        _shop.Button.ExecuteOnClick();
        
        _shop.BaseCard.Button.ExecuteOnClick(_player.CountCoins, _shop.BaseCard.Price, 
            _shop.BaseCard.State, _shop.BaseCard);
        CheckSetCard(_shop.BaseCard, _shop.FrogCard, _shop.FlowerCard);
        GetCardSprite(_shop.BaseCard);
        
        _shop.FrogCard.Button.ExecuteOnClick(_player.CountCoins, _shop.FrogCard.Price, 
            _shop.FrogCard.State, _shop.FrogCard);
        CheckSetCard(_shop.FrogCard, _shop.BaseCard, _shop.FlowerCard);
        GetCardSprite(_shop.FrogCard);
        
        _shop.FlowerCard.Button.ExecuteOnClick(_player.CountCoins, _shop.FlowerCard.Price, 
            _shop.FlowerCard.State, _shop.FlowerCard);
        CheckSetCard(_shop.FlowerCard, _shop.FrogCard, _shop.BaseCard);
        GetCardSprite(_shop.FlowerCard);
    }

    public void GetCardSprite(Card card)
    {
        if (card.Price > _player.CountCoins && !card.IsPay && !card.IsSelected)
            card.Button.GetStateBuy(StateBuy.NotBuy, card);
        if (card.Price <= _player.CountCoins && !card.IsPay && !card.IsSelected)
            card.Button.GetStateBuy(StateBuy.Buy, card);
        if (card.IsPay && !card.IsSelected)
            card.Button.GetStateBuy(StateBuy.Choose, card);
        if (card.IsSelected)
            card.Button.GetStateBuy(StateBuy.Selected, card);
    }

    public void CheckSetCard(Card cardForCheck, Card other1, Card other2)
    {
        if (cardForCheck.Button.IsClick)
        {
            if (!cardForCheck.IsPay)
            {
                cardForCheck.IsPay = true;
                _player.CountCoins -= cardForCheck.Price;
            }

            cardForCheck.IsSelected = true;
            _race.SetCharacter(cardForCheck.Character);
            _gameDrawer.DrawCharacter(_race.Character);

            other1.IsSelected = false;
            other1.Button.IsClick = false;
            
            other2.IsSelected = false;
            other2.Button.IsClick = false;
        }
    }

    public void InitializeCharacter()
    {
        _gameDrawer.DrawCharacter(_race.Character);
    }

    private void Jump()
    {
        if ((Keyboard.GetState().IsKeyDown(Keys.Up) || 
             Keyboard.GetState().IsKeyDown(Keys.Space) ||
             Keyboard.GetState().IsKeyDown(Keys.W)) && _gameDrawer.CanJump)
        {
            MediaPlayer.Play(_race.Character.JumpSong);
            _isJump = true;
            _gameDrawer.CanJump = false;
        }
    
        if (_isJump)
        {
            if (_race.CharacterPosition.Y > _race.FlightLevel) 
                _race.CharacterPosition.Y -= _race.JumpSpeed;
            else _isJump = false;
        }
    
        if (!_isJump)
        {
            if (_race.CharacterPosition.Y < _race.RunningLevel) 
                _race.CharacterPosition.Y += _race.JumpSpeed;
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
        if (_gameDrawer.Let != null &&
            (_race.CharacterPosition.Y + _race.Character.Padding >= _gameDrawer.Let.Height) &&
            (_race.CharacterPosition.X + _race.Character.FrameRunWidth - _race.Character.Padding >= _gameDrawer.Let.CurrentPosition && 
             _race.CharacterPosition.X <= _gameDrawer.Let.CurrentPosition + _gameDrawer.Let.Width))
        {
            MediaPlayer.Play(_race.GameOverSong);
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
            _race.CountCoins += 1;
            _race.Coin.CurrentPosition = _race.Coin.CountOvercomeLets();
            MediaPlayer.Play(_race.Coin.Song);
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