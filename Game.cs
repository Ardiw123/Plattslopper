using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System.Security.Cryptography.X509Certificates;

namespace Plattslopper;

// this class should handle room switching
public static class Game
{
    public static Room currentRoom;
    public static Vector2u WindowSize = new(1920, 1080);

    // dags att plugga det här: 

    /*
        public: Access isn't restricted.
        protected: Access is limited to the containing class or types derived from the containing class.
        internal: Access is limited to the current assembly.
        protected internal: Access is limited to the current assembly or types derived from the containing class.
        private: Access is limited to the containing type.
        private protected: Access is limited to the containing class or types derived from the containing class within the current assembly.
    */

    internal static void StartGame()
    {
        using (var window = new RenderWindow(new VideoMode(WindowSize.X, WindowSize.Y), "Plattslopper"))
        {
            window.SetFramerateLimit(60);
            window.Closed += (o, e) => window.Close();

            SpriteDrawer.InitilizeAllSprites();

            currentRoom = Room.MakeRoomFromRoomData(RoomData.LoadFromFile("levels/level0.json"));
          /*  {
                Player p = new()
                {
                    position = new(1000, 500),
                };
                Block b = new()
                {
                    position = new(100, 500),
                };
                Block c = new()
                {
                    position = new(300, 500),
                };
                Block d = new()
                {
                    position = new(500, 500),
                };
                Block e = new()
                {
                    position = new(700, 500),
                };
                Block f = new()
                {
                    position = new(900, 500),
                };

                currentRoom.RoomObjects.AddRange(p, b, c, e, f);
            }*/

            currentRoom.roomData.print();
            currentRoom.RoomObjects.AddRange(currentRoom.roomData.roomObjects);

            Clock clock = new Clock();
            //mainloop
            while (window.IsOpen)
            {
                window.DispatchEvents();
                float deltaTime = clock.Restart().AsSeconds();

                window.Clear();

                currentRoom.Update(deltaTime);
                currentRoom.Draw(window);

                window.Display();
            }
        }
    }
}