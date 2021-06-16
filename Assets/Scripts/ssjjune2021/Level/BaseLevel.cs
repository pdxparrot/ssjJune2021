using pdxpartyparrot.ssjjune2021.Items;
using pdxpartyparrot.ssjjune2021.World;

namespace pdxpartyparrot.ssjjune2021.Level
{
    public interface IBaseLevel
    {
        Clues Clues { get; }

        void RegisterMemoryFragment(MemoryFragment fragment);

        void UnRegisterMemoryFragment(MemoryFragment fragment);

        void RegisterExit(Exit exit);

        void UnRegisterExit(Exit exit);

        void Exit();
    }
}
