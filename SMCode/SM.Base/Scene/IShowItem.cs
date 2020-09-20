using SM.Base.Contexts;

namespace SM.Base.Scene
{
    public interface IShowItem
    {
        void Update(UpdateContext context);
        void Draw(DrawContext context);
    }
}