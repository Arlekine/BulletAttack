using DG.Tweening;
using UnityEngine;

public class CarriedAmmo : MonoBehaviour
{
    [SerializeField] private AmmoSizeType _isBig;
    [SerializeField] private MeshRenderer _mesh;

    public AmmoSizeType IsBig => _isBig;
    public float VerticalSize => _mesh.bounds.size.x;

    public void MoveToCarriedPosition(Transform parent, Vector3 localPosition, float moveTime)
    {
        transform.parent = parent;
        transform.DOLocalMove(localPosition, moveTime);
        transform.DOLocalRotate(Vector3.zero, moveTime);
    }

    public void MoveToWeaponPosition(Transform weapon, float moveTime, float offset)
    {
        transform.parent = weapon;
        transform.DOLocalMove(Vector3.zero, moveTime).SetDelay(offset);
        transform.DOLocalRotate(Vector3.zero, moveTime).SetDelay(offset);
        transform.DOScale(Vector3.zero, moveTime).SetDelay(offset).onComplete += () => {Destroy(gameObject); };
    }
}