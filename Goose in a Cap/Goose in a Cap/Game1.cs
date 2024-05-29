using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GooseInCap;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private static SpriteBatch _spriteBatch;
    public static State State = State.MainMenu;
    
    private ContentLoad _loader = new ContentLoad();
    private GameDrawer _gameDrawer;
    private GameController _gameController;
    
    // public static event Action initializeGoose;

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
        
        var baseRun = Content.Load<Texture2D>("goose_run_sprite");
        _loader.LoadSprite(baseRun, new Point(2, 1), "run_base");
        var baseJump = Content.Load<Texture2D>("goose_jump");
        _loader.LoadSprite(baseJump, new Point(1, 1), "jump_base");
        
        _gameController.InitializeCharacter();
    }

    protected override void Update(GameTime gameTime)
    {
        if (_gameController.IsRunning)
            _gameDrawer.CurrentTime += gameTime.ElapsedGameTime.Milliseconds;
        switch (State) //TODO добавить другие состояния
        {
            case State.MainMenu:
                _gameController.MenuUpdate();
                break;
            
            case State.Game:
                _gameController.IsRunning = true;
                _gameController.GameUpdate();
                break;
        }
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

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
        }
        base.Draw(gameTime);
    }
}