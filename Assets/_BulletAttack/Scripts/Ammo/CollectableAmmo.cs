using System;
using System.Collections;
using UnityEngine;

public class CollectableAmmo : MonoBehaviour
{
    public Action<CollectableAmmo> Collected;

    [SerializeField] private AmmoData _data;

    [Space]
    [SerializeField] private Collider _collectionTriggerCollider;
    [SerializeField] private MeshRenderer _model;

    [Space] 
    [SerializeField] private ProgressView _collectionProgressView;
    [SerializeField] protected float _collectionTime;
    [SerializeField] protected float _restoreTime;

    private Coroutine _collectingRoutine;

    public AmmoData AmmoData => _data;
    public Vector3 ModelPosition => _model.transform.position;
    public Quaternion ModelRotation => _model.transform.rotation;

    public void StartCollecting(float timeModifier)
    {
        _collectingRoutine = StartCoroutine(CollectingRoutine(_collectionTime / timeModifier));
    }

    public void StopCollecting()
    {
        if (_collectingRoutine != null)
        {
            StopCoroutine(_collectingRoutine);

            _collectingRoutine = null;
            _collectionProgressView.Hide();
        }
    }

    private IEnumerator CollectingRoutine(float time)
    {
        var currentTime = 0f;
        _collectionProgressView.Show();

        while (currentTime <= time)
        {
            _collectionProgressView.SetProgress(currentTime / time);

            yield return null;

            currentTime += Time.deltaTime;
        }

        _collectingRoutine = null;
        _collectionProgressView.Hide();

        _collectionTriggerCollider.enabled = false;
        _model.gameObject.SetActive(false);
        Collected?.Invoke(this);

        StartCoroutine(RestoreRoutine(_restoreTime));
    }

    private IEnumerator RestoreRoutine(float time)
    {
        yield return new WaitForSeconds(time);

        _collectionTriggerCollider.enabled = true;
        _model.gameObject.SetActive(true);
    }
}