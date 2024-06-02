using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GooseInCap;

public class Card
{
    public Texture2D Background { get; private set; }
    public PayBtn Button { get; private set; }

    public Card(ContentManager content)
    {
        Background = content.Load<Texture2D>("goose_base");
        Button = new PayBtn(content);
    }
}