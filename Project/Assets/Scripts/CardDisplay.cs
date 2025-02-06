using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CardDisplay : MonoBehaviour
{
    [SerializeField] Card card;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI DamageAmount;
    [SerializeField] TextMeshProUGUI HealthAmount;
    [SerializeField] TextMeshProUGUI PriceAmount;
    [SerializeField] Image CharacterImage;
    private void Start()
    {
        Name.text = card.Name;
        DamageAmount.text = card.Damage.ToString();
        HealthAmount.text = card.Health.ToString();
        PriceAmount.text = card.Price.ToString();
        CharacterImage.sprite = card.CharacterImage;

    }
}
