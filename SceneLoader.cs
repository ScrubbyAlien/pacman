using System.Text;
using SFML.System;

namespace pacman;

public class SceneLoader
{
    private readonly Dictionary<char, Func<Entity>> loaders;
    private string currentScene = "";
    private string nextScene = "";

    public SceneLoader()
    {
        loaders = new()
        {
            {'#', () => new Wall()}
        };
    }

    public void Load(string scene) => nextScene = scene;
    public void Reload() => nextScene = currentScene;
    
    private bool Create(char symbol, out Entity? created)
    {
        if (loaders.TryGetValue(symbol, out Func<Entity>? loader))
        {
            created = loader();
            return true;
        }
        created = null;
        return false;
    }

    public void HandleSceneLoad(Scene scene)
    {
        if (nextScene == "") return;
        scene.Clear();

        // file path hardcoded, make variable if more levels will be added
        List<string> maze = File.ReadLines($"assets/{nextScene}.txt", Encoding.UTF8).ToList();

        for (int i = 0; i < maze.Count; i++)
        {
            for (int j = 0; j < maze[i].Length; j++)
            {
                if (Create(maze[i][j], out Entity? created))
                {
                    scene.Spawn(created!);
                    created!.Position = new Vector2f(18 * j, 18 * i);
                }
            }
        }
        
        
        currentScene = nextScene;
        nextScene = "";
    }
    
}