public struct ContentData
{
    public ContentData(string path, string groupName, string label)
    {
        Path = path;
        GroupName = groupName;
        Label = label;
    }

    public string Path;
    public string GroupName;
    public string Label;
}