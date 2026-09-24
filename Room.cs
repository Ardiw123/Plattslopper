using SFML.Graphics;
using SFML.System;
using SFML.Audio;

namespace Plattslopper;

public class Room
{
    public static Room MakeRoomFromRoomData(RoomData roomData)
    {
        roomData.print();
        Room newRoom = new()
        {
            RoomObjects = roomData.roomObjects,
            spriteName = roomData.backgroundName,
            songName = roomData.songName
        };
        newRoom.backGroundDrawer.InitializeSprites([newRoom.spriteName]);
        newRoom.roomData = roomData;
        return newRoom;
    }

    public List<RoomObject> RoomObjects;
    string spriteName;
    SpriteDrawer backGroundDrawer;
    public RoomData roomData;
    public string songName;

    Room()
    {
        backGroundDrawer = new();
        InitilizeEpicSongs();
    }

    public void StartRoom()
    {

        for (int i = 0; i < RoomObjects.Count; i++)
        {
            //först uppdatera alla värden
            RoomObjects[i].RoomStart();
        }

        PlayMusic(songName);
    }

    public void Update(float deltaTime)
    {

        for (int i = 0; i < RoomObjects.Count; i++)
        {
            //först uppdatera alla värden
            RoomObjects[i].Update(deltaTime);
        }
        // tar bort alla objekt efter man har itererat så inte listan förstörs
        RoomObjects.RemoveAll(obj => obj.remove == true);
    }

    public void Draw(RenderWindow window)
    {
        backGroundDrawer.DrawSprite(new(0, 0), (Vector2f)Game.WindowSize, backGroundDrawer.GetSprite(spriteName), window);
        for (int i = 0; i < RoomObjects.Count; i++)
        {
            RoomObjects[i].Draw(window);
        }
    }

    Dictionary<string, Sound> songs = new(StringComparer.OrdinalIgnoreCase);

    void InitilizeEpicSongs()
    {
        //får alla wav filer i assets och sparar de i en dictionary
        foreach (string filePath in Directory.EnumerateFiles("musik", "*.wav"))
        {
            string name = Path.GetFileNameWithoutExtension(filePath);
            songs[name] = new Sound(new SoundBuffer(filePath));
        }
    }

    void PlayMusic(string songName)
    {
        //😂😂
        if (songs.TryGetValue(songName, out Sound sound)) sound.Play();
    }

}
