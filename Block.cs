using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
namespace Plattslopper;

public class Block : RoomObject
{
    public Block()
    {
        size = new(200, 200);

        collisionBox = new(new(100, 100), size);
    }

    public void Animate()
    {
        
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

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
    }
}
