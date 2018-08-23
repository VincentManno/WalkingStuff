using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour {

    public Text attackName;
    Button button;

    private Attack attack;
    private BattleManager battleManager;

    void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        button = GetComponent<Button>();
    }

    public void Setup(Attack attack)
    {
        this.attack = attack;
        attackName.text = this.attack.name;

    }

    public void HandleClick()
    {
        //battleManager.AttackButtonClicked(attack);
        battleManager.playerChosenAttack = attack;
    }

    public void OnPointerEnter()
    {
        battleManager.playerAttackInfoDisplay.text = attack.name + "\nDamage Mod: " + attack.damageModifier + 
                                                     "\n" + attack.description;
    }

    public void OnPointerExit()
    {
        battleManager.playerAttackInfoDisplay.text = "";
    }
}
