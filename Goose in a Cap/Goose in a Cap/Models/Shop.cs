using System.Numerics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GooseInCap;

public class Shop
{
    private int _distance = 540;
    private int _distanceBtn = 550;
    private int _startPositionCard = 150;
    private int _startPositionButton = 320;
    private int _cardPositionY = 280;
    private int _btnPositionY = 830;
    private BackBtnInShop _button;
    private Texture2D _coin;
    private Vector2 _coinPosition = new Vector2(120, 100);
    private Vector2 _coinTextPosition = new Vector2(180, 100);
    
    public Card BaseCard { get; private set; }
    public Card FrogCard { get; private set; }
    public Card FlowerCard { get; private set; }
    public BackBtn Button => _button;
    public Texture2D Coin => _coin;
    public Vector2 CoinPosition => _coinPosition;
    public Vector2 CoinTextPosition => _coinTextPosition;

    public Shop(ContentManager content)
    {
        _coin = content.Load<Texture2D>("coin_for_shop");
        _button = new BackBtnInShop(content);
        BaseCard = new Card(content, "goose_base", 
            new Vector2(_startPositionCard, _cardPositionY), 
            new Vector2(_startPositionButton, _btnPositionY), 0,
            new Goose(content));
        BaseCard.IsPay = true;
        BaseCard.IsSelected = true;
        FrogCard = new Card(content, "goose_frog", 
            new Vector2(_startPositionCard + _distance, _cardPositionY), 
            new Vector2(_startPositionButton + _distanceBtn, _btnPositionY), 15,
            new GooseFrog(content));
        FlowerCard = new Card(content, "goose_flower", 
            new Vector2(_startPositionCard + 2 * _distance, _cardPositionY), 
            new Vector2(_startPositionButton + 2 * _distanceBtn, _btnPositionY), 20,
            new GooseFlower(content));
    }
}