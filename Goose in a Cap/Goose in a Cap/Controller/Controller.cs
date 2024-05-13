using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Goose_in_a_Cap;

public class Controller
{
    private bool _flag;
    private Race _race = new Race();
    private Drawer _drawer;
    private ContentLoad _loader;

    public Controller(Drawer drawer, ContentLoad loader)
    {
        _drawer = drawer;
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
            "jump_base", _loader.Sprites, _race.Position); //эта строчка заменится на отслеживание интерфейса
        //TODO сделать дефолтную инициализацию гуся
        
        _drawer.DrawCharacter(_race.Character);
    }
    
    private Goose ChooseGoose(string nameRunSprite, string nameJumpSprite, 
        string nameWaitSprite, Dictionary<string, (Texture2D, Point)> sprites, Vector2 position)
    {
        return new Goose(nameRunSprite, nameJumpSprite, nameWaitSprite, sprites, position);
    }

    private void Jump()
    {
        if ((Keyboard.GetState().IsKeyDown(Keys.Up) || 
             Keyboard.GetState().IsKeyDown(Keys.Space) ||
             Keyboard.GetState().IsKeyDown(Keys.W)) && _drawer.CanJump)
        {
            _flag = true;
            _drawer.CanJump = false;
        }
    
        if (_flag)
        {
            if (_race.position.Y > 270) _race.position.Y -= 20; //TODO вынести константы в Race
            else _flag = false;
        }
    
        if (!_flag)
        {
            if (_race.position.Y < 510) _race.position.Y += 20;
            else
            {
                _drawer.CanLand = true;
                _drawer.CanJump = true;
            }
        }
    }

    private void Run()
    {
        //бежать, когда началась игра
        _drawer.CanRun = true;
    }
}