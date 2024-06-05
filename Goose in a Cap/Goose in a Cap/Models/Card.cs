using System.Numerics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GooseInCap;

public class Card
{
    private Vector2 _cardPosition;
    private Vector2 _buttonPosition;
    public Texture2D Background { get; private set; }
    public PayBtn Button { get; private set; }
    public Vector2 CardPosition => _cardPosition;
    public Vector2 ButtonPosition => _buttonPosition;
    public int Price { get; private set; }
    public bool IsPay { get; set; }
    public bool IsSelected { get; set; }
    public StateBuy State { get; set; }
    public Goose Character { get; private set; }
    public Song PickSong { get; private set; }

    public Card(ContentManager content, string spriteName, 
        Vector2 cardPosition, Vector2 btnPosition, int price, Goose character)
    {
        Background = content.Load<Texture2D>(spriteName);
        Button = new PayBtn(content, btnPosition);
        _buttonPosition = btnPosition;
        _cardPosition = cardPosition;
        Price = price;
        Character = character;
        PickSong = content.Load<Song>("hat");
    }
}