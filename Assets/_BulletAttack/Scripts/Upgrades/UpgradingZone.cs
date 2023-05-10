using UnityEngine;

public class UpgradingZone : MonoBehaviour
{
    private UpgradesMenu _upgradesMenu;

    [EditorButton]
    public void SetUpgradesMenu(UpgradesMenu menu)
    {
        _upgradesMenu = menu;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player != null && _upgradesMenu != null)
            _upgradesMenu.Open();
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player != null && _upgradesMenu != null)
            _upgradesMenu.Close();
    }
}