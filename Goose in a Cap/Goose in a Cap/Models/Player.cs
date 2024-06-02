using Microsoft.Xna.Framework.Content;

namespace GooseInCap;

public class Player
{
    public Goose Character { get; set; }
    public int CountCoins { get; set; }
    public int Record { get; set; }

    public Player(ContentManager content)
    {
        Character = new GooseFlower(content);
    }
}
