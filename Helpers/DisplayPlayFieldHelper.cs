using System.Text;
using BattleShipsLightConsole.Enums;
using BattleShipsLightConsole.Models;

namespace BattleShipsLightConsole.Helpers;

/// <summary>
/// Helper class for displaying the battle zone and ships on the console.
/// </summary>
public static class DisplayPlayFieldHelper
{
    #region Public Methods

    /// <summary>
    /// Displays the battle zone with the player's ships on the console.
    /// </summary>
    /// <param name="battleZone">The 2D integer array representing the battle zone.</param>
    public static void DisplayBattleZone(int[,] battleZone)
    {
        string[] letterCoordinates = _letterCoordinates;
        string numberCoordinates = _numberCoordinates;

        int battleZoneHeight = battleZone.GetLength(0);
        int battleZoneWidth = battleZone.GetLength(1);

        string legendLeft = "Your ships";

        string column = "";

        var playerBattleZoneString = new StringBuilder();

        playerBattleZoneString.AppendLine($"{legendLeft.PadLeft(14)}\n");

        playerBattleZoneString.AppendLine($"{numberCoordinates.PadLeft(17)}\n");

        for (int y = 0; y < battleZoneHeight; y++)
        {
            column += $"{letterCoordinates[y].PadRight(3)}";

            for (int x = 0; x < battleZoneWidth; x++)
            {
                column += GetPlayerGridStatusString(battleZone[x, y]);
            }

            playerBattleZoneString.AppendLine(column);

            column = "";
        }

        Console.WriteLine(playerBattleZoneString.ToString());
    }

    /// <summary>
    /// Displays both the player's and opponent's battle zones with their ships on the console.
    /// </summary>
    /// <param name="player">The PlayerModel object representing the player.</param>
    /// <param name="opponent">The PlayerModel object representing the opponent.</param>
    /// <param name="isFinalResult">Indicates whether it's the final result to reveal opponent's ship positions.</param>
    public static void DisplayBothBattleZones(PlayerModel player, PlayerModel opponent, bool isFinalResult=false)
    {
        string[] letterCoordinates = _letterCoordinates;
        string numberCoordinates = _numberCoordinates;

        int battleZoneHeight = player.BattleZone.GetLength(0);
        int battleZoneWidth = player.BattleZone.GetLength(1);

        string legendLeft = $"{player.Name} ships";
        string legendRight = $"{opponent.Name} ships";

        string column = "";

        var playerPlayFieldString = new StringBuilder();

        playerPlayFieldString.AppendLine($"{legendLeft.PadLeft(14)}\t\t{legendRight.PadLeft(17)}\n");

        playerPlayFieldString.AppendLine($"{numberCoordinates.PadLeft(17)}\t{numberCoordinates.PadLeft(17)}\n");

        for (int y = 0; y < battleZoneHeight; y++)
        {
            column += $"{letterCoordinates[y].PadRight(3)}";

            for (int x = 0; x < battleZoneWidth; x++)
            {
                column += GetPlayerGridStatusString(player.BattleZone[x, y]);
            }

            column += $"\t{_letterCoordinates[y].PadRight(3)}";

            for (int x = 0; x < battleZoneWidth; x++)
            {
                column += GetOpponentGridStatusString(opponent.BattleZone[x, y], isFinalResult);
            }

            playerPlayFieldString.AppendLine(column);

            column = "";
        }

        Console.WriteLine(playerPlayFieldString.ToString());
    }

    #endregion


    #region Private Fields

    /// <summary>
    /// Letter coordinates used for displaying the battle zone.
    /// </summary>
    private static string[] _letterCoordinates = { "A", "B", "C", "D", "E" };

    /// <summary>
    /// Represents the number coordinates used for displaying the battle zone.
    /// </summary>
    private static string _numberCoordinates = "1  2  3  4  5";

    #endregion

    #region Private Methods

    private static string GetPlayerGridStatusString(int gridStatus) =>
        gridStatus switch
        {
            (int)GridStatus.Empty => "   ",
            (int)GridStatus.Ship => " @ ",
            (int)GridStatus.Sunk => " X ",
            (int)GridStatus.Miss => " * ",
            _ => "   "
        };

    private static string GetOpponentGridStatusString(int gridStatus, bool isFinalResult = false) =>
        gridStatus switch
        {
            (int)GridStatus.Empty => "   ",
            (int)GridStatus.Ship when isFinalResult => " @ ",
            (int)GridStatus.Ship => "   ",
            (int)GridStatus.Sunk => " X ",
            (int)GridStatus.Miss => " * ",
            _ => "   "
        };

    #endregion
}
