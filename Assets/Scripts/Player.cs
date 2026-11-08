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
    public TextMeshProUGUI nextDmgLbl;
    public TextMeshProUGUI shieldLbl;

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
        luckyNumber = Random.Range(2, 13);
        Health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = Health;
        UpdateHealth();
        stamina = 5;
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

    private void UpdateHealth()
    {
        healthBar.value = Health;
        healthLbl.text = $"{Health}/{maxHealth}";
        healthBar.value = Health;
    }
}
