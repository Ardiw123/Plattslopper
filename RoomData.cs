using System.Text.Json;
using System.Text.Json.Serialization;

namespace Plattslopper;

public struct RoomData
{
    //public static Dictionary<string,RoomData> 

    public static RoomData LoadFromFile(string filePath)
    {
        string data = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<RoomData>(data, new JsonSerializerOptions { IncludeFields = true });
        // ?? throw new InvalidOperationException("Could not load RoomData");
    }

    [JsonInclude] List<RoomObject> roomObjects;
    [JsonInclude] string backgroundName;

    public RoomData()
    {
        roomObjects = new();
    }

    public void SloppaNerDet()
    {
        var slop = LoadObjects("levels/level0.json");
        roomObjects = slop.roomObjects;
        backgroundName = slop.backgroundName;
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

    RoomData LoadObjects(string filePath)
    {

        string data = File.ReadAllText(filePath);

        //detta kan vi använda om vi  vill spara allt i samma json
        //List<RoomObject> loadedLocations = JsonSerializer.Deserialize<List<RoomObject>>(data, new JsonSerializerOptions { IncludeFields = true }) ?? [];

        var sloppigaKirk = JsonSerializer.Deserialize<RoomData>(data, new JsonSerializerOptions { IncludeFields = true });

        return sloppigaKirk;
    }

}