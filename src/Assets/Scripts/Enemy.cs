using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int goldReward = 25;
    [SerializeField] private int goldPenalty = 25;

    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (bank == null) return;

        bank.Deposit(goldReward);
    }

    public void PenaltyGold()
    {
        if (bank == null) return;

        bank.Withdraw(goldPenalty);
    }
}
