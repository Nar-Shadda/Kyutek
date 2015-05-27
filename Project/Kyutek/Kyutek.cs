using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Media;

namespace Kyutek
{
    class Kyutek
    {
        const int WindowHeight = 36;
        public const string typewriterPath = @"audio/typewriter.wav";
        public static double difficultyMultiplier;
        static void Main()
        {

            //set window size and buffer size
            Console.WindowHeight = WindowHeight;
            Console.BufferHeight = WindowHeight;
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            Console.ForegroundColor = ConsoleColor.Gray;
            
            //initialize random generator
            Random rng = new Random();

            //intro
            PlayIntro();

            //choose difficulty
            ClearScreen();
            ChooseDifficulty();

            //part 1 - introduction
            ClearScreen();
            Console.SetCursorPosition(0, 13);
            string tempName = StoryIntroduction();

            //choose and create a character
            Hero player = CreateCharacter();

            //shortcut
            if (tempName == "SoftUniRocks")
            {
                player.Name = tempName;
                BattleReward(player);
                BattleReward(player);
                goto shortCut;
            }

            //choose a name
            Console.SetCursorPosition(0, 13);
            PrintText(String.Format("Честито! Вече си дипломиран {0}.", player.HeroClass));
            PrintText("Сега, когато вече си нов човек, е време да си избереш и ново име...");
            PrintText(String.Format("Тоя път гледай да измислиш нещо по-добро от '{0}'!", tempName));

            Console.CursorVisible = true;

            player.Name = Console.ReadLine();
            Console.WriteLine("И така, {0}, време е да да се разкършим!", player.Name); /* to save name in cyrillic use input encoding Unicode*/

            //story - 3 (going to the bar, first interaction) 
            ClearScreen();
            Console.SetCursorPosition(0, 13);
            PrintTextFromFile(@"text-files/story/story-3.txt");

            //first battle
            ClearScreen();
            Battle(player, new Enemy(1), rng);
            ClearScreen();
            Console.SetCursorPosition(0, 13);
            RegenerateLife(player);
            BattleReward(player);

            //story - 4
            ClearScreen();
            Console.SetCursorPosition(0, 13);
            PrintTextFromFile(@"text-files/story/story-4.txt");

            //second battle
            ClearScreen();
            Battle(player, new Enemy(2), rng);
            ClearScreen();
            Console.SetCursorPosition(0, 13);
            RegenerateLife(player);
            BattleReward(player);

            //story - 5
            ClearScreen();
            Console.SetCursorPosition(0, 13);
            PrintTextFromFile(@"text-files/story/story-5.txt");
            //taking a rest
            Thread.Sleep(2500);
            ClearScreen();
            RegenerateLife(player);
            PrintTextFromFile(@"text-files/story/story-51.txt");

            //third battle
            ClearScreen();
            Battle(player, new Enemy(3), rng);
            ClearScreen();
            RegenerateLife(player);

            //shortcut
            shortCut:

            //secret encounter
            ClearScreen();
            Console.SetCursorPosition(0, 13);
            PrintTextFromFile(@"text-files/story/story-6.txt");
            Console.WriteLine("Y/N");

            string choice = Console.ReadLine().ToLower();
            while (choice != "y" && choice != "n")
            {
                Console.Write('\r');
                Console.Write("Опитай пак.");
                Console.Write('\r');
                choice = Console.ReadLine().ToLower();
            }
            if (choice == "y")
            {
                int chance = rng.Next(10);
                if (chance == 3 || chance == 6 || chance == 9)
                {
                    PrintText("Намери бъклицата на дядо. Ще я изпиеш ли?");
                    Console.WriteLine("Y/N");

                    string drink = Console.ReadLine().ToLower();
                    if (drink == "y")
                    {
                        PrintText("Мъдро решение! Няма нищо по-добро от отлежала гроздова!");
                        PrintText("Усещаш мощен прилив на енергия. Получаваш допълнително 15 сила.");
                        player.MinDmg += 15;
                        player.MaxDmg += 15;
                    }
                }

                else if (chance == 2 || chance == 4 || chance == 8)
                {
                    PrintText("Намираш много запазено пардесю! Пробваш го и установяваш, че ти седи чудесно!");
                    PrintText("Получаваш допълнително 30 точки живот.");
                    player.MaxLife += 30;
                    player.CurrentLife = player.MaxLife;
                }
                else if (chance == 0)
                {
                    PrintText("Навеждаш се, за да огледаш хубаво какво има в дупката и изведнъж");
                    PrintText("някой ти бърка в окото 'Кво зяпаш бе, тюфлек!'");
                    PrintText("Губиш 5 точки живот. Другият път може би е по-добре \nда не се вреш в чуждите работи.");
                    player.CurrentLife -= 5;
                }
                else
                {
                    PrintText("Драсваш клечка кибрит и оглеждаш съдържанието на дупката.");
                    PrintText("За съжаление намираш само старо списание със слепени страници и шишенце вазелин.");
                    PrintText("Помисляш си 'Каква странна комбинация' и продължаваш по пътя си.");
                }
            }

            //secret encounter before battle reward due to text dependancy
            BattleReward(player);

            //right before the boss
            ClearScreen();
            Console.SetCursorPosition(0, 13);
            PrintTextFromFile(@"text-files/story/story-7.txt");

            //final battle
            ClearScreen();
            Battle(player, new Enemy(4), rng);
            ClearScreen();
            Console.SetCursorPosition(0, 13);
            PrintTextFromFile(@"text-files/story/story-8.txt");

            //conversation with gitsa
            ClearScreen();
            ConversationWithGitsa(player);

            //the end
            TheEnd();

            // outro (credits)
            PrintTextFromFile(@"text-files/intro-outro/outro.txt");
        }

        private static void ClearScreen()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.CursorVisible = false;
            Thread.Sleep(1500);
            Console.Clear();
        }

        private static void ChooseDifficulty()
        {
            Console.SetCursorPosition(20, 12);
            Console.WriteLine("Избери трудност:");
            Console.SetCursorPosition(20, 14);
            Console.WriteLine("[1.Пълен айляк]");
            Console.SetCursorPosition(20, 16);
            Console.WriteLine("[2.Нек'во нормално]");
            Console.SetCursorPosition(20, 18);
            Console.WriteLine("[3.Като да се вредиш за автограф от Милко]");
            Console.SetCursorPosition(20, 20);
            Console.Write("Какво избираш? ");
            Console.CursorVisible = true;
            string difficultyChoice = Console.ReadLine();
            Console.CursorVisible = false;

            //set difficulty multiplier according to choice
            switch (difficultyChoice)
            {
                case "1":
                    difficultyMultiplier = 0.5;
                    break;
                case "2":
                    difficultyMultiplier = 1;
                    break;
                case "3":
                    difficultyMultiplier = 1.5;
                    break;
            }
        }

        private static void PlayIntro()
        {
            SoundPlayer player = new SoundPlayer(@"audio/intro.wav");

            //team nar-shadda presents
            player.Play();
            PrintAsciiText(@"text-files/intro-outro/team.txt");
            PrintAsciiText(@"text-files/intro-outro/nar-shadda.txt");
            PrintAsciiText(@"text-files/intro-outro/presents.txt");
            Thread.Sleep(1000);
            player.Stop();

            //game name
            player = new SoundPlayer(@"audio/vynil-rew.wav");
            player.Play();
            PrintAsciiText(@"text-files/intro-outro/kyutek.txt", false);
            player.Stop();
            Console.SetCursorPosition(28, 22);
            PrintTextFromFile(@"text-files/intro-outro/quest.txt");

            //press enter to continue
            while (true)
            {
                if (Console.KeyAvailable) //needs testing, doesn't work properly
                {
                    ConsoleKeyInfo key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        break;
                    }
                }
                Console.SetCursorPosition(33, 24);
                Console.WriteLine("press enter");
                Thread.Sleep(500);
                Console.SetCursorPosition(33, 24);
                Console.WriteLine("           ");
                Thread.Sleep(500);
            }
        }

        private static string StoryIntroduction()
        {
            Console.SetCursorPosition(0, 10);
            PrintTextFromFile(@"text-files/story/story-1.txt");
            Console.CursorVisible = true;
            string tempName = Console.ReadLine();
            Console.CursorVisible = false;
            Thread.Sleep(750);
            Console.Clear();
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Браааат...");
            Thread.Sleep(500);
            PrintTextFromFile(@"text-files/story/story-2.txt");
            Console.WriteLine();
            return tempName;
        }

        static Hero CreateCharacter()
        {
            ClearScreen();

            //choose hero class
            Console.SetCursorPosition(30, 12);
            Console.WriteLine("Choose your destiny!!!");
            Console.WriteLine();
            Console.SetCursorPosition(10, 14);
            Console.WriteLine("[(1) Чекръкчийство]   [(2) Кражби и убийства]   [(3) Мистика]");
            Console.SetCursorPosition(40, 16);
            Console.CursorVisible = true;

            //read input and create hero according to choice
            string choice = Console.ReadLine();

            while (choice != "1" && choice != "2" && choice != "3")
            {
                Console.CursorVisible = false;

                Console.SetCursorPosition(30, 18);
                Console.WriteLine("Нема такъв, пробвай пак!");
                Thread.Sleep(1500);
                Console.SetCursorPosition(30, 18);
                Console.Write("                        ");
                Console.SetCursorPosition(40, 16);

                Console.CursorVisible = true;

                choice = Console.ReadLine();
            }

            Console.CursorVisible = false;

            // create hero
            Hero player = new Hero(choice);

            string[] heroClass = File.ReadAllLines(@"text-files/text/class.txt");

            ClearScreen();
            Console.SetCursorPosition(0, 10);
            switch (choice)
            {
                case "1":
                    PrintText(heroClass[0]);
                    break;
                case "2":
                    PrintText(heroClass[1]);
                    break;
                case "3":
                    PrintText(heroClass[2]);
                    break;
            }

            return player;
        }

        static void PrintAsciiText(string textPath, bool clearScreen = true)
        {
            StreamReader reader = new StreamReader(textPath);
            using (reader)
            {
                string[] fileContents = File.ReadAllLines(textPath);
                int numberOfLines = fileContents.Length;
                int posX = (Console.BufferWidth - fileContents[0].Length) / 2;
                int posY = (WindowHeight - numberOfLines) / 2;


                while (true)
                {

                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    Console.SetCursorPosition(posX, posY);
                    Console.WriteLine(line);
                    posY++;
                }
            }
            Thread.Sleep(2000);
            if (clearScreen)
            {
                Console.Clear();
            }
        }

        static void PrintText(string text)
        {
            SoundPlayer player = new SoundPlayer(typewriterPath);

            player.Play();
            foreach (char letter in text)
            {
                Console.Write(letter);
                Thread.Sleep(35);
            }
            player.Stop();
            Console.WriteLine();
            Thread.Sleep(400);
        }

        static void PrintTextFromFile(string textPath, bool lineNumbers = false)
        {
            StreamReader reader = new StreamReader(textPath);

            using (reader)
            {
                int lineCount = 1;
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    //print line
                    if (lineNumbers)
                    {
                        PrintText(String.Format("{0}. {1}", lineCount, line));
                        lineCount++;
                    }
                    else
                    {
                        PrintText(line);
                    }
                }
            }
        }

        static void PrintDrawing(string path)
        {
            string[] drawing = File.ReadAllLines(path);
            //startIndex and startRow to ensure picture is centered
            int startIndex = (Console.WindowWidth - drawing[0].Length) / 2;
            int startRow = (WindowHeight - drawing.Length) / 2;

            foreach (var line in drawing)
            {
                Console.SetCursorPosition(startIndex, startRow);
                Console.WriteLine(line);
                startRow++;
            }
        }

        static int GetDrawingHeigth(string path)
        {
            string[] drawing = File.ReadAllLines(path);

            return (WindowHeight - drawing.Length) / 2 + drawing.Length;
        }

        static void PrintDrawing(string path, Random rng)
        {
            string[] drawing = File.ReadAllLines(path);
            int startIndex = (Console.WindowWidth - drawing[0].Length) / 2;
            int startRow = 3;
            foreach (var item in drawing)
            {
                Console.SetCursorPosition(startIndex, startRow);
                Console.WriteLine(item);
                startRow++;
            }
            PrintRandomLine(@"text-files/text/taunts.txt", startIndex, startRow + 1, rng);
        }

        static void PrintRandomLine(string textPath, int startIndex, int startRow, Random rng)
        {
            // save file contents as string array
            string[] text = File.ReadAllLines(textPath);

            // roll random number between 0 and array length (note! upper boundary is exclusive)
            int index = rng.Next(text.Length);

            // declare a string variable to hold the random element from the array
            string currentText = text[index];

            // print the string on the console using PrintText method
            Console.SetCursorPosition(startIndex, startRow);
            foreach (char letter in currentText)
            {
                if (letter == '+')
                {
                    Console.WriteLine();
                    Console.SetCursorPosition(startIndex, ++startRow);
                }
                Console.Write(letter);
            }
        }

        static void PrintRandomLine(string textPath, Random rng)
        {
            // save file contents as string array
            string[] text = File.ReadAllLines(textPath);

            // roll random number between 0 and array length (note! upper boundary is exclusive)
            int index = rng.Next(text.Length);

            // declare a string variable to hold the random element from the array
            string currentText = text[index];

            // print the string on the console using PrintText method
            int startIndex = (Console.WindowWidth - currentText.Length) / 2;
            Console.SetCursorPosition(startIndex, WindowHeight/2);
            PrintText(currentText);
        }

        static void GameOver(Random rng)
        {
            ClearScreen();
            PrintRandomLine(@"text-files/text/lose.txt", rng);

            ClearScreen();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            PrintAsciiText(@"text-files/text/game-over.txt", false);
            Console.ForegroundColor = ConsoleColor.Gray;

            Thread.Sleep(3000);
            Environment.Exit(0);
        }

        static void Victory(Random rng)
        {
            ClearScreen();
            Console.ForegroundColor = ConsoleColor.Green;
            
            PrintRandomLine(@"text-files/text/victory.txt", rng);

            ClearScreen();
            Console.ForegroundColor = ConsoleColor.Green;
            PrintAsciiText(@"text-files/text/win.txt");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void TheEnd()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintAsciiText(@"text-files/story/the-end.txt");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void Battle(Hero player, Enemy enemy, Random rng)
        {
            ClearScreen();
            PrintDrawing(player.DrawingPath, rng);
            Thread.Sleep(500);
            ClearScreen();
            PrintDrawing(enemy.DrawingPath, rng);

            ClearScreen();
            Console.SetCursorPosition(5, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Команди: hit, stun (чекръкчия), double (тарикат), heal (ельоменат)");
            Console.ForegroundColor = ConsoleColor.Gray;
            PrintDrawing(@"text-files/drawings/battle.txt");
            int firstToHit = rng.Next(2);
            bool isPlayer = firstToHit == 1 ? true : false;

            Console.WriteLine();

            //battle text should start below picture and continue untill end of screen
            //if screen end is reached, continue from top
            int drawingHeight = GetDrawingHeigth(@"text-files/drawings/battle.txt");
            int eventRow = (WindowHeight - drawingHeight) / 2 + drawingHeight - 2;
            int talkRow = eventRow + 1;
            int commandRow = eventRow + 2;

            bool isStunnedEnemy = false;

            while (true)
            {
                PrintLifeLeft(player, enemy);
                
                for (int i = 0; i < 2; i++)
                {
                    Thread.Sleep(500);
                    if (isPlayer)
                    {
                        ClearRows(eventRow, commandRow);
                        Console.CursorVisible = true;
                        Console.SetCursorPosition(2, commandRow);
                        string command = Console.ReadLine();

                        while (command != "hit" && command != player.SpecialAttack)
                        {
                            ClearRows(eventRow, commandRow);
                            Console.SetCursorPosition(2, commandRow);
                            Console.Write("Ко е туй?!?");
                            ClearRows(eventRow, commandRow);
                            Console.SetCursorPosition(2, commandRow);
                            command = Console.ReadLine();
                        }
                        Console.CursorVisible = false;

                        if (command == "hit")
                        {
                            //check if player hits (10% chance to miss)
                            int chanceToHit = rng.Next(10);
                            if (chanceToHit == 0)
                            {
                                Thread.Sleep(500);

                                ClearRows(eventRow, commandRow);
                                Console.SetCursorPosition(2, eventRow);
                                Console.WriteLine("Замахваш и пропускаш... кофти.");
                                isPlayer = !isPlayer;
                                continue;
                            }
                            else
                            {
                                Thread.Sleep(500);
                                //calculate dmg between hero min and max dmg
                                int damage = rng.Next(player.MinDmg, player.MaxDmg + 1);

                                ClearRows(eventRow, commandRow);
                                Console.SetCursorPosition(2, eventRow);
                                Console.WriteLine("Чудесен удар. {0} губи {1} точки живот.", enemy.Name, damage);

                                enemy.CurrentLife -= damage;

                                //check if you kill the enemy
                                if (!enemy.IsAlive())
                                {
                                    Thread.Sleep(500);
                                    Victory(rng);
                                    return;
                                }

                                //check if player will say something
                                int saySomething = rng.Next(2);
                                if (saySomething == 1)
                                {
                                    Thread.Sleep(500);
                                    Console.SetCursorPosition(2, talkRow);
                                    Console.Write("{0}: ", player.Name);
                                    PrintRandomLine(@"text-files/text/infight.txt", 4 + player.Name.Length, talkRow, rng);
                                }

                                //change player
                                isPlayer = !isPlayer;
                            }
                        }

                        else
                        {
                            switch (command)
                            {
                                case "stun":
                                    int chanceToStun = rng.Next(10);
                                    if (chanceToStun > 5)
                                    {
                                        isStunnedEnemy = true;
                                        int damage = rng.Next(player.MinDmg, player.MaxDmg + 1) / 2;

                                        ClearRows(eventRow, commandRow);
                                        Console.SetCursorPosition(2, eventRow);
                                        Console.WriteLine("Успешна атака. {0} е зашеметен и губи {1} точки живот.", enemy.Name, damage);

                                        enemy.CurrentLife -= damage;
                                        if (!enemy.IsAlive())
                                        {
                                            Thread.Sleep(500);
                                            Victory(rng);
                                            return;
                                        }
                                        //change player
                                        isPlayer = !isPlayer;
                                    }
                                    else
                                    {
                                        ClearRows(eventRow, commandRow);
                                        Console.SetCursorPosition(2, eventRow);
                                        Console.WriteLine("Неуспешна атака, пропускаш хода си.");
                                        //change player
                                        isPlayer = !isPlayer;
                                        continue;
                                    }
                                    break;

                                case "double":
                                    int chanceForDouble = rng.Next(10);
                                    if (chanceForDouble > 5)
                                    {
                                        int damage = rng.Next(player.MinDmg, player.MaxDmg + 1) * 2;

                                        ClearRows(eventRow, commandRow);
                                        Console.SetCursorPosition(2, eventRow);
                                        Console.WriteLine("Успешна двойна атака! {0} губи {1} точки живот.", enemy.Name, damage);

                                        enemy.CurrentLife -= damage;
                                        if (!enemy.IsAlive())
                                        {
                                            Thread.Sleep(500);
                                            Victory(rng);
                                            return;
                                        }
                                        //change player
                                        isPlayer = !isPlayer;
                                    }
                                    else
                                    {
                                        ClearRows(eventRow, commandRow);
                                        Console.SetCursorPosition(2, eventRow);
                                        Console.WriteLine("Неуспешна атака, пропускаш хода си.");
                                        //change player
                                        isPlayer = !isPlayer;
                                        continue;
                                    }
                                    break;

                                case "heal":
                                    int chanceForHeal = rng.Next(10);
                                    if (chanceForHeal > 6)
                                    {
                                        ClearRows(eventRow, commandRow);
                                        Console.SetCursorPosition(2, eventRow);
                                        Console.WriteLine("Успех! Точките ти живот са възстановени напълно!");
                                        player.CurrentLife = player.MaxLife;
                                        //change player
                                        isPlayer = !isPlayer;
                                        continue;
                                    }
                                    else
                                    {
                                        ClearRows(eventRow, commandRow);
                                        Console.SetCursorPosition(2, eventRow);
                                        Console.WriteLine("Заклинанието ти е неуспешно. Пропускаш хода си.");
                                        //change player
                                        isPlayer = !isPlayer;
                                        continue;
                                    }
                                    break;
                            }
                        }

                    }
                    else
                    {
                        if (isStunnedEnemy)
                        {
                            ClearRows(eventRow, commandRow);
                            Console.SetCursorPosition(2, eventRow);
                            Console.WriteLine("{0} е зашеметен и пропуска хода си.", enemy.Name);
                            isStunnedEnemy = false;
                            isPlayer = !isPlayer;
                            continue;
                        }

                        // slightly lower chance to hit for enemies
                        int chanceToHit = rng.Next(9);
                        if (chanceToHit == 0)
                        {
                            Thread.Sleep(500);
                            ClearRows(eventRow, commandRow);
                            Console.SetCursorPosition(2, eventRow);
                            Console.WriteLine("{0} не успява да те улучи. Голям позор!", enemy.Name);
                            isPlayer = !isPlayer;
                            continue;
                        }
                        else
                        {
                            Thread.Sleep(500);
                            int damage = rng.Next(enemy.MinDmg, enemy.MaxDmg + 1);
                            //difficulty multiplier adjusts enemy dmg
                            damage = (int)(Math.Round(damage * difficultyMultiplier));

                            ClearRows(eventRow, commandRow);
                            Console.SetCursorPosition(2, eventRow);
                            Console.WriteLine("{0} за малко да ти отнесе главата. Губиш {1} точки живот.", enemy.Name, damage);

                            player.CurrentLife -= damage;

                            if (!player.IsAlive())
                            {
                                Thread.Sleep(500);
                                GameOver(rng);
                            }

                            //check if enemy says something
                            int saySomething = rng.Next(2);
                            if (saySomething == 1)
                            {
                                Thread.Sleep(500);
                                Console.SetCursorPosition(2, talkRow);
                                Console.Write("{0}: ", enemy.Name);
                                PrintRandomLine(@"text-files/text/infight-enemy.txt", 4 + enemy.Name.Length, talkRow, rng);
                            }
                            isPlayer = !isPlayer;
                        }
                    }
                }

                //at the end of the turn change players again to switch turns next round
                isPlayer = !isPlayer;
                Thread.Sleep(500);
            }
        }

        private static void RegenerateLife(Hero player)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(4, 12);
            // regenerate half of the lost health points
            int regeneratedLife = (player.MaxLife - player.CurrentLife) / 2;
            player.CurrentLife += regeneratedLife;

            PrintText(String.Format("Усещаш как силите ти се възвръщат. Възобновяваш {0} точки живот.", regeneratedLife));

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void PrintLifeLeft(Hero player, Enemy enemy)
        {
            //print player and enemy current life above battle drawing
            Console.SetCursorPosition(0, 3);
            Console.Write(new string(' ', 75));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(20, 3);
            Console.Write(String.Format("{0}: {1}HP", player.Name, player.CurrentLife));
            Console.SetCursorPosition(45, 3);
            Console.Write(String.Format("{0}: {1}HP", enemy.Name, enemy.CurrentLife));
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void ClearRows(int startRow, int endRow)
        {
            Thread.Sleep(1000);
            for (int i = startRow; i <= endRow; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', 80));
            }
        }

        static void BattleReward(Hero player)
        {
            ClearScreen();
            //add logic for battle rewards
            //choose between +dmg, +life or +dmg and +life
            Console.SetCursorPosition(15, 12);
            PrintText("Отрязваш главата на противника и изпиваш кръвта му.");
            Console.SetCursorPosition(15, 14);
            PrintText("Усещаш как силите му се вливат в теб.");
            Console.SetCursorPosition(15, 16);
            PrintText("Избери си награда!");

            Console.SetCursorPosition(15, 18);
            Console.WriteLine("[(1) Здраве]   [(2) Сила]   [(3) Баланс]");

            string choice = Console.ReadLine();
            string result = String.Empty; // add results
            switch (choice)
            {
                case "1":
                    player.MaxLife += 50;
                    player.CurrentLife += 50;
                    result = String.Format("Ти избра {0}, получаваш {1}", "Здраве", "50 точки живот.");
                    break;
                case "2":
                    player.MinDmg += 15;
                    player.MaxDmg += 15;
                    result = String.Format("Ти избра {0}, получаваш {1}", "Сила", "15 точки сила.");
                    break;
                case "3":
                    player.MaxLife += 20;
                    player.MinDmg += 8;
                    player.MaxDmg += 8;
                    player.CurrentLife += 15;
                    result = String.Format("Ти избра {0}, получаваш {1}", "Баланс", "20 точки живот и 8 точки сила.");
                    break;
            }

            PrintText(result);

        }

        static void ConversationWithGitsa(Hero player)
        {
            ClearScreen();
            PrintDrawing(@"text-files/drawings/gitsa.txt");
            Console.WriteLine();
            PrintTextFromFile(@"text-files/story/gitsa.txt");
            ClearScreen();

            string[] playerDrawing = File.ReadAllLines(player.DrawingPath);
            string[] playerTalk = File.ReadAllLines(@"text-files/story/player.txt");
            int textIndex = 0;
            for (int i = 0; i < playerDrawing.Length; i++)
            {
                Console.Write(playerDrawing[i]);
                if (i >= playerDrawing.Length / 3 && textIndex < playerTalk.Length)
                {
                    Console.WriteLine(" {0}. {1}", textIndex + 1, playerTalk[textIndex]);
                    textIndex++;
                }
                else
                {
                    Console.WriteLine();
                }
            }

            string choice = Console.ReadLine();
            Thread.Sleep(500);
            Console.Clear();
           
            string[] endings = File.ReadAllLines(@"text-files/story/story-endings.txt");
            ClearScreen();
            switch (choice)
            {
                case "1":
                    PrintText(endings[0]);
                    break;
                case "2":
                    PrintText(endings[1]);
                    break;
                case "3":
                    PrintText(endings[2]);
                    break;
                case "4":
                    PrintText(endings[3]);
                    break;
                case "5":
                    PrintText(endings[4]);
                    break;
                case "6":
                    PrintText(endings[5]);
                    break;
                case "7":
                    PrintText(endings[6]);
                    break;
            }
            ClearScreen();
        }
    }
}