using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Plattslopper;

public class Player : RoomObject
{
    string spriteName = "adolf";
    public Vector2f velocity = new(0, 0);
    bool isGrounded = false;
    bool isJumping = false;
    const int MAXJUMPSTEPS = 7;
    int jumpSteps = 0;

    int playerMoveSpeed = 800;
    int playerJumpPower = 850;
    const float playerMoveDecayConstant = 0.6f;
    int playerGravity = 100;

    public Player()
    {
        size = new Vector2f(100, 100);
        collisionBox = new(new(100, 100), size);

        Console.WriteLine(collisionBox.center);
        spriteDrawer.InitializeSprites([spriteName]);
    }

    public override void Update(float deltaTime)
    {
        Inputs(deltaTime);

        //Console.WriteLine($"{velocity}: {position}");
        //Console.WriteLine(jumpSteps);

		position += velocity;
        collisionBox.position = position;
        collisionBox.size = size;

		foreach (RoomObject roomObject in Game.currentRoom.RoomObjects)
        {
            if (roomObject is Player)
                continue;

            Collision.Hit hit;
            Vector2f collitionNormal = collisionBox.Collide(roomObject, out hit);
            if (collitionNormal.X == 1 && collitionNormal.Y == 1)
            {
                isGrounded = false;
            }

            if (collitionNormal.Y > 0)
            {
                cancelJump();
                isGrounded = true;
            }

			velocity.X *= collitionNormal.X;
            velocity.Y *= collitionNormal.Y;
            position += hit.Normal * hit.Overlap;
		}
	}

    public override void Draw(RenderWindow window)
    {
        spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window);

        collisionBox.DrawHitbox(window);
    }

    void Inputs(float deltatime)
    {
        velocity = new Vector2f(DecayVelocity(velocity, deltatime), velocity.Y);

        if (KeyboardHandler.IsKeyDown(Keyboard.Key.A))
        {
            velocity = new Vector2f(-playerMoveSpeed * deltatime, velocity.Y);
        }
        if (KeyboardHandler.IsKeyDown(Keyboard.Key.D))
        {
            velocity = new Vector2f(playerMoveSpeed * deltatime, velocity.Y);
        }

        Gravity(deltatime);
        Jump(deltatime);
    }

    float DecayVelocity(Vector2f vector, float deltatime)
    {
        if (vector.X > 0)
        {
            if (vector.X < 0.1)
                return 0;

            return vector.X *= playerMoveDecayConstant;
        }

        if (vector.X < 0)
        {
            if (vector.X > -0.1)
                return 0;

            return vector.X *= playerMoveDecayConstant;
        }

        return 0;
    }

    void Gravity(float deltatime)
    {
        // Temporary floor will be repaced when ther is an actual floor to stand on.
        if (position.Y >= 500)
        {
            isGrounded = true;
            position.Y = 500;
            velocity.Y = 0;
            cancelJump();
            return;
        }

        velocity.Y += playerGravity * deltatime;
    }

    void Jump(float deltatime)
    {
        if (KeyboardHandler.WasKeyJustPressed(Keyboard.Key.Space) && !isJumping && isGrounded)
        {
            velocity += new Vector2f(0, -playerJumpPower * deltatime);
            isGrounded = false;
            isJumping = true;
            jumpSteps++;
        }

        jumpSteps++;

        if (KeyboardHandler.IsKeyDown(Keyboard.Key.Space) && !(jumpSteps == MAXJUMPSTEPS) && isJumping)
        {
            velocity += new Vector2f(0, -playerJumpPower * deltatime);
        }
        else
        {
            isJumping = false;
        }
		Console.WriteLine(jumpSteps);
    }

    public void cancelJump()
    {
        isJumping = false;
        jumpSteps = 0;
    }
}
