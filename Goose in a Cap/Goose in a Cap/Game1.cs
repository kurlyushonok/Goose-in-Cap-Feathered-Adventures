using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GooseInCap;

public class Game1 : Game
{
    public static State State = State.MainMenu;
    
    private GraphicsDeviceManager _graphics;
    private static SpriteBatch _spriteBatch;

    private ContentLoad _loader = new ContentLoad();
    private GameDrawer _gameDrawer;
    private GameController _gameController;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1024;
        _graphics.IsFullScreen = false;
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        var player = new Player();
        _gameDrawer = new GameDrawer(_spriteBatch, Content, player)
        {
            Font26 = Content.Load<SpriteFont>("CoinsFont")
        };
        _gameController = new GameController(_gameDrawer, _loader, player);

        _gameController.InitializeCharacter();
    }

    protected override void Update(GameTime gameTime)
    {
        if (_gameController.IsRunning)
        {
            _gameDrawer.CurrentTime += gameTime.ElapsedGameTime.Milliseconds;
            _gameDrawer.CurrentCorralTime += gameTime.ElapsedGameTime.Milliseconds;
        }
        switch (State) 
        {
            case State.MainMenu:
                _gameController.MenuUpdate();
                break;
            
            case State.Game:
                if (_gameController.Final.ReplayButton.IsClick ||
                    _gameController.Final.BackButton.IsClick)
                {
                    _gameDrawer.CurrentTime = 0;
                    _gameDrawer.CurrentCorralTime = 0;
                }
                _gameController.IsRunning = true;
                _gameController.GameUpdate();
                break;
            
            case State.Final:
                _gameController.FinalUpdate();
                break;
            
            case State.Pause:
                _gameDrawer.CurrentTime = 0;
                _gameDrawer.CurrentCorralTime = 0;
                _gameController.PauseUpdate();
                break;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        switch (State)
        {
            case State.MainMenu:
                _gameDrawer.DrawMainMenu(_gameController.Menu);
                break;
            
            case State.Game:
                _gameDrawer.DrawGame(_gameController.Race);
                break;
            
            case State.Final:
                _gameDrawer.DrawFinal(_gameController.Final);
                break;
            
            case State.Pause:
                _gameDrawer.DrawPause(_gameController.Pause);
                break;
        }
        base.Draw(gameTime);
    }
}