namespace Kyutek
{
    class Enemy
    {
        private int maxLife;
        private int currentLife;
        private int minDmg;
        private int maxDmg;
        private string name;

        public string Name { get; set; }
        public int MaxLife { get; set; }
        public int CurrentLife { get; set; } // <- can put validation in setter
        public int MinDmg { get; set; }
        public int MaxDmg { get; set; }

        public Enemy(string choice)
        {
            switch (choice)
            {
                //first battle
                case "1":
                    this.MaxLife = 50;
                    this.CurrentLife = 50;
                    this.MinDmg = 3;
                    this.MaxDmg = 5;
                    this.Name = "Портата";
                    break;
                //second battle
                case "2":
                    this.MaxLife = 100;
                    this.CurrentLife = 100;
                    this.MinDmg = 6;
                    this.MaxDmg = 10;
                    this.Name = "Булдозера";
                    break;
                //third battle
                case "3":
                    this.MaxLife = 150;
                    this.CurrentLife = 150;
                    this.MinDmg = 9;
                    this.MaxDmg = 15;
                    this.Name = "Базата";
                    break;

                //boss battle
                case "4":
                    this.MaxLife = 200;
                    this.CurrentLife = 200;
                    this.MinDmg = 20;
                    this.MaxDmg = 30;
                    this.Name = "Патлака";
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