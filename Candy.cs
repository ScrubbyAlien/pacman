using SFML.Graphics;

namespace pacman;

public class Candy : Entity
{
    public Candy() : base("pacman") { }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        sprite.TextureRect = new IntRect(54, 36, 18, 18);
    }
}