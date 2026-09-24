using System.Security.Cryptography.X509Certificates;
using SFML.Graphics;
using SFML.System;

namespace Plattslopper;

public class Room
{
    public static Room MakeRoomFromRoomData(RoomData roomData)
    {   
        roomData.print();
        Room newRoom = new()
        {
            RoomObjects = roomData.roomObjects,
            spriteName = roomData.backgroundName,
        };
        newRoom.roomData = roomData;
        return newRoom;
    }
    public List<RoomObject> RoomObjects;
    string spriteName;
    SpriteDrawer backGroundDrawer;
    public RoomData roomData;
    Room()
    {
         backGroundDrawer = new();
    }

    public void Update(float deltaTime)
    {

        for (int i = 0; i < RoomObjects.Count; i++)
        {
            //först uppdatera alla värden
            RoomObjects[i].Update(deltaTime);
        }
        // tar bort alla objekt efter man har itererat så inte listan förstörs
        RoomObjects.RemoveAll(obj => obj.remove == true);
    }

    public void Draw(RenderWindow window)
    {
        backGroundDrawer.DrawSprite(new(0, 0), (Vector2f)Game.WindowSize, backGroundDrawer.GetSprite(spriteName), window);
        for (int i = 0; i < RoomObjects.Count; i++)
        {
            RoomObjects[i].Draw(window); // sen ritar man ut allt till skärmen
        }
    }

}
