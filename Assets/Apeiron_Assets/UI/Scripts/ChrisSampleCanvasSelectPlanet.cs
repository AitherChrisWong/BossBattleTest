using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrisSampleCanvasSelectPlanet : MonoBehaviour
{
    Animator anim;
    public int selectedPlanetCardId = 0;

    public Transform planetCardGroup;
    public List<GameObject> planetCards = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        foreach(Transform child in planetCardGroup)
        {
            planetCards.Add(child.gameObject);
        }

        UpdateSelectCard(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnSelectPlanetOnPress()
    {
        anim.Play("quit");
    }

    public void UpdateSelectCard(int id)
    {
        planetCards[selectedPlanetCardId].transform.Find("group").GetChild(0).gameObject.SetActive(false);

        selectedPlanetCardId = id;

        planetCards[id].transform.Find("group").GetChild(0).gameObject.SetActive(true);
    }
}
