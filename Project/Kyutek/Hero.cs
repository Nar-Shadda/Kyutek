namespace Kyutek
{
    class Hero
    {
        private int maxLife;
        private int currentLife;
        private string heroClass;
        private int minDmg;
        private int maxDmg;
        private string name;
        private string drawingPath;
        private string specialAttack;

        public string Name { get; set; }
        public int MaxLife { get; set; }
        public int CurrentLife { get; set; } // <- can put validation in setter
        public int MinDmg { get; set; }
        public int MaxDmg { get; set; }
        public string HeroClass { get; set; }
        public string DrawingPath { get; set; }
        public string SpecialAttack { get; set; }
        
        public Hero(string choice)
        {
            switch (choice)
            {
                //warrior
                case "1":
                    this.MaxLife = 120;
                    this.CurrentLife = 120;
                    this.MinDmg = 11;
                    this.MaxDmg = 16;
                    this.HeroClass = "Манаф-чекръкчия";
                    this.DrawingPath = @"text-files/drawings/warrior.txt";
                    this.SpecialAttack = "stun";
                    break;
                //rogue
                case "2":
                    this.MaxLife = 105;
                    this.CurrentLife = 105;
                    this.MinDmg = 9;
                    this.MaxDmg = 17;
                    this.HeroClass = "Дребен тарикат";
                    this.DrawingPath = @"text-files/drawings/rogue.txt";
                    this.SpecialAttack = "double";
                    break;
                //wizzard
                case "3":
                    this.MaxLife = 90;
                    this.CurrentLife = 90;
                    this.MinDmg = 14;
                    this.MaxDmg = 14;
                    this.HeroClass = "Ельоменат";
                    this.DrawingPath = @"text-files/drawings/wizard.txt";
                    this.SpecialAttack = "heal";
                    break;
            }
        }

        // method check if hero is alive
        public bool IsAlive()
        {
            bool isAlive = this.CurrentLife > 0;
            return isAlive;
        }
    }
}