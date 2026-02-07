namespace SnakeGame.Core;

public sealed class Speed
{
    private readonly float _baseTick;
    private readonly float _minTick;
    private readonly float _step;

    public float TickInterval { get; private set; }

    public Speed (float baseTick, float minTick, float step)
    {
        _baseTick = baseTick;
        _minTick = minTick;
        _step = step;
        TickInterval = _baseTick;
    }

    public void Reset() => TickInterval = _baseTick;

    public void OnEat()
    {
        var next = TickInterval - _step;
        TickInterval = next < _minTick ? _minTick : next;
    }
}