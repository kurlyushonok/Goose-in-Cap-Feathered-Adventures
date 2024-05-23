using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GooseInCap;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    public static SpriteBatch _spriteBatch;
    private State _state = State.Game;
    
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
        
        _gameDrawer = new GameDrawer(_spriteBatch, Content)
        {
            Font = Content.Load<SpriteFont>("CoinsFont")
        };
        _gameController = new GameController(_gameDrawer, _loader);
        
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
        switch (_state) //TODO добавить другие состояния
        {
            case State.Game:
                _gameController.Update();
                break;
        }
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        switch (_state)
        {
            case State.Game:
                _gameDrawer.Draw(_gameController.Race);
                break;
        }
        base.Draw(gameTime);
    }
}