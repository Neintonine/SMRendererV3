namespace SM.Base.Window
{
    public interface ISetup
    {
        void Applied(IGenericWindow window);
        void Load(IGenericWindow window);
        void Loaded(IGenericWindow window);
        void Resize(IGenericWindow window);
    }
}