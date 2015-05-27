using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TEST
{
    class Program
    {
        const int WindowHeight = 36;
        static void Main()
        {
            
            Console.WindowHeight = WindowHeight;
            Console.BufferHeight = WindowHeight;
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;
            Random rng = new Random();
            


            string[] drawing = File.ReadAllLines(@"../../../Kyutek/bin/Debug/text-files/drawings/wizard.txt");
            int startIndex = (80-drawing[0].Length)/2;
            int y = 3;
            foreach (string row in drawing)
            {
                Console.SetCursorPosition(startIndex, y);
                Console.WriteLine(row);
                y++;
            }
            Console.WriteLine();

            string[] line = File.ReadAllLines(@"../../../Kyutek/bin/Debug/text-files/text/taunts.txt");
            Console.SetCursorPosition(startIndex, ++y);
            string print = line[rng.Next(line.Length)];
            //print = line[11];
            for (int i = 0; i < print.Length; i++)
            {
                if (print[i] =='+')
                {
                    Console.WriteLine();
                    Console.SetCursorPosition(startIndex, ++y);
                    continue;
                }
                Console.Write(print[i]);
            }
            Console.ReadLine();
        }
    }
}
