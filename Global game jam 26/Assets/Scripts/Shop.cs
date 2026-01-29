using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    private GameSceneManager gsm;
    
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gsm = FindObjectOfType<GameSceneManager>();
        
        visual1.gameObject.SetActive(true);
        visual2.gameObject.SetActive(true);
        visual3.gameObject.SetActive(true);
        
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
        // Fix: Put masks back and CLEAR before loading new ones
        foreach (Mask mask in masksThisShop)
        {
            if (mask.rarity == Rarity.Special) specialMaskPrefabs.Add(mask);
            else if (mask.rarity == Rarity.Uncommon) uncommonMaskPrefabs.Add(mask);
            else commonMaskPrefabs.Add(mask);
        }
        masksThisShop.Clear();

        visual1.material = null;
        visual2.material = null;
        visual3.material = null;

        choice1 = GetRandomMask();
        SetupDisplay(choice1, visual1, name1, description1, rarity1);

        choice2 = GetRandomMask();
        SetupDisplay(choice2, visual2, name2, description2, rarity2);

        choice3 = GetRandomMask();
        SetupDisplay(choice3, visual3, name3, description3, rarity3);
    }

    private void SetupDisplay(Mask m, Image img, TextMeshProUGUI n, TextMeshProUGUI d, TextMeshProUGUI r)
    {
        if (m == null) return;
        img.sprite = m.sprite;
        n.text = m.maskName;
        d.text = m.description;
        r.text = m.rarity.ToString();

        if (m.rarity == Rarity.Uncommon) r.color = Color.green;
        else if (m.rarity == Rarity.Special)
        {
            r.color = Color.purple;
            img.material = m.material;
        }
        else r.color = Color.lightGray;
    }

    private Mask GetRandomMask()
    {
        int rng = Random.Range(0, 1000);
        Mask selected = null;

        if (rng > 800 && specialMaskPrefabs.Count > 0)
        {
            selected = specialMaskPrefabs[Random.Range(0, specialMaskPrefabs.Count)];
            specialMaskPrefabs.Remove(selected);
        }
        else if (rng > 500 && uncommonMaskPrefabs.Count > 0)
        {
            selected = uncommonMaskPrefabs[Random.Range(0, uncommonMaskPrefabs.Count)];
            uncommonMaskPrefabs.Remove(selected);
        }
        else if (commonMaskPrefabs.Count > 0)
        {
            selected = commonMaskPrefabs[Random.Range(0, commonMaskPrefabs.Count)];
            commonMaskPrefabs.Remove(selected);
        }

        if (selected != null) masksThisShop.Add(selected);
        return selected;
    }

    public void Option1Click()
    {
        if (gm.masks.Any(m => m.id == 0))
        {
            RemoveSpecificMask(0);
            gm.AddMask(choice1);
            visual1.gameObject.SetActive(false); 
        }
        else 
        {
            gm.AddMask(choice1);
            NextScene();
        }
    }

    public void Option2Click()
    {
        if (gm.masks.Any(m => m.id == 0))
        {
            RemoveSpecificMask(0);
            gm.AddMask(choice2);
            visual2.gameObject.SetActive(false);
        }
        else
        {
            gm.AddMask(choice2);
            NextScene();
        }
    }

    public void Option3Click()
    {
        if (gm.masks.Any(m => m.id == 0))
        {
            RemoveSpecificMask(0);
            gm.AddMask(choice3);
            visual3.gameObject.SetActive(false);
        }
        else
        {
            gm.AddMask(choice3);
            NextScene();
        }
    }

    private void RemoveSpecificMask(int idToRemove)
    {
        // Use a list to find the item
        List<Mask> temp = gm.masks.ToList();
        // Remove only the first instance of ID 0 found
        Mask toRemove = temp.FirstOrDefault(m => m.id == idToRemove);
        
        if (toRemove != null)
        {
            temp.Remove(toRemove);
            gm.masks.Clear();
            // Re-stack items in correct order
            temp.Reverse();
            foreach (var m in temp)
            {
                gm.masks.Push(m);
            }
        }
    }

    private void NextScene()
    {
        gsm.ExitShop();
    }
}