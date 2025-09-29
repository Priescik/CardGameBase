using UnityEngine;

public interface IDamagable
{
    public bool TakeDamage(int amount);
    /// Returns bool if damage was fatal

}
