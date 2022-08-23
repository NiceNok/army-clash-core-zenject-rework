using UnityEngine;

namespace Project.Scripts.UI
{
    public class BottomPanelStateChanger : MonoBehaviour
    {
        [SerializeField] private GameObject _bottomPanel;

        public void EnableBottomPanel(bool state)
        {
            _bottomPanel.SetActive(state);
        }
    }
}
