using UnityEngine.Events;

public class ApplyEffectCommand : Command
{

  public ApplyEffectCommand(ApplyEffectData data) : base(data)
  {
  }

  public override void Execute(UnityAction onCompleted)
  {
    var effectData = (ApplyEffectData)_commandData;

    var effectMediator = effectData.Triggerable.EffectMediator;

    var effect = effectData.EffectModel.EffectType.GetEffect(effectData.EffectModel);

    effectMediator.Replace(effect);
  }
}
