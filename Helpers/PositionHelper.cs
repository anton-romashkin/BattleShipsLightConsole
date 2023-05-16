using System.Drawing;

namespace BattleShipsLightConsole.Helpers;

/// <summary>
/// Helper class for position-related operations.
/// </summary>
public static class PositionHelper
{
    #region Public Methods
    /// <summary>
    /// Checks if the provided ship position is valid within the given battle zone.
    /// </summary>
    /// <param name="shipPosition">The position of the ship as a Point object.</param>
    /// <param name="battleZone">The 2D integer array representing the battle zone.</param>
    /// <param name="statusMessage">An output parameter that contains a status message regarding the validity of the ship position.</param>
    /// <returns>True if the ship position is valid; otherwise, false.</returns>
    public static bool IsValidPositionForShip(Point shipPosition, int[,] battleZone, out string statusMessage)
    {
        statusMessage = string.Empty;

        if (shipPosition.X == 99 && shipPosition.Y == 99)
        {
            statusMessage = "Position must be from A5 to E5. Please try again";

            return false;
        }
        else if (battleZone[shipPosition.X, shipPosition.Y] == (int)Enums.GridStatus.Ship)
        {
            statusMessage = "This position is occupied by another ship";

            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the provided shot position is valid within the given battle zone.
    /// </summary>
    /// <param name="shotPosition">The position of the shot as a Point object.</param>
    /// <param name="battleZone">The 2D integer array representing the battle zone.</param>
    /// <param name="statusMessage">An output parameter that contains a status message regarding the validity of the shot position.</param>
    /// <returns>True if the shot position is valid; otherwise, false.</returns>
    public static bool IsValidPositionForShot(Point shipPosition, int[,] battleZone, out string statusMessage)
    {
        statusMessage = string.Empty;

        if (shipPosition.X == 99 && shipPosition.Y == 99)
        {
            statusMessage = "Target must be from A5 to E5. Please try again";

            return false;
        }
        else if (battleZone[shipPosition.X, shipPosition.Y] == (int)Enums.GridStatus.Sunk
            || battleZone[shipPosition.X, shipPosition.Y] == (int)Enums.GridStatus.Miss)
        {
            statusMessage = "You already shot at this position";

            return false;
        }

        return true;
    }

    /// <summary>
    /// Gets the position of a ship from the user and returns it as a Point object.
    /// </summary>
    /// <param name="message">The message to display when prompting the user for the ship position.</param>
    /// <returns>The position of the ship as a Point object.</returns>
    public static Point GetShipPosition(string message)
    {
        Console.Write(message);

        string? position = Console.ReadLine();

        position ??= "";

        if (IsValidPositionFormat(position))
        {
            char positionChar = Char.ToLower(position[0]);
            int positionCharAsNumber = GetPositionCharAsNumber(positionChar);

            int positionNumber = int.Parse(position[1].ToString());
            positionNumber--;

            return new Point() { X = positionNumber, Y = positionCharAsNumber };
        }
        else
        {
            return new Point() { X = 99, Y = 99 };
        }
    }

    #endregion

    #region Private Methods

    private static int GetPositionCharAsNumber(char positionChar) =>
        positionChar switch
        {
            'a' => 0,
            'b' => 1,
            'c' => 2,
            'd' => 3,
            'e' => 4,
            _ => 0
        };

    private static bool IsValidPositionFormat(string position)
    {
        if (position.Length != 2)
        {
            return false;
        }
        else
        {
            // checks letter
            char positionChar = Char.ToLower(position[0]);
            List<char> checkListChars = new List<char> { 'a', 'b', 'c', 'd', 'e' };
            bool isValidPositionChar = checkListChars.Contains(positionChar);

            // checks number
            int positionNumber;
            bool isvalidPositionNumber;
            if (int.TryParse(position[1].ToString(), out positionNumber))
            {
                if (positionNumber > 0 && positionNumber < 6)
                {
                    isvalidPositionNumber = true;
                }
                else
                {
                    isvalidPositionNumber = false;
                }
            }
            else
            {
                isvalidPositionNumber = false;
            }

            if (isValidPositionChar && isvalidPositionNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    #endregion
}

