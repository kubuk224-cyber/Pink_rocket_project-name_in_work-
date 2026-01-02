using UnityEngine;
using System.Collections;

public class PlayerPowerUps : MonoBehaviour
{
    [Header("Ustawienia Tarczy")]
    public bool shieldActive = false;
    public float shieldDuration = 5f;
    // Usunąłem shieldVisuals (GameObject), bo teraz zmieniamy sprite'a gracza

    [Header("Ustawienia SpeedBoost")]
    public float normalSpeed = 5f;
    public float boostedSpeed = 10f;
    private Coroutine speedCoroutine;

    private PlayerLives playerLivesScript;
    private PlayerController controller;

    private void Start()
    {
        playerLivesScript = GetComponent<PlayerLives>();
        controller = GetComponent<PlayerController>();
        if (controller != null) normalSpeed = controller.moveSpeed;
    }

    // --- APTECZKA ---
    public void Heal(int amount)
    {
        if (playerLivesScript != null) playerLivesScript.Heal(amount);
    }

    // --- TARCZA ---
    public void ActivateShield()
    {
        if (!shieldActive) StartCoroutine(ShieldTimer());
    }

    private IEnumerator ShieldTimer()
    {
        shieldActive = true;
        Debug.Log("Tarcza włączona!");
        
        // Zmień grafikę na tarczę natychmiast
        if (playerLivesScript != null) playerLivesScript.UpdateShipVisuals();

        yield return new WaitForSeconds(shieldDuration);
        
        shieldActive = false;
        Debug.Log("Tarcza wyłączona!");

        // Zmień grafikę z powrotem na normalną (zniszczoną)
        if (playerLivesScript != null) playerLivesScript.UpdateShipVisuals();
    }

    // --- SPEED BOOST ---
    public void ActivateSpeedBoost(float duration)
    {
        if (speedCoroutine != null) StopCoroutine(speedCoroutine);
        speedCoroutine = StartCoroutine(SpeedBoostTimer(duration));
    }

    private IEnumerator SpeedBoostTimer(float duration)
    {
        if (controller != null) controller.moveSpeed = boostedSpeed;
        yield return new WaitForSeconds(duration);
        if (controller != null) controller.moveSpeed = normalSpeed;
    }

    // --- NUKE ---
    public void ActivateNuke()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) Destroy(enemy);
    }
}