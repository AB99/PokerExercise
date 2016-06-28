namespace PokerExercise
{
    public interface IPlayer
    {
        string Name { get; }
        Card[] Hand { get; }
    }
}