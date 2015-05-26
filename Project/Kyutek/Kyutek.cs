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

        static void Main(string[] args)
        {
            //set window size and buffer size
            Console.WindowHeight = WindowHeight;
            Console.BufferHeight = WindowHeight;
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;

            Victory();
            GameOver();
            //intro
            // PlayIntro();

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

            Console.WriteLine("И така, {0}, време е да да се разършим!", player.Name); /* does not print cyrillic characters properly
                                                                                        probably have to set encoding in hero class when setting hero name*/
            Console.ReadLine();

            // story - 3 (going to the bar, first interaction) 
            PrintTextFromFile(@"text-files/story/story-3.txt");

            // first battle

            // story - 4
            PrintTextFromFile(@"text-files/story/story-4.txt");

            // second battle

            // story - 5
            PrintTextFromFile(@"text-files/story/story-5.txt");

            // third battle

            // story 6
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



            PrintTextFromFile(@"text-files/story/story-7.txt");

            // final battle

            //conversation with gitsa

            // outro

            // credits
        }



        private static void PlayIntro()
        {
            SoundPlayer player = new SoundPlayer(@"audio/intro.wav");
            //team nar-shadda presents
            player.Play();
            PrintAsciiText(@"text-files/intro-outro/team.txt", 20);
            PrintAsciiText(@"text-files/intro-outro/nar-shadda.txt", 14);
            PrintAsciiText(@"text-files/intro-outro/presents.txt", 3);
            Thread.Sleep(1000);
            player.Stop();

            //game name
            player = new SoundPlayer(@"audio/vynil-rew.wav");
            player.Play();
            PrintAsciiText(@"text-files/intro-outro/kyutek.txt", 8, false);
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

        static void PrintAsciiText(string textPath, int posX = 0, bool clearScreen = true)
        {
            StreamReader reader = new StreamReader(textPath);
            int posY = 10;
            using (reader)
            {
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
            Console.OutputEncoding = Encoding.UTF8;

            SoundPlayer player = new System.Media.SoundPlayer(typewriterPath);

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

        static void PrintTextFromFile(string textPath)
        {
            StreamReader reader = new StreamReader(textPath);

            using (reader)
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    //print line
                    PrintText(line);
                }
            }
        }

        static void PrintDrawings(string path)
        {
            StreamReader reader = new StreamReader(path);
            using (reader)
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    Console.WriteLine(line);
                }
            }
        }
        
        static void GameOver()
        {
            // invoke losing taunt
            Console.ForegroundColor = ConsoleColor.DarkRed;
            PrintAsciiText(@"text-files/text/game-over.txt", 12, false);
        }

        static void Victory()
        {
            // invoke victory taunt
            Console.ForegroundColor = ConsoleColor.Green;
            PrintAsciiText(@"text-files/text/win.txt", 27);
        }

        static int Rng(int min, int max)
        {
            /* write logic for random generator
             * has to work with dmg
             * array indexes
             */
            return 1;
        }

        static void Battle(Hero player, Enemy enemy)
        {
            // invoke ascii drawing
            // invoke taunts

            //add bool var to track whose turn it is to strike

            while (true)
            {
                // check whose turn it is
                // rolls should be done by the player (read command from console)
                // commands can be "hit", "double", "stun", "heal"
                // check for hit (roll random 0-10, if it is 0 - miss)
                // if hit successful check dmg (roll random (MinDmg, MaxDmg)
                // substract dmg done from player/enemy current health
                // check if current health is <= 0
                // if current health <= 0
                // character dies - if hero dies invoke GameOver() and return, else invoke Victory()
            }
        }
    }
}