using UnityEngine;

public class UnitAnimationHandler : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    
    private void HandleDieAnimation()
    {
        _unit.gameObject.SetActive(false);
    }

    private void HandleHitTarget()
    {
        _unit.HitTarget();
    }
}
