using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SelectLevelSlot : SelectableMenuSlot, IPointerClickHandler
{
    [SerializeField] private StringValueView _maxScoreText;
    [SerializeField] private IntValueView _levelText;

    public int ID { get; private set; }
    public int Score { get; private set; }

    public void Init(int id, int score)
    {
        Init();

        ID = id;
        Score = score;
        _levelText.Show(ID + 1);
    }

    public override void Lock()
    {
        base.Lock();
        _maxScoreText.Hide();
    }

    public override void UnLock()
    {
        base.UnLock();
        _maxScoreText.Show(Score.ToString());
    }
}
