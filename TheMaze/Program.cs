﻿Console.ForegroundColor = ConsoleColor.DarkCyan;
string[,] levelOne = new string[27,50];
int PlayerX = 0, PlayerY = 0;
bool a = true;
int score = 0;

while (true)
{
    levelOne = LevelGenerate(levelOne);
    levelOne = Game(levelOne, "██", a, PlayerX, PlayerY,ref score);
}

string[,] Game(string[,] gameSpace,string player, bool a, int PlayerX, int PlayerY,ref int score)
{
    int startPosX = 0;
    int startPosY = 0;
    while (a)
    {
        Random ran1 = new Random();
        int x = ran1.Next(27);
        int y = ran1.Next(50);
        if (gameSpace[x, y] == "░░")
        {
            PlayerX = x;
            startPosX = x;
            PlayerY = y;
            startPosY = y;
            a = false;
        }
    }
    a = true;
    while (a)
    {
        if (gameSpace[PlayerX,PlayerY] == "▒▒")
        {
            PlayerX = startPosX;
            PlayerY = startPosY;
        }
        for (int i = 0; i < gameSpace.GetLength(0); i++)
        {
            for (int j = 0; j < gameSpace.GetLength(1); j++)
            {
                if (i == PlayerX && j ==PlayerY)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(player);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    Console.Write(gameSpace[i, j]);
                }
            }
            Console.WriteLine();
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("очки: " + score + "  || для перехода на новый уровень найдите дыру || для перезапуска карты нажмите f1  ");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        ConsoleKey key = Console.ReadKey().Key;
        if (key == ConsoleKey.W && gameSpace[PlayerX - 1,PlayerY] != "▓▓")
        {
            PlayerX--;
        }
        if (key == ConsoleKey.S && gameSpace[PlayerX + 1, PlayerY] != "▓▓")
        {
            PlayerX++;
        }
        if (key == ConsoleKey.A && gameSpace[PlayerX, PlayerY - 1] != "▓▓")
        {
            PlayerY--;
        }
        if (key == ConsoleKey.D && gameSpace[PlayerX, PlayerY + 1] != "▓▓")
        {
            PlayerY++;
        }
        if (key == ConsoleKey.F1)
        {
            a = false;
        }
        Console.SetCursorPosition(0, 0);
        if (gameSpace[PlayerX, PlayerY] == "  ")
        {
            PlayerX = startPosX;
            PlayerY = startPosY;
            a = false;
            score+= 1;
        }
    }
    return gameSpace;
}
string[,] LevelGenerate(string[,] gameSpace)
{
    for (int i = 0; i < gameSpace.GetLength(0); i++)
    {
        for (int j = 0; j < gameSpace.GetLength(1); j++)
        {
            Random ran = new Random();
            switch (ran.Next(7))
            {
                case 0:
                    gameSpace[i, j] = "▒▒";
                    break;
                case 1:
                    gameSpace[i, j] = "▓▓";
                    break;
                case 2:
                    gameSpace[i, j] = "░░";
                    break;
                case 3:
                    gameSpace[i, j] = "░░";
                    break;
                case 4:
                    gameSpace[i, j] = "░░";
                    break;
                case 5:
                    gameSpace[i, j] = "░░";
                    break;
                case 6:
                    gameSpace[i, j] = "░░";
                    break;
            }
            if (i == 0||i==26)
            {
                gameSpace[i, j] = "▓▓";
            }
            if (j == 0 || j == 49)
            {
                gameSpace[i, j] = "▓▓";
            }
        }
    }
    bool a =true;
    while (a)
    {
        Random ran1 = new Random();
        int x = ran1.Next(27);
        int y = ran1.Next(50);

        if (gameSpace[x, y] == "░░")
        {
            gameSpace[x, y] = "  ";
            a = false;
        }
    }
    return gameSpace;
}

