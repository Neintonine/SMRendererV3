namespace SM.Utility
{
    public interface IInitializable
    {
        bool IsInitialized { get; set; }

        void Activate();
        void Initialization();
    }
}