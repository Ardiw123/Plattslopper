using SFML.Graphics;

namespace Plattslopper;

public class Room
{
    public List<RoomObject> RoomObjects = new();
    SpriteDrawer backGroundDrawer = new();

    public Room()
    {
        backGroundDrawer.InitializeSprites();
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
        backGroundDrawer.DrawSprite(backGroundDrawer.GetSprite("sprite"), window);
         for (int i = 0; i < RoomObjects.Count; i++)
        {
           RoomObjects[i].Draw(window); // sen ritar man ut allt till skärmen
        }
    }
}
