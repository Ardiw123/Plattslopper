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

    public void ChangeRoom()
    {
        Game.currentRoom = Room.MakeRoomFromRoomData(RoomData.LoadFromFile("levels/" + nextRoomName));
	}
}
