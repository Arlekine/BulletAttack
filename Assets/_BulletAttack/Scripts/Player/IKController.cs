using DitzelGames.FastIK;
using UnityEngine;

public class IKController : MonoBehaviour
{
    [SerializeField] private FastIKFabric _leftHand;
    [SerializeField] private FastIKFabric _rightHand;
    [SerializeField] private Transform _standartLeftTarget;
    [SerializeField] private Transform _standartRightTarget;

    public void SetIKPoint(Transform left, Transform right)
    {
        _leftHand.Target = left;
        _rightHand.Target = right;
    }

    public void SetStandart()
    {
        _leftHand.Target = _standartLeftTarget;
        _rightHand.Target = _standartRightTarget;
    }

    public void Activate()
    {
        _leftHand.enabled = true;
        _rightHand.enabled = true;
    }

    public void Deactivate()
    {
        _leftHand.enabled = false;
        _rightHand.enabled = false;
    }
}