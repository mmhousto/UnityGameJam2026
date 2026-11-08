using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamagable
{
    private static Player instance;

    public static Player Instance { get { return instance; } }

    public Slider healthBar;
    public TextMeshProUGUI healthLbl;
    public TextMeshProUGUI luckyLbl;
    public TextMeshProUGUI shieldLbl;
    public List<GameObject> staminaUI;
    public GameObject playerInfoHUD;

    public int Health { get; set; }
    public int maxHealth = 100;
    public int luckyNumber;
    public int baseDamage = 5;
    public int stamina;
    public int block = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetHealth(maxHealth);

        stamina = 4;
        UpdateStaminaUI();

        luckyNumber = Random.Range(2, 13);
        luckyLbl.text = luckyNumber.ToString();

        AddBlock(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        UpdateHealth();
    }

    private void SetHealth(int newHealth)
    {
        maxHealth = newHealth;
        healthBar.maxValue = maxHealth;
        Health = maxHealth;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        healthBar.value = Health;
        healthLbl.text = $"{Health}/{maxHealth}";
    }

    private void UpdateStaminaUI()
    {
        int i = 0;
        foreach(GameObject can in staminaUI)
        {
            if(i < stamina)
            {
                can.SetActive(true);
            }
            else
            {
                can.SetActive(false);
            }
            i++;
        }
    }

    private void AddBlock(int blockToAdd)
    {
        block += blockToAdd;
        UpdateShieldUI();
    }

    private void UpdateShieldUI()
    {
        shieldLbl.text = block.ToString();
    }
}
