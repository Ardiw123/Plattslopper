using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace Plattslopper;

// this class should handle room switching
public static class Game
{

    public static Room currentRoom;
    public static void StartGame()
    {
        using (var window = new RenderWindow(new VideoMode(800, 600), "Plattsopper"))
        {
            window.Closed += (o, e) => window.Close();

            Clock clock = new Clock();

            currentRoom = new();

            Player p = new();

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