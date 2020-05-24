using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    // Awake is called before the first frame update
    void Awake()
    {
        if(!PlayerPrefs.HasKey("Selected Character"))
        {
            players[0].SetActive(true);
        }
        else
        {
            int playerPrefsNumber = PlayerPrefs.GetInt("Selected Character");
            players[playerPrefsNumber].SetActive(true);
        }
    }
}
