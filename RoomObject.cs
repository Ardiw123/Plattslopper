using SFML.Graphics;
using SFML.System;

namespace Plattslopper;

public abstract class RoomObject
{
	protected SpriteDrawer spriteDrawer;

	protected CollisionBox hurtBox;
	public Vector2f position;
	public Vector2f size;
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
