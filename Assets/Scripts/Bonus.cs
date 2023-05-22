using System.Linq;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private static GameManager _gameManager;

    private static float _x2Timer;
    private static float _x3Timer;

    private const string BAppleEffect = "x2";
    private const string BPeachEffect = "+20";
    private const string BAvocadoEffect = "Magic explosion";
    private const string BBananaEffect = "+10";
    private const string BOrangeEffect = "x3";
    private const string BWatermelonEffect = "+5";

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public static void ApplyBonusEffect(string name, Vector3 position)
    {
        switch (name)
        {
            case not null when name.Contains("BApple"): // x2
                _gameManager.ShowFloatingText(BAppleEffect, position, 151, 145, 24);
                _x2Timer = 10f;
                _gameManager.DisableDoublePoints(_x2Timer);
                break;
            case not null when name.Contains("BPeach"): // +20
                _gameManager.AddScore(20, false);
                _gameManager.ShowFloatingText(BPeachEffect, position, 255, 210, 55);
                break;
            case not null when name.Contains("BAvocado"): // Magic explosion
                _gameManager.ShowFloatingText(BAvocadoEffect, position, 154, 62, 0);
                var fruits = GameObject.FindGameObjectsWithTag("Fruit");
                fruits = fruits.Where(fruit => !fruit.name.Contains("Particle")).ToArray();
                _gameManager.PlayExplosionSound();
                foreach (var fruit in fruits)
                {
                    Fruit.SliceFruit(fruit, fruit.GetComponent<Fruit>().slicedFruit, fruit.GetComponent<Rigidbody>(),
                        fruit.GetComponent<Fruit>().fruitJuice, fruit.GetComponent<Fruit>().explosionVFX, _gameManager,
                        1);
                }

                break;
            case not null when name.Contains("BBanana"): // +10
                _gameManager.AddScore(10, false);
                _gameManager.ShowFloatingText(BBananaEffect, position, 151, 145, 24);
                break;
            case not null when name.Contains("BOrange"): // x3
                _gameManager.ShowFloatingText(BOrangeEffect, position, 203, 93, 16);
                _x3Timer = 10f;
                _gameManager.DisableTriplePoints(_x3Timer);
                break;
            case not null when name.Contains("BWatermelon"): // +5
                _gameManager.AddScore(5, false);
                _gameManager.ShowFloatingText(BWatermelonEffect, position, 173, 48, 49);
                break;
        }
    }
}