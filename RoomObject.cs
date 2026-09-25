using SFML.Graphics;
using SFML.System;
using System.Text.Json;
using System.Text.Json.Serialization;
using SFML.Audio;

namespace Plattslopper;

[JsonDerivedType(typeof(Door), typeDiscriminator: "door")]
[JsonDerivedType(typeof(Block), typeDiscriminator: "block")]
[JsonDerivedType(typeof(Player), typeDiscriminator: "player")]
[JsonDerivedType(typeof(Key), typeDiscriminator: "key")] 
[JsonDerivedType(typeof(Coin), typeDiscriminator: "coin")] 
public abstract class RoomObject
{

	public Vector2f position;
	public Vector2f size;
	public string spriteName;
	protected SpriteDrawer spriteDrawer;
	public CollisionBox collisionBox;
	public bool remove = false;

	abstract public void Update(float deltaTime);

	abstract public void Draw(RenderWindow window);

	public RoomObject()
	{
		//Game.currentRoom.RoomObjects.Add(this);
		spriteDrawer = new();
	}
	abstract public void RoomStart();

}
