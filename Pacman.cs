using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace pacman;

public class Pacman : Actor
{
    public Pacman() : base("pacman") { }

    private void OnLoseHealth(Scene scene, int amount)
    {
        Reset();
    }
    
    public override void Create(Scene scene)
    {
        originalSpeed = 100;
        originalPosition = Position;
        base.Create(scene);
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
        scene.LoseHealth += OnLoseHealth;
    }

    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.LoseHealth -= OnLoseHealth;
    }

    protected override int PickDirection(Scene scene)
    {
        int dir = direction;
        if (Keyboard.IsKeyPressed(Right))
        {
            dir = 0;
            moving = true;
        }
        else if (Keyboard.IsKeyPressed(Up))
        {
            dir = 1;
            moving = true;
        }
        else if (Keyboard.IsKeyPressed(Left))
        {
            dir = 2;
            moving = true;
        }
        else if (Keyboard.IsKeyPressed(Down))
        {
            dir = 3;
            moving = true;
        }

        if (IsFree(scene, dir)) return dir;
        if (!IsFree(scene, direction)) moving = false;
        return direction;
    }
    
    
}