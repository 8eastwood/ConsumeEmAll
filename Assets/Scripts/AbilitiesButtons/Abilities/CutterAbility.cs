using System;
using UnityEngine;

[RequireComponent(typeof(AbilityButton))]
public class CutterAbility : MonoBehaviour
{
    [SerializeField] private TargetSelectionPanelUI _targetSelectionPanelUI;
    [SerializeField] private TimeTokens tokens;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _bombMask;

    private AbilityButton _abilityButton;

    public event Action<Bomb> BombTargeted;

    private void Awake()
    {
        _abilityButton = GetComponent<AbilityButton>();
    }

    private void OnEnable()
    {
        _abilityButton.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _abilityButton.StateChanged -= OnStateChanged;
    }

    private void OnStateChanged()
    {
        if (_abilityButton.IsActive)
        {
            return;
        }

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, _bombMask))
        {
            GameObject clickedObject = hit.collider.gameObject;

            if (clickedObject.TryGetComponent(out Bomb bomb))
            {
                tokens.RemoveToken();
                _targetSelectionPanelUI.HideSelectionPanel();
                BombTargeted?.Invoke(bomb);
            }
        }
    }
}