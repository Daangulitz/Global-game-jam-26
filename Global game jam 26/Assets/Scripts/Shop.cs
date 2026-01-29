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
    public List<Mask> commonMaskPrefabs = new();
    public List<Mask> uncommonMaskPrefabs = new();
    public List<Mask> specialMaskPrefabs = new();

    public List<Mask> masksThisShop = new();

    private GameManager gm;
    
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        LoadNewShop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadNewShop();
        }
    }

    public void LoadNewShop()
    {
        visual1.material = null;
        visual2.material = null;
        visual3.material = null;

        Mask randomMask1 = GetRandomMask();

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
            visual1.material = randomMask1.material;
        }
        else
        {
            rarity1.color = Color.lightGray;
        }
        choice1 = randomMask1;

        Mask randomMask2 = GetRandomMask();
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
            visual2.material = randomMask2.material;
        }
        else
        {
            rarity2.color = Color.lightGray;
        }
        choice2 = randomMask2;

        Mask randomMask3 = GetRandomMask();
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
            visual3.material = randomMask3.material;
        }
        else
        {
            rarity3.color = Color.lightGray;
        }
        choice3 = randomMask3;

        foreach (Mask mask in masksThisShop)
        {
            if (mask.rarity == Rarity.Special)
                specialMaskPrefabs.Add(mask);
            else if (mask.rarity == Rarity.Uncommon)
                uncommonMaskPrefabs.Add(mask);
            else
                commonMaskPrefabs.Add(mask);
          
        }
    }


    private Mask GetRandomMask()
    {
        int rng = Random.Range(0, 1000);
        if (rng > 800 && specialMaskPrefabs.Count > 0)
        {
            Mask specialMask = specialMaskPrefabs[Random.Range(0, specialMaskPrefabs.Count)];
            specialMaskPrefabs.Remove(specialMask);
            masksThisShop.Add(specialMask);
            return specialMask;
        }
        else if (rng > 500 && uncommonMaskPrefabs.Count > 0)
        {
            Mask uncommonMask = uncommonMaskPrefabs[Random.Range(0, uncommonMaskPrefabs.Count)];
            uncommonMaskPrefabs.Remove(uncommonMask);
            masksThisShop.Add(uncommonMask);
            return uncommonMask;
        }
        else
        {
            Mask commonMask = commonMaskPrefabs[Random.Range(0, commonMaskPrefabs.Count)];
            commonMaskPrefabs.Remove(commonMask);
            masksThisShop.Add(commonMask);
            return commonMask;
        }
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
