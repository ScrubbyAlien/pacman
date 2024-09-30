using SFML.Graphics;
using SFML.System;

namespace pacman;

public class Entity
{
    private string textureName;
    protected Sprite sprite = new();
    public bool Dead = false;

    protected Entity(string textureName)
    {
        this.textureName = textureName;
    }

    public virtual Vector2f Position
    {
        get => sprite.Position;
        set => sprite.Position = value;
    }

    public virtual FloatRect Bounds => sprite.GetGlobalBounds();
    public virtual bool Solid => false;

    public virtual void Create(Scene scene)
    {
        sprite.Texture = scene.AssetManager.LoadTexture(textureName);
    }

    public virtual void Destroy(Scene scene) { }
    
    public virtual void Update(Scene scene, float deltaTime)
    {
        foreach (Entity found in scene.FindIntersects(Bounds))
        {
            CollideWith(scene, found);
        }
    }

    protected virtual void CollideWith(Scene scene, Entity found) { }

    public void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }

}