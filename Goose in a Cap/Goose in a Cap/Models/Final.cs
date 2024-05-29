using Microsoft.Xna.Framework.Content;

namespace GooseInCap;

public class Final
{
    private ReplayBtn _replayBtn;
    private BackBtn _backBtn;

    public Final(ContentManager content)
    {
        _replayBtn = new ReplayBtn(content);
        _backBtn = new BackBtn(content);
    }

    public Button ReplayButton => _replayBtn;
    public BackBtn BackButton => _backBtn;
}