using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject option1, option2, option3;
    [SerializeField] TextMeshProUGUI name1, name2, name3;
    [SerializeField] TextMeshProUGUI description1, description2, description3;
    public List<Mask> maskPrefabs = new();

    private void Start()
    {
        LoadNewShop();
    }


    public void LoadNewShop()
    {
        Mask randomMask1 = maskPrefabs[Random.Range(0, maskPrefabs.Count)];
        option1.GetComponent<Image>().sprite = randomMask1.sprite;
        name1.text = randomMask1.maskName;
        description1.text = randomMask1.description;

        Mask randomMask2 = maskPrefabs[Random.Range(0, maskPrefabs.Count)];
        option2.GetComponent<Image>().sprite = randomMask2.sprite;
        name2.text = randomMask2.maskName;
        description2.text = randomMask2.description;

        Mask randomMask3 = maskPrefabs[Random.Range(0, maskPrefabs.Count)];
        option3.GetComponent<Image>().sprite = randomMask3.sprite;
        name3.text = randomMask3.maskName;
        description3.text = randomMask3.description;
    }
}
