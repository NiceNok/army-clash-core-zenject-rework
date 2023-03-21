using UnityEngine;

namespace Project.Scripts.UI
{
    public class BottomPanelView : MonoBehaviour
    {
        [SerializeField] private GameObject bottomPanel;

        public void EnableBottomPanel(bool state)
        {
            bottomPanel.SetActive(state);
            bottomPanel.SetActive(state);
        }
    }
}
