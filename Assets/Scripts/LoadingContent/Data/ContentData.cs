public readonly struct ContentData
{
    public ContentData(string path, string groupName, string label)
    {
        Path = path;
        GroupName = groupName;
        Label = label;
    }

    public readonly string Path;
    public readonly string GroupName;
    public readonly string Label;
}