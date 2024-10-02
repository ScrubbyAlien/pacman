using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace pacman;

public class Pacman : Actor
{
    private static readonly IntRect OPEN_RIGHT   = new IntRect(18, 0, 18, 18);
    private static readonly IntRect OPEN_UP      = new IntRect(18, 18, 18, 18);
    private static readonly IntRect OPEN_LEFT    = new IntRect(18, 36, 18,18);
    private static readonly IntRect OPEN_DOWN    = new IntRect(18, 54, 18, 18);
    private static readonly IntRect MIDDLE_RIGHT = new IntRect(0, 0, 18, 18);
    private static readonly IntRect MIDDLE_UP    = new IntRect(0, 18, 18, 18);
    private static readonly IntRect MIDDLE_LEFT  = new IntRect(0, 36, 18,18);
    private static readonly IntRect MIDDLE_DOWN  = new IntRect(0, 54, 18, 18);
    private static readonly IntRect CLOSED       = new IntRect(36, 54, 18, 18);
    
    private AnimationStage animationStage = AnimationStage.Open;
    private bool wasClosed;
    private float animationRate = 0.1f;
    private float animationTimer;
    
    private float invincibleTimer;
    public bool isInvincible => invincibleTimer > 0;
    
    public Pacman() : base("pacman") { }


    public override FloatRect Bounds
    {
        get
        {
            FloatRect bounds = base.Bounds;
            bounds.Left += 3;
            bounds.Width -= 6;
            bounds.Top += 3;
            bounds.Height -= 6;
            return bounds;
        }  
    }

    protected override void Reset()
    {
        base.Reset();
        invincibleTimer = 1f;
        direction = 1;
        animationStage = AnimationStage.Open;
    }

    public override void Create(Scene scene)
    {
        direction = 1;
        originalSpeed = 100;
        originalPosition = Position;
        base.Create(scene);
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
        scene.EventManager.LoseHealth += OnLoseHealth;
    }

    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.EventManager.LoseHealth -= OnLoseHealth;
    }

    public override void Update(Scene scene, float deltaTime)
    {
        animationTimer += deltaTime;
        Animate();
        if (animationTimer >= animationRate)
        {
            animationTimer = 0;
            
        }
        if (!isInvincible)
        {
            base.Update(scene, deltaTime);
        }
        invincibleTimer = MathF.Max(invincibleTimer - deltaTime, 0.0f);
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
    
    private void OnLoseHealth(Scene scene, int amount)
    {
        Reset();
    }

    private void Animate()
    {
        if (animationTimer >= animationRate)
        {
            if (isInvincible)
            {
                if (sprite.TextureRect == new IntRect(0, 0, 0, 0))
                    sprite.TextureRect = OPEN_UP;
                else if (sprite.TextureRect == OPEN_UP)
                    sprite.TextureRect = new IntRect(0, 0, 0, 0);
                else sprite.TextureRect = OPEN_UP;
                return;
            }
            
            if (animationStage != AnimationStage.Middle)
            {
                wasClosed = animationStage == AnimationStage.Closed;
            }
            
            animationStage = animationStage switch
            {
                AnimationStage.Closed => AnimationStage.Middle,
                AnimationStage.Middle => wasClosed ? AnimationStage.Open : AnimationStage.Closed,
                AnimationStage.Open => AnimationStage.Middle,
                _ => throw new ArgumentOutOfRangeException()
            };    
        }

        if (!isInvincible)
        {
            switch (direction)
            {
                case 0: // Right
                    sprite.TextureRect = animationStage switch
                    {
                        AnimationStage.Closed => CLOSED,
                        AnimationStage.Middle => MIDDLE_RIGHT,
                        AnimationStage.Open => OPEN_RIGHT,
                        _ => CLOSED
                    };
                    break;
                case 1: // Up
                    sprite.TextureRect = animationStage switch
                    {
                        AnimationStage.Closed => CLOSED,
                        AnimationStage.Middle => MIDDLE_UP,
                        AnimationStage.Open => OPEN_UP,
                        _ => CLOSED
                    };
                    break;
                case 2: // Left
                    sprite.TextureRect = animationStage switch
                    {
                        AnimationStage.Closed => CLOSED,
                        AnimationStage.Middle => MIDDLE_LEFT,
                        AnimationStage.Open => OPEN_LEFT,
                        _ => CLOSED
                    };
                    break;
                case 3: // Down
                    sprite.TextureRect = animationStage switch
                    {
                        AnimationStage.Closed => CLOSED,
                        AnimationStage.Middle => MIDDLE_DOWN,
                        AnimationStage.Open => OPEN_DOWN,
                        _ => CLOSED
                    };
                    break;
            }
        }
    }
    
    private enum AnimationStage
    {
        Closed, Middle, Open
    }
}

