namespace BattleShipsLightConsole.Models;

/// <summary>
/// Represents a player in the game.
/// </summary>
public class PlayerModel
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the name of the player.
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// Gets or sets the player's zone for ships represented as a 2D integer array.
    /// </summary>
    public int[,] BattleZone { get; set; } = new int[5, 5];

    /// <summary>
    /// Gets or sets the number of non-sunk ships that the player has.
    /// </summary>
    public int ActiveShips { get; set; }

    #endregion
}

