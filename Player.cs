using SFML.Graphics;
using SFML.System;

namespace Plattslopper;

public class Player : RoomObject
{
    string spriteName = "adolf";
    public Player()
    {
        size = new Vector2f(100, 100);
        hurtBox = new(new(100, 100), new(120, 300));
        Console.WriteLine(hurtBox.center);
        spriteDrawer.InitializeSprites([spriteName]);
    }

    public override void Update(float deltaTime)
    {

    }

    public override void Draw(RenderWindow window)
    {
        spriteDrawer.DrawSprite(spriteDrawer.GetSprite(spriteName), window);
    }
}
