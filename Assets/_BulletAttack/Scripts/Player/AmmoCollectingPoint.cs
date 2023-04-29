using UnityEngine;

public class AmmoCollectingPoint : MonoBehaviour
{
    [SerializeField] private Transform _ammoCollectionPoint;

    public Transform AmmoCollectionPoint => _ammoCollectionPoint;

}