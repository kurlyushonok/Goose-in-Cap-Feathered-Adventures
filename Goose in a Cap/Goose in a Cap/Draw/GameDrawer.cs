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
    
    private int _currentTime;
    private readonly int _environmentSpeed = 12;
    private readonly int _skySpeed = 1;
    private readonly int _runPeriod = 80;
    private int _backgroundEarthPosition;
    private int _backgroundSkyPosition;
    private int _currentletPosition;

    private Point _currentFrame = new Point(0, 0);
    private Point _currentCharacterFrameSize;
    private Point _currentCharacterSpriteSize;

    private bool _canRun;
    private bool _canJump = true;
    private bool _canLand;

    private readonly GenerateLets _letsGenerator;
    private Let _let;
    private Player _player;

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
        _player = player;
        ContentManager = content;
    }

    public Let Let => _let;
    public int CurrentLetPosition => _currentletPosition;
    public bool IsCollision { get; set; }
    public ContentManager ContentManager { get; set; }
    
    public Point CurrentFrame => _currentFrame;

    public Texture2D CurrentCharacterSprite => _currentCharacterSprite;

    public Point CurrentCharacterFrameSize  => _currentCharacterFrameSize;

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

    public SpriteFont Font26 { get; set; }
    
    public bool CanLand
    {
        get => _canLand;
        set => _canLand = value;
    }
    
    public void Draw(Race race)
    {
        _spriteBatch.Begin();
        
        DrawSky();
        DrawEarth();
        DrawLet(race);
        DrawCoin(race);
        DrawCoinsScore(race);
        DrawScore(race);
        DrawRecord();
        if (_canRun) DrawRun(race);
        if (!_canJump) DrawJump(race);
        if (_canLand) DrawCharacter(race);
        if (IsCollision)
        {
            DrawCollision();
        }
        
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
            new Rectangle(_backgroundEarthPosition, 0, 1920, 1080),
            Color.White);
        if (!IsCollision) _backgroundEarthPosition += _environmentSpeed;
        if (_backgroundEarthPosition >= 1920) _backgroundEarthPosition = 0;
    }
    
    private void DrawSky()
    {
        _spriteBatch.Draw(_skyTexture, Vector2.Zero,
            new Rectangle(_backgroundSkyPosition, 0, 1920, 1080),
            Color.White);
        if (!IsCollision) _backgroundSkyPosition += _skySpeed;
        if (_backgroundSkyPosition >= 1920) _backgroundSkyPosition = 0;
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
        _currentletPosition = _let.CurrentPosition;
        if (_let.CurrentPosition <= (0 - _let.Sprite.Width))
        {
            _let.CurrentPosition = 1920;
            _currentletPosition = 1920;
            _let = null;
            race.NumberOfLetsPassed += 1;
        }
    }

    private void DrawCollision()
    {
        _spriteBatch.Draw(_endSprite, new Vector2(960 - _endSprite.Width / 2, 400 - _endSprite.Height / 2), Color.White);
    }

    private void DrawScore(Race race)
    {
        _spriteBatch.DrawString(spriteFont: Font26, text: race.Score.ToString(), 
            position: new Vector2(1850, 90), color: Color.Black);
    }

    private void DrawCoinsScore(Race race)
    {
        _spriteBatch.DrawString(spriteFont: Font26, text: race.CountCoins.ToString(), 
        position: new Vector2(1850, 60), color: Color.Black);
    }
    
    private void DrawRecord()
    {
        _spriteBatch.DrawString(spriteFont: Font26, text: _player.Record.ToString(), 
            position: new Vector2(1850, 120), color: Color.Gold);
    }

    private void DrawCoin(Race race)
    {
        _spriteBatch.Draw(race.Coin.Sprite, new Vector2(race.Coin.CurrentPosition, race.Coin.Level),
            Color.White);
        if (!IsCollision) race.Coin.CurrentPosition -= _environmentSpeed;
        if (race.Coin.CurrentPosition <= (0 - race.Coin.Sprite.Width))
        {
            race.Coin.CurrentPosition = race.Coin.CountOvercomeLets(CurrentLetPosition);
        }
    }
}