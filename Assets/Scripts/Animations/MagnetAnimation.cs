using UnityEngine;
using DG.Tweening;

public class MagnetAnimation : MonoBehaviour
{
    [SerializeField] private MagnetAbility _magnetAbility;
    
    private readonly float _bombArcHeight = 3f;
    private readonly float _pullAnimationDuration = .5f;
    private readonly float _magnetScaleValue = 2f;
    private readonly float _scaleDuration = .3f;
    
    private Sequence _magnetSequence;
    
    private void OnEnable()
    {
        _magnetAbility.BombScanned += OnBombsSelected;
    }

    private void OnDisable()
    {
        _magnetAbility.BombScanned -= OnBombsSelected;

        //подчистка анимаций?
    }

    private void OnBombsSelected(Bomb bomb, Vector3 targetPosition)
    {
        Vector3 startPosition = bomb.transform.position;
        Vector3 startScale = bomb.transform.localScale;
        
        _magnetSequence = DOTween.Sequence();

        _magnetSequence
            .Append(bomb.transform.DOJump(targetPosition, _bombArcHeight, 1, _pullAnimationDuration));
    }
}
