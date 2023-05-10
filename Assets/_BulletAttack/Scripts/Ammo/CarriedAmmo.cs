using DG.Tweening;
using UnityEngine;

public class CarriedAmmo : MonoBehaviour
{
    [SerializeField] private AmmoSizeType _isBig;
    [SerializeField] private MeshRenderer _mesh;

    [Space] 
    [SerializeField] private Transform _sizePoint_1;
    [SerializeField] private Transform _sizePoint_2;

    public AmmoSizeType IsBig => _isBig;
    public float VerticalSize => Vector3.Distance(_sizePoint_1.position, _sizePoint_2.position);
    public float HalfVerticalSize => VerticalSize * 0.5f;

    public void MoveToCarriedPosition(Transform parent, Vector3 localPosition, float moveTime)
    {
        transform.parent = parent;
        transform.DOLocalMove(localPosition, moveTime);
        transform.DOLocalRotate(Vector3.zero, moveTime);
    }

    public void MoveToWeaponPosition(Transform weapon, float moveTime)
    {
        transform.parent = weapon;
        transform.DOLocalMove(Vector3.zero, moveTime);
        transform.DOLocalRotate(Vector3.zero, moveTime);
        transform.DOScale(Vector3.zero, moveTime).onComplete += () => {Destroy(gameObject); };
    }
}