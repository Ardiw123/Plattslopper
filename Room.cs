using SFML.Graphics;
using SFML.System;

namespace Plattslopper;

public class Room
{
    public List<RoomObject> RoomObjects = new();
    string spriteName = "flower";
    public RoomData roomData;
    SpriteDrawer backGroundDrawer = new();

    public Room()
    {
        backGroundDrawer.InitializeSprites([spriteName]);
        roomData = RoomData.LoadFromFile("levels/level0.json");
        roomData.print();
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
