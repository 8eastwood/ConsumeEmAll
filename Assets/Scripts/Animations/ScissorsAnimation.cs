using UnityEngine;
using DG.Tweening;

public class ScissorsAnimation : MonoBehaviour
{
    private const string LeftBladeName = "LeftBlade";
    private const string RightBladeName = "RightBlade";

    [SerializeField] private CutterAbilityButton _cutterAbilityButton;
    [SerializeField] private GameObject _scissorsPrefab;

    private readonly float _scissorsPositionY = 0.7f;
    private readonly float _scissorsPositionZ = 1f;
    private readonly float _scissorsRotationX = -90f;
    private readonly float _scissorsScaleValue = 4f;
    private readonly float _scaleAnimationDuration = 0.25f;
    private readonly float _moveAnimationDuration = 0.5f;
    private readonly float _scissorsBladeRotationX = -90;
    
    private GameObject _scissorsInstance;
    private Transform _scissorsTransform;
    private Transform _scissorsRightBlade;
    private Transform _scissorsLeftBlade;
    private Sequence _scissorsSequence;
   

    private void OnEnable()
    {
        _cutterAbilityButton.BombTargeted += OnBombTargeted;
    }

    private void OnDisable()
    {
        _cutterAbilityButton.BombTargeted -= OnBombTargeted;
    }

    private void OnBombTargeted(Bomb bomb)
    {
        ScissorsInitialize();

        if (_scissorsSequence != null && _scissorsSequence.IsActive())
        {
            _scissorsSequence.Kill();
        }

        _scissorsSequence = DOTween.Sequence();
        Vector3 bombPosition = bomb.transform.position;
        Vector3 targetPosition = bombPosition + new Vector3(0, _scissorsPositionY, _scissorsPositionZ);
        _scissorsTransform.rotation = Quaternion.Euler(_scissorsRotationX, 0f, 0f);

        _scissorsSequence
            .AppendCallback(() => { _scissorsTransform.localPosition = targetPosition; })
            .Append(_scissorsTransform.transform.DOScale(_scissorsScaleValue, _scaleAnimationDuration).SetEase(Ease.OutBack))
            .Append(_scissorsTransform.transform.DOLocalMoveZ(bomb.transform.position.z + .5f, _moveAnimationDuration))
            .Join(_scissorsRightBlade.transform.DORotate(new Vector3(_scissorsBladeRotationX, 0, 0), _moveAnimationDuration))
            .Join(_scissorsLeftBlade.transform.DORotate(new Vector3(_scissorsBladeRotationX, 0, 0), _moveAnimationDuration))
            .AppendCallback(() =>
            {
                BombAnimator bombAnimator = bomb.GetComponent<BombAnimator>();
                ColorIdentity bombColor = bomb.GetComponent<ColorIdentity>();

                if (bombAnimator != null)
                {
                    bombAnimator.PlayDefuseAnimation();
                    BombEvents.NotifyBombDestroyed(bomb, bombColor.Color);
                }
                else
                    Debug.Log("animator wasn't found on bomb");
            })
            .Append(_scissorsTransform.transform.DOScale(0f, 0.25f).SetEase(Ease.InBack))
            .OnComplete(() => { Destroy(_scissorsInstance); });
    }

    private void ScissorsInitialize()
    {
        _scissorsInstance = Instantiate(_scissorsPrefab);
        _scissorsTransform = _scissorsInstance.transform;
        _scissorsRightBlade = _scissorsTransform.Find(RightBladeName);
        _scissorsLeftBlade = _scissorsTransform.Find(LeftBladeName);
    }
}