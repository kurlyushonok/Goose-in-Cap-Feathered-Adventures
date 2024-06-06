using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Vector2 = System.Numerics.Vector2;

namespace GooseInCap;

public class Button
{
    protected Vector2 _position;
    protected Texture2D _sprite;
    protected Texture2D _spriteHover;
    protected Texture2D _currentSprite;
    protected Rectangle _btnRectangle;
    protected bool _isHovering;
    protected SoundEffect _song;

    private MouseState _currentState;
    private MouseState _previousState;

    public Button(ContentManager content)
    {
        _sprite = content.Load<Texture2D>("play_btn");
        _currentSprite = _sprite;
        _spriteHover = content.Load<Texture2D>("play_btn_hover");
        _position = new Vector2(960 - _sprite.Width / 2, 500 - _sprite.Height / 2);
        _btnRectangle = new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, _sprite.Height);
        _song = content.Load<SoundEffect>("btn");
    }
    
    public bool IsClick { get; set; }

    public Texture2D Sprite => _currentSprite;
    public Vector2 Position => _position;

    protected bool EnterButton()
    {
        _previousState = _currentState;
        _currentState = Mouse.GetState();
        var mouseRectangle = new Rectangle(_currentState.X, _currentState.Y, 1, 1);
        _isHovering = mouseRectangle.Intersects(_btnRectangle);
        if (_isHovering) _currentSprite = _spriteHover;
        else _currentSprite = _sprite;

        return _isHovering
               && _currentState.LeftButton == ButtonState.Released
               && _previousState.LeftButton == ButtonState.Pressed;
    }

    public void ExecuteOnClick()
    {
        if (EnterButton())
        {
            _song.Play();
            Game1.State = State.Game;
            IsClick = true;
        }
    }
}

public class StoreBtn : Button
{
    public bool IsClick { get; set; }
    public StoreBtn(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("store_btn");
        _currentSprite = _sprite;
        _spriteHover = content.Load<Texture2D>("srore_btn_hover");
        _position = new Vector2(960 - _sprite.Width / 2, 620 - _sprite.Height / 2);
        _btnRectangle = new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, _sprite.Height);
    }
    
    public void ExecuteOnClick()
    {
        if (EnterButton())
        {
            _song.Play();            
            Game1.State = State.Shop;
            IsClick = true;
        }
    }
}

public class ReplayBtn : Button
{
    public bool IsClick { get; set; }
    public ReplayBtn(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("replay_btn");
        _currentSprite = _sprite;
        _spriteHover = content.Load<Texture2D>("replay_hover");
        _position = new Vector2(960 - _sprite.Width / 2, 620 - _sprite.Height / 2);
        _btnRectangle = new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, _sprite.Height);
    }
    
    public void ExecuteOnClick()
    {
        if (EnterButton())
        {
            _song.Play();
            Game1.State = State.Game;
            IsClick = true;
        }
    }
}

public class BackBtn : Button
{
    public bool IsClick { get; set; }
    public BackBtn(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("to_menu_btn");
        _currentSprite = _sprite;
        _spriteHover = content.Load<Texture2D>("to_menu_btn_hover");
        _position = new Vector2(960 - _sprite.Width / 2, 740 - _sprite.Height / 2);
        _btnRectangle = new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, _sprite.Height);
    }

    public void ExecuteOnClick()
    {
        if (EnterButton())
        {
            _song.Play();
            Game1.State = State.MainMenu;
            IsClick = true;
        }
    }
}

public class BackBtnInShop : BackBtn
{
    public BackBtnInShop(ContentManager content) : base(content)
    {
        _position = new Vector2(960 - _sprite.Width / 2, 925);
        _btnRectangle = new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, _sprite.Height);
    }
}

public class BackBtnInClue : BackBtn
{
    public BackBtnInClue(ContentManager content) : base(content)
    {
        _position = new Vector2(960 - _sprite.Width / 2, 925);
        _btnRectangle = new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, _sprite.Height);
    }
}

public class PauseBtn : Button
{
    public bool IsClick { get; set; }
    public PauseBtn(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("continue_btn");
        _currentSprite = _sprite;
        _spriteHover = content.Load<Texture2D>("continue_btn_hover");
        _position = new Vector2(960 - _sprite.Width / 2, 500 - _sprite.Height / 2);
        _btnRectangle = new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, _sprite.Height);
    }

    public void ExecuteOnClick()
    {
        if (EnterButton())
        {
            _song.Play();
            Game1.State = State.Game;
            IsClick = true;
        }
    }
}

public class ClueBtn : Button
{
    public bool IsClick { get; set; }
    public ClueBtn(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("clue");
        _currentSprite = _sprite;
        _spriteHover = content.Load<Texture2D>("clue_hover");
        _position = new Vector2(960 - _sprite.Width / 2, 740 - _sprite.Height / 2);
        _btnRectangle = new Rectangle((int)_position.X, (int)_position.Y, _sprite.Width, _sprite.Height);
    }
    
    public void ExecuteOnClick()
    {
        if (EnterButton())
        {
            _song.Play();
            Game1.State = State.Clue;
            IsClick = true;
        }
    }
}

public class PayBtn : Button
{
    public bool IsClick { get; set; }
    
    private Texture2D _notPaySprite;
    private Texture2D _selectedSprite;
    private Texture2D _chooseSprite;
    
    public PayBtn(ContentManager content, Vector2 position) : base(content)
    {
        _sprite = content.Load<Texture2D>("pay_btn");
        _currentSprite = _sprite;
        _position = position;
        _notPaySprite = content.Load<Texture2D>("not_pay_btn");
        _selectedSprite = content.Load<Texture2D>("selected_btn");
        _chooseSprite = content.Load<Texture2D>("choose_btn");
        _spriteHover = content.Load<Texture2D>("pay_btn_hover");
        _btnRectangle = new Rectangle((int)_position.X, (int)_position.Y, _currentSprite.Width, _currentSprite.Height);
    }

    public void GetStateBuy(StateBuy state, Card card)
    {
        switch (state)
        {
            case StateBuy.Buy:
                _currentSprite = _sprite;
                card.State = StateBuy.Buy;
                break;
            case StateBuy.NotBuy:
                _currentSprite = _notPaySprite;
                card.State = StateBuy.NotBuy;
                break;
            case StateBuy.Choose:
                _currentSprite = _chooseSprite;
                card.State = StateBuy.Choose;
                break;
            case StateBuy.Selected:
                _currentSprite = _selectedSprite;
                card.State = StateBuy.Selected;
                break;
        }
    }

    public void ExecuteOnClick(int coins, int price, StateBuy state, Card card)
    {
        if (EnterButton() && ((state == StateBuy.Buy && coins >= price)
                              || (state == StateBuy.Choose)))
        {
            card.PickSong.Play();
            IsClick = true;
        }
    }
}