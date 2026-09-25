using SFML.Graphics;

namespace Plattslopper;

public class Key : RoomObject
{
    public string keyId;

    public Key()
    {
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

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
    }

    public void SendKeyId()
    {
        var currentDoors = Game.currentRoom.RoomObjects.Where(c => c is Door).ToList();
        foreach (Door door in currentDoors)
        {
            if (keyId == door.doorId)
                door.isUnlocked = true;
        }
    }
}
