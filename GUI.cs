﻿using SFML.Graphics;
using SFML.System;

namespace pacman;

public class GUI : Entity
{
    private Text scoreText = new();
    private int maxHealth = 3;
    private int currentHealth;
    private int currentScore;
    
    public GUI() : base("pacman") { }

    public override void Create(Scene scene)
    {
        scene.EventManager.LoseHealth += OnLoseHealth;
        scene.EventManager.GainScore += OnGainScore;
        base.Create(scene);
        scoreText.DisplayedString = "Score";
        currentHealth = maxHealth;
        scoreText.Font = scene.AssetManager.LoadFont("pixel-font");
        scoreText.CharacterSize = 54;
        scoreText.Scale = new Vector2f(0.5f, 0.5f);
    }

    public override void Render(RenderTarget target)
    {
        sprite.Position = new Vector2f(36, 396);
        for (int i = 0; i < maxHealth; i++)
        {
            sprite.TextureRect = i < currentHealth
                ? new IntRect(72, 36, 18, 18)
                : new IntRect(72, 0, 18, 18);
            base.Render(target);
            sprite.Position += new Vector2f(18, 0);
        }

        scoreText.DisplayedString = $"Score: {currentScore}";
        scoreText.Position = new Vector2f(414 - scoreText.GetGlobalBounds().Width, 396);
        target.Draw(scoreText);
    }

    private void OnLoseHealth(Scene scene, int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            DontDestroyOnLoad = false;
            currentHealth = maxHealth;
            scene.Loader.Reload();
        }
    }
    
    
    private void OnGainScore(Scene scene, int value)
    {
        currentScore += value;
        if (!scene.FindByType<Pellet>(out _))
        {
            DontDestroyOnLoad = true;
            scene.Loader.Reload();
        }
    }
}
