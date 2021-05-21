using OpenTK;

namespace SM.Utils.Controls
{
    public struct GameControllerStateThumbs
    {
        public static GameControllerStateThumbs Default = new GameControllerStateThumbs()
            {Left = Vector2.Zero, Right = Vector2.Zero};

        public Vector2 Left;
        public Vector2 Right;

        public bool PressedLeft;
        public bool PressedRight;

        public bool AnyInteraction => Left != Vector2.Zero || Right != Vector2.Zero || PressedLeft || PressedRight;

        public override string ToString()
        {
            return $"Left: ({Left.X}; {Left.Y}){(PressedLeft ? " Pressed" : "")}; Right: ({Right.X}; {Right.Y}){(PressedRight ? " Pressed" : "")}";
        }
    }
}