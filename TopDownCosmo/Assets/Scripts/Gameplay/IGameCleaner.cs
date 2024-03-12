using System.Collections.Generic;

public interface IGameModeCleaner
{
    IEnumerable<GameObjectFactory> Factories { get; }
    string SceneName { get; }
    void Cleanup();
}
