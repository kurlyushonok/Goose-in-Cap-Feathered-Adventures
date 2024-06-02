using System.Numerics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GooseInCap;

public class Shop
{
    public Card BaseCard { get; set; }
    public Card FrogCard { get; set; }
    public Card FlowerCard { get; set; }

    public Shop(ContentManager content)
    {
        BaseCard = new Card(content, "goose_base", new Vector2(120, 200));
    }
}