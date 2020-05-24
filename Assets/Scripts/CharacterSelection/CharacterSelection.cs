using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    private int selectedCharacterIndex = 0;

    [Header("List of characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>();

    [Header("UI References")]
    // [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSplash;

    [Header("Level")]
    public string playGameLevel;

    [Header("Crossfade Animator")]
    public Animator crossfadeAnimator;

    private void Start()
    {

        UpdateCharacterSelectionUI();

    }

    public void LeftArrow()
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
        {
            selectedCharacterIndex = characterList.Count - 1;
        }

        UpdateCharacterSelectionUI();
    }

    public void RightArrow()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterList.Count)
        {
            selectedCharacterIndex = 0;
        }

        UpdateCharacterSelectionUI();
    }

    public void PlayGame()
    {
        StartCoroutine(CondfirmSelection());
    }

    private IEnumerator CondfirmSelection()
    {
        PlayerPrefs.SetInt("Selected Character", selectedCharacterIndex);

        crossfadeAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(playGameLevel);
    }

    private void UpdateCharacterSelectionUI()
    {
        characterSplash.sprite = characterList[selectedCharacterIndex].splash;
        //characterName.text = characterList[selectedCharacterIndex].characterName;

        //change the image size to fit the percentage of the given sprite
        float percentage = characterList[selectedCharacterIndex].splash.rect.width / characterList[selectedCharacterIndex].splash.rect.height;
        float baseSize = 350f;
        float rectSize = percentage * baseSize;
        characterSplash.rectTransform.sizeDelta = new Vector2(rectSize, baseSize);
    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string characterName;

    }
}
