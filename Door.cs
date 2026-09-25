using SFML.Graphics;
using SFML.System;
namespace Plattslopper;

public class Door : RoomObject
{
    public bool isUnlocked;
    public string doorId;
    public string nextRoomName;

    public Door()
    {
        collisionBox = new(new(100, 100), size);
    }

    public override void Update(float deltaTime)
    {
        collisionBox.position = position;
        collisionBox.size = size;
		//System.Console.WriteLine(isUnlocked);
    }

    public override void Draw(RenderWindow window)
    {
        if (isUnlocked)
            spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window, new IntRect(203, 103, 1, 1));
        else
            spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window, new IntRect(180, 103, 18, 23));

        collisionBox.DrawHitbox(window);
    }

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
    }

    public void ChangeRoom()
    {
        Game.currentRoom = Room.MakeRoomFromRoomData(RoomData.LoadFromFile("levels/" + nextRoomName));
	}
}
