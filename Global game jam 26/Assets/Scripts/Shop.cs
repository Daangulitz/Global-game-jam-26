using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Button option1, option2, option3;
    [SerializeField] TextMeshProUGUI name1, name2, name3;
    [SerializeField] TextMeshProUGUI description1, description2, description3;
    [SerializeField] TextMeshProUGUI rarity1, rarity2, rarity3;
    

    public Mask choice1, choice2, choice3;
    public List<Mask> maskPrefabs = new();

    private GameManager gm;
    
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        LoadNewShop();
    }


    public void LoadNewShop()
    {
        Mask randomMask1 = maskPrefabs[Random.Range(0, maskPrefabs.Count)];
        option1.GetComponent<Image>().sprite = randomMask1.sprite;
        name1.text = randomMask1.maskName;
        description1.text = randomMask1.description;
        rarity1.text = randomMask1.rarity.ToString();

        if (randomMask1.rarity == Rarity.Uncommon)
        {
            rarity1.color = Color.green;
        }
        else if (randomMask1.rarity == Rarity.Special)
        {
            rarity1.color = Color.purple;
        }
        else
        {
            rarity1.color = Color.grey;
        }
        choice1 = randomMask1;
        maskPrefabs.Remove(randomMask1);

        Mask randomMask2 = maskPrefabs[Random.Range(0, maskPrefabs.Count)];
        option2.GetComponent<Image>().sprite = randomMask2.sprite;
        name2.text = randomMask2.maskName;
        description2.text = randomMask2.description;
        rarity2.text = randomMask2.rarity.ToString();
        if (randomMask2.rarity == Rarity.Uncommon)
        {
            rarity2.color = Color.green;
        }
        else if (randomMask2.rarity == Rarity.Special)
        {
            rarity2.color = Color.purple;
        }
        else
        {
            rarity2.color = Color.grey;
        }
        choice2 = randomMask2;
        maskPrefabs.Remove(randomMask2);

        Mask randomMask3 = maskPrefabs[Random.Range(0, maskPrefabs.Count)];
        option3.GetComponent<Image>().sprite = randomMask3.sprite;
        name3.text = randomMask3.maskName;
        description3.text = randomMask3.description;
        rarity3.text = randomMask3.rarity.ToString();
        if (randomMask3.rarity == Rarity.Uncommon)
        {
            rarity3.color = Color.green;
        }
        else if (randomMask3.rarity == Rarity.Special)
        {
            rarity3.color = Color.purple;
        }
        else
        {
            rarity3.color = Color.grey;
        }
        choice3 = randomMask3;
        maskPrefabs.Remove(randomMask3);
    }
    
    public void ButtonClicked(int i)
    {
        if (i == 0)
        {
            gm.AddMask(choice1);
            Debug.Log("Mask added: " + choice1.maskName);
        }
        else if (i == 1)
        {
            gm.AddMask(choice2);
            Debug.Log("Mask added: " + choice2.maskName);
        }
        else
        {
            gm.AddMask(choice3);
            Debug.Log("Mask added: " + choice3.maskName);
        }
    }


    public void Option1Click()
    {
        ButtonClicked(0);
    }
    public void Option2Click()
    {
        ButtonClicked(1);
    }
    public void Option3Click()
    {
        ButtonClicked(2);
    }

}
