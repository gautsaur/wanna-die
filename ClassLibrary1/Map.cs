// CS/INFO 1182
// Jon Holmes
// Description - Representation of our Map. It needs to know everything about what is on the Map.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1 {
    [Serializable]

    public class Map {
        #region "Private Variables"
        private MapCell[,] _Cells = null;
        private List<Monster> _Monsters;
        private List<Item> _Items;
        private Hero _Adventurer;
        #endregion

        #region "Constructors"
        /// <summary>
        /// Create and fill the Map
        /// </summary>
        /// <param name="rows">Number of Rows the map should have</param>
        /// <param name="cols">Number of Columns the map should have</param>
        public Map(int rows, int cols) {
            _Cells = new MapCell[rows, cols];
            fillMonsters();
            _Items = new List<Item>();
            fillPotions();
            fillWeapons();
            fillMap();
        }
        #endregion

        #region "Private Methods"
        /// <summary>
        /// Fill the List of Potions
        /// </summary>
        private void fillPotions() {
            _Items.Add(new Potion("Angel's Tears", 25, Potion.Colors.Red));
            _Items.Add(new Potion("Dragon's Blood", 50, Potion.Colors.Blue));
            _Items.Add(new Potion("Elixir of Athens", 100, Potion.Colors.Green));
            _Items.Add(new Potion("Blood wine", 200, Potion.Colors.Purple));
        }
        /// <summary>
        /// Fill the list of Weapons
        /// </summary>
        private void fillWeapons() {
            _Items.Add(new Weapon("Dagger", 10, -1));
            _Items.Add(new Weapon("Wand", 20, -3));
            _Items.Add(new Weapon("Sword", 30, -2));
            _Items.Add(new Weapon("Bow-arrow", 40, -4));
        }
        /// <summary>
        /// Fill the List of Monsters
        /// </summary>
        private void fillMonsters() {
            _Monsters = new List<Monster>();
            _Monsters.Add(new Monster("Orc", "", 3, 100, 0, 0, 10));
            _Monsters.Add(new Monster("Goblin", "", 1, 30, 0, 0, 5));
            _Monsters.Add(new Monster("Slug", "", 5, 3, 0, 0, 2));
            _Monsters.Add(new Monster("Rat", "", 0, 5, 0, 0, 1));
            _Monsters.Add(new Monster("Skeleton", "", 4, 70, 0, 0, 7));
        }



        /// <summary>
        /// Fill the map with empty MapCells
        /// </summary>
        private void fillMap() {
            Random rnd = new Random();
            int rows = Cells.GetLength(0);
            int cols = Cells.GetLength(1);
            for (int row = 0; row < rows; row++) {
                for (int col = 0; col < cols; col++) {
                    MapCell mc = new MapCell();
                    if (rnd.Next(10) % 10 == 0) {
                        // add a monster
                        mc.Monster = (Monster)Monsters[rnd.Next(Monsters.Count)].CreateCopy();
                    } else if (rnd.Next(10) % 10 == 0) {
                        // add an item
                        Item itm = (Item)_Items[rnd.Next(Items.Count)];
                        if (itm.GetType() == typeof(Weapon))
                            mc.Item = (Weapon)((Weapon)itm).CreateCopy();
                        else if (itm.GetType() == typeof(Potion))
                            mc.Item = (Potion)((Potion)itm).CreateCopy();
                    }
                    Cells[row, col] = mc;
                }
            }
            SetKeyAndDoorLocation(rnd, rows - 1, cols - 1, "AAAA");
        }

        /// <summary>
        /// Set the location of the Key and Door.
        /// </summary>
        /// <param name="rnd">Randomizer to use</param>
        /// <param name="rows">Max number of rows in the map</param>
        /// <param name="cols">Max number of columms in the map</param>
        /// <param name="code">Code to be shared by key and door.</param>
        private void SetKeyAndDoorLocation(Random rnd, int rows, int cols, String code) {
            MapCell keyLocation, doorLocation;
            keyLocation = doorLocation = Cells[rnd.Next(rows), rnd.Next(cols)];
            while (keyLocation.Monster != null || keyLocation.Item != null) // get new location
                keyLocation = Cells[rnd.Next(rows), rnd.Next(cols)];
            while (keyLocation == doorLocation || doorLocation.Monster != null || doorLocation.Item != null) // get new location
                doorLocation = Cells[rnd.Next(rows), rnd.Next(cols)];
            // set key and door.
            keyLocation.Item = new DoorKey("Key", 0, code);
            doorLocation.Item = new Door("Door", 0, code);
        }

        /// <summary>
        /// Hero that the user controls
        /// </summary>
        public Hero Adventurer {
            get {
                return _Adventurer;
            }

            set {
                _Adventurer = value;
            }
        }

        /// <summary>
        /// Moves hero in the map
        /// </summary>
        /// <param name="heroDir">direction that hero moves</param>
        /// <returns>if Location has anything or not</returns>
        public bool MoveHero(Actor.Direction heroDir)
        {
            int Xaxis = Cells.GetLength(1);
            int Yaxis = Cells.GetLength(0);
            Adventurer.Move(heroDir);

            //if hero goes outside the map, it decrease that value of positions by 1
            if (Adventurer.PositionX < 0)   Adventurer.Move(Actor.Direction.Right);
            if (Adventurer.PositionX > Xaxis) Adventurer.Move(Actor.Direction.Left);
            if (Adventurer.PositionY < 0)   Adventurer.Move(Actor.Direction.Down);
            if (Adventurer.PositionY > Yaxis) Adventurer.Move(Actor.Direction.Up);
            CurrentLocation.HasBeenSeen = true;
            return CurrentLocation.HasItem || CurrentLocation.HasMonster;
        }

        /// <summary>
        /// Enum for countables
        /// </summary>
        private enum Countables
        {
            Monster,
            Item,
            Discovered
        }

        /// <summary>
        /// Counts countables according to given logic
        /// </summary>
        /// <param name="c">countable</param>
        /// <returns> the count</returns>
        private int Count(Countables c)
        {
            int count = 0;
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {

                    if ((c == Countables.Monster && Cells[i,j].HasMonster) ||( c == Countables.Item && Cells[i, j].HasItem )||( c == Countables.Discovered && Cells[i, j].HasBeenSeen))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        
        #endregion

        #region "Public Properties"
        /// <summary>
        /// Get all of the cells of the map
        /// </summary>
        public MapCell[,] Cells {
            get {
                if (_Cells == null) fillMap();
                return _Cells;
            }

            set {
                _Cells = value;
            }
        }
        /// <summary>
        /// Get the List of Monsters available on our map
        /// </summary>
        protected List<Monster> Monsters {
            get {
                return _Monsters;
            }
        }
        /// <summary>
        /// Get the List of Potions and Weapons available on our map
        /// </summary>
        protected List<Item> Items {
            get {
                return _Items;
            }
        }

        /// <summary>
        /// Returns no. of Monsters
        /// </summary>
        public int CountMonsters
        {

            get
            {
                return Count(Countables.Monster);
            }
        }

        /// <summary>
        /// Returns no. of items
        /// </summary>
        public int CountItems
        {

            get
            {
                return Count(Countables.Item);
            }
        }

        /// <summary>
        /// Returns percentage of map discovered
        /// </summary>
        public double MapDiscPercent
        {

            get
            {
                return (Count(Countables.Discovered)*100.0/Cells.Length);
            }
        }

        /// <summary>
        /// Returns maxium Hp of monsters
        /// </summary>
        public double MostHP
        {

            get
            {
                var monsHP = from Monster in _Monsters select Monster.CurrentHitPoints;
                return monsHP.Max();
            }
        }

        /// <summary>
        /// returns min affectvalue of weapons
        /// </summary>
        public int LeastWeaponDamageValue
        {
            get
            {
                var weapList = Items.Where(x => x.GetType() == typeof(Weapon));
                int weapValue = weapList.Min(x => x.AffectValue);
                return weapValue;
            }
        }


        /// <summary>
        /// returns average value of potion's affect
        /// </summary>
        public double PotionAVG
        {
            get
            {
                var potList = Items.Where(x => x.GetType() == typeof(Potion));
                double avgPotion = potList.Average(x => x.AffectValue);
                return avgPotion;
            }
        }

        /// <summary>
        /// Gets the mapcell where the adventurer is on the map.
        /// </summary>
        public MapCell CurrentLocation
        {
            get
            {
                return Cells[Adventurer.PositionY, Adventurer.PositionX];
            }
        }
        #endregion
    }
}
