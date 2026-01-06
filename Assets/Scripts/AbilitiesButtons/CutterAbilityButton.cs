using System;
using System.Collections;
using UnityEngine;

public class CutterAbilityButton : ButtonListener
{
    [SerializeField] private AbilityTokensHandler _tokensHandler;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _bombMask;

    [SerializeField] private TargetSelectionPanelUI _targetSelectionPanelUI;

    // [SerializeField] private TargetSelector _targetSelector;
    [SerializeField] private ScissorsAnimation _scissorsAnimation;

    // private bool _isSelectingTarget = false;
    private bool _isActive = false;

    public event Action<Bomb> BombTargeted;

    // private void OnEnable()
    // {
    //     _targetSelector.BombTargeted += OnBombSelected;
    // }
    //
    // private void OnDisable()
    // {
    //     _targetSelector.BombTargeted -= OnBombSelected;
    // }

    private void Update()
    {
        // if (!_targetSelector.IsSelectingTarget)
        //     return;
        
        if (_isActive && Input.GetMouseButtonDown(0))
        {
            TrySelectTarget();
        }
    }

    protected override void OnClickButton()
    {
        if (_tokensHandler.Tokens == 0)
        {
            return;
        }

        _targetSelectionPanelUI.gameObject.SetActive(true);
        _targetSelectionPanelUI.ShowAbilityPanel();
        // _targetSelector.BeginSelection(_camera, _bombMask);
        // _isSelectingTarget = true;
        _isActive = true;
    }

    // private void OnBombSelected(Bomb bomb)
    // {
    //     _targetSelectionPanelUI.HideAbilityPanel();
    //     _tokensHandler.RemoveToken();
    //     _scissorsAnimation.OnBombTargeted(bomb);
    // }

    private void TrySelectTarget()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, _bombMask))
        {
            GameObject clickedObject = hit.collider.gameObject;

            if (clickedObject.TryGetComponent(out Bomb bomb))
            {
                BombTargeted?.Invoke(bomb);
                _tokensHandler.RemoveToken();
                // _isSelectingTarget = false;
                _targetSelectionPanelUI.HideAbilityPanel();
            }
        }
    }
}