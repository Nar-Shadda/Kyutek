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

        public string Name { get; set; }
        public int MaxLife { get; set; }
        public int CurrentLife { get; set; } // <- can put validation in setter
        public int MinDmg { get; set; }
        public int MaxDmg { get; set; }
        public string HeroClass { get; set; }
        
        public Hero(string choice)
        {
            switch (choice)
            {
                //warrior
                case "1":
                    this.MaxLife = 120;
                    this.CurrentLife = 120;
                    this.MinDmg = 9;
                    this.MaxDmg = 12;
                    this.HeroClass = "Манаф-чекръкчия";
                    break;
                //rogue
                case "2":
                    this.CurrentLife = 100;
                    this.MinDmg = 6;
                    this.MaxDmg = 15;
                    this.HeroClass = "Дребен тарикат";
                    break;
                //wizzard
                case "3":
                    this.CurrentLife = 80;
                    this.MinDmg = 11;
                    this.MaxDmg = 11;
                    this.HeroClass = "Ельоменат";
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