namespace BattleShipsLightConsole.Enums;

/// <summary>
/// Represent's status of each player field position.
/// </summary>
public enum GridStatus
{
    Undefined,

    // Empty position
    Empty,

    // Active ship
    Ship,

    // Sunked ship
    Sunk,

    // Missed shot
    Miss
}

