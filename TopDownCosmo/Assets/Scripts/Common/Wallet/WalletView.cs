using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Text))]
public class WalletView : MonoBehaviour
{
    [SerializeField] private string _template;
    private Text _value;

    private Wallet _wallet;

    private void OnValidate() =>
        _value ??= GetComponent<Text>();

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;

        UpdateValue(_wallet.GetCurrentGold());

        _wallet.GoldChange += UpdateValue;
    }

    public void UpdateValue(int value) => _value.text = $"{_template}{value}";

    private void OnDestroy() => _wallet.GoldChange -= UpdateValue;
}
