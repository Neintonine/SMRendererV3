using SM.Base.Contexts;
using SM.Base.Scene;
using SM.Base.Types;
using SM2D.Types;

namespace SM2D.Scene
{
    public class ItemCollection : GenericItemCollection<I2DShowItem, Transformation>, I2DShowItem
    {
        public ItemCollection()
        {
            Transform.Size = new Vector2(1);
        }

        public override void Draw(DrawContext context)
        {
            Objects.Sort((x, y) => x.ZIndex - y.ZIndex);

            base.Draw(context);
        }

        public int ZIndex { get; set; }
    }
}