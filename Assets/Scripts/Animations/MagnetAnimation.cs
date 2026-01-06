using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagnetAnimation : MonoBehaviour
{
    private Sequence _magnetSequence;

    private readonly float _bombArcHeight = 3f;
    private readonly float _pullAnimationDuration = .5f;
    private readonly float _magnetScaleValue = 2f;
    private readonly float _scaleDuration = .3f;
    
    
    private void OnEnable()
    {
        //подписка на событие в MagnetAbility +=ActivateMagnetAnimation;
    }

    private void OnDisable()
    {
        //отписка -=ActivateMagnetAnimation;
    }

    private void OnBombsSelected(List<Bomb> bombs, Vector3 targetPosition, Color targetColor)
    {
        _magnetSequence = DOTween.Sequence();
        
        
    }
}
