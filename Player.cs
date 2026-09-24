using System.Diagnostics;
using System.Security.Cryptography;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Plattslopper;

public class Player : RoomObject
{
    public Stack<Key> keys = new();

    public Vector2f direction = new(0, 0);

    public Vector2f velocity = new(0, 0);
    bool isGrounded = false;
    bool isJumping = false;
    const int MAXJUMPSTEPS = 7;
    int jumpSteps = 0;
    int playerMoveSpeed = 800;
    int playerJumpPower = 250;
    const float playerMoveDecayConstant = 0.6f;
    int playerGravity = 100;

    public Player()
    {
        size = new Vector2f(100, 100);
        collisionBox = new(new(100, 100), size);
    }

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
    }

    public override void Update(float deltaTime)
    {
        Inputs(deltaTime);

        position += velocity;
        collisionBox.position = position;
        collisionBox.size = size;

        int numberOfCollitions = 0;
        Vector2f positionChange = new Vector2f(0, 0);
        bool isGroundedNow = false;

        foreach (RoomObject roomObject in Game.currentRoom.RoomObjects)
        {
            if (roomObject is Player) continue;

            //dåligt kollisions system

            // if (roomObject is Key)
            // {
            //     keys.Append(roomObject as Key);
            //     // roomObject.Remove();
            // }

            // if (roomObject is Door)
            // {
            //     var d = roomObject as Door;
            //     if (!d.isUnlocked)
            //     {
            //         if (keys.Count <= 1)
            //         {
            //             keys.Pop();
            //             d.isUnlocked = true;
            //         }

            //     }
            // }

            Collision.Hit hit;
            Vector2f collitionNormal = collisionBox.Collide(roomObject, out hit);

            if (collitionNormal.X == 1 && collitionNormal.Y == 1)
                continue;

            numberOfCollitions++;

            if (collitionNormal.Y == 0)
            {
                isGroundedNow = true;
                cancelJump();
            }

            velocity.X *= collitionNormal.X;
            velocity.Y *= collitionNormal.Y;
            positionChange += hit.Normal * hit.Overlap;
        }

        isGrounded = isGroundedNow;

        if (numberOfCollitions > 0)
            position += positionChange / numberOfCollitions;
    }

    public override void Draw(RenderWindow window)
    {
        spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window);

        collisionBox.DrawHitbox(window);
    }

    void Inputs(float deltatime)
    {
        velocity = new Vector2f(DecayVelocity(velocity, deltatime), velocity.Y);

        bool isVelocityChanged = false;
        float velocityChangeX = 0;
        if (KeyboardHandler.IsKeyDown(Keyboard.Key.A))
        {
            isVelocityChanged = true;
            velocityChangeX += -playerMoveSpeed * deltatime;
        }
        if (KeyboardHandler.IsKeyDown(Keyboard.Key.D))
        {
            isVelocityChanged = true;
            velocityChangeX += playerMoveSpeed * deltatime;
        }

        if (isVelocityChanged)
            velocity = new Vector2f(velocityChangeX, velocity.Y);

        Gravity(deltatime);
        Jump(deltatime);
    }

    float DecayVelocity(Vector2f vector, float deltatime)
    {
        if (MathF.Abs(vector.X) > 0.1)
        {
            return vector.X *= playerMoveDecayConstant;
        }
        return 0;
    }

    void Gravity(float deltatime)
    {
        // Temporary floor will be repaced when ther is an actual floor to stand on.
        if (position.Y >= 700)
        {
            isGrounded = true;
            position.Y = 700;
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
        }

        jumpSteps++;

        if (KeyboardHandler.IsKeyDown(Keyboard.Key.Space) && jumpSteps <= MAXJUMPSTEPS && isJumping)
        {
            velocity += new Vector2f(0, -playerJumpPower * deltatime);
        }
        else
        {
            cancelJump();
        }
    }

    public void cancelJump()
    {
        isJumping = false;
        jumpSteps = 0;
    }
}
