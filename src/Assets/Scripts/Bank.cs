using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 100;
    [SerializeField] private int currentBalance;
    [SerializeField] private TextMeshProUGUI displayBalance;
    public int CurrentBalance => currentBalance;
    
    private void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        amount = Mathf.Abs(amount);
        currentBalance -= amount;
        UpdateDisplay();

        if (currentBalance < 0)
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateDisplay()
    {
        displayBalance.text = $"Gold: {currentBalance}";
    }
}
