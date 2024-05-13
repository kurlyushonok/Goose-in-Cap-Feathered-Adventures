using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Goose_in_a_Cap;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    public static SpriteBatch _spriteBatch;
    
    private ContentLoad _loader = new ContentLoad();
    private Drawer _drawer;
    private Controller _controller;
    
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
        
        _drawer = new Drawer(_spriteBatch)
        {
            Background = Content.Load<Texture2D>("background_sprite")
        };
        _controller = new Controller(_drawer, _loader);
        
        var baseRun = Content.Load<Texture2D>("goose_run_sprite");
        _loader.LoadSprite(baseRun, new Point(2, 1), "run_base");
        var baseJump = Content.Load<Texture2D>("goose_jump");
        _loader.LoadSprite(baseJump, new Point(1, 1), "jump_base");
        
        _controller.InitializeCharacter();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _drawer.CurrentTime += gameTime.ElapsedGameTime.Milliseconds;
        _controller.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _drawer.Draw(_controller.Race);
        base.Draw(gameTime);
    }
}