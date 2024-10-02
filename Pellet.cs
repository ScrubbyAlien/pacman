using SFML.Graphics;

namespace pacman;

public class Pellet : Entity
{
    public Pellet() : base("pacman") { }

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