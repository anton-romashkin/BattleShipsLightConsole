using System.Drawing;
using BattleShipsLightConsole.Models;
using BattleShipsLightConsole.Helpers;

namespace BattleShipsLightConsole.Services;

/// <summary>
/// Represents the game process service responsible for managing the game flow.
/// </summary>
public class GameProcessService
{
    public GameProcessService(PlayerModel playerOne, PlayerModel playerTwo)
    {
        int coin = PlayerSelectionHelper.FlipACoinForFirstMove();

        if (coin == 0)
        {
            ActivePlayer = playerOne;
            Opponent = playerTwo;
        }
        else
        {
            ActivePlayer = playerTwo;
            Opponent = playerOne;
        }
    }

    #region Public Properties

    /// <summary>
    /// Ends the game cycle if set to true
    /// </summary>
    public bool IsGameOver { get; set; }

    /// <summary>
    /// Gives player one more shot if set to true
    /// </summary>
    public bool BonusShot { get; set; }

    /// <summary>
    /// Gets or sets the active player.
    /// </summary>
    public PlayerModel ActivePlayer { get; set; }


    /// <summary>
    /// Gets or sets the opponent player.
    /// </summary>
    public PlayerModel Opponent { get; set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Executes the game process where players take turns to shoot at each other's ships until one player runs out of ships.
    /// </summary>
    public void ShootShipsProcess()
    {
        MessageHelper.ShowMessage($"Next turn: {ActivePlayer.Name}");

        while (!IsGameOver)
        {
            Console.Clear();

            DisplayPlayFieldHelper.DisplayBothBattleZones(ActivePlayer, Opponent);

            ShootAShip(ActivePlayer, Opponent);

            if (Opponent.ActiveShips == 0)
            {
                IsGameOver = true;
            }
            else if (!BonusShot)
            {
                SwitchPlayerTurn();

                MessageHelper.ShowMessage($"{Opponent.Name}: {Opponent.ActiveShips} ships\n" +
                    $"{ActivePlayer.Name}: {ActivePlayer.ActiveShips} ships\n\n" +
                    $"Next turn: {ActivePlayer.Name}");
            }
        }

        MessageHelper.ShowMessage($"Game is over. {ActivePlayer.Name} wins.");

        bool isFinalResult = true;

        DisplayPlayFieldHelper.DisplayBothBattleZones(ActivePlayer, Opponent, isFinalResult);
    }

    #endregion

    #region Private Methods

    private void ShootAShip(PlayerModel player, PlayerModel opponent)
    {
        BonusShot = false;

        Point target = PositionHelper
            .GetShipPosition($"{player.Name}, where to shoot?  ");

        bool isValidShot = PositionHelper.IsValidPositionForShot(target, opponent.BattleZone, out string statusMessage);

        if (!isValidShot)
        {
            MessageHelper.ShowMessage(statusMessage);

            BonusShot = true;

            return;
        }

        if (opponent.BattleZone[target.X, target.Y] == (int)Enums.GridStatus.Ship)
        {
            statusMessage = "Hit! You get another shot.";

            BonusShot = true;

            opponent.BattleZone[target.X, target.Y] = (int)Enums.GridStatus.Sunk
                ;
            opponent.ActiveShips--;

            MessageHelper.ShowMessage(statusMessage);
        }
        else
        {
            statusMessage = "Miss";

            BonusShot = false;

            opponent.BattleZone[target.X, target.Y] = (int)Enums.GridStatus.Miss;

            MessageHelper.ShowMessage(statusMessage);
        }
    }

    private void SwitchPlayerTurn()
    {
        PlayerModel playerOne = ActivePlayer;
        PlayerModel playerTwo = Opponent;

        ActivePlayer = playerTwo;
        Opponent = playerOne;
    }

    #endregion
}

