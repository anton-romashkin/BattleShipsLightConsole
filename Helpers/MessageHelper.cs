namespace BattleShipsLightConsole.Helpers;

public static class MessageHelper
{
    #region Public Methods

    /// <summary>
    /// Displays text in the console. Asks for a button to be pressed
    /// </summary>
    /// <param name="message">Text to display</param>
    public static void ShowMessage(string message)
    {
        Console.Clear();

        Console.WriteLine(message);

        Console.WriteLine();

        ReadKeyPress(ConsoleKey.Enter);

        Console.Clear();
    }

    /// <summary>
    /// Asks for a button to be pressed. Used in <see cref="ShowMessage(string)"/ method>
    /// </summary>
    /// <param name="key"></param>
    public static void ReadKeyPress(ConsoleKey key)
    {
        ConsoleKeyInfo keyInfo;

        Console.WriteLine($"Press {key} to continue");

        do
        {
            while (Console.KeyAvailable == false)
                Thread.Sleep(250);

            keyInfo = Console.ReadKey(true);
        } while (keyInfo.Key != key);
    }

    #endregion
}

