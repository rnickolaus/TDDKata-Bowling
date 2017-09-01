namespace Bowling
{
    /// <summary>
    /// Interface to model a bowling game scoring system
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Store the results of one ball being rolled by capturing the number of pins that were downed
        /// </summary>
        /// <param name="pPins">Number of pins felled by the ball roll</param>
        void ThrowABall(int pPins);

        /// <summary>
        /// Calculate the total score of the bowling game (all balls thrown are accounted for) using the standard bowling scoring rules
        /// </summary>
        /// <returns>Game score (0-300)</returns>
        int TallyScore();
    }
}
