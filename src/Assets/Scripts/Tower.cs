using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost = 30;
    [SerializeField] private float buildDelay = 0.5f;

    private void Start()
    {
        StartCoroutine(Build());
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance < cost)
        {
            return false;
        }

        bank.Withdraw(cost);
        Instantiate(tower, position, Quaternion.identity);
        return true;
    }


    private IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }


        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);

            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }

        yield return new WaitForSeconds(0.5f);
    }
}
