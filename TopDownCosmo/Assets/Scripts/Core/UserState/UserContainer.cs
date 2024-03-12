public sealed class UserContainer
{
    public UserState State { get; private set; }

    public void SetState(UserState state) =>
        State = state;

    public void ChangeLevel(int id) => State.SurviveScenario.ChangeLevel(id);
    public void OpenLevel(int id) => State.SurviveScenario.OpenLevel(id);
    public int CurrentLevel() => State.SurviveScenario.CurrentLevelID;
    public int GetLevelScore(int id) => State.SurviveScenario.Levels[id].Score;
    public bool IsSelectedLevel(int id) => State.SurviveScenario.Levels[id].Selected;
    public bool IsOpenedLevel(int id) => State.SurviveScenario.Levels[id].IsLevelOpen;
    public bool IsLastLevel(int id) => State.SurviveScenario.Levels.Count == id + 1;
    public UserLevelState GetLevelState() => State.SurviveScenario.Levels[CurrentLevel()];

    public void ChangeShip(int id) => State.ChangeShip(id);
    public void OpenShip(int id) => State.OpenShip(id);

    public void ChangeShip(int id, ShipType type) => State.ChangeShip(id, type);
    public void OpenShip(int id, ShipType type) => State.OpenShip(id, type);

    public int CurrentShip() => State.CurrentShipID;
    public bool IsSelectedShip(int id)
    {
        foreach (var contentShip in State.ContentShips)
        {
            foreach (var ship in contentShip.Ships)
            {
                if (ship.ID == id)
                    return ship.Selected;
            }
        }

        return false;
    }

    public bool IsOppenedShip(int id)
    {
        foreach (var contentShip in State.ContentShips)
        {
            foreach (var ship in contentShip.Ships)
            {
                if (ship.ID == id)
                    return ship.IsShipOpen;
            }
        }

        return false;
    }

    public void AddGold(int value) => State.Currencies.AddGold(value);
    public void SpendGold(int value) => State.Currencies.SpendGold(value);
    public bool IsEnought(int value) => GetCurrentGold() >= value;
    public int GetCurrentGold() => State.Currencies.Gold;

    public void ChangeScore(int value) => State.SurviveScenario.Levels[CurrentLevel()].Score += value;
    public int GetCurrentLevelScore() => State.SurviveScenario.Levels[CurrentLevel()].Score;
}