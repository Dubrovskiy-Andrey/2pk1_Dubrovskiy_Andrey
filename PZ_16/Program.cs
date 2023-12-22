using System.Collections.Concurrent;
using System;
using System.IO;
using System.Data;
using System.Diagnostics;

namespace PZ_16
{
    internal class Program
    {
        static int mapSize = 25; //размер карты
        static char[,] map = new char[mapSize, mapSize]; //карта
                                                         //координаты на карте игрока
        static int playerY = mapSize / 2;
        static int playerX = mapSize / 2;
        static byte enemies = 5; //количество врагов
        static byte buffs = 5; //количество усилений
        static int health = 5;  // количество аптечек
        static int HP_P = 50;
        static int HP_E = 30;
        static int Damage_P = 10;
        static int Damage_E = 5;
        static int new_count = 0;
        static int count = 0;
        static int boss = 0;
        static int boss_damage = 10;
        static int stage = 0;
        static int boss_health = 60;
        static bool bringbuff = false;
        static bool boss_activate = false;
        static void Start()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(51, 15);
            Console.WriteLine("Начать новую игру (N)");
            Console.SetCursorPosition(50, 16);
            Console.WriteLine("Загрузить сохранение (L)");
            Console.SetCursorPosition(50, 17);
            Console.WriteLine("Покинуть игру (Enter x2)");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.N:
                    Console.SetCursorPosition(0, 0);
                    HP_P = 50;
                    boss_activate = false;
                    stage = 0;
                    count = 0;
                    Reset();
                    GenerationMap();
                    Move();
                    break;
                case ConsoleKey.L:
                    LoadGame();
                    break;
                case ConsoleKey.Enter:
                    Environment.Exit(0);
                    break;
                default:
                    Start();
                    break;
            }
        }

        static void Main(string[] args)
        {
            Start();
        }

        /// <summary>
        /// генерация карты с расставлением врагов, аптечек, баффов
        /// </summary>
        static void GenerationMap()
        {
            Random random = new Random();
            //создание пустой карты
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    map[i, j] = '_';
                }
            }

            map[playerY, playerX] = 'P'; // в чередину карты ставится игрок

            //временные координаты для проверки занятости ячейки
            int x;
            int y;
            //добавление врагов
            while (enemies > 0)
            {
                x = random.Next(0, mapSize - 1);
                y = random.Next(1, mapSize - 1);

                //если ячейка пуста  - туда добавляется враг
                if (map[x, y] == '_')
                {
                    map[x, y] = 'E';
                    enemies--; //при добавлении врагов уменьшается количество нерасставленных врагов
                }
            }
            //добавление баффов
            while (buffs > 0)
            {
                x = random.Next(0, mapSize - 1);
                y = random.Next(1, mapSize - 1);

                if (map[x, y] == '_')
                {
                    map[x, y] = 'B';
                    buffs--;
                }
            }
            //добавление аптечек
            while (health > 0)
            {
                x = random.Next(0, mapSize - 1);
                y = random.Next(1, mapSize - 1);

                if (map[x, y] == '_')
                {
                    map[x, y] = 'H';
                    health--;
                }
            }




            UpdateMap(); //отображение заполненной карты на консоли
        }
        /// <summary>
        /// перемещение персонажа
        /// </summary>
        static void Move()
        {
            //предыдущие координаты игрока
            int playerOldY;
            int playerOldX;

            while (true)
            {
                playerOldX = playerX;
                playerOldY = playerY;
                Console.CursorVisible = false; //скрытный курсов
                Console.SetCursorPosition(0, 26);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Количество шагов: {count}    ");
                Console.SetCursorPosition(0, 27);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Нр: {HP_P}     ");
                Console.SetCursorPosition(0, 28);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Dmg: {Damage_P}      ");
                Console.SetCursorPosition(40, 0);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Враги имеют 30 хп и 5 урона");
                Console.SetCursorPosition(40, 1);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Для победы одержите верх над всеми врагами");
                Console.SetCursorPosition(40, 2);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Дейстаие баффа распространяется на 25 шагов и увеличивыет урон игрока в 2 раза");
                if (boss_activate == true)
                {
                    Console.SetCursorPosition(40, 5);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"Внимание! Босс имеет слишком много здоровья!");
                    Console.SetCursorPosition(40, 6);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"Так что рекомендуем собрать усиления перед битвой с ним");
                    Console.SetCursorPosition(40, 7);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"А также восполнить своё здоровье до максимума");
                    Console.SetCursorPosition(40, 8);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"(Подсказка!) Для убийства босса необходимо также убить и его преспешников");
                    Console.SetCursorPosition(40, 11);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("ХП босса: [/////////////////////]");
                    if (stage == 1)
                    {
                        Console.SetCursorPosition(40, 11);
                        Console.WriteLine("ХП босса: [//////////////       ]");
                    }
                    if (stage == 2)
                    {
                        Console.SetCursorPosition(40, 11);
                        Console.WriteLine("ХП босса: [///////              ]");
                    }
                    if (stage == 3)
                    {
                        Console.SetCursorPosition(40, 11);
                        Console.WriteLine("ХП босса: [                     ]");
                    }

                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                //смена координат в зависимости от нажатия клавиш

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        playerX--;
                        if (playerX < 0)
                        {
                            playerX = 0;
                        }

                        break;
                    case ConsoleKey.DownArrow:
                        playerX++;
                        if (playerX >= mapSize)
                        {
                            playerX = mapSize - 1;
                        }

                        break;
                    case ConsoleKey.LeftArrow:
                        playerY--;
                        if (playerY < 0)
                        {
                            playerY = 0;
                        }

                        break;
                    case ConsoleKey.RightArrow:
                        playerY++;
                        if (playerY >= mapSize)
                        {
                            playerY = mapSize - 1;
                        }
                        break;
                    case ConsoleKey.Escape:
                        Pause();
                        break;


                }
                Fight();
                BuffUp();
                AidAup();
                BossFight();




                //предыдущее положение игрока затирается
                Console.ForegroundColor = ConsoleColor.Yellow;
                map[playerOldX, playerOldY] = '_';
                Console.SetCursorPosition(playerOldY, playerOldX);
                Console.Write('_');
                //обновленное положение игрока

                map[playerX, playerY] = 'P';
                Console.SetCursorPosition(playerY, playerX);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write('P');
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (playerX != playerOldX || playerY != playerOldY)
                {

                    count++;
                }
            }
        }
        /// <summary>
        /// вывод карты на консоль
        /// </summary>
        static void UpdateMap()
        {
            Console.Clear();
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i, j] == 'B')
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else if (map[i, j] == 'H')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (map[i, j] == 'E')
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (map[i, j] == 'P')
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    else if (map[i, j] == '_')
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (map[i, j] == 'K')
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(map[i, j]);

                }
                Console.WriteLine(map[i, 0]);
            }
        }
        static void BossFight()
        {

            if (map[playerX, playerY] == 'K')
            {


                while (HP_P > 0 && boss_health > 0)
                {
                    HP_P -= boss_damage;
                    boss_health -= Damage_P;
                    if (HP_P <= 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(55, 15);
                        Console.WriteLine("Вы проиграли!");
                        Console.SetCursorPosition(46, 16);
                        Console.WriteLine($"Ваше путешествие заняло {count} шагов");
                        Console.SetCursorPosition(46, 17);
                        Console.WriteLine("Для начала новой игры нажмите N");
                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.N:
                                Console.SetCursorPosition(0, 0);
                                Console.Clear();
                                boss_activate = false;
                                HP_P = 50;
                                stage = 0;
                                count = 0;
                                Reset();
                                GenerationMap();
                                Move();
                                break;
                        }
                    }
                }
                boss_health = 60;
                map[playerX, playerY] = '_';
                if (HP_P > 0)
                {
                    stage++;
                    boss++;
                    if (boss == 1 && enemies == 3)
                    {
                        Console.Clear();
                        Reset();
                        boss = 1;
                        Boss();
                        if (stage == 3)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(46, 15);
                            Console.WriteLine("Вы прошли игру!!! Поздравляем!");
                            Console.SetCursorPosition(46, 16);
                            Console.WriteLine($"Ваше путешествие заняло {count} шагов");
                            Console.SetCursorPosition(46, 17);
                            Console.WriteLine("Для начала новой игры нажмите N");
                            switch (Console.ReadKey().Key)
                            {
                                case ConsoleKey.N:
                                    Console.SetCursorPosition(0, 0);
                                    Console.Clear();
                                    stage = 0;
                                    boss_activate = false;
                                    count = 0;
                                    HP_P = 50;
                                    Reset();
                                    GenerationMap();
                                    Move();
                                    break;
                            }
                        }
                    }
                    else // Анимация боя
                    {
                        for (int i = 0; i < 3; i++) // Перебор символов анимации
                        {
                            Console.SetCursorPosition(playerY, playerX);
                            Console.Write('|');
                            Thread.Sleep(60);
                            Console.SetCursorPosition(playerY, playerX);
                            Console.Write('/');
                            Thread.Sleep(60);
                            Console.SetCursorPosition(playerY, playerX);
                            Console.Write('-');
                            Thread.Sleep(60);
                        }
                    }
                }
            }
        }
        static void Fight()
        {
            if (map[playerX, playerY] == 'E')
            {
                while (HP_P > 0 && HP_E > 0)
                {
                    HP_P -= Damage_E;
                    HP_E -= Damage_P;
                    if (HP_P <= 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(55, 15);
                        Console.WriteLine("Вы проиграли!");
                        Console.SetCursorPosition(46, 16);
                        Console.WriteLine($"Ваше путешествие заняло {count} шагов");
                        Console.SetCursorPosition(46, 17);
                        Console.WriteLine("Для начала новой игры нажмите N");
                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.N:
                                Console.SetCursorPosition(0, 0);
                                Console.Clear();
                                stage = 0;
                                boss_activate = false;
                                count = 0;
                                HP_P = 50;
                                Reset();
                                GenerationMap();
                                Move();
                                break;
                        }
                    }
                }
                HP_E = 30;
                map[playerX, playerY] = '_';

                if (HP_P > 0)
                {
                    enemies++;
                    if (enemies == 5)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.SetCursorPosition(53, 15);
                        Console.WriteLine("Босс сейчас появится.");
                        Console.SetCursorPosition(45, 16);
                        Console.WriteLine("У него будет 60 здоровья и 10 урона!");
                        Console.SetCursorPosition(50, 17);
                        Console.WriteLine("Нажмите N для начала битвы!");
                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.N:
                                Console.Clear();
                                Reset();
                                boss_activate = true;
                                boss = 1;
                                BossFight();
                                Boss();
                                Move();
                                break;
                        }
                    }
                    else // Анимация боя
                    {
                        for (int i = 0; i < 3; i++) // Перебор символов анимации
                        {
                            Console.SetCursorPosition(playerY, playerX);
                            Console.Write('|');
                            Thread.Sleep(60);
                            Console.SetCursorPosition(playerY, playerX);
                            Console.Write('/');
                            Thread.Sleep(60);
                            Console.SetCursorPosition(playerY, playerX);
                            Console.Write('-');
                            Thread.Sleep(60);
                        }
                    }
                    if (boss == 1 && enemies == 3)
                    {
                        Console.Clear();
                        Reset();
                        Boss();
                        if (stage == 3)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(47, 15);
                            Console.WriteLine("Вы прошли игру!!! Поздравляем!");
                            Console.SetCursorPosition(46, 16);
                            Console.WriteLine($"Ваше путешествие заняло {count} шагов");
                            Console.SetCursorPosition(47, 17);
                            Console.WriteLine("Для начала новой игры нажмите N");
                            switch (Console.ReadKey().Key)
                            {
                                case ConsoleKey.N:
                                    Console.SetCursorPosition(0, 0);
                                    Console.Clear();
                                    HP_P = 50;
                                    boss_activate = false;
                                    stage = 0;
                                    count = 0;
                                    Reset();
                                    GenerationMap();
                                    Move();
                                    break;
                            }
                        }
                    }
                }
            }
        }
        static void BuffUp() // Логика баффов
        {
            if (map[playerX, playerY] == 'B')
            {
                bringbuff = true;
                new_count = count; //сохранение шага на котором взят бафф
                Damage_P = Damage_P * 2;
                map[playerX, playerY] = '_'; // Решение проблемы "фантомного элемента"

                for (int i = 0; i < 3; i++) // Перебор символов анимации
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(playerY, playerX);
                    Console.Write('+');
                    Thread.Sleep(120);
                    Console.SetCursorPosition(playerY, playerX);
                    Console.Write('x');
                    Thread.Sleep(120);
                    Console.ForegroundColor = ConsoleColor.Yellow;

                }
            }
            if (new_count == count - 25) // Расчитан на 25 шагов
            {
                bringbuff = false;
                Damage_P = 10; // Возврат к изначальному урону 
            }
        }
        static void AidAup()
        {
            if (map[playerX, playerY] == 'H')
            {

                HP_P = 50;
                map[playerX, playerY] = '_';


                for (int i = 0; i < 3; i++) // Перебор символов анимации
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(playerY, playerX);
                    Console.Write('+');
                    Thread.Sleep(120);
                    Console.SetCursorPosition(playerY, playerX);
                    Console.Write('x');
                    Thread.Sleep(120);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
            }
        }
        static void SaveGame() // Сохранение
        {
            string path = "save.txt"; // Создание текстового файла
            using (StreamWriter writer = new StreamWriter(path)) // Запись в него параметров
            {
                writer.WriteLine($"playerX={playerX}");
                writer.WriteLine($"playerY={playerY}");
                writer.WriteLine($"playerHP={HP_P}");
                writer.WriteLine($"Damage_P={Damage_P}");
                writer.WriteLine($"playerStepCount={count}");
                writer.WriteLine($"enemyHP={HP_E}");
                writer.WriteLine($"hasBuff={buffs}");
                writer.WriteLine($"buffStep={new_count}");

                for (int i = 0; i < mapSize; i++) // Запись карты
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        if (map[i, j] == 'P')
                        {
                            map[i, j] = '_';
                        }
                        writer.Write(map[i, j]);
                    }
                    writer.WriteLine();
                }
            }
        }
        static void Pause()
        {
            SaveGame();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(55, 15);
            Console.WriteLine("ВВы хотите выйти?");
            Console.SetCursorPosition(55, 16);
            Console.WriteLine("Esc - вернуться");
            Console.SetCursorPosition(55, 17);
            Console.WriteLine("Enter - сохранить и выйти");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Escape:
                    LoadGame();
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    Start();
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        static void LoadGame() // Загрузка
        {
            string path = "save.txt"; // Путь

            if (File.Exists(path)) // Если существует
            {
                string[] lines = File.ReadAllLines(path); // Передача файлов с документа в игру

                if (lines.Length >= mapSize)
                {
                    if (int.TryParse(lines[0].Split('=')[1], out int loadedPlayerX) &&
                    int.TryParse(lines[1].Split('=')[1], out int loadedPlayerY) &&
                    int.TryParse(lines[2].Split('=')[1], out int loadedHP_P) &&
                    int.TryParse(lines[3].Split('=')[1], out int loadedDamage_P) &&
                    int.TryParse(lines[4].Split('=')[1], out int loadedcount) &&
                    int.TryParse(lines[5].Split('=')[1], out int loadedHP_E) &&
                    bool.TryParse(lines[6].Split('=')[1], out bool loadedHasbuffs) &&
                    int.TryParse(lines[7].Split('=')[1], out int loadednew_count))
                    {
                        playerX = loadedPlayerX;
                        playerY = loadedPlayerY;
                        HP_P = loadedHP_P;
                        Damage_P = loadedDamage_P;
                        count = loadedcount;
                        new_count = loadednew_count;
                        HP_E = loadedHP_E;
                        bringbuff = loadedHasbuffs;

                        Console.Clear();
                        for (int i = 0; i < mapSize; i++)
                        {
                            for (int j = 0; j < mapSize; j++)
                            {
                                map[i, j] = lines[i + 8][j];
                                if (map[i, j] == 'B')
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                else if (map[i, j] == 'H')
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else if (map[i, j] == 'E')
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else if (map[i, j] == 'P')
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                else if (map[i, j] == '_')
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                else
                                    Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(map[i, j]);
                            }
                            Console.WriteLine();
                        }
                    }
                    UpdateMap(); //Вывод на консоль
                }
                else
                {
                    Console.WriteLine("Ошибка чтения файла сохранения.");
                }
            }
            else
            {
                Console.WriteLine("Файл сохранения не найден.");
            }
        }
        static void Reset()
        {
            health = 5;
            buffs = 5;
            enemies = 5;
            Damage_P = 10;
            HP_E = 30;
            new_count = 0;
            playerY = mapSize / 2;
            playerX = mapSize / 2;
            boss_health = 60;
            boss = 0;
        }
        static void Boss()
        {
            boss = 1;
            Random random = new Random();
            //создание пустой карты
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    map[i, j] = '_';
                }
            }

            map[playerY, playerX] = 'P'; // в чередину карты ставится игрок

            //временные координаты для проверки занятости ячейки
            int x;
            int y;
            //добавление врагов
            while (enemies > 2)
            {
                x = random.Next(0, mapSize - 1);
                y = random.Next(1, mapSize - 1);

                //если ячейка пуста  - туда добавляется враг
                if (map[x, y] == '_')
                {
                    map[x, y] = 'E';
                    enemies--; //при добавлении врагов уменьшается количество нерасставленных врагов
                    if (enemies == 2)
                    {
                        enemies = 0;
                    }
                }
            }
            while (boss > 0)
            {
                x = random.Next(0, mapSize - 1);
                y = random.Next(1, mapSize - 1);

                //если ячейка пуста  - туда добавляется враг
                if (map[x, y] == '_')
                {
                    map[x, y] = 'K';
                    boss--; //при добавлении врагов уменьшается количество нерасставленных врагов
                }
            }
            //добавление баффов
            while (buffs > 0)
            {
                x = random.Next(0, mapSize - 1);
                y = random.Next(1, mapSize - 1);
                if (map[x, y] == '_')
                {
                    map[x, y] = 'B';
                    buffs--;
                }
            }
            //добавление аптечек
            while (health > 0)
            {
                x = random.Next(0, mapSize - 1);
                y = random.Next(1, mapSize - 1);

                if (map[x, y] == '_')
                {
                    map[x, y] = 'H';
                    health--;
                }
            }
            UpdateMap(); //отображение заполненной карты на консоли
        }
    }
}
