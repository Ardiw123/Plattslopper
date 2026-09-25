using SFML.Graphics;

namespace Plattslopper;

public class Coin : RoomObject
{

    public Coin()
    {
        collisionBox = new(new(100, 100), size);
    }

    int slop = 0;

    public void Animate(RenderWindow window)
    {
        slop++;
        if (slop < 30)
        {
            spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window, new IntRect(198, 162 - 36, 18, 18));
        }
        else if (slop > 30)
        {
            spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window, new IntRect(198 + 18, 162 - 36, 18, 18));
        }

        if (slop > 60)
        {
            slop = 0;
        }

    }

    public override void Update(float deltaTime)
    {
        collisionBox.position = position;
        collisionBox.size = size;
    }

    public override void Draw(RenderWindow window)
    {
        Animate(window);
        collisionBox.DrawHitbox(window);
    }

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
    }


}
