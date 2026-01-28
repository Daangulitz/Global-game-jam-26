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
        maskPrefabs.Remove(randomMask1);

        Mask randomMask2 = maskPrefabs[Random.Range(0, maskPrefabs.Count)];
        option2.GetComponent<Image>().sprite = randomMask2.sprite;
        name2.text = randomMask2.maskName;
        description2.text = randomMask2.description;
        maskPrefabs.Remove(randomMask2);

        Mask randomMask3 = maskPrefabs[Random.Range(0, maskPrefabs.Count)];
        option3.GetComponent<Image>().sprite = randomMask3.sprite;
        name3.text = randomMask3.maskName;
        description3.text = randomMask3.description;
        maskPrefabs.Remove(randomMask3);
    }


    private void DisableButtons()
    {
        option1.GetComponent<Button>().interactable = false;
        option2.GetComponent<Button>().interactable = false;
        option3.GetComponent<Button>().interactable = false;
    }
    public void ButtonClicked(int i)
    {
        DisableButtons();
        //maybe play cool animation here
        //then load new scene
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
