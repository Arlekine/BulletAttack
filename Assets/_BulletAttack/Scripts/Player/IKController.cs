using System.Collections;
using DG.Tweening;
using DitzelGames.FastIK;
using UnityEngine;

public class IKController : MonoBehaviour
{
    [SerializeField] private FastIKFabric _leftHand;
    [SerializeField] private FastIKFabric _rightHand;
    [SerializeField] private Transform _standartLeftTarget;
    [SerializeField] private Transform _standartRightTarget;

    private Vector3 _standartPositionLeft;
    private Vector3 _standartPositionRigth;

    private void Start()
    {
        _standartPositionLeft = _standartLeftTarget.localPosition;
        _standartPositionRigth = _standartRightTarget.localPosition;
    }

    public void RecalculateIK()
    {
        _leftHand.ChainLength = 8;
        _rightHand.ChainLength = 8;

        _leftHand.Init();
        _rightHand.Init();

        StopAllCoroutines();
        StartCoroutine(ChainLengthReturnRoutine());
    }

    public void SetIKPoint(Transform left, Transform right, bool isLocal = false)
    {
        if (isLocal)
        {
            _standartLeftTarget.DOLocalMove(left.localPosition, 0.5f);
            _standartRightTarget.DOLocalMove(right.localPosition, 0.5f);
        }
        else
        {
            _standartLeftTarget.DOMove(left.position, 0.5f);
            _standartRightTarget.DOMove(right.position, 0.5f);
        }
    }

    public void SetStandart()
    {
        _standartLeftTarget.DOLocalMove(_standartPositionLeft, 0.5f);
        _standartRightTarget.DOLocalMove(_standartPositionRigth, 0.5f);
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

    private IEnumerator ChainLengthReturnRoutine()
    {
        yield return null;
        yield return null;

        _leftHand.ChainLength = 7;
        _rightHand.ChainLength = 7;
    }
}