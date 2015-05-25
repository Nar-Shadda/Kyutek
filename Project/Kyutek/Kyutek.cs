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

        static void Main(string[] args)
        {
            //set window size and buffer size
            Console.WindowHeight = WindowHeight;
            Console.BufferHeight = WindowHeight;
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;

            //intro
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
            PrintText(@"text-files/intro-outro/quest.txt", @"audio/typewriter.wav");

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

            // part 1 - introduction and character creation
            Console.SetCursorPosition(0, 10);
            PrintText(@"text-files/story/story-1.txt", @"audio/typewriter.wav");
            Console.CursorVisible = true;
            Console.ReadLine();
            Console.CursorVisible = false;
            Thread.Sleep(750);
            Console.Clear();
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Браааат...");
            Thread.Sleep(500);
            PrintText(@"text-files/story/story-2.txt", @"audio/typewriter.wav");
            Console.WriteLine();

            // choose and create a character
            CreateCharacter();
            

            
            // choose a name

            // story - 2 (prequel)

            // choose a character class

            // story - 3 (going to the bar, first interaction) 

            // first battle

            // story - 4

            // second battle

            // story - 5

            // third battle

            // story 6

            // final battle

            // outro

            // credits
        }

        static void CreateCharacter()
        {
            // ask user to choose a character class
            
            string choice = Console.ReadLine();
            Hero myHero = new Hero(choice);
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

        static void PrintText(string textPath, string audioPath)
        {
            StreamReader reader = new StreamReader(textPath);
            SoundPlayer player = new System.Media.SoundPlayer(audioPath);

            using (reader)
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        player.Stop();
                        break;
                    }
                    //print line
                    player.Play();
                    foreach (char letter in line)
                    {
                        Console.Write(letter);
                        Thread.Sleep(40);
                    }
                    player.Stop();
                    Console.WriteLine();
                    Thread.Sleep(400);
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
            Console.ForegroundColor = ConsoleColor.DarkRed;
            PrintAsciiText(@"text-files/text/game-over.txt", 12, false);
        }
    }
}