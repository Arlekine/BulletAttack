using UnityEngine;

public class AnimationYOnPartLocker : MonoBehaviour
{
    [SerializeField] private float _targetY;

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _targetY, transform.localEulerAngles.z);
    }
}