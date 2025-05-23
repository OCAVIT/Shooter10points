using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerUI : NetworkBehaviour
{
    public Text healthText;
    public Text ammoText;
    public PlayerHealth playerHealth;
    public Weapon weapon;

    void Update()
    {
        if (!isLocalPlayer) return;
        healthText.text = "HP: " + playerHealth.currentHealth;
        ammoText.text = "Ammo: " + weapon.currentAmmo + "/" + weapon.maxAmmo;
    }
}