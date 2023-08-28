using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost = 30;

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if(bank.CurrentBalance < cost)
        {
            return false;
        }

        bank.Withdraw(cost);
        Instantiate(tower, position, Quaternion.identity);
        return true;
    }
}
