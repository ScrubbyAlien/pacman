using SFML.Graphics;

namespace pacman;

public class AssetManager
{
    public static readonly string AssetPath = "assets";
    private readonly Dictionary<string, Texture> textures;
    private readonly Dictionary<string, Font> fonts;

    public AssetManager()
    {
        textures = new();
        fonts = new();
    }

    public Texture LoadTexture(string name)
    {
        return new Texture($"{AssetPath}/{name}.png");
    }

    public Font LoadFont(string name)
    {
        return new Font($"{AssetPath}/{name}.ttf");
    }
}