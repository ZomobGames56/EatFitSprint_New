using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    // Example symbols
    string[] symbols = { "Cherry", "Lemon", "Orange", "Bell", "Seven" };

    string slot1;
    string slot2;
    string slot3;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Input");
            Spin();
        }
    }

    public void Spin()
    {
        // Randomly pick symbols
        slot1 = symbols[Random.Range(0, symbols.Length)];
        slot2 = symbols[Random.Range(0, symbols.Length)];
        slot3 = symbols[Random.Range(0, symbols.Length)];

        Debug.Log($"Spin Results: {slot1}, {slot2}, {slot3}");

        CheckResult();
    }

    void CheckResult()
    {
        if (slot1 == slot2 && slot2 == slot3)
        {
            Debug.Log("Jackpot! All symbols match!");
        }
        else
        {
            Debug.Log("No match. Try again!");
        }
    }
}