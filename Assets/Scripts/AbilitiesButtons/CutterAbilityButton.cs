using System;
using System.Collections;
using UnityEngine;

public class CutterAbilityButton : ButtonListener
{
    [SerializeField] private AbilityTokensHandler _tokensHandler;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _bombMask;

    private bool _isSelectingTarget = false;

    public event Action<Bomb> BombTargeted;

    private void Update()
    {
        if (!_isSelectingTarget)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TrySelectTarget());
        }
    }

    protected override void ClickOnButton()
    {
        // Debug.Log("had to pick a bomb for it's fuse to be cutted");
        OnButtonClick();
    }

    private void OnButtonClick()
    {
        if (_tokensHandler.Tokens > 0)
        {
            _isSelectingTarget = true;
            Debug.Log("selecting is ongoing");
        }
        else
            Debug.Log("can't use ability rn - no tokens");
    }

    private IEnumerator TrySelectTarget()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, _bombMask))
        {
            GameObject clickedObject = hit.collider.gameObject;
            
            if (clickedObject.TryGetComponent(out Bomb bomb))
            {
                Debug.Log("Bomb selected");
                BombTargeted?.Invoke(bomb);
                _tokensHandler.RemoveToken();
                _isSelectingTarget = false;
            }
            else
            {
                Debug.Log("choose correct target");
            }
        }
        else
        {
            Debug.Log("No valid target found");
        }

        yield return null;
    }
}
