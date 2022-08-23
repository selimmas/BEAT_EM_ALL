using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerSM playerSM;

    private float maxHealth;

    private Image healthbar;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = playerSM.health;
    }


    // Actualise les points de vie pour rester entre 0 et hpmax
    public void UpdateHealth()
    {

        float hp = Mathf.Clamp(playerSM.health, 0, maxHealth);
        float amount = hp / maxHealth;

        healthbar.fillAmount = amount;
    }
}
