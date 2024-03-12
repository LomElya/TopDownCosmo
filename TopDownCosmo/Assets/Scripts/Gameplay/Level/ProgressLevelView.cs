using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ProgressLevelView : MonoBehaviour
{
    [SerializeField] private Image _filler;

    private Timer _timer;

    [Inject]
    private void Construct(Level level)
    {
        _timer = level.Timer;

        _timer.HasBeenUpdated += OnTimerUpdate;
    }

    private void OnTimerUpdate() =>
        _filler.fillAmount = _timer.RemainigTime / _timer.MaxTime;


    private void OnDestroy() =>
        _timer.HasBeenUpdated -= OnTimerUpdate;
}
