using SFML.Graphics;
using SFML.System;

namespace Plattslopper;

public class Player : RoomObject
{
    public Player()
    {
        hurtBox = new CollisionBox(new Vector2f(100, 100), new Vector2f(120, 300));
        Console.WriteLine(hurtBox.collisionBoxRect.Center);
        spriteDrawer.InitializeSprites();
    }

    public override void Update(float deltaTime)
    {

    }

    public override void Draw(RenderWindow window)
    {
        spriteDrawer.DrawSprite(spriteDrawer.GetSprite("sprite"), window);
    }
}
