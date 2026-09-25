using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;


namespace Plattslopper;

// this class should handle room switching
public static class Game
{
    public static Room currentRoom;
    public static Vector2u WindowSize = new(1920, 1080);

    static void LoadRoom()
    {
        currentRoom = Room.MakeRoomFromRoomData(RoomData.LoadFromFile("levels/level0.json"));
        currentRoom.roomData.Print();
        currentRoom.StartRoom();
    }


    internal static void StartGame()
    {
        using (var window = new RenderWindow(new VideoMode(WindowSize.X, WindowSize.Y), "Plattslopper"))
        {
            window.SetFramerateLimit(600);
            window.Closed += (o, e) => window.Close();

            SpriteDrawer.InitilizeAllSprites();
            Room.InitilizeEpicSongs();

            LoadRoom();

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