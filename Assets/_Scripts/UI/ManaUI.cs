using UnityEngine;
using TMPro;

public class ManaUI : MonoBehaviour
{
    [SerializeField] TMP_Text _mana;

    public void UpdateManaText(int newMana)
    {
        _mana.text = newMana.ToString();
    }
}
