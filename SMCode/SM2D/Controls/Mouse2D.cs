using System.Collections.Generic;
using OpenTK;
using SM.Base.Controls;
using SM.Base.Drawing;
using SM.Base.Scene;
using SM2D.Scene;
using SM2D.Types;

namespace SM2D.Controls
{
    public class Mouse2D
    {
        public static Vector2 InWorld(Vector2 worldScale)
        {
            var res = worldScale;
            res.Y *= -1;
            return Mouse.InScreenNormalized * res - res / 2;
        }

        public static Vector2 InWorld(Camera cam)
        {
            return InWorld(cam.WorldScale) + cam.Position;
        }

        public static Vector2 InWorld(Vector2 worldScale, Vector2 position)
        {
            return InWorld(worldScale) + position;
        }

        public static bool MouseOver<TObject>(Vector2 mousePos, params TObject[] checkingObjects)
            where TObject : IModelItem, ITransformItem<Transformation>
            => MouseOver(mousePos, out _, checkingObjects);

        public static bool MouseOver<TObject>(Vector2 mousePos, ICollection<TObject> checkingObjects)
            where TObject : IModelItem, ITransformItem<Transformation>
            => MouseOver<TObject>(mousePos, out _, checkingObjects);

        public static bool MouseOver<TObject>(Vector2 mousePos, out TObject clicked, params TObject[] checkingObjects) 
            where TObject : IModelItem, ITransformItem<Transformation>
            => MouseOver<TObject>(mousePos, out clicked, (ICollection<TObject>)checkingObjects);

        public static bool MouseOver<TObject>(Vector2 mousePos, out TObject clickedObj, ICollection<TObject> checkingObjects)
            where TObject : IModelItem, ITransformItem<Transformation>
        {
            clickedObj = default;

            foreach (TObject obj in checkingObjects)
            {
                Vector3 min = obj.Mesh.BoundingBox.Get(obj.Transform.MergeMatrix(obj.Transform.LastMaster), false);
                Vector3 max = obj.Mesh.BoundingBox.Get(obj.Transform.MergeMatrix(obj.Transform.LastMaster), true);

                if (mousePos.X > min.X && mousePos.X < max.X &&
                    mousePos.Y > min.Y && mousePos.Y < max.Y)
                {
                    clickedObj = obj;
                    return true;
                }
            }

            return false;
        }
    }
}