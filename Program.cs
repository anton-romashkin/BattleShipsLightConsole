using BattleShipsLightConsole.Factories;
using BattleShipsLightConsole.Services;

namespace BattleShipsLightConsole;

class Program
{
    static void Main(string[] args)
    {
        var playerOne = PlayerFactory.CreateNewPlayer();
        var playerTwo = PlayerFactory.CreateNewPlayer();

        var gameProcessService = new GameProcessService(playerOne, playerTwo);
        gameProcessService.ShootShipsProcess();
    }
}