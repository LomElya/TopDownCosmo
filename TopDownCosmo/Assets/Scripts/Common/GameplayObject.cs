using UnityEngine;

public class GameplayObject : MonoBehaviour
{
    public GameplayObject Owner { get; private set; }

    public virtual void Init()
    {
        SetOwner(this);
    }

    protected void SetOwner(GameplayObject owner) => Owner = owner;
}
