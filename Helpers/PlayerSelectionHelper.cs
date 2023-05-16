namespace BattleShipsLightConsole.Helpers;

/// <summary>
/// Helper class for player selection operations.
/// </summary>
public static class PlayerSelectionHelper
{
    #region Public Methods

    /// <summary>
    /// Flips a coin to determine the first move. Returns 0 for heads and 1 for tails.
    /// </summary>
    /// <returns>An integer representing the result of the coin flip (0 or 1).</returns>
    public static int FlipACoinForFirstMove()
    {
        Random coin = new Random();
        int flip = coin.Next(0, 2);

        return flip;
    }

    #endregion
}

