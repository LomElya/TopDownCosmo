using UnityEngine;
using Zenject;

public class SelectLevelMenu : MainMenu
{
    [SerializeField] private SelectLevelPanel _selectLevelPanel;

    private LevelConfig _levelConfig;
    private Level _level;

    private SelectLevelSlot _selectSlot;

    [Inject]
    private void Construct(LevelConfig levelConfig, Level level)
    {
        _levelConfig = levelConfig;
        _level = level;
    }

    protected override void OnShow()
    {
        _selectLevelPanel.SlotViewClicked += OnItemViewClicked;

        _selectLevelPanel.Show(_levelConfig.LevelData);
    }

    protected override void Hide()
    {
        if (_selectSlot == null)
            return;

        int id = _selectSlot.ID;

        _level.ChangeLevel(id);

        base.Hide();
    }

    private void OnItemViewClicked(SelectLevelSlot slot) => _selectSlot = slot;
}
