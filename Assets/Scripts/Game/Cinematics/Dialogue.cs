using UnityEngine;

using pdxpartyparrot.Core.UI;

namespace pdxpartyparrot.Game.Cinematics
{
    [RequireComponent(typeof(UIObject))]
    public class Dialogue : MonoBehaviour
    {
        [SerializeField]
        private Dialogue _nextDialogue;

        public Dialogue NextDialogue => _nextDialogue;
    }
}
