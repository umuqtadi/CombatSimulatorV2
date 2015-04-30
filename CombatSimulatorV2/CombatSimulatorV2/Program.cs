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

        }
    }
    public abstract class Actor
    {
        Random rng = new Random();
        public string Name { get; set; }
        public int HP { get; set; }
        public bool IsAlive 
        {
            get
            {
                if (HP > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
            : base(name,hp)
        {

        }

        public override void Attack(Actor actor)
        {
            actor.HP -= Actor
        }
    }
}
