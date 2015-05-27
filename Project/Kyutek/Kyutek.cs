using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Media;

namespace Kyutek
{
    class Kyutek
    {
        const int WindowHeight = 36;
        public const string typewriterPath = @"audio/typewriter.wav";
        static void Main()
        {

            //set window size and buffer size
            Console.WindowHeight = WindowHeight;
            Console.BufferHeight = WindowHeight;
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;

            //initialize random generator
            Random rng = new Random();
            //intro
            //PlayIntro();

            //ChooseDifficulty(); - not complete

            // part 1 - introduction
            //StoryIntroduction();

            // choose and create a character
            Hero player = CreateCharacter();

            // choose a name
            Console.SetCursorPosition(0, 13);
            PrintText("Сега, когато вече си нов човек, е време да си избереш и ново име...");
            PrintText("Тоя път гледай да се постараеш повече!");
            Console.CursorVisible = true;
            player.Name = Console.ReadLine();

            Console.WriteLine("И така, {0}, време е да да се разкършим!", player.Name); /* does not print cyrillic characters properly
                                                                                        probably have to set encoding in hero.name setter*/
            // story - 3 (going to the bar, first interaction) 
            PrintTextFromFile(@"text-files/story/story-3.txt");

            // first battle
            Battle(player, new Enemy(1), rng);

            // story - 4
            PrintTextFromFile(@"text-files/story/story-4.txt");

            // second battle
            Battle(player, new Enemy(2), rng);

            // story - 5
            PrintTextFromFile(@"text-files/story/story-5.txt");

            // third battle
            Battle(player, new Enemy(3), rng);

            // secret encounter
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
                    PrintText("Намери бъклицата на дядо, ще я изпиеш ли?");
                    Console.WriteLine("Y/N");
                    
                    string drink = Console.ReadLine().ToLower();
                    if (drink == "y")
                    {
                        PrintText("Добро решение, няма нищо по-добро от отлежала гроздова!");
                        PrintText("Усещаш мощен прилив на енергия, получаваш допълнително 10 сила");
                        player.MinDmg += 10;
                        player.MaxDmg += 10;
                    }
                }

                else if (chance == 2 || chance == 4 || chance == 8)
                {
                    PrintText("Намираш много запазено пардесю! Пробваш го и установяваш, че ти седи чудесно!");
                    PrintText("Получаваш допълнително 30 точки живот");
                    player.MaxLife += 30;
                    player.CurrentLife = player.MaxLife;
                }
                else if (chance == 0)
                {
                    PrintText("Навеждаш се, за да огледаш хубаво какво има в дупката и изведнъж");
                    PrintText("някой ти бърка в окото 'Кво зяпаш бе, тюфлек!'");
                    PrintText("Губиш 5 точки живот. Другият път може би е по-добре да не се вреш в чуждите работи.");
                    player.CurrentLife -= 5;
                }
                else
                {
                    PrintText("Драсваш клечка кибрит и оглеждаш съдържанието на дупката");
                    PrintText("За съжаление намираш само старо списание със слепени страници и шишенце вазелин");
                    PrintText("Помисляш си 'Каква странна комбинация' и продължаваш по пътя си.");

                }
            }
            
            Thread.Sleep(1500);
            Console.Clear();

            // right before the boss
            PrintTextFromFile(@"text-files/story/story-7.txt");

            // final battle
            Battle(player, new Enemy(4), rng);

            //conversation with gitsa
            ConversationWithGitsa(player);
            
            //the end
            TheEnd();

            // outro (credits)
            PrintTextFromFile(@"text-files/intro-outro/outro.txt");
        }

        private static void ChooseDifficulty()
        {

            // need to add global var and this method shoud set its value according to user choice
            Console.WriteLine("Избери трудност:");
            string difficultyChoice = Console.ReadLine();
            switch (difficultyChoice)
            {
                case "Пълен айляк":
                    break;
                case "Нек'во нормално":
                    break;
                case "Като да се пребориш за концерт на Милко":
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
            Console.SetCursorPosition(28, 20);
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
                Console.SetCursorPosition(33, 23);
                Console.WriteLine("press enter");
                Thread.Sleep(500);
                Console.SetCursorPosition(33, 23);
                Console.WriteLine("           ");
                Thread.Sleep(500);
            }
        }

        private static void StoryIntroduction()
        {
            Console.SetCursorPosition(0, 10);
            PrintTextFromFile(@"text-files/story/story-1.txt");
            Console.CursorVisible = true;
            Console.ReadLine();
            Console.CursorVisible = false;
            Thread.Sleep(750);
            Console.Clear();
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Браааат...");
            Thread.Sleep(500);
            PrintTextFromFile(@"text-files/story/story-2.txt");
            Console.WriteLine();
        }

        static Hero CreateCharacter()
        {
            Thread.Sleep(1000);
            // ask user to choose a character class
            Console.Clear();
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
            Hero player = new Hero(choice);
            string[] heroClass = File.ReadAllLines(@"text-files/text/class.txt");
            Thread.Sleep(1000);
            Console.Clear();
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
            int startIndex = (Console.WindowWidth - drawing[0].Length) / 2;
            int startRow = (WindowHeight - drawing.Length) / 2;
            foreach (var item in drawing)
            {
                Console.SetCursorPosition(startIndex, startRow);
                Console.WriteLine(item);
                startRow++;
            }

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
            PrintText(currentText);
        }

        static void GameOver()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            PrintAsciiText(@"text-files/text/game-over.txt", false);
            Thread.Sleep(3000);
            Environment.Exit(0);
        }

        static void Victory()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            PrintAsciiText(@"text-files/text/win.txt");
        }

        static void TheEnd()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintAsciiText(@"text-files/story/the-end.txt");
        }

        static void Battle(Hero player, Enemy enemy, Random rng)
        {

            // ignore this: invoke ascii drawing
            PrintDrawing(player.DrawingPath, rng);

            PrintDrawing(enemy.DrawingPath, rng);

            //add bool var to track whose turn it is to strike

            bool isPlayer = false;
            int playerHit = rng.Next(0, 10);
            enemy.CurrentLife -= rng.Next(player.MaxDmg, player.MaxDmg);

            while (true)
            {

                if (isPlayer == true)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        playerHit = rng.Next(0, 11);
                        if (playerHit > 0)
                        {
                            continue;
                        }
                        else
                        {
                            enemy.CurrentLife -= rng.Next(player.MaxDmg, player.MaxDmg);
                            if (enemy.CurrentLife <= 0)
                            {
                                Victory();
                            }
                        }
                    }
                    isPlayer = false;
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        playerHit = rng.Next(0, 11);
                        if (playerHit > 0)
                        {
                            continue;
                        }
                        else
                        {
                            player.CurrentLife -= rng.Next(enemy.MaxDmg, enemy.MaxDmg);
                            if (player.CurrentLife <= 0)
                            {
                                GameOver();
                            }
                        }
                    }
                    isPlayer = true;

                }

                // check whose turn it is

                // ignore this: commands can be "hit", "double", "stun", "heal"
                // check for hit (roll random 0-10, if it is 0 - skip turn)
                // if hit successful check dmg (roll random (MinDmg, MaxDmg+1)
                // substract dmg done from player/enemy current health
                // check if current health is <= 0
                // if current health <= 0
                // character dies - if hero dies invoke GameOver() and return, else invoke Victory()
            }
        }

        static void ConversationWithGitsa(Hero player)
        {
            PrintDrawing(@"text-files/drawings/gitsa.txt");
            Console.WriteLine();
            PrintTextFromFile(@"text-files/story/gitsa.txt");
            Thread.Sleep(1500);
            Console.Clear();

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
            Thread.Sleep(1500);
            Console.Clear();
        }
    }
}