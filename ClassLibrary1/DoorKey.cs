using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// used prof.holme's code
/// </summary>
namespace ClassLibrary1 {
    [Serializable]

    /// <summary>
    /// Object needed to unlock a door
    /// </summary>
    public class DoorKey:Item {
        private string _Code;

        public DoorKey(string name, int value, string code):base(name, value) {
            _Code = code;
        }
        /// <summary>
        /// Code to unlock a door
        /// </summary>
        public string Code {
            get {
                return _Code;
            }
        }
    }
}
