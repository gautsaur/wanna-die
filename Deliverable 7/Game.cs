using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace Deliverable_7
{
    public static class Game
    {
        /// <summary>
        /// Enum for game states
        /// </summary>
        public enum GameState
        {
            Running, Lost, Won
        }

        private static int _Height;
        private static int _Width;
        private static GameState _GameStates;


        /// <summary>
        /// Returns the game state
        /// </summary>
        public static GameState GameStates
        {
            get
            {
                if (Map.Adventurer.IsAlive == false) _GameStates = GameState.Lost;
                return _GameStates;
            }
            set { _GameStates = value; }
        }

        private static Map _Map;

        /// <summary>
        /// Returns the map
        /// </summary>
        public static Map Map
        {
            get
            {
                return _Map;
            }
            set
            {
                _Map = value;
            }
        }

        /// <summary>
        /// Resets the game with parameters
        /// </summary>
        /// <param name="height">y-axis</param>
        /// <param name="width">x-axis</param>
        public static void ResetGame(int height, int width)
        {
            _GameStates = GameState.Running;

            _Map = new Map(width, height);

            Random rnd = new Random();
            int X, Y;

            //looping to make sure there is a hero in map
            do
            {
                X = rnd.Next(0, width);
                Y = rnd.Next(0, height);
                if (_Map.Cells[X, Y].HasItem == false && _Map.Cells[X, Y].HasMonster == false)
                {

                    _Map.Adventurer = new Hero(frmCharacter.firstName + frmCharacter.lastName, frmCharacter.title, 50, 50, X, Y);
                    if (frmCharacter.femaleClick == 1)
                    {
                        _Map.Adventurer.Gender = "Male";
                    }
                    else if (frmCharacter.maleClick == 1)
                    {
                        _Map.Adventurer.Gender = "Female";
                    }
                    _Map.CurrentLocation.HasBeenSeen = true;
                }
            } while (Map.Cells[X, Y].HasItem || Map.Cells[X, Y].HasMonster);

        }

        /// <summary>
        /// Resets the game with parameters
        /// </summary>
        /// <param name="height">y-axis</param>
        /// <param name="width">x-axis</param>
        public static void ResetGame()
        {
            _GameStates = GameState.Running;

            _Map = new Map(_Width, _Height);

            Random rnd = new Random();
            int X, Y;

            //looping to make sure there is a hero in map
            do
            {
                X = rnd.Next(0, _Width);
                Y = rnd.Next(0, _Height);
                if (_Map.Cells[X, Y].HasItem == false && _Map.Cells[X, Y].HasMonster == false)
                {

                    _Map.Adventurer = new Hero(frmCharacter.firstName + frmCharacter.lastName, frmCharacter.title, 50, 50, X, Y);
                    _Map.CurrentLocation.HasBeenSeen = true;
                }
            } while (Map.Cells[X, Y].HasItem || Map.Cells[X, Y].HasMonster);

        }
    }
}