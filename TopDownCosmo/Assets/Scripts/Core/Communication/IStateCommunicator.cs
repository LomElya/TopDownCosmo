using Cysharp.Threading.Tasks;

public interface IStateCommunicator
{
    UniTask<bool> SaveState(UserState state);
    UniTask<UserState> GetState();

    UniTask<byte[]> ReadBytes();
}