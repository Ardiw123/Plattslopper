using SFML.Graphics;
using SFML.System;

public class SpriteDrawer
{
    static string spriteDirectory = "sprites";

    static Dictionary<string, Sprite> AllSprites = new(StringComparer.OrdinalIgnoreCase);

    public static void InitilizeAllSprites()
    {
        AllSprites.Clear();

        foreach (string filePath in Directory.EnumerateFiles(spriteDirectory, "*.png"))
        {
            string name = Path.GetFileNameWithoutExtension(filePath);
            AllSprites[name] = new Sprite(new Texture(filePath));
        }
    }

    static Sprite GetStaticSprite(string spriteName)
    {
        if (AllSprites.TryGetValue(spriteName, out Sprite sprite))
        {
            return sprite;
        }
        throw new KeyNotFoundException($"Hittade inte {spriteName} inuti sloppet: {spriteDirectory}.");
    }

    Dictionary<string, Sprite> sprites = new(StringComparer.OrdinalIgnoreCase);

    public Sprite GetSprite(string spriteName)
    {
        if (sprites.TryGetValue(spriteName, out Sprite sprite))
        {
            return sprite;
        }

        return GetStaticSprite(spriteName);
    }

    public void InitializeSprites(string[] spriteNames)
    { 
        for (int i = 0; i < spriteNames.Length; i++)
        {
            string spriteName = spriteNames[i];

            //dubbelcheckar att den finns så att inget blir fel
            if (!sprites.ContainsKey(spriteName))
            {
                sprites[spriteName] = GetStaticSprite(spriteName);
            }
        }
    }

    public void DrawSprite(Vector2f position, Vector2f size, Sprite sprite, RenderWindow window)
    {
        sprite.Position = position;
        sprite.Scale = new Vector2f(size.X / sprite.Texture.Size.X, size.Y / sprite.Texture.Size.Y);
        window.Draw(sprite);
    }

    public void DrawSprite(Vector2f position, Vector2f size, Sprite sprite, RenderWindow window, bool flip)
    {
        sprite.Origin = new Vector2f(sprite.Texture.Size.X / 2, sprite.Texture.Size.Y / 2);
        Vector2f scale = new Vector2f(size.X / sprite.Texture.Size.X, size.Y / sprite.Texture.Size.Y);

        if (flip)
            scale.X *= -1;
        else
            scale.X *= 1;

        sprite.Scale = scale;

        sprite.Position = position + new Vector2f(34, 35);

        window.Draw(sprite);
    }

    public void DrawSprite(Vector2f position, Vector2f size, Sprite sprite, RenderWindow window, IntRect region)
    {
        Sprite niklasAdolfus = sprite;
        niklasAdolfus.TextureRect = region;

        niklasAdolfus.Position = position;
        //niklasAdolfus.Scale = new Vector2f(size.X / niklasAdolfus.Texture.Size.X, size.Y / niklasAdolfus.Texture.Size.Y);
        niklasAdolfus.Scale = new Vector2f(size.X / region.Width, size.Y / region.Height);
        window.Draw(sprite);
    }
}