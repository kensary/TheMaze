Console.ForegroundColor = ConsoleColor.DarkCyan;
string[,] levelOne = new string[27,50];
int PlayerX = 0, PlayerY = 0;
bool a = true;
int score = 0;

while (true)
{
    levelOne = LevelGenerate(50,27);
    Game(levelOne, "██", a, PlayerX, PlayerY,ref score);
}

static void Game(string[,] gameSpace,string player, bool a, int PlayerX, int PlayerY,ref int score)
{
    a = true;
    int startPosX = 0;
    int startPosY = 0;
    while (a)
    {
        Random ran1 = new Random();
        int x = ran1.Next(27);
        int y = ran1.Next(50);
        if (gameSpace[x, y] == "  ")
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
        if (gameSpace[PlayerX, PlayerY] == "▒▒")
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
        Console.Write("очки: " + score + "  || для перехода на новый уровень найдите \"▄▀\" || для перезапуска карты нажмите f1  ");
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
        if (gameSpace[PlayerX, PlayerY] == "▄▀")
        {
            PlayerX = startPosX;
            PlayerY = startPosY;
            a = false;
            score+= 1;
        }
    }
}
static void CreatePath(string[,] maze, int x, int y, Random rand)
{
    int[] dirX = { 2, -2, 0, 0 }; // Движения по x
    int[] dirY = { 0, 0, 2, -2 }; // Движения по y
    int[] dirs = { 0, 1, 2, 3 }; // Направления

    // Перемешиваем направления
    for (int i = 0; i < dirs.Length; i++)
    {
        int j = rand.Next(i, dirs.Length);
        int temp = dirs[i];
        dirs[i] = dirs[j];
        dirs[j] = temp;
    }

    // Генерация пути
    foreach (int dir in dirs)
    {
        int newX = x + dirX[dir];
        int newY = y + dirY[dir];

        if (newX > 0 && newY > 0 && newX < maze.GetLength(1) && newY < maze.GetLength(0) && (maze[newY, newX] == "▓▓" || maze[newY, newX] == "▒▒"))
        {
            maze[newY, newX] = "  "; // Открываем путь
            maze[y + dirY[dir] / 2, x + dirX[dir] / 2] = "  "; // Открываем стену между
            CreatePath(maze, newX, newY, rand);
        }
    }
}
string[,] LevelGenerate(int width, int height)
{
    string[,] maze = new string[height, width + 1];

    // Инициализация лабиринта стенами
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            Random ran = new Random();
            switch (ran.Next(3))
            {
                case 0:
                    maze[y, x] = "▓▓"; // Стена
                    break;
                case 1:
                    maze[y, x] = "▒▒"; // Стена
                    break;
                case 2:
                    maze[y, x] = "▒▒"; // Стена
                    break;

            }
        }
    }
    for (int i = 0; i < maze.GetLength(0); i++)
    {
        for (int j = 0; j < maze.GetLength(1); j++)
        {
            if (i == 0 || i == maze.GetLength(0) - 1)
            {
                maze[i, j] = "▓▓";
            }
            if (j == 0 || j == maze.GetLength(1) - 1)
            {
                maze[i, j] = "▓▓";
            }
        }
    }

    // Создание начальной точки
    Random rand = new Random();
    maze[1, 1] = "  "; // Путь
    CreatePath(maze, 1, 1, rand);
    a = true;
    while (a)
    {
        Random ran1 = new Random();
        int x = ran1.Next(25);
        int y = ran1.Next(40);

        if (maze[x, y] == "  ")
        {
            maze[x, y] = "▄▀";
            a = false;
        }
    }
    return maze;
}

