namespace SM.Base.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInitializable
    {
        /// <summary>
        /// If true, its already initialized.
        /// </summary>
        bool IsInitialized { get; set; }

        /// <summary>
        /// A event when the object was activated.
        /// </summary>
        void Activate();
        /// <summary>
        /// A event, when the object was first initialized.
        /// </summary>
        void Initialization();
    }
}