using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Plattslopper;

public class Player : RoomObject
{
    public Stack<Key> keys = new();
    public int coins = 0;
    public Vector2f direction = new(0, 0);
    public Vector2f velocity = new(0, 0);
    public Vector2f startPosition;
    Text gui;
    bool isGrounded = false;
    bool isJumping = false;
    const int MAXJUMPSTEPS = 7;
    int jumpSteps = 0;
    int playerMoveSpeed = 400;
    int playerJumpPower = 200;
    const float playerMoveDecayConstant = 0.6f;
    int playerGravity = 75;
    bool isLeft = false;


    public Player()
    {
        size = new Vector2f(100, 100);
        collisionBox = new(new(100, 100), size);
        gui = new()
        {
            CharacterSize = 67,
            Font = new Font("fonts/saturno.ttf")

        };
    }

    public override void RoomStart()
    {
        spriteDrawer.InitializeSprites([spriteName]);
    }

    public override void Update(float deltaTime)
    {
        if (position.Y > Game.WindowSize.Y)
            position = startPosition;

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

            if (roomObject is Key)
            {
                Key key = (Key)roomObject;

                if (Collision.RectangleRectangle(key.collisionBox.collisionBoxRect, this.collisionBox.collisionBoxRect, out Collision.Hit keyHit))
                {
                    key.SendKeyId();
                    key.remove = true;
                }
            }

            if (roomObject is Door)
            {
                Door door = (Door)roomObject;

                if (door.isUnlocked)
                {
                    if (Collision.RectangleRectangle(door.collisionBox.collisionBoxRect, this.collisionBox.collisionBoxRect, out Collision.Hit doorHit))
                    {
                        foreach (RoomObject roomObjectRemove in Game.currentRoom.RoomObjects)
                            roomObjectRemove.remove = true;
                        door.ChangeRoom();
                    }
                }
            }

            if (roomObject is Coin)
            {
                Coin coin = (Coin)roomObject;
                if (Collision.RectangleRectangle(coin.collisionBox.collisionBoxRect, this.collisionBox.collisionBoxRect, out Collision.Hit doorHit))
                {
                    coins++;
                    coin.remove = true;
                }

            }
            if (roomObject is Door || roomObject is Key || roomObject is Coin)
                continue;

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
        spriteDrawer.DrawSprite(position, size, spriteDrawer.GetSprite(spriteName), window, isLeft);
        gui.DisplayedString = $"coins: {coins}";
        gui.Position = new Vector2f(400, 400);
        gui.FillColor = Color.Red;
        gui.OutlineThickness = 10;
        gui.OutlineColor = Color.Black;

        window.Draw(gui);

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
            isLeft = true;
            velocityChangeX += -playerMoveSpeed * deltatime;
        }
        if (KeyboardHandler.IsKeyDown(Keyboard.Key.D))
        {
            isVelocityChanged = true;
            isLeft = false;
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
