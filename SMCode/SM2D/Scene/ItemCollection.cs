#region usings

using SM.Base.Scene;
using SM.Base.Types;
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
    }
}