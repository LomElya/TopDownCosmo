using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UserStateExtensions
{
    public static UserLevelState TryGetLevel(this UserState state, int id)
    {
        UserLevelState levelState = state.SurviveScenario.Levels.FirstOrDefault(x => x.ID == id);

        return levelState;
    }

    public static void AddOrReplaceLevel(this UserState state, UserLevelState levelState)
    {
        int id = levelState.ID;
        var stateLevel = state.TryGetLevel(id);

        if (stateLevel == null)
        {
            if (state.IsFirstLevel())
            {
                levelState.Selected = true;
                levelState.IsLevelOpen = true;
            }

            state.SurviveScenario.Levels.Add(levelState);
        }
        else
        {
            levelState.Selected = state.SurviveScenario.Levels[id].Selected;
            levelState.IsLevelOpen = state.SurviveScenario.Levels[id].IsLevelOpen;
            levelState.Score = state.SurviveScenario.Levels[id].Score;
            
            state.SurviveScenario.Levels[id] = levelState;
        }
    }

    public static InteractableSpawnState TryGetInteractable(this UserLevelState levelState, InteractableType type)
    {
        var interactableState = levelState.InteractableSpawn.FirstOrDefault(x => x.InteractableType == type);

        return interactableState;
    }

    public static void AddOrReplaceLevelInteractable(this UserLevelState levelState, InteractableSpawnState interactableState)
    {
        var index = levelState.InteractableSpawn.IndexOf(interactableState);

        if (index == -1)
            levelState.InteractableSpawn.Add(interactableState);
        else
            levelState.InteractableSpawn[index] = interactableState;
    }

    public static UserContentShipState TryGetContentShip(this UserState state, ShipType type)
    {
        UserContentShipState contentState = state.ContentShips.FirstOrDefault(x => x.Type == type);

        return contentState;
    }

    public static void AddOrReplaceContentShip(this UserState state, UserContentShipState contentShip)
    {
        int index = state.ContentShips.IndexOf(contentShip);

        if (index == -1)
            state.ContentShips.Add(contentShip);
        else
            state.ContentShips[index] = contentShip;
    }

    public static UserShipState TryGetShip(this UserContentShipState contentState, int id)
    {
        UserShipState shipState = contentState.Ships.FirstOrDefault(x => x.ID == id);

        return shipState;
    }

    public static void AddOrReplaceShip(this UserContentShipState contentState, UserShipState ship)
    {
        int index = contentState.Ships.IndexOf(ship);

        if (index == -1)
        {
            contentState.Ships.Add(ship);
        }
        else
        {
            ship.IsShipOpen = contentState.Ships[index].IsShipOpen;
            ship.Selected = contentState.Ships[index].Selected;
            contentState.Ships[index] = ship;
        }
    }

    public static bool IsValid(this UserState state) => state.ID > 0;
    public static bool IsFirstStart(this UserState state) => state.IsFirstStart;
    public static bool IsFirstLevel(this UserState state) => state.SurviveScenario.Levels.Count == 0;
}