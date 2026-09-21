using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace platformer;

class Program
{
	static void Main(string[] args)
	{
		using (var window = new RenderWindow(new VideoMode(new Vector2u(800, 600)), "Plattsopper"))
		{
			window.Closed += (o, e) => window.Close();
			// TODO: Initialize
			Clock clock = new Clock();
			while (window.IsOpen)
			{
				window.DispatchEvents();
				float deltaTime = clock.Restart().AsSeconds();
				// TODO: Updates
				window.Clear();
				// TODO: Drawing
				window.Display();
			}
		}
	}
}