using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOnPartLocker : MonoBehaviour
{
    private Vector3 _awakeRotation;

    private void Awake()
    {
        _awakeRotation = transform.localEulerAngles;
    }

    private void LateUpdate()
    {
        transform.localEulerAngles = _awakeRotation;
    }
}