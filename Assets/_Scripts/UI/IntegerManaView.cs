using UnityEngine;
using TMPro;

public class IntegerManaView : MonoBehaviour
{
    [SerializeField] TMP_Text _manaText;
    IntegerMana _mana;

    public void Bind(IntegerMana mana)
    {
        Unbind();

        _mana = mana;
        _mana.Changed += UpdateManaText;

        //UpdateManaText(_mana.Current);
    }
    private void Unbind()
    {
        if (_mana == null)
            return;

        _mana.Changed -= UpdateManaText;
        _mana = null;
    }

    public void UpdateManaText(IntegerManaAmount newAmount)
    {
        _manaText.text = newAmount.Amount.ToString();
    }
    private void OnDestroy()
    {
        Unbind();
    }
}
