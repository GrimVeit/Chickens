using UnityEngine;
using UnityEngine.UI;

public class PointAnimationView : MonoBehaviour
{
    [Header("Baby chicken")]
    [SerializeField] private Transform parentBabyChickens;
    [SerializeField] private Transform parentChickenMoveTo;
    [SerializeField] private Image babyChickenPrefab;
    [SerializeField] private Sprite spriteBabyChicken1;
    [SerializeField] private Sprite spriteBabyChicken2;
}
