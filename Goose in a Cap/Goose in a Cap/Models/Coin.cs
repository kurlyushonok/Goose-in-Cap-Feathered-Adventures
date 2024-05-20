using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GooseInCap;

public class Coin
{
    private int _level = 560;
    private int _currentPosition = 1920;
    private Texture2D _sprite;

    public Coin(ContentManager content)
    {
        _sprite = content.Load<Texture2D>("coin");
    }
}