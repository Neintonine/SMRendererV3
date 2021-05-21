namespace SM.Utils.Controls
{
    public struct GameControllerStateTriggers
    {
        public static GameControllerStateTriggers Default = new GameControllerStateTriggers {Left = 0f, Right = 0f};

        public float Left;
        public float Right;

        public bool AnyInteraction => Left != 0f || Right != 0f;

        public override string ToString()
        {
            return $"Left: {Left}; Right: {Right}";
        }
    }
}