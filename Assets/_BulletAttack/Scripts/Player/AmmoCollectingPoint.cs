using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AmmoCollectingPoint : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _ammoCollectionPoint;
    [SerializeField] private Collider _blockingCollider;
    [SerializeField] private Transform _model;

    public Transform AmmoCollectionPoint => _ammoCollectionPoint;

    public Weapon Weapon => _weapon;


    public void StartCollecting(float offset)
    {
        _blockingCollider.enabled = true;
        StartCoroutine(BoopRoutine(offset));
    }

    public void StopCollecting()
    {
        _blockingCollider.enabled = false;
        StopAllCoroutines();
    }

    private IEnumerator BoopRoutine(float offset)
    {
        yield return new WaitForSeconds(Mathf.Max(0, offset - 0.2f));
        while (true)
        {
            var modelScale = _model.localScale.x;
            yield return _model.DOScale((modelScale + 0.1f), 0.05f).WaitForCompletion();
            SoundManager.Instance.WeaponAmmo.Play();

            yield return null;
            yield return null;

            yield return _model.DOScale(modelScale, 0.05f).WaitForCompletion();

            yield return null;
            yield return null;
        }
    }
}