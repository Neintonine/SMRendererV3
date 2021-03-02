#region usings

using SM.Base;
using SM.Base.Scene;
using SM.Base.Types;
using SM.Base.Windows;
using SM2D.Types;

#endregion

namespace SM2D.Scene
{
    public class ItemCollection : GenericItemCollection<Transformation>
    {
        public ItemCollection()
        {
            Transform.Size = new CVector2(1);
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
        }
    }
}