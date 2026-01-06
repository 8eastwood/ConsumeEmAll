using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityButton))]
public class MagnetAbility : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _shapeMask;
    [SerializeField] private LayerMask _bombMask;
    [SerializeField] private TargetSelectionPanelUI _targetSelectionPanelUI;
    [SerializeField] private AbilityTokensHandler _tokensHandler;
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _pullAnimationDuration = .5f;

    private AbilityButton _abilityButton;
    private bool _isSelectingTarget = false;
    
    private readonly Collider[] _collidersBuffer = new Collider[20];


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

        if (Physics.Raycast(ray, out hit, 1000f, _shapeMask))
        {
            GameObject clickedObject = hit.collider.gameObject;

            if (clickedObject.TryGetComponent(out ColorIdentity shapeColor))
            {
                // тут вместо метода запускаем корутину
                // но может быть адаптирую через апдейт

                StartCoroutine(ActivateMagnet(shapeColor));
                _tokensHandler.RemoveToken();
                _targetSelectionPanelUI.HideAbilityPanel();
            }
        }
    }

    private IEnumerator ActivateMagnet(ColorIdentity shapeColor)
    {
        Vector3 centerPosition = shapeColor.transform.position;
        ColorType targetColor = shapeColor.Color;
        
        int bombCount = Physics.OverlapSphereNonAlloc(
            transform.position,
            _radius,
            _collidersBuffer,
            _bombMask
        );

        List<Bomb> bombsToPull = new List<Bomb>();

        for (int i = 0; i < bombCount; i++)
        {
            if (_collidersBuffer[i].TryGetComponent(out Bomb bomb))
            {
                if (bomb.Color == targetColor)
                {
                    bombsToPull.Add(bomb);
                }
            }
        }

        if (bombsToPull.Count > 0)
        {
            yield return StartCoroutine(PullBombsCoroutine(bombsToPull, centerPosition));
        }
        else
        {
            Debug.Log("No matching bombs found in radius");
        }
    }

    private IEnumerator PullBombsCoroutine(List<Bomb> bombs, Vector3 targetPosition)
    {
        Dictionary<Bomb, Vector3> startPositions = new Dictionary<Bomb, Vector3>();
        
        foreach (var bomb in bombs)
        {
            if(bomb!= null)
                startPositions[bomb] = bomb.transform.position;
        }

        float elapsedTime = 0f;

        while (elapsedTime < _pullAnimationDuration)
        {
            elapsedTime += Time.deltaTime;
            float time = elapsedTime / _pullAnimationDuration;

            for (int i = bombs.Count - 1; i >= 0; i--)
            {
                if(bombs[i] == null)
                    bombs.RemoveAt(i);
            }

            foreach (var bomb in bombs)
            {
                if (bomb == null) continue;
                
                Vector3 startPosition = startPositions.ContainsKey(bomb) ?
                    startPositions[bomb] : bomb.transform.position;
                bomb.transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            }
            
            yield return null;
        }
    }
}