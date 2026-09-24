using System.Text.Json;
using System.Text.Json.Serialization;

namespace Plattslopper;

public struct RoomData
{
    //public static Dictionary<string,RoomData> 

    public static RoomData LoadFromFile(string filePath)
    {
        string json = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<RoomData>(json, new JsonSerializerOptions { IncludeFields = true });

    }

    [JsonInclude] List<RoomObject> roomObjects;
    [JsonInclude] string backgroundName;

    public RoomData()
    {
        roomObjects = new();
    }


    public void print()
    {
        System.Console.WriteLine(backgroundName);
        System.Console.WriteLine(roomObjects.Count);
        foreach (RoomObject ob in roomObjects)
        {
            System.Console.WriteLine(ob.tag);
            System.Console.WriteLine(ob.position);
            System.Console.WriteLine(ob.size);
        }
    }



}