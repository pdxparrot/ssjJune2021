using System;

using UnityEngine;

using pdxpartyparrot.Game.Interactables;

namespace pdxpartyparrot.ssjjune2021.Items
{
    [RequireComponent(typeof(Collider))]
    public sealed class Clue : MonoBehaviour, IInteractable
    {
        public bool CanInteract => true;

        public Type InteractableType => typeof(Clue);
    }
}
