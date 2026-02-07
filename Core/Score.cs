namespace SnakeGame.Core;

public sealed class Score
{
    public int Value { get; private set; }

    public void Reset() => Value = 0;
    public void Add(int amount) => Value += amount;
}