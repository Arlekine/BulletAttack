using UnityEngine;

public class WeaponScaler : MonoBehaviour
{
    [SerializeField] private float _maxScale;
    [SerializeField] private Transform _turretPart;

    private PlayerGrower _playerGrower;

    public void SetPlayer(PlayerGrower grower)
    {
        _playerGrower = grower;
        _playerGrower.Growed += Scale;
    }

    private void OnDestroy()
    {
        if (_playerGrower != null)
            _playerGrower.Growed -= Scale;
    }

    [EditorButton]
    public void Scale(PlayerGrower player)
    {
        _turretPart.localScale = Vector3.one * Mathf.Lerp(1f, _maxScale, player.ScaleProgress);
    }
}