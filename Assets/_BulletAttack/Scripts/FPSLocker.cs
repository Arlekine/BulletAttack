using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLocker : MonoBehaviour
{
    [SerializeField] private int _targetFramerate;

    private void Start()
    {
        Application.targetFrameRate = _targetFramerate;
    }
}
