using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;

    public TMP_Text hpText;

    void Start()
    {
        currentHP = maxHP;
        UpdateHPText();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPText();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void UpdateHPText()
    {
        if (hpText)
            hpText.text = currentHP + " / " + maxHP;
    }

    void Die()
    {
        SceneManager.LoadScene("Apt");
    }
}
