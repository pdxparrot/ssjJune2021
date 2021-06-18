using System;
using System.Collections.Generic;

using UnityEngine;

using pdxpartyparrot.Core.Util;
using pdxpartyparrot.ssjjune2021.Items;
using pdxpartyparrot.ssjjune2021.UI;

namespace pdxpartyparrot.ssjjune2021.Level
{
    public sealed class Clues : MonoBehaviour
    {
        #region Events

        public event EventHandler<EventArgs> CompleteEvent;

        #endregion

        //[SerializeReference]
        [ReadOnly]
        private /*readonly*/ HashSet<Clue> _activeClues = new HashSet<Clue>();

        //[SerializeReference]
        [ReadOnly]
        private /*readonly*/ HashSet<Clue> _solvedClues = new HashSet<Clue>();

        public int CluesRemaining => _activeClues.Count;

        public void RegisterClue(Clue clue)
        {
            _activeClues.Add(clue);
            _solvedClues.Remove(clue);

            GameUIManager.Instance.GameGameUI.PlayerHUD.UpdateClues(CluesRemaining);
        }

        public void UnRegisterClue(Clue clue)
        {
            _solvedClues.Remove(clue);
            _activeClues.Remove(clue);

            GameUIManager.Instance.GameGameUI.PlayerHUD.UpdateClues(CluesRemaining);
        }

        public void SolveClue(Clue clue)
        {
            Debug.Log($"Solved clue {clue.name}");

            _activeClues.Remove(clue);
            _solvedClues.Add(clue);

            GameUIManager.Instance.GameGameUI.PlayerHUD.UpdateClues(CluesRemaining);

            if(_activeClues.Count < 1) {
                CompleteEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Reset()
        {
            Debug.Log("Resetting clues...");

            foreach(Clue clue in _solvedClues) {
                clue.Reset();
                _activeClues.Add(clue);
            }
            _solvedClues.Clear();

            GameUIManager.Instance.GameGameUI.PlayerHUD.UpdateClues(CluesRemaining);
        }
    }
}
