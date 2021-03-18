namespace SM.Base.Utility
{
    /// <summary>
    ///     A assistant to control the delta time.
    /// </summary>
    public class Deltatime
    {
        /// <summary>
        ///     Scaling of the delta time.
        ///     <para>Default: 1</para>
        /// </summary>
        public float Scale;

        /// <summary>
        ///     If true, it uses <see cref="RenderDelta" />, otherwise <see cref="UpdateDelta" />.
        ///     <para>Default: false</para>
        /// </summary>
        public bool UseRender;


        /// <summary>
        ///     The current update delta time.
        /// </summary>
        public static float UpdateDelta { get; internal set; }

        public static float FixedUpdateDelta { get; set; }

        /// <summary>
        ///     The current render delta time.
        /// </summary>
        public static float RenderDelta { get; internal set; }

        /// <summary>
        ///     The calculated delta time.
        /// </summary>
        public float DeltaTime => (UseRender ? RenderDelta : UpdateDelta) * Scale;

        /// <summary>
        ///     Creates a delta time assistant.
        /// </summary>
        /// <param name="scale">The start scale for the delta time. Default: 1</param>
        /// <param name="useRender">
        ///     If true, it uses <see cref="RenderDelta" />, otherwise <see cref="UpdateDelta" />. Default:
        ///     false
        /// </param>
        public Deltatime(float scale = 1, bool useRender = false)
        {
            UseRender = useRender;
            Scale = scale;
        }
    }
}