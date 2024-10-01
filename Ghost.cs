﻿using SFML.Graphics;

namespace pacman;

public class Ghost : Actor
{
    public Ghost() : base("pacman")
    {
        
    }

    public override void Create(Scene scene)
    {
        direction = -1;
        originalSpeed = 100;
        originalPosition = Position;
        moving = true;
        base.Create(scene);
        sprite.TextureRect = new IntRect(36, 0, 18, 18);
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
        if (entity is Pacman)
        {
            scene.PublishLostHealth(1);
            Reset();
        }
    }
}