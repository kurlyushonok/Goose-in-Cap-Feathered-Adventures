using Microsoft.Xna.Framework.Content;

namespace GooseInCap;

public class Pause
{
    private PauseBtn _pauseBtn;

    public Pause(ContentManager content)
    {
        _pauseBtn = new PauseBtn(content);
    }

    public PauseBtn PauseButton => _pauseBtn;
}