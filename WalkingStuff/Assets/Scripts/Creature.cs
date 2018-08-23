using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public int baseDamage, maxHealth, armor;

    [SerializeField]
    private int health;

    public int Health
    {
        get { return health; }
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    public List<Attack> Attacks = new List<Attack>();

#if UNITY_EDITOR
    public void OnValidate()
    {
        Health = health;
    }
#endif
}
