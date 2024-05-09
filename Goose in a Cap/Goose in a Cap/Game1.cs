using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Goose_in_a_Cap;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private ContentLoad _loader = new ContentLoad();
    private Drawer _drawer = new Drawer();
    private Controller _controller = new Controller();

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
        _drawer.Background = Content.Load<Texture2D>("background_sprite");
        
        var baseRun = Content.Load<Texture2D>("goose_run_sprite");
        _loader.LoadSprite(baseRun, new Point(2, 1), "run_base");
        var baseJump = Content.Load<Texture2D>("goose_jump");
        _loader.LoadSprite(baseJump, new Point(1, 1), "jump_base");
        
        _controller.InitializeCharacter(_loader);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _controller.CurrentTime += gameTime.ElapsedGameTime.Milliseconds;
        _drawer._currentTime += gameTime.ElapsedGameTime.Milliseconds;
        _controller.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White); //перенести в drawer
        // _spriteBatch.Begin();
        // _spriteBatch.Draw(_drawer.Background, Vector2.Zero, Color.White);
        // _spriteBatch.Draw(_controller.CurrentCharacterSprite, _controller.Character.Position, 
        //     new Rectangle(_controller.CurrentFrame.X * _controller.CurrentCharacterFrameSize.X, 
        //         _controller.CurrentFrame.Y * _controller.CurrentCharacterFrameSize.Y, 
        //         _controller.CurrentCharacterFrameSize.X,  _controller.CurrentCharacterFrameSize.Y), Color.White);
        // _spriteBatch.End();
        
        _drawer.Draw(_spriteBatch, _controller);

        base.Draw(gameTime);
    }
}