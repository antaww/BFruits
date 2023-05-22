using UnityEngine;

public class Penalty : MonoBehaviour
{
    private static GameManager _gameManager;

    private const string BBombEffect = "-1 life";
    private const string BRobEffect = "-50";

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public static void ApplyPenaltyEffect(string name, Vector3 position)
    {
        switch (name)
        {
            case not null when name.Contains("BBomb"): // -1 life
                print(name + position);
                _gameManager.ShowFloatingText(BBombEffect, position, 26, 25, 32);
                _gameManager.RemoveLife();
                break;
            case not null when name.Contains("BRob"): // -50
                _gameManager.RemoveScore(50);
                _gameManager.ShowFloatingText(BRobEffect, position, 237, 231, 145);
                break;
        }
    }
}
