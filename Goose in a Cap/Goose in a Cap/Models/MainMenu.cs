using Microsoft.Xna.Framework.Content;

namespace GooseInCap;

public class MainMenu
{
    private Button _playBtn;
    private StoreBtn _storeBtn;
    private ClueBtn _clueBtn;

    public MainMenu(ContentManager content)
    {
        _playBtn = new Button(content);
        _storeBtn = new StoreBtn(content);
        _clueBtn = new ClueBtn(content);
    }

    public Button PlayButton => _playBtn;
    public StoreBtn StoreButton => _storeBtn;
    public ClueBtn ClueButton => _clueBtn;
}