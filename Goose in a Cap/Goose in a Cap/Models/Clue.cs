using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GooseInCap;

public class Clue
{
    public Texture2D Text { get; private set; }
    public BackBtnInClue Button { get; private set; }
    public Vector2 Position { get; private set; }

    public Clue(ContentManager content)
    {
        Text = content.Load<Texture2D>("clue_text");
        Button = new BackBtnInClue(content);
        Position = new Vector2(960 - Text.Width / 2, 512 - Text.Height / 2);
    }
}