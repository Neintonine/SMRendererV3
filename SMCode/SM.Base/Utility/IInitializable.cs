namespace SM.Base.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInitializable
    {
        bool IsInitialized { get; set; }

        void Activate();
        void Initialization();
    }
}