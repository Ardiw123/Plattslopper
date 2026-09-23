namespace Plattslopper;

public struct RoomData
{
    List<RoomObject> roomObjects;
    string backgroundName;
    /*
    List<Location> LoadLocations(string filePath)
    {

        string data;

        if (S.Testing == true) data = File.ReadAllText(S.TestFilePath);
        else data = File.ReadAllText(filePath);

        var loadedLocations = JsonSerializer.Deserialize<List<Location>>(data, new JsonSerializerOptions { IncludeFields = true }) ?? [];

        for (var i = 0; i < loadedLocations.Count(); ++i)
        {
            var theLocation = loadedLocations[i];
            theLocation.NeedItemToEnter = !string.IsNullOrEmpty(theLocation.KeyItem);
            loadedLocations[i] = theLocation;
        }

        return loadedLocations;
    }
*/
}