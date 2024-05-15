using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GooseInCap;

public class GameController
{
    private bool _flag;
    private Race _race = new Race();
    private GameDrawer _gameDrawer;
    private ContentLoad _loader;

    public GameController(GameDrawer gameDrawer, ContentLoad loader)
    {
        _gameDrawer = gameDrawer;
        _loader = loader;
    }

    public Race Race
    {
        get => _race;
    }

    public void Update()
    {
        Run();
        Jump();
    }

    public void InitializeCharacter()
    {
        _race.Character = ChooseGoose("run_base", "jump_base",
            "jump_base", _loader.Sprites); //эта строчка заменится на отслеживание интерфейса
        //TODO сделать дефолтную инициализацию гуся
        
        _gameDrawer.DrawCharacter(_race.Character);
    }
    
    private Goose ChooseGoose(string nameRunSprite, string nameJumpSprite, 
        string nameWaitSprite, Dictionary<string, (Texture2D, Point)> sprites)
    {
        return new Goose(nameRunSprite, nameJumpSprite, nameWaitSprite, sprites);
    }

    private void Jump()
    {
        if ((Keyboard.GetState().IsKeyDown(Keys.Up) || 
             Keyboard.GetState().IsKeyDown(Keys.Space) ||
             Keyboard.GetState().IsKeyDown(Keys.W)) && _gameDrawer.CanJump)
        {
            _flag = true;
            _gameDrawer.CanJump = false;
        }
    
        if (_flag)
        {
            if (_race.Position.Y > _race.FlightLevel) _race.Position.Y -= _race.JumpSpeed;
            else _flag = false;
        }
    
        if (!_flag)
        {
            if (_race.Position.Y < _race.RunningLevel) _race.Position.Y += _race.JumpSpeed;
            else
            {
                _gameDrawer.CanLand = true;
                _gameDrawer.CanJump = true;
            }
        }
    }

    private void Run()
    {
        //бежать, когда началась игра
        _gameDrawer.CanRun = true;
    }
}