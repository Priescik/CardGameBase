using UnityEngine;
using System.Collections.Generic;
using SerializeReferenceEditor;

[System.Serializable]
public abstract class Effect
{
    public abstract GameAction GetGameAction(CardInstance cardSource, EntityView entitySource, List<EntityView> targets);
    // TODO this feels like not very object-oriented:
    // some effects will only use cardSource and some will only use entitySource
    // one of two will offten be set to null
}
