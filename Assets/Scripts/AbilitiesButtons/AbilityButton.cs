using System;
using UnityEngine;

public class AbilityButton : ButtonListener
{
    [SerializeField] private TimeTokens tokens;
    [SerializeField] private TargetSelectionPanelUI _targetSelectionPanelUI;

    private bool _isActive = false;

    public event Action StateChanged;
    
    public bool IsActive => _isActive;

    private void Update()
    {
        if (_isActive && Input.GetMouseButtonDown(0))
        {
            ChangeState(false);
        }
    }

    protected override void OnClickButton()
    {
        if (tokens.Tokens == 0)
        {
            return;
        }

        _targetSelectionPanelUI.gameObject.SetActive(true);
        _targetSelectionPanelUI.ShowSelectionPanel();
        ChangeState(true);
    }

    private void ChangeState(bool value)
    {
        _isActive = value;
        StateChanged?.Invoke();
    }
}