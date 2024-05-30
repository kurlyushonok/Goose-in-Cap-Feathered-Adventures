using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GooseInCap;

public class GameDrawer
{
    private readonly Texture2D _earthTexture;
    private readonly Texture2D _skyTexture;
    private Texture2D _currentCharacterSprite;
    private readonly Texture2D _scoreSprite;
    private readonly Texture2D _mainMenuSprite;
    private readonly Texture2D _corral;
    
    private readonly int _environmentSpeed = 10;
    private readonly int _skySpeed = 1;
    private readonly int _runPeriod = 75;
    private readonly int _corralPeriod = 150;
    private int _currentCorralPosition = 400;

    private Point _currentFrame = new Point(0, 0);
    private Point _currentCharacterFrameSize;
    private Point _currentCharacterSpriteSize;
    private readonly Point _corralSize = new Point(2, 1);
    private Point _currentCorralFrame = new Point(0, 0);

    private bool _canRun;
    private bool _canJump = true;
    private bool _canLand;

    private readonly GenerateLets _letsGenerator;
    private Let _let;
    private readonly Player _player;

    private readonly SpriteBatch _spriteBatch;

    private readonly Texture2D _endSprite;

    public GameDrawer(SpriteBatch spriteBatch, ContentManager content, Player player)
    {
        _spriteBatch = spriteBatch;
        _letsGenerator = new GenerateLets(content);
        _letsGenerator.LoadLet();
        _endSprite = content.Load<Texture2D>("end");
        _earthTexture = content.Load<Texture2D>("background_earth");
        _skyTexture = content.Load<Texture2D>("background_sky");
        _scoreSprite = content.Load<Texture2D>("score");
        _mainMenuSprite = content.Load<Texture2D>("start_screen");
        _corral = content.Load<Texture2D>("corral");
        _player = player;
        ContentManager = content;
    }

    public Let Let{
        get => _let;
        set => _let = value;
    }
    
    public int BackgroundEarthPosition { get; set; }
    public int BackgroundSkyPosition { get; set; }

    public int CurrentCorralPosition
    {
        get => _currentCorralPosition;
        set => _currentCorralPosition = value;
    }

    public bool IsCollision { get; set; }
    public ContentManager ContentManager { get; set; }
    
    public Point CurrentFrame => _currentFrame;

    public Texture2D CurrentCharacterSprite => _currentCharacterSprite;

    public Point CurrentCharacterFrameSize  => _currentCharacterFrameSize;

    public int CurrentTime { get; set; }

    public int CurrentCorralTime { get; set; }

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

    public SpriteFont Font26 { get; set; }
    
    public bool CanLand
    {
        get => _canLand;
        set => _canLand = value;
    }

    public void DrawMainMenu(MainMenu menu)
    {
        _spriteBatch.Begin();
        
        DrawMainMenuBckg();
        DrawButton(menu.PlayButton);
        DrawButton(menu.StoreButton);
        
        _spriteBatch.End();
    }
    
    public void DrawGame(Race race)
    {
        _spriteBatch.Begin();
        
        DrawSky();
        DrawEarth();
        DrawScoreSprite();
        if (_currentCorralPosition >= 0 - _corral.Width / 2) DrawCorral();
        DrawLet(race);
        DrawCoin(race);
        DrawCoinsScore(race);
        DrawScore(race);
        DrawRecord();
        if (_canRun) DrawRun(race);
        if (!_canJump) DrawJump(race);
        if (_canLand) DrawCharacter(race);

        _spriteBatch.End();
    }

    public void DrawFinal(Final final)
    {
        _spriteBatch.Begin();
        
        DrawCollision();
        DrawButton(final.ReplayButton);
        DrawButton(final.BackButton);
        
        _spriteBatch.End();
    }

    public void DrawPause(Pause pause)
    {
        _spriteBatch.Begin();
        
        DrawButton(pause.PauseButton);

        _spriteBatch.End();
    }
    
    public void DrawCharacter(Goose goose)
    {
        _currentCharacterSprite = goose.RunSprite;
        _currentCharacterFrameSize = new Point(goose.FrameRunWidth, goose.FrameRunHeight);
        _currentCharacterSpriteSize = goose.SpriteSizeRun;
    }

    private void DrawMainMenuBckg()
    {
        _spriteBatch.Draw(_mainMenuSprite, Vector2.Zero, Color.White);
    }

    private void DrawCorral()
    {
        if (CurrentCorralTime > _corralPeriod)
        {
            CurrentCorralTime -= _corralPeriod;
            ++_currentCorralFrame.X;
            if (_currentCorralFrame.X >= _corralSize.X)
            {
                _currentCorralFrame.X = 0;
            }
        }
        var characterRectangle = new Rectangle(_currentCorralFrame.X * _corral.Width / 2,
            _currentCorralFrame.Y * _corral.Height,
            _corral.Width / 2, _corral.Height);
        
        _spriteBatch.Draw(_corral, new Vector2(CurrentCorralPosition, 520), 
            characterRectangle, Color.White);
        CurrentCorralPosition -= _environmentSpeed;
    }

    private void DrawButton(Button btn)
    {
        _spriteBatch.Draw(btn.Sprite, btn.Position, Color.White);
    }

    private void DrawScoreSprite()
    {
        _spriteBatch.Draw(_scoreSprite, new Vector2(150, 67), Color.White);
    }

    private void DrawRun(Race race)
    {
        if (CurrentTime > _runPeriod)
        {
            CurrentTime -= _runPeriod;
            ++_currentFrame.X;
            if (_currentFrame.X >= _currentCharacterSpriteSize.X)
            {
                _currentFrame.X = 0;
            }
        }
        var characterRectangle = new Rectangle(_currentFrame.X * _currentCharacterFrameSize.X,
            _currentFrame.Y * _currentCharacterFrameSize.Y,
            _currentCharacterFrameSize.X, _currentCharacterFrameSize.Y);
        
        _spriteBatch.Draw(_currentCharacterSprite, race.CharacterPosition, 
            characterRectangle, Color.White);
    }

    private void DrawJump(Race race)
    {
        _currentCharacterSprite = race.Character.JumpSprite;
        _currentCharacterFrameSize = new Point(race.Character.FrameJumpWidth, race.Character.FrameJumpHeight);
        _currentCharacterSpriteSize = race.Character.SpriteSizeJump;
    }
    
    private void DrawCharacter(Race race)
    {
        _currentCharacterSprite = race.Character.RunSprite;
        _currentCharacterFrameSize = new Point(race.Character.FrameRunWidth, race.Character.FrameRunHeight);
        _currentCharacterSpriteSize = race.Character.SpriteSizeRun;
        _canLand = false;
    }
    
    private void DrawEarth()
    {
        _spriteBatch.Draw(_earthTexture, Vector2.Zero,
            new Rectangle(BackgroundEarthPosition, 0, 1920, 1080),
            Color.White);
        if (!IsCollision) BackgroundEarthPosition += _environmentSpeed;
        if (BackgroundEarthPosition >= 1920) BackgroundEarthPosition = 0;
    }
    
    private void DrawSky()
    {
        _spriteBatch.Draw(_skyTexture, Vector2.Zero,
            new Rectangle(BackgroundSkyPosition, 0, 1920, 1080),
            Color.White);
        if (!IsCollision) BackgroundSkyPosition += _skySpeed;
        if (BackgroundSkyPosition >= 1920) BackgroundSkyPosition = 0;
    }

    private void DrawLet(Race race)
    {
        if (_let == null)
        {
            _let = _letsGenerator.GenerateLet();
        }
        var letPosition = new Vector2(_let.CurrentPosition, _let.Level);
        _spriteBatch.Draw(_let.Sprite, letPosition, Color.White);
        if (!IsCollision) _let.CurrentPosition -= _environmentSpeed;
        if (_let.CurrentPosition <= (0 - _let.Sprite.Width))
        {
            _let.CurrentPosition = 1920;
            _let = null;
            race.NumberOfLetsPassed += 1;
        }
    }

    private void DrawCollision()
    {
        _spriteBatch.Draw(_endSprite, new Vector2(960 - _endSprite.Width / 2, 400 - _endSprite.Height / 2), Color.White);
    }
    
    private void DrawCoinsScore(Race race)
    {
        _spriteBatch.DrawString(spriteFont: Font26, text: race.CountCoins.ToString(), 
            position: new Vector2(200, 60), color: Color.Black);
    }

    private void DrawScore(Race race)
    {
        _spriteBatch.DrawString(spriteFont: Font26, text: race.Score.ToString(), 
            position: new Vector2(200, 93), color: Color.Black);
    }

    private void DrawRecord()
    {
        _spriteBatch.DrawString(spriteFont: Font26, text: _player.Record.ToString(), 
            position: new Vector2(200, 124), color: Color.Gold);
    }

    private void DrawCoin(Race race)
    {
        _spriteBatch.Draw(race.Coin.Sprite, new Vector2(race.Coin.CurrentPosition, race.Coin.Level),
            Color.White);
        if (!IsCollision) race.Coin.CurrentPosition -= _environmentSpeed;
        if (race.Coin.CurrentPosition <= (0 - race.Coin.Sprite.Width))
        {
            if (_let != null) race.Coin.CurrentPosition = race.Coin.CountOvercomeLets(_let);
        }
    }
}