using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Goose_in_a_Cap;

public class Race
{
    public Vector2 position = new Vector2(150, 520);

    public Vector2 Position
    {
        get => position;
        set => position = value;
    }
}