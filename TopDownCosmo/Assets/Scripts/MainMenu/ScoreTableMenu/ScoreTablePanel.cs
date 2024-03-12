using System.Linq;
using Zenject;

public class ScoreTablePanel : MainMenuPanel<ScoreTableSlot>
{
    private UserContainer _userContainer;

    [Inject]
    private void Construct(UserContainer userContainer)
    {
        _userContainer = userContainer;
    }

    protected override void OnShow(ScoreTableSlot slot, int id)
    {
        int score = _userContainer.GetLevelScore(id);

        slot.Init(id, score);
    }

    public override void Sort()
    {
        _slots = _slots
            .OrderByDescending(x => x.Score)
            .ToList();

        for (int i = 0; i < _slots.Count; i++)
            _slots[i].transform.SetSiblingIndex(i);
    }
}
