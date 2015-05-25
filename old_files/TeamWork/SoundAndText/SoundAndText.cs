using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Media;

namespace BitBuilderNew
{
    class BitBuilderNew
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            string textPath = @"E:\Fundamentials\TeamWork\text.txt";
            string backGround = @"E:\Fundamentials\TeamWork\Star_Wars.wav";
            SoundPlayer backGroundsong = new SoundPlayer(backGround);
            backGroundsong.Play();

            string logoPath = @"E:\Fundamentials\TeamWork\logo.txt";
            string[] logo = File.ReadAllLines(logoPath);
            Console.WriteLine();
            foreach (string item in logo)
            {
                Console.Write(new string(' ', (Console.WindowWidth - item.Length) / 2));
                Console.WriteLine(item);
                Thread.Sleep(1000);
            }
            Thread.Sleep(3000);
            Console.Clear();
            backGroundsong.Dispose();

            PrintTypewriterStyle(textPath);            
        }
 
        static void PrintTypewriterStyle(string textPath)
        {
            string soundPath = @"E:\Fundamentials\TeamWork\typewriter-1.wav";
            string[] text = File.ReadAllLines(textPath);

            SoundPlayer typewriter = new SoundPlayer(soundPath);            
            typewriter.LoadAsync();

            foreach (string item in text)
            {
                typewriter.Play();
                foreach (char ch in item)
                {
                    Thread.Sleep(100);
                    Console.Write(ch);
                }
                typewriter.Stop();
                Thread.Sleep(1000);
                Console.Clear();
            }
            typewriter.Dispose();
        }
    }
}
