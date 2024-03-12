using UnityEngine;
using Zenject;

public class InteractableMover : MonoBehaviour
{
    [SerializeField] private float _speedCoef;

    public float SpeedCoef => _speedCoef;

    private const float _minBordedPosition = Constants.Border.DOWN_POSITION;

    private float _startSpeed;
    private float _endSpeed;
    private float _levelDuration;
    private Level _level;
    private Timer _timer;

    [Inject]
    private void Construct(Level level)
    {
        _level = level;
        _timer = _level.Timer;
    }

    public void StartProcess(UserLevelState levelState)
    {
        _startSpeed = levelState.StartSpeed;
        _endSpeed = levelState.EndSpeed;
        _levelDuration = levelState.LevelLength;
    }

    public void Move(Interactable interactable)
    {
        interactable.transform.Translate(Vector2.down * (Time.deltaTime * _speedCoef));

        _speedCoef = Mathf.Lerp(_startSpeed, _endSpeed, (_timer.RemainigTime / _levelDuration));

        if (interactable.transform.position.y <= _minBordedPosition)
            interactable.Hide();
    }
}
