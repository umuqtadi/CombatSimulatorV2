using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulatorV2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"  ___                         ___ _                     ___ ___ 
 |   \ _ _ __ _ __ _ ___ _ _ / __| |__ _ _  _ ___ _ _  / _ \ __|
 | |) | '_/ _` / _` / _ \ ' \\__ \ / _` | || / -_) '_| \_, /__ \
 |___/|_| \__,_\__, \___/_||_|___/_\__,_|\_, \___|_|    /_/|___/
               |___/                     |__/                   

");

            Console.WriteLine("I hope you are ready for an adventure. You have just learned that you come from a long lineage of Dragon Slayers. To prove your value you must fight a dragon.");
            Console.WriteLine("\nYou will have three options when fighting this majestic beast. \n1. Use your sword. It will cause 20-35 damage, but will only hit 70% of the time \n2. You can use your magic and it will always hit the dragon, but will only cause damage between 10-15. \n\n3. Your last option is to heal. This will improve your health anywhere \nfrom 10-20 hp, but you can still be hit by the dragon.");

            Game game = new Game();
            game.PlayGame();

            Console.ReadKey();
        }
    }


    #region "Game"
    public abstract class Actor
    {
        public Random rng = new Random();
        public string Name { get; set; }
        public int HP { get; set; }
        public bool IsAlive 
        {
            get
            {
                return this.HP > 0;
            }
        }

        public Actor(string name, int hp)
        {
            this.Name = name;
            this.HP = hp;
        }

        public virtual void Attack(Actor actor)
        {
        }
    }
    public class Enemy : Actor
    {
        public Enemy(string name, int hp)
            : base(name,hp) { }

        public override void Attack(Actor actor)
        {
            int hitAmt = rng.Next(10, 20);
            actor.HP -= hitAmt;
            Console.WriteLine("You have been hit for {0} HP",hitAmt);
        }
    }
    public class Player : Actor
    {
        public enum AttackType
        {
            Sword = 1,
            magic,
            heal
        }

        

        public Player(string name, int hp)
            : base(name, hp)
        {

        }

        public override void Attack(Actor actor)
        {
           switch((int)ChooseAttack())
            {
                case 1:
                    //The sword attack will only succeed 70% of the time
                    if(rng.Next(1, 11) < 7)
                    {
                        //Allows for an attack between 20 and 35
                        actor.HP -= rng.Next(20, 36);
                        Console.WriteLine("You have hit your nemesis");
                    }
                    break;
                case 2:
                    //Allows for an attack between 10 and 15
                    actor.HP -= rng.Next(10, 16);
                    Console.WriteLine("It is a hit");
                    break;
                //This is the case for the heal
                default:
                    //Allows the user to heal between 10 and 20 points of health
                    this.HP += rng.Next(10, 21);
                    Console.WriteLine("You have healed");
                    break;
            }
        }

             private AttackType ChooseAttack()
            {
                string userChoice = string.Empty;

               while (true)
            {
                userChoice = Console.ReadLine();

                int playerChoiceInt = 0;
                bool isNumber = int.TryParse(userChoice, out playerChoiceInt);

                if (isNumber && userChoice != string.Empty)
                {
                    switch (playerChoiceInt)
                    {
                        case 1:
                            return AttackType.Sword;
                        case 2:
                            return AttackType.magic;
                        case 3:
                            return AttackType.heal;
                    }
                }
            }
        }
    }
        public class Game
        {
            public Player Player { get; set; }

            public Enemy Enemy { get; set; }

            public Game()
            {
                Player = new Player("Homer Simpson", 100);
                Enemy = new  Enemy("Ersan Ilyasova", 200);
                Console.WriteLine("Press 1 for sword");
                Console.WriteLine("Press 2 for magic");
                Console.WriteLine("Press 3 to heal");
            }
            public void DisplayCombatInfo()
            {
                Console.WriteLine(this.Player.HP);
                Console.WriteLine(this.Enemy.HP);
            }
            public void PlayGame()
            {
                while (this.Player.IsAlive && this.Enemy.IsAlive)
                {
                    DisplayCombatInfo();
                    this.Player.Attack(this.Enemy);
                    this.Enemy.Attack(this.Player);
                }
                if (this.Player.IsAlive) { Console.WriteLine("You won!"); }
                else { Console.WriteLine("You lost!"); }
            }
        }

    }
#endregion

