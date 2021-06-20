using System;

using UnityEngine;

namespace pdxpartyparrot.ssjjune2021
{
    [Serializable]
    public sealed class Settings
    {
        #region Events

        public event EventHandler<EventArgs> SettingsUpdatedEvent;

        #endregion

        [SerializeField]
        private bool _invertLookVertical;

        public bool InvertLookVertical
        {
            get => _invertLookVertical;

            set
            {
                _invertLookVertical = value;
                SettingsUpdatedEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Reset()
        {
            _invertLookVertical = false;
        }
    }
}
