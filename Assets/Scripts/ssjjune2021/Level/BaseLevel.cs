using pdxpartyparrot.ssjjune2021.Items;

namespace pdxpartyparrot.ssjjune2021.Level
{
    public interface IBaseLevel
    {
        Clues Clues { get; }

        void RegisterExit(Exit exit);

        void UnRegisterExit(Exit exit);
    }
}
