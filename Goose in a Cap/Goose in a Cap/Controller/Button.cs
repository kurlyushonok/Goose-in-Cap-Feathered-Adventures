using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = System.Numerics.Vector2;

namespace GooseInCap;

public class Button
{
    protected Vector2 _position;
    protected Texture2D _sprite;
    protected Rectangle _btnRectangle;

    private MouseState _currentState;
    private MouseState _previousState;

    public Button(ContentManager content)
    {
        _sprite = content.Load<Texture2D>("play_btn");
        _position = new Vector2(960 - _sprite.Width / 2, 500 - _sprite.Height / 2);
        _btnRectangle = new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, _sprite.Height);
    }

    public Texture2D Sprite => _sprite;
    public Vector2 Position => _position;

    protected bool EnterButton()
    {
        _previousState = _currentState;
        _currentState = Mouse.GetState();
        var mouseRectangle = new Rectangle(_currentState.X, _currentState.Y, 1, 1);

        return mouseRectangle.Intersects(_btnRectangle)
               && _currentState.LeftButton == ButtonState.Released
               && _previousState.LeftButton == ButtonState.Pressed;
    }

    public void ExecuteOnClick()
    {
        if (EnterButton()) Game1.State = State.Game;
    }
}

public class StoreBtn : Button
{
    public StoreBtn(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("store_btn");
        _position = new Vector2(960 - _sprite.Width / 2, 600 - _sprite.Height / 2);
        _btnRectangle = new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, _sprite.Height);
    }
}