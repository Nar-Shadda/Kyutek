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




        }
        static void Battle(Hero player, Enemy enemy, Random rng) // can implement difficulty with multiplier in battle, not in classes
        {
            int firstToHit = rng.Next(2);
            bool isPlayer = firstToHit == 1 ? true : false;

            while (true)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (isPlayer)
                    {
                        //check if player hits (10% chance to miss)
                        int chanceToHit = rng.Next(10);
                        if (chanceToHit == 0)
                        {
                            Console.WriteLine("Замахваш и пропускаш... кофти.");
                            !isPlayer;
                            continue;
                        }
                        else
                        {
                            //calculate dmg between hero min and max dmg
                            int damage = rng.Next(player.MinDmg, player.MaxDmg + 1);
                            enemy.CurrentLife -= damage;

                            // check if you kill the enemy
                            if (!enemy.IsAlive)
                            {
                                //Victory
                                // call rewards - to implement
                            }

                            // check if player will say something
                            int saySomething = rng.Next(2);
                            if (saySomething == 1)
                            {
                                // print random line from infight
                            }

                            // change player
                            !isPlayer;
                        }
                    }
                    else
                    {
                        int chanceToHit = rng.Next(9); // slightly lower chance to hit for enemies
                        if (chanceToHit == 0)
                        {
                            Console.WriteLine("{0} не успява да те улучи. Голям позор!", enemy.Name);
                            !isPlayer;
                            continue;
                        }
                        else
                        {
                            int damage = rng.Next(enemy.MinDmg, enemy.MaxDmg + 1);
                            player.CurrentLife -= damage;
                            if (!player.IsAlive)
                            {
                                //game over
                            }

                            int saySomething = rng.Next(2);
                            if (saySomething == 1)
                            {
                                // say something from infight-enemy
                            }
                            !isPlayer;
                        }
                    }
                    // at the end of the turn change players again to switch turns next round
                    !isPlayer;

                }

            }
        }
    }
}
