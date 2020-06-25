using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1 {
    /// <summary>
    /// Interface for an Object that needs to be copied
    /// </summary>
    interface IRepeatable <t>{
        /// <summary>
        /// Create a copy of this object
        /// </summary>
        /// <returns>copy of the object</returns>
        t CreateCopy();
    }
}
