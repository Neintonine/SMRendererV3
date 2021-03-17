using SM.Base.Window.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Base.Scene
{
    public interface IFixedScriptable
    {
        void FixedUpdate(FixedUpdateContext context);

    }
}
