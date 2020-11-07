using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BonusUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text bonuxTest;
    [SerializeField] private MovementPlayer player;

    private void Awake()
    {
        player.onBonusUpdate += updateBonusText;
    }


    private void updateBonusText(List<(string,int)> bonuses)
    {
        string to_show = "Bonus activé :";
        foreach (var bonusToAdd in bonuses)
        {
            if (!to_show.Contains(bonusToAdd.Item1))
            {
                to_show += " " + bonusToAdd.Item1 + " " + bonusToAdd.Item2;
            }
        }
        bonuxTest.SetText(to_show);
    }

    
}
