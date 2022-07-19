using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public float maxHealth = 5f;
    public float currentHealth;

    public float invincTime = 1f;
    private float timeSinceDamage;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = 5f;
        UIController.instance.healthSlider.value = currentHealth;
    }

    private void Update()
    {
        if(timeSinceDamage > 0)
        {
            timeSinceDamage -= Time.deltaTime;

            if(timeSinceDamage <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 1f);
            }
        }
    }

    public void DamageToPlayer()
    {
        if (timeSinceDamage <= 0)
        {
            timeSinceDamage = invincTime;

            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 0.5f);

            currentHealth--;

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
                
            }

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }
}
