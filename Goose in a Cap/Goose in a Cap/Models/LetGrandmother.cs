using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GooseInCap;

public class LetGrandmother
{
    private Vector2 _position = new Vector2(1920, 600);
    private Texture2D _sprite;
    private int _currentPosition = 1920;

    public LetGrandmother(ContentManager content)
    {
        _sprite = content.Load<Texture2D>("grandmother");
    }

    public int CurrentPosition
    {
        get => _currentPosition;
        set => _currentPosition = value;
    }

    public Texture2D Sprite => _sprite;
}