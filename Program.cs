using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace Plattslopper;

class Program
{
	static void Main(string[] args)
	{
		using (var window = new RenderWindow(new VideoMode(new Vector2u(800, 600)), "Plattsopper"))
		{
			HitBox box = new HitBox(new Vector2f(100, 100), new Vector2f(120, 300));

			Console.WriteLine(box.hitBoxRect.Center); 

			window.Closed += (o, e) => window.Close();
			
			Clock clock = new Clock();
			while (window.IsOpen)
			{
				window.DispatchEvents();
				float deltaTime = clock.Restart().AsSeconds();
				
				window.Clear();

				box.DebugDraw(window);

				window.Display();
			}
		}
	}
}