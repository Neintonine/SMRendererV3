namespace SM.Utility
{
    public class Deltatime
    {
        public static float UpdateDelta { get; internal set; }
        public static float RenderDelta { get; internal set; }

        public bool UseRender;
        public float Scale;

        public float DeltaTime => (UseRender ? RenderDelta : UpdateDelta) * Scale;

        public Deltatime(float scale = 1,  bool useRender = false)
        {
            UseRender = useRender;
            Scale = scale;
        }


    }
}