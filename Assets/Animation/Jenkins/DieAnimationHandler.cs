using UnityEngine;

public class DieAnimationHandler : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    
    private void HandleDieAnimation()
    {
        _unit.gameObject.SetActive(false);
    }
}
