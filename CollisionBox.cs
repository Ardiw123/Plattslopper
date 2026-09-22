using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plattslopper;

public class CollisionBox
{
	private Vector2f position;
	private Vector2f size;
	public FloatRect collisionBoxRect => new FloatRect(position, size);

	public CollisionBox(Vector2f position, Vector2f size)
	{
		this.position = position;
		this.size = size;
	}

	public bool Collide(FloatRect otherRect, out Collision.Hit hit)
	{
		return Collision.RectangleRectangle(collisionBoxRect, otherRect, out hit);
	}

	public void DebugDraw(RenderWindow window)
	{
		RectangleShape shape = new RectangleShape()
		{
			Size = size,
			Position = position,
		};

		CircleShape point = new CircleShape(2f)
		{
			Position = collisionBoxRect.Center,
			FillColor = Color.Red
		};

		window.Draw(shape);
		window.Draw(point);
	}
}
