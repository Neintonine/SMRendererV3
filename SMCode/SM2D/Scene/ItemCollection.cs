#region usings

using System;
using SM.Base.Scene;
using SM.Base.Types;
using SM.Base.Window;
using SM2D.Types;

#endregion

namespace SM2D.Scene
{
    /// <inheritdoc />
    public class ItemCollection : GenericItemCollection<Transformation>
    {
        /// <inheritdoc />
        public ItemCollection()
        {
            Transform.Size = new CVector2(1);
        }

        public override void Draw(DrawContext context)
        {
            this.Sort(Comparitor);

            base.Draw(context);
        }

        private int Comparitor(IShowItem a, IShowItem b)
        {
            if (a is ITransformItem<Transformation> ta)
            {
                if (b is ITransformItem<Transformation> tb)
                {
                    return (int)((ta.Transform.GetMatrix()[3, 2] - tb.Transform.GetMatrix()[3, 2]) * Transformation.ZIndexPercision);
                }
                else return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}