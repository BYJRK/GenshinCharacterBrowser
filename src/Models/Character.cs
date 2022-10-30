namespace GenshinCharacterBrowser.Models;

public class Character
{
    public string Name { get; init; }
    public string IconUrl { get; init; }
    public string ProtraitUrl { get; init; }
    public string NameUrl { get; set; }
    public string ElementUrl { get; set; }
    public string DialogueUrl { get; set; }
}
