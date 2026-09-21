using SFML.Graphics;
using SFML.System;

namespace Plattslopper;

class RoomObject
{
	public readonly Sprite sprite;

	private Vector2f position;
	private Vector2f size;
	public FloatRect hitBox => new FloatRect(position, size);
}
