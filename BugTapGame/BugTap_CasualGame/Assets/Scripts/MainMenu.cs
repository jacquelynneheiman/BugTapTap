using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelect;
    public GameObject quitDialog;

    public Button[] levelSelectButtons;
    public Sprite activeLevelButtonSprite;
    public Sprite inactiveLevelButtonSprite;

    private void Start()
    {
        LoadGame();
    }

    public void PlayGame()
    {
        levelSelect.SetActive(true);
        
    }

    public void CloseLevelSelect()
    {
        //close level select and go back to main menu
        levelSelect.SetActive(false);
    }

    public void PlayLevel(int level)
    {
        GameManager.instance.LevelToLoad = level;
        GameManager.instance.PlayedLevel();
        SaveSystem.SavePlayer(GameManager.instance.player);
        SceneManager.LoadScene("Loading");
    }

    public void ExitGame()
    {
        //are you sure you want to quit dialog
    }

    public void ConfirmExit()
    {
        //exit application
    }

    public void DenyExit()
    {
        //close dialog box
    }

    public void ToggleMusic()
    {
        //turn music on and off
        //switch sprite one button
    }

    public void ToggleSFX()
    {
        //turn sfx on and off
        //switch sprite one button
    }

    void LoadGame()
    {
        for(int i = 0; i < GameManager.instance.player.lastLevelUnlocked; i++)
        {
            levelSelectButtons[i].interactable = true;
            levelSelectButtons[i].image.sprite = activeLevelButtonSprite;
            levelSelectButtons[i].transform.GetChild(0).gameObject.SetActive(true);

            GameObject star1 = levelSelectButtons[i].transform.GetChild(1).gameObject;
            GameObject star2 = levelSelectButtons[i].transform.GetChild(2).gameObject;
            GameObject star3 = levelSelectButtons[i].transform.GetChild(3).gameObject;

            switch (GameManager.instance.player.starsEarnedPerLevel[i])
            {
                case 3:
                    if (star1 && star2 && star2)
                    {
                        star1.SetActive(true);
                        star2.SetActive(true);
                        star3.SetActive(true);
                    }
                    break;
                case 2:
                    if (star1 && star2 && star3)
                    {
                        star1.SetActive(true);
                        star2.SetActive(true);
                        star3.SetActive(false);
                    }
                    break;
                case 1:
                    if (star1 && star2 && star3)
                    {
                        star1.SetActive(true);
                        star2.SetActive(false);
                        star3.SetActive(false);
                    }
                    break;
                default:
                    if (star1 && star2 && star3)
                    {
                        star1.SetActive(false);
                        star2.SetActive(false);
                        star3.SetActive(false);
                    }
                    break;

            }
           
        }

        for(int j = GameManager.instance.player.lastLevelUnlocked; j < 19; j++)
        {
            levelSelectButtons[j].interactable = false;
            levelSelectButtons[j].image.sprite = inactiveLevelButtonSprite;
            levelSelectButtons[j].transform.GetChild(0).gameObject.SetActive(false);

            GameObject star1 = levelSelectButtons[j].transform.GetChild(1).gameObject;
            GameObject star2 = levelSelectButtons[j].transform.GetChild(2).gameObject;
            GameObject star3 = levelSelectButtons[j].transform.GetChild(3).gameObject;

            if(star1 && star2 && star3)
            {
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(false);
            }
        }
    }
}
