using SerializeReferenceEditor;
using UnityEngine;
[System.Serializable]

public class AutoTargetEffect
{
    [field: SerializeReference, SR] public Effect Effect { get; private set; }
    //[SerializeField] public TargetMode TargetMode { get; private set; } = new TargetMode();
    [SerializeField] private TargetMode _targetMode = new TargetMode();
    public TargetMode TargetMode => _targetMode;
}
