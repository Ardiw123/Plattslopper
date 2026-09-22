using SFML.Graphics;
using SFML.System;

namespace Plattslopper;

public abstract class RoomObject
{
	protected SpriteDrawer spriteDrawer;

	protected CollisionBox hurtBox;
	private Vector2f position;
	private Vector2f size;
	public FloatRect hitBox => new FloatRect(position, size);
	public bool remove = false;

	abstract public void Update(float deltaTime);

	abstract public void Draw(RenderWindow window);

	public RoomObject()
	{
		Game.currentRoom.RoomObjects.Add(this);
		spriteDrawer = new();
	}
	//abstract public void RoomStart();
}
