using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
namespace Plattslopper;

public class Block : RoomObject
{
    string spriteName = "mcnutt";

    public Block()
    {
        size = new(200, 200);
        spriteDrawer.InitializeSprites([spriteName]);
        collisionBox = new(new(100, 100), size);
    }

    public override void Update(float deltaTime)
    {
        collisionBox.position = position;
        collisionBox.size = size;
    }

    public override void Draw(RenderWindow window)
    {
        spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window);
        collisionBox.DrawHitbox(window);
    }
}
