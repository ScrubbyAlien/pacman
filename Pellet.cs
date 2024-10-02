using SFML.Graphics;

namespace pacman;

public class Pellet : Entity
{
    public Pellet() : base("pacman") { }

    public override FloatRect Bounds
    {
        get
        {
            FloatRect bounds = base.Bounds;
            bounds.Left += 6;
            bounds.Width -= 12;
            bounds.Top += 6;
            bounds.Height -= 12;
            return bounds;
        }  
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        sprite.TextureRect = new IntRect(36, 36, 18, 18);
    }

    protected override void CollideWith(Scene scene, Entity found)
    {
        if (found is Pacman)
        {
            scene.EventManager.PublishGainScore(100);
            Dead = true;
        }
    }
}