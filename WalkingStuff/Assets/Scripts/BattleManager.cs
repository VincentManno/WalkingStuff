using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

    public int currentState;

    private enum BattleStates
    {
        START,
        PLAYER_CHOICE,
        ENEMY_CHOICE,
        WIN,
        LOSE
    }
    
    //Battle Participants
    public Creature player, enemy;

    //UI References
    public GameObject playerAttackDisplay;
    public GameObject attackButtonPrefab;
    public List<GameObject> attackButtons;
    public Text playerAttackInfoDisplay;

    public Text battleOutcomeDisplay;

    public Text playerHealthDisplay, enemyHealthDisplay;

    //Gameloop Vars
    public Attack playerChosenAttack;
    public float simulatedAnimationTime = 2f;

    void Start()
    {
        currentState = (int)BattleStates.START;
        attackButtons = new List<GameObject>();
        playerChosenAttack = null;
    }

    void Update()
    {
        Debug.Log("Current State: " + currentState);

        playerHealthDisplay.text = "Player HP: " + player.Health;
        enemyHealthDisplay.text = "Enemy HP: " + enemy.Health;

        if (enemy.Health == 0)
        {
            currentState = (int)BattleStates.WIN;
        }
        else if (player.Health == 0)
        {
            currentState = (int)BattleStates.LOSE;
        }

        switch (currentState)
        {
            case (int)BattleStates.START:

                //Initialize Attacks
                for (int i = 0; i < player.Attacks.Count; i++)
                {
                    GameObject attackButtonGameObject = Instantiate(attackButtonPrefab);
                    attackButtons.Add(attackButtonGameObject);

                    attackButtonGameObject.transform.SetParent(playerAttackDisplay.transform, false);

                    AttackButton attackButton = attackButtonGameObject.GetComponent<AttackButton>();
                    attackButton.Setup(player.Attacks[i]);
                }
                playerAttackDisplay.SetActive(false);

                currentState = (int)BattleStates.PLAYER_CHOICE;

                break;
            case (int)BattleStates.PLAYER_CHOICE:

                playerAttackDisplay.SetActive(true);
                simulatedAnimationTime = 2f;

                if (playerChosenAttack != null)
                {
                    //Deal damage to enemy
                    int playerDamage = Mathf.RoundToInt(player.baseDamage * playerChosenAttack.damageModifier);

                    enemy.Health -= Mathf.Clamp(playerDamage - enemy.armor, 1, playerDamage);

                    //Prevent player from providing input during next state and reset input references
                    playerAttackDisplay.SetActive(false);
                    playerChosenAttack = null;

                    //Change state
                    currentState = (int)BattleStates.ENEMY_CHOICE;
                }
                break;
            case (int)BattleStates.ENEMY_CHOICE:

                //Simulate animations
                if (simulatedAnimationTime > 0f)
                {
                    simulatedAnimationTime -= Time.deltaTime;
                }
                else //Deal damage to player
                {
                    int enemyDamage = Mathf.RoundToInt(enemy.baseDamage);

                    player.Health -= Mathf.Clamp(enemyDamage - player.armor, 1, enemyDamage);

                    //Change state
                    currentState = (int)BattleStates.PLAYER_CHOICE;
                }

                break;
            case (int)BattleStates.WIN:

                //End battle with player winning
                battleOutcomeDisplay.text = "Player wins!";
                   
                break;
            case (int)BattleStates.LOSE:

                //End battle with player losing
                battleOutcomeDisplay.text = "Enemy wins!";

                break;
        } //END SWITCH
    } //END UPDATE
}
