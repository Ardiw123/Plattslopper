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

    public Player()
    {
        size = new Vector2f(100, 100);
        hurtBox = new(new(100, 100), size);

        Console.WriteLine(hurtBox.center);
        spriteDrawer.InitializeSprites([spriteName]);
    }

    public override void Update(float deltaTime)
    {
        Inputs(deltaTime);

        Console.WriteLine(velocity);

        position += velocity;
        hurtBox = new CollisionBox(position, hurtBox.size);
    }

    public override void Draw(RenderWindow window)
    {
        spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window);

        hurtBox.DrawHitbox(window);
    }

    private void Inputs(float deltatime)
    {
        velocity = new Vector2f(DecayVelocity(velocity, deltatime), velocity.Y);

        if (KeyboardHandler.IsKeyDown(Keyboard.Key.A))
        {
            velocity = new Vector2f(-200 * deltatime, velocity.Y);
        }
        if (KeyboardHandler.IsKeyDown(Keyboard.Key.D))
        {
            velocity = new Vector2f(200 * deltatime, velocity.Y);
        }

        Gravity(deltatime);
        Jump(deltatime);
    }

    private float DecayVelocity(Vector2f vector, float deltatime)
    {
        if (vector.X > 0)
        {
            if (vector.X < 0.1)
                return 0;

            return vector.X *= 0.7f;
        }

        if (vector.X < 0)
        {
            if (vector.X > -0.1)
                return 0;

            return vector.X *= 0.7f;
        }

        return 0;
    }

    private void Gravity(float deltatime)
    {
        // Temporary floor will be repaced when ther is an actual floor to stand on.
        if (position.Y >= 500)
        {
            isGrounded = true;
            position.Y = 500;
            velocity.Y = 0;
            cancelJump();
        }

        velocity.Y += 100 * deltatime;
    }

    private void Jump(float deltatime)
    {
        if (KeyboardHandler.WasKeyJustPressed(Keyboard.Key.Space) && !isJumping && isGrounded)
        {
            velocity += new Vector2f(0, -250 * deltatime);
            isGrounded = false;
            isJumping = true;
            jumpSteps++;
        }

        jumpSteps++;

        if (KeyboardHandler.IsKeyDown(Keyboard.Key.Space) && !(jumpSteps == MAXJUMPSTEPS) && isJumping)
        {
            velocity += new Vector2f(0, -250 * deltatime);
        }
        else
        {
            isJumping = false;
        }
    }

    public void cancelJump()
    {
        isJumping = false;
        jumpSteps = 0;
    }
}
