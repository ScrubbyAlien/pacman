using SFML.Graphics;

namespace pacman;

public class Ghost : Actor
{
    private float preyTimer = 0.0f;
    private bool isPrey => preyTimer > 0;
    
    public Ghost() : base("pacman") { }

    public override void Create(Scene scene)
    {
        direction = -1;
        originalSpeed = 100;
        originalPosition = Position;
        moving = true;
        base.Create(scene);
        sprite.TextureRect = new IntRect(36, 0, 18, 18);
        scene.EventManager.CandyEaten += (s,i) => preyTimer = 5.0f;
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        preyTimer = MathF.Max(preyTimer - deltaTime, 0.0f);
        if (isPrey)
        {
            sprite.TextureRect = new IntRect(36, 18, 18, 18);
            speed = 50;
        }
        else
        {
            sprite.TextureRect = new IntRect(36, 0, 18, 18);
            speed = originalSpeed;
        }
    }

    protected override void Reset()
    {
        base.Reset();
        preyTimer = 0.0f;
    }

    protected override int PickDirection(Scene scene)
    {
        List<int> validMoves = new();
        for (int i = 0; i < 4; i++)
        {
            if ((i + 2) % 4 == direction) continue;
            if (IsFree(scene, i)) validMoves.Add(i);
        }

        return validMoves[new Random().Next(0, validMoves.Count)];
    }

    protected override void CollideWith(Scene scene, Entity entity)
    {
        if (entity is Pacman pacman)
        {
            if (pacman.isInvincible) return;
            if (isPrey)
            {
                scene.EventManager.PublishGainScore(500);
                Reset();
                return;
            }
            scene.EventManager.PublishLostHealth(1);
            Reset();
        }
    }
}