using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Goose_in_a_Cap;

public class Drawer
{
    private Texture2D _background;
    public int _currentTime;
    private int _currentPosition;
    private int _backgroundSpeed = 10;

    public Texture2D Background
    {
        get => _background; 
        set => _background = value; 
    }
    
    public void Draw(SpriteBatch spriteBatch, Controller controller)
    {
        spriteBatch.Begin();
        
        // spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        DrawBackground(spriteBatch, controller);
        DrawRun(spriteBatch, controller);
        
        spriteBatch.End();
    }
    
    private void DrawRun(SpriteBatch spriteBatch, Controller controller)
    {
        spriteBatch.Draw(controller.CurrentCharacterSprite, controller.Character.Position, 
            new Rectangle(controller.CurrentFrame.X * controller.CurrentCharacterFrameSize.X, 
                controller.CurrentFrame.Y * controller.CurrentCharacterFrameSize.Y, 
                controller.CurrentCharacterFrameSize.X,  controller.CurrentCharacterFrameSize.Y), Color.White);
    }

    private void DrawBackground(SpriteBatch spriteBatch, Controller controller)
    {
        spriteBatch.Draw(_background, Vector2.Zero,
            new Rectangle(_currentPosition, 0, 1920, 1080),
            Color.White);
        _currentPosition += _backgroundSpeed;
        if (_currentPosition >= 1920) _currentPosition = 0;
    }
}