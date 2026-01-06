using System;
using System.Collections;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    private bool _isSelectingTarget = false;

    public bool IsSelectingTarget => _isSelectingTarget;

    public event Action<Bomb> BombTargeted;

    private IEnumerator TrySelectTarget(Camera camera, LayerMask bombMask)
    {
        while (_isSelectingTarget)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, bombMask))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.TryGetComponent(out Bomb bomb))
                {
                    BombTargeted?.Invoke(bomb);
                    _isSelectingTarget = false;
                    Debug.Log("Bomb Targeted");
                    yield break;
                    // tokensHandler.RemoveToken();
                    // targetSelectionPanelUI.HideAbilityPanel();
                }
            }

            yield return null;
        }
    }

    public void BeginSelection(Camera camera, LayerMask bombMask)
    {
        if (_isSelectingTarget) return;
        
        _isSelectingTarget = true;

        StartCoroutine(TrySelectTarget(camera, bombMask));
        Debug.Log("Target selection started");
    }
}