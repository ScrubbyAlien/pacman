using SFML.Graphics;
using SFML.Window;

namespace pacman;

public sealed class Scene
{
    private List<Entity> entities = new();
    public readonly SceneLoader Loader = new();
    public readonly AssetManager AssetManager = new();
    public readonly EventManager EventManager = new(); 
    
    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }

    public void Clear()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];

            if (!entity.DontDestroyOnLoad)
            {
                entities.RemoveAt(i);
                entity.Destroy(this);    
            }
        }
    }

    public void UpdateAll(float deltaTime)
    {
        Loader.HandleSceneLoad(this);
        foreach (Entity entity in entities)
        {
            if (!entity.Dead) entity.Update(this, deltaTime);
        }

        EventManager.BroadcastEvents(this);
    }

    public void RenderAll(RenderTarget target)
    {
        foreach (Entity entity in entities)
        {
            if (!entity.Dead) entity.Render(target);
        }
    }
    
    public bool FindByType<T>(out T found) where T : Entity
    {
        foreach (Entity entity in entities)
        {
            if (!entity.Dead && entity is T typed)
            {
                found = typed;
                return true;
            }
        }

        found = default!;
        return false;
    }

    public IEnumerable<Entity> FindIntersects(FloatRect bounds)
    {
        int lastEntity = entities.Count - 1;

        for (int i = lastEntity; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (entity.Dead) continue;
            if (entity.Bounds.Intersects(bounds))
            {
                yield return entity;
            }
        }
    }

    

}