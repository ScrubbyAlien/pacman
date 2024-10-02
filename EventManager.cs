namespace pacman;

public class EventManager
{
    public delegate void ValueChangedEvent(Scene scene, int value);
    
    public event ValueChangedEvent? GainScore;
    public event ValueChangedEvent? LoseHealth;
    public event ValueChangedEvent? CandyEaten;
    
    private int scoreGained;
    private int healthLost;
    private int candiesEaten;
    
    public void PublishGainScore(int amount) => scoreGained += amount;
    public void PublishLostHealth(int amount) => healthLost += amount;
    public void PublishCandyEaten(int amount) => candiesEaten += amount;
    
    private void BroadcastValueChangedEvent(ValueChangedEvent? e, ref int value, Scene scene)
    {
        if (value != 0)
        {
            e?.Invoke(scene, value);
            value = 0;
        }
    }

    public void BroadcastEvents(Scene scene)
    {
        BroadcastValueChangedEvent(GainScore, ref scoreGained, scene);
        BroadcastValueChangedEvent(LoseHealth, ref healthLost, scene);
        BroadcastValueChangedEvent(CandyEaten, ref candiesEaten, scene);
    }
    
}
