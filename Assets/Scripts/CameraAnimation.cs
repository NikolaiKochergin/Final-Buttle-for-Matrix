using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    [SerializeField] private Animator _mainCameraAnimator;

    public void StartCameraAnimation()
    {
        _mainCameraAnimator.enabled = true;
    }
}
