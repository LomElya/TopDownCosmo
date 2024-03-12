public class TypeShipButtonManager : ButtonManager<ShipType>
{
    protected override void OnClick(ShipType obj, ButtonMain button)
    {
        base.OnClick(obj, button);

        foreach (var s in _settings)
            s.Button.UnSelect();

        button.Select();
    }
}
