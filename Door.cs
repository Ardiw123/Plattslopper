using SFML.Graphics;
using SFML.System;
namespace Plattslopper;

public class Door : RoomObject
{
    public bool isUnlocked;

    public Door()
    {
        collisionBox = new(new(100, 100), size);
    }

    public override void Update(float deltaTime)
    {
        collisionBox.position = position;
        collisionBox.size = size;
        System.Console.WriteLine(isUnlocked);
    }

    public override void Draw(RenderWindow window)
    {
        spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window);

        collisionBox.DrawHitbox(window);
    }

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
    }

    
}
