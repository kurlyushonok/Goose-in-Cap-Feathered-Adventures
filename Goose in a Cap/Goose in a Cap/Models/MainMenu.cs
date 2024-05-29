using Microsoft.Xna.Framework.Content;

namespace GooseInCap;

public class MainMenu
{
    private Button _playBtn;
    private StoreBtn _storeBtn;

    public MainMenu(ContentManager content)
    {
        _playBtn = new Button(content);
        _storeBtn = new StoreBtn(content);
    }

    public Button PlayButton => _playBtn;
    public StoreBtn StoreButton => _storeBtn;
}