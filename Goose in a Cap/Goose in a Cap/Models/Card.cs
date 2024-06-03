using System.Numerics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

    public Card(ContentManager content, string spriteName, 
        Vector2 cardPosition, Vector2 btnPosition, int price)
    {
        Background = content.Load<Texture2D>(spriteName);
        Button = new PayBtn(content, btnPosition);
        _buttonPosition = btnPosition;
        _cardPosition = cardPosition;
        Price = price;
    }
}