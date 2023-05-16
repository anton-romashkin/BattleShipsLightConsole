using System.Drawing;
using BattleShipsLightConsole.Models;
using BattleShipsLightConsole.Enums;
using BattleShipsLightConsole.Helpers;

namespace BattleShipsLightConsole.Factories;

/// <summary>
/// Factory class responsible for creating new instances of players and assisting in the setup of ships on the battle zone.
/// </summary>
public static class PlayerFactory
{
    #region Public Methods

    /// <summary>
    /// Creates a new player by guiding through the process of setting up ships on the battle zone.
    /// </summary>
    /// <returns>The created PlayerModel object.</returns>
    public static PlayerModel CreateNewPlayer()
    {
        Console.Clear();

        var newPlayer = new PlayerModel();

        newPlayer.Name = AddPlayerName($"Enter Player {_playersCreated + 1} name:  ");

        _playersCreated++;

        int[,] battleZone = newPlayer.BattleZone;

        while (newPlayer.ActiveShips < 5)
        {
            Console.Clear();

            DisplayPlayFieldHelper.DisplayBattleZone(battleZone);

            int shipNumber = newPlayer.ActiveShips + 1;

            var position = PositionHelper.GetShipPosition(
                    $"{newPlayer.Name}, select the position for the ship {shipNumber}:  "
                    );

            bool isValid = PositionHelper.IsValidPositionForShip(position, battleZone, out string statusMessage);

            if (!isValid)
            {
                MessageHelper.ShowMessage(statusMessage);

                continue;
            }

            PlaceShip(position, newPlayer);
        }

        MessageHelper.ShowMessage("All ships are placed");

        return newPlayer;
    }

    #endregion

    #region Private Fields

    /// <summary>
    /// The number of players that have been created.
    /// </summary>
    private static int _playersCreated = 0;

    /// <summary>
    /// An array containing the names that have already been used by players.
    /// </summary>
    private static string[] _usedNames = new string[5];

    #endregion

    #region Private Methods

    private static string AddPlayerName(string message)
    {
        Console.Write(message);

        string? newName = Console.ReadLine();

        if (newName == null || newName.Length == 0)
        {
            newName = "_";
        }

        if (_usedNames.Contains(newName))
        {
            newName += $"_2";
        }

        return newName;
    }

    private static void PlaceShip(Point position, PlayerModel player)
    {
        var playerField = player.BattleZone;

        playerField[position.X, position.Y] = (int)GridStatus.Ship;

        player.ActiveShips++;
    }

    #endregion
}

