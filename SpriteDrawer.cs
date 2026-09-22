using SFML.Graphics;
using SFML.System;

public class SpriteDrawer
{

    static Dictionary<string, Sprite> AllSprites = new(StringComparer.OrdinalIgnoreCase);

    public static void InitilizeAllSprites()
    {
        //får alla wav filer i assets och sparar de i en dictionary
        foreach (string filePath in Directory.EnumerateFiles("assets", "*.png"))
        {
            string name = Path.GetFileNameWithoutExtension(filePath);
            System.Console.WriteLine(name);
            AllSprites[name] = new Sprite(new Texture(filePath));
        }
    }

    static Sprite GetStaticSprite(string soundName)
    {
        //if it gets a string that doesnt exist it doesnt crash😂😂
        if (AllSprites.TryGetValue(soundName, out Sprite sprite))
        {
            return sprite;
        }
        else return new(new Texture("flower"));
    }

    Dictionary<string, Sprite> sprites = new();

    public Sprite GetSprite(string spriteName)
    {
        return sprites[spriteName];
    }
    public void InitializeSprites(string[] spriteNames)
    {
        for (int i = 0; i < spriteNames.Length; i++)
        {
            sprites.Add(spriteNames[i], GetStaticSprite(spriteNames[i]));
        }
    }

    public void DrawSprite(Sprite sprite, RenderWindow window)
    {
        window.Draw(sprite);
    }
}