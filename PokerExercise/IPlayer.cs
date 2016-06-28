namespace PokerExercise
{
    public interface IPlayer
    {
        string Name { get; }
        Card[] Cards { get; }
    }
}