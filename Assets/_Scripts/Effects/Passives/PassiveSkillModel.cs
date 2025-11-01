using UnityEngine;
using System.Collections.Generic;


public class PassiveSkillModel
{
    readonly PassiveSkillData _data;
    readonly TriggerCondition _condition;
    readonly AutoTargetEffect _effect;
    readonly EntityView _owner;
    public PassiveSkillModel(PassiveSkillData passiveSkillData, EntityView owner)
    {
        _data = passiveSkillData;
        _condition = _data.TriggerCondition;
        _effect = _data.AutoTargetEffect;
        _owner = owner;
    }
    //onadd onremove 1:27
    public void OnAdd()
    {
        _condition.SubscribeCondition(Reaction);
    }

    public void OnRemove()
    {
        _condition.UnsubscribeCondition(Reaction);
    }

    private void Reaction(GameAction gameAction)
    {
        if (_condition.SubConditionIsMet(_owner, gameAction))
        {
            List<EntityView> targets = new();
            if (_data.AddActionSourceAsTarget && gameAction is IHasEntitySource actionWithSource)
            {
                targets.Add(actionWithSource.EntitySource);
            }
            if (_data.AddActionTargetAsTarget && gameAction is IHasTargets actionWithTarget) // && //*has target* gameAction is IHasEntitySource entitySource)
            {
                targets.AddRange(actionWithTarget.Targets);
            }
            if (_data.AddPassiveOwnerAsTarget)
            {
                targets.Add(_owner);
            }
            if (_effect.TargetMode.GetTargets(_owner.Side) != null)
            {
                targets.AddRange(_effect.TargetMode.GetTargets(_owner.Side)); 
            }
            GameAction passiveSkillEffectAction = _effect.Effect.GetGameAction(null, _owner, targets); // TODO pass cardSource:_owner._cardInstance?
            ActionSystem.Instance.AddReaction(passiveSkillEffectAction);
        }
    }

}
