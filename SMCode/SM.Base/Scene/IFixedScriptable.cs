using SM.Base.Window.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Base.Scene
{
    /// <summary>
    /// This interface allows for a object to implerment a fixed update.
    /// </summary>
    public interface IFixedScriptable : ICollectionItem
    {
        /// <summary>
        /// Executes a fixed update.
        /// </summary>
        /// <param name="context"></param>
        void FixedUpdate(FixedUpdateContext context);

    }
}
