/*
CS/INFO 1182 
Jon Holmes
2/1/2016
Description - Locations on the game board where an actor or item can be.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1 {
    [Serializable]

    public class MapCell {
        private bool _HasBeenSeen;
        private Monster _Monster;
        private Item _Item;

        /// <summary>
        /// Get or set whether the MapCell has been discovered.
        /// </summary>
        public bool HasBeenSeen
        {
            get
            {
                return _HasBeenSeen;
            }

            set
            {
                _HasBeenSeen = value;
            }
        }

        /// <summary>
        /// Get or set the monster in the MapCell.
        /// </summary>
        public Monster Monster
        {
            get
            {
                return _Monster;
            }

            set
            {
                _Monster = value;
            }
        }
        
        /// <summary>
        /// Get or set the item found in the MapCell.
        /// </summary>
        public Item Item
        {
            get
            {
                return _Item;
            }

            set
            {
                _Item = value;
            }
        }

        /// <summary>
        /// Get or set whether the MapCell has a monster.
        /// </summary>
        public bool HasMonster
        {
            get
            {
                return _Monster != null;
            }

        }

        /// <summary>
        /// Get or set whether the MapCell has an item.
        /// </summary>
        public bool HasItem
        {
            get
            {
                return _Item != null;
            }


        }
    }
}
