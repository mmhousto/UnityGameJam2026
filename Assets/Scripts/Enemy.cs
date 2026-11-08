using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamagable
{

    public TextMeshProUGUI healthLbl;
    public TextMeshProUGUI luckyLbl;
    public TextMeshProUGUI nextDmgLbl;
    public TextMeshProUGUI shieldLbl;

    public int maxHealth = 50;
    public int luckyNum;
    public int baseDamage = 5;
    public int block = 0;

    private Slider healthBar;
    private int nextAttackDmg;

    public int Health { get; set; }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        UpdateHealth();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = maxHealth;
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = Health;
        UpdateHealth();

        luckyNum = Random.Range(2, 13);
        luckyLbl.text = luckyNum.ToString();

        shieldLbl.text = block.ToString();

        GetNextHit();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateHealth()
    {
        if (Health > maxHealth)
        {
            Health = maxHealth;
        }
        healthBar.value = Health;
        healthLbl.text = $"{Health}/{maxHealth}";
    }

    public void UpdateShield()
    {
        shieldLbl.text = block.ToString();
    }

    /*
     * 2-12 Range
     * 
     * base + roll
     * 
     * Lucky number exact - x7 & +7 health +7 block
     * Opposite Lucky # - +7 dmg, health, block
     *
     *                      
     * • Inverted Pair (Opposite Lucky): (2-12, 3-11, 4-10, 5-9, 6-8, 7-7, 8-6, ...) sum = 14
     *                    -10, -8,    -6,   -4,  -2,   0,   2,  4, 6, 8, 10
     *                    Exclude 7-7?
     */

    public void GetNextHit()
    {
        int roll = Random.Range(2, 13);
        Debug.Log($"Enemy Rolled: {roll}");

        int differnce = roll - luckyNum;
        int sum = roll + luckyNum;

        nextAttackDmg = baseDamage;

        if (differnce == 0)
        {
            nextAttackDmg *= 7;
            block += 7;
            TakeDamage(-7);
            baseDamage += 7;
        }
        else if (sum == 14)
        {
            nextAttackDmg += 7 + roll;
            block += 7;
            TakeDamage(-7);
        }
        else
        {
            int typeAttack = 0;
            
            if(Health >= maxHealth)
                typeAttack = Random.Range(0, 2);
            else
                typeAttack = Random.Range(0, 3);

            switch (typeAttack)
            {
                case 0://Attack
                    nextAttackDmg += roll;
                    Debug.Log("Enemy Attack");
                    break;
                case 1://Block
                    block += roll;
                    Debug.Log("Enemy Block");
                    break;
                case 2://Heal
                    TakeDamage(-roll);
                    Debug.Log("Enemy Heal");
                    break;

            }
            
        }

        Debug.Log($"Enemy Next Attack Damage: {nextAttackDmg}");
        nextDmgLbl.text = nextAttackDmg.ToString();
        shieldLbl.text = block.ToString();
    }

    public int GetCurrentHit()
    {
        return nextAttackDmg;
    }

}
