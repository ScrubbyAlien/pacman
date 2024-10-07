using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace pacman;

class Program
{
    public static void Main(string[] args)
    {
        using (RenderWindow window = new RenderWindow(new VideoMode(828, 900), "Pacman"))
        {
            // ReSharper disable once AccessToDisposedClosure
            window.Closed += (o, e) => window.Close();

            Clock clock = new();
            Scene scene = new();

            scene.Loader.Load("maze");
            window.SetView(new View(new FloatRect(18, 0, 414, 450)));
            while (window.IsOpen)
            {
                window.DispatchEvents();

                float deltaTime = clock.Restart().AsSeconds();
                deltaTime = MathF.Min(deltaTime, 0.01f); // see IsAligned comment in Actor.cs
                
                scene.UpdateAll(deltaTime);
                
                window.Clear(new Color(0,0,0));
                scene.RenderAll(window);
                
                window.Display();
            }

        }
    }
}

