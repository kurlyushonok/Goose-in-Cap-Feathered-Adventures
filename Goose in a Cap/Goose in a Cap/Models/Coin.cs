using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GooseInCap;

public class Coin
{
    private int _level = 580;
    private int _currentPosition = 2100;
    private Texture2D _sprite;

    public Coin(ContentManager content)
    {
        _sprite = content.Load<Texture2D>("coin");
    }

    public int CurrentPosition
    {
        get => _currentPosition;
        set => _currentPosition = value;
    }

    public int Level => _level;

    public Texture2D Sprite => _sprite;
}