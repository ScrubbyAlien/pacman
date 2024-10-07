using SFML.Graphics;
using SFML.System;

namespace pacman;

public class Entity
{
    private string textureName;
    protected Sprite sprite = new();
    public bool Dead = false;
    public bool DontDestroyOnLoad = false;

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
        // this is so slow, should only check for collisions with relevant entities
        // could do this by giving FindIntersects a generic type parameter and implement loop in 
        // child classes' own overriden update method, instead of here
        foreach (Entity found in scene.FindIntersects(Bounds))
        {
            CollideWith(scene, found);
        }
    }

    protected virtual void CollideWith(Scene scene, Entity found) { }

    public virtual void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }

}