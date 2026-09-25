using System.Text.Json;
using System.Text.Json.Serialization;
using SFML.Audio;

namespace Plattslopper;

public struct RoomData
{
    //public static Dictionary<string,RoomData> 

    public static RoomData LoadFromFile(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<RoomData>(json, new JsonSerializerOptions { IncludeFields = true });
    }

    [JsonInclude] public List<RoomObject> roomObjects;
    [JsonInclude] public string backgroundName;
    [JsonInclude] public string songName;

    public RoomData()
    {
        roomObjects = new();
    }


    public void Print()
    {
        System.Console.WriteLine(backgroundName);
        System.Console.WriteLine(roomObjects.Count);
        foreach (RoomObject ob in roomObjects)
        {
            System.Console.WriteLine(ob.position);
            System.Console.WriteLine(ob.size);
        }
    }



}