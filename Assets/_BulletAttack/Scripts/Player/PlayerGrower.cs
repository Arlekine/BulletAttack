using System;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class PlayerGrower : MonoBehaviour
{
    public Action<PlayerGrower> Growed;

    [SerializeField] private SkinnedMeshRenderer _playerSkin;
    [SerializeField] private ParticleSystem _upgradeEffect;
    [SerializeField] private IKController _ikController;
    [SerializeField] private Transform _model;
    [Min(0f)][SerializeField] private float _blendShapeStep = 1.5f;

    [Space] 
    [Min(1.1f)][SerializeField] private float _maxScale = 1.5f;
    [Min(0f)][SerializeField] private float _scaleStep = 0.2f;

    public float CurrentScale => _model.localScale.x;

    public float ScaleProgress => (_model.localScale.x - 1f) / (_maxScale - 1f);

    private void Start()
    {
        _upgradeEffect.gameObject.SetActive(true);
    }

    [EditorButton]
    public void IncreaseGrowth()
    {
        var currentBlendShape = _playerSkin.GetBlendShapeWeight(0);
        _playerSkin.SetBlendShapeWeight(0, Mathf.Max(currentBlendShape - _blendShapeStep, 0));
        _model.localScale = Vector3.one * Mathf.Min(_maxScale, _model.localScale.x + _scaleStep);
        Growed?.Invoke(this);

        if (_model.localScale.x < _maxScale)
        {
            _ikController.RecalculateIK();
            _upgradeEffect.Play();
        }
    }
}