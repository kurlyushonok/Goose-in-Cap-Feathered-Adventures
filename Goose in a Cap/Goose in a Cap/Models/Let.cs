using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GooseInCap;

public class Let
{
    protected int _level = 590;
    protected Texture2D _sprite;
    private int _currentPosition = 1920;
    private int _height;

    public Let(ContentManager content)
    {
        _sprite = content.Load<Texture2D>("grandmother");
        _height = _level - _sprite.Height;
    }

    public int CurrentPosition
    {
        get => _currentPosition;
        set => _currentPosition = value;
    }

    public Texture2D Sprite => _sprite;
    
    public int Level => _level;

    public int Height => _height;
    public int Width => _sprite.Width;
}

public class LetButchFlower : Let
{
    public LetButchFlower(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("buch_flower");
        _level = 560;
    }
}

public class LetBuches : Let
{
    public LetBuches(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("buches");
        _level = 530;
    }
}

public class LetBurdock : Let
{
    public LetBurdock(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("burdock");
        _level = 540;
    }
}

public class LetCabbage : Let
{
    public LetCabbage(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("cabbage");
        _level = 600;
    }
}

public class LetCarrot : Let
{
    public LetCarrot(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("carrot");
        _level = 590;
    }
}

public class LetFence : Let
{
    public LetFence(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("fence");
        _level = 595;
    }
}

public class LetFlowersBig : Let
{
    public LetFlowersBig(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("flowers_big");
        _level = 560;
    }
}

public class LetFlowersSmall : Let
{
    public LetFlowersSmall(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("flowers_small");
        _level = 580;
    }
}

public class LetNechaev : Let
{
    public LetNechaev(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("p3nechaev");
        _level = 570;
    }
}

public class LetStone : Let
{
    public LetStone(ContentManager content) : base(content)
    {
        _sprite = content.Load<Texture2D>("stone");
        _level = 620;
    }
}