using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Image visual1, visual2, visual3;
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
        visual1.sprite = randomMask1.sprite;
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
        visual2.sprite = randomMask2.sprite;
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
        visual3.sprite = randomMask3.sprite;
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
            rarity3.color = Color.lightGray;
        }
        choice3 = randomMask3;
        maskPrefabs.Remove(randomMask3);
    }

    public void Option1Click()
    {
        Debug.LogError("Mask added: " + choice1.maskName);
        gm.AddMask(choice1);
        NextScene();
    }
    public void Option2Click()
    { 
        gm.AddMask(choice2);
        Debug.LogError("Mask added: " + choice2.maskName);
        NextScene();
    }
    public void Option3Click()
    {
        gm.AddMask(choice3);
        Debug.LogError("Mask added: " + choice3.maskName);
        NextScene();
    }

    private void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
