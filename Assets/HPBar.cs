using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private TMP_Text T_text;
    [SerializeField] private Scrollbar _scrollbar;
    private int _maxhp;
    private int _hp;
    public void setText(int currenthp)
    {
        //T_text.text = text;
        _hp = currenthp;
        _scrollbar.size = (float)_hp / _maxhp;
        T_text.text = $"HP: {currenthp}";
    }
    public void setup(int maxhp)
    {
        _maxhp = _hp = maxhp;
    }
}
