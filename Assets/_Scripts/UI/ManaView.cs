using UnityEngine;
using TMPro;

public class ManaView : MonoBehaviour
{
    [SerializeField] TMP_Text _manaText;
    Mana _mana;

    public void Bind(Mana mana)
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

    public void UpdateManaText(int newMana)
    {
        _manaText.text = newMana.ToString();
    }
    private void OnDestroy()
    {
        Unbind();
    }
}
