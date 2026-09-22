using SFML.Graphics;
using SFML.System;

public class SpriteDrawer
{
    Dictionary<string, Sprite> sprites = new();

    public Sprite GetSprite(string spriteName)
    {
        return sprites[spriteName];
    }
    public void InitializeSprites(/*string[] filePaths*/)
    {
        Sprite DefaultSprite = new(new Texture("assets/flower.png"));

        sprites.Add("sprite", DefaultSprite);
    }

    public void DrawSprite(Sprite sprite, RenderWindow window)
    {
        window.Draw(sprite);
    }
}