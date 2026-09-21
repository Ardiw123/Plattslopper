using SFML.Graphics;
using SFML.System;

namespace Plattslopper;

abstract class RoomObject
{
	public readonly Sprite sprite;
	SpriteDrawer spriteDrawer;

	private Vector2f position;
	private Vector2f size;
	public FloatRect hitBox => new FloatRect(position, size);

	abstract public void Update(float deltaTime);

	abstract public void Draw(RenderWindow window);
}
