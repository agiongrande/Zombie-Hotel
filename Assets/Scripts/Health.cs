using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    [SerializeField] private Image healthBar;
    private int _health;

    void Awake()
    {
        _health = maxHealth;
    }

    void ChangeLife(int amount){
        _health += amount;
        
        if (_health > 0){
                float life = (float) _health/ (float) maxHealth;
                healthBar.fillAmount = life;
            return;
        }
        _health = 0;
        healthBar.enabled = false;

    }

    public void TakeDamage(int amount){
        ChangeLife(-amount);
    }

    public int GetHealth(){
        return _health;
    }

    public void RecoveryHealth(int amount){
        ChangeLife(amount);
    }
}
