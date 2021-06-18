using pdxpartyparrot.Game.UI;
using pdxpartyparrot.ssjjune2021.Items;

using TMPro;

using UnityEngine;

namespace pdxpartyparrot.ssjjune2021.UI
{
    public sealed class PlayerHUD : HUD
    {
        [SerializeField]
        private TextMeshProUGUI _workingMemoryFragments;

        [SerializeField]
        private TextMeshProUGUI _shortMemoryFragments;

        [SerializeField]
        private TextMeshProUGUI _longMemoryFragments;

        [SerializeField]
        private TextMeshProUGUI _cluesRemaining;

        public void Reset()
        {
            _workingMemoryFragments.text = "0";
            _shortMemoryFragments.text = "0";
            _longMemoryFragments.text = "0";
            _cluesRemaining.text = "0";
        }

        public void UpdateMemoryFragments(MemoryFragmentType fragmentType, int count)
        {
            switch(fragmentType) {
            case MemoryFragmentType.Working:
                _workingMemoryFragments.text = count.ToString();
                break;
            case MemoryFragmentType.Short:
                _shortMemoryFragments.text = count.ToString();
                break;
            case MemoryFragmentType.Long:
                _longMemoryFragments.text = count.ToString();
                break;
            }
        }

        public void UpdateClues(int remaining)
        {
            _cluesRemaining.text = remaining.ToString();
        }
    }
}
