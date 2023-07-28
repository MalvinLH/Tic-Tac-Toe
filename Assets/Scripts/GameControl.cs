using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public int turn;                
    public int turnCount;          
    public GameObject[] changeTurn; 
    public Sprite[] playerSymbol;   
    public Button[] gridButtons;    
    public GameObject p1WinImage;   
    public GameObject p2WinImage;   
    public GameObject drawImage;    
    public Button playAgain;      
    public Button mainMenu;       
    private bool isXFirst;          
    private LineRenderer winLine;
    private int sortingOrder = 1;
    
    void Start()
    {
        isXFirst = true;
        Setup();
    }

    public void Setup()
    {
        turnCount = 0;
        if (isXFirst)
        {
            turn = 0;
            changeTurn[0].SetActive(true);
            changeTurn[1].SetActive(false);
        }
        else
        {
            turn = 1;
            changeTurn[0].SetActive(false);
            changeTurn[1].SetActive(true);
        }
        for (int i = 0; i < gridButtons.Length; i++)
        {
            gridButtons[i].interactable = true;
            gridButtons[i].GetComponent<Image>().sprite = null;
        }
        GameObject pauseButton = GameObject.FindGameObjectWithTag("PauseButton");
        pauseButton.GetComponent<Button>().interactable = true;
        playAgain.gameObject.SetActive(false);
        playAgain.interactable = false;
        mainMenu.gameObject.SetActive(false);
        mainMenu.interactable = false;
        p1WinImage.SetActive(false);
        p2WinImage.SetActive(false);
        drawImage.SetActive(false);
        winLine = GetComponent<LineRenderer>();
        winLine.enabled = false;
        winLine.sortingOrder = sortingOrder;
    }

    public void TableButtons(int num)
    {
        Image buttonImage = gridButtons[num].GetComponent<Image>();

        if (turn == 0)
        {
            buttonImage.sprite = playerSymbol[0];
        }
        else if (turn == 1)
        {
            buttonImage.sprite = playerSymbol[1];
        }
        
        turnCount++;

        gridButtons[num].interactable = false;

        if (CheckWinner())
        {
            WinnerDisplay(turn);
            for (int i = 0; i < gridButtons.Length; i++)
            {
                gridButtons[i].interactable = false;
            }
            GameObject pauseButton = GameObject.FindGameObjectWithTag("PauseButton");
            pauseButton.GetComponent<Button>().interactable = false;
            playAgain.gameObject.SetActive(true);
            playAgain.interactable = true;
            mainMenu.gameObject.SetActive(true);
            mainMenu.interactable = true;
            winLine.enabled = true;
            return;
        } 
        else
        {
            if (turn == 0)
            {
                turn = 1;
                changeTurn[0].SetActive(false);
                changeTurn[1].SetActive(true);
            }
            else
            {
                turn = 0;
                changeTurn[0].SetActive(true);
                changeTurn[1].SetActive(false);
            }
            if(turnCount == 9)
            {
                drawImage.SetActive(true);
                GameObject pauseButton = GameObject.FindGameObjectWithTag("PauseButton");
                pauseButton.GetComponent<Button>().interactable = false;
                playAgain.gameObject.SetActive(true);
                playAgain.interactable = true;
                mainMenu.gameObject.SetActive(true);
                mainMenu.interactable = true;
                winLine.enabled = false;
                if(turn == 0)
                {
                    changeTurn[0].SetActive(false);
                    changeTurn[1].SetActive(true);
                }
                else if(turn == 1)
                {
                    changeTurn[1].SetActive(false);
                    changeTurn[0].SetActive(true);
                }
            }
            return;
        }
    }

    public bool CheckWinner()
    {
        for (int i = 0; i < 9; i += 3)
        {
            if (gridButtons[i].GetComponent<Image>().sprite == playerSymbol[turn] &&
                gridButtons[i + 1].GetComponent<Image>().sprite == playerSymbol[turn] &&
                gridButtons[i + 2].GetComponent<Image>().sprite == playerSymbol[turn])
            {
                winLine.SetPosition(0, gridButtons[i].transform.position);
                winLine.SetPosition(1, gridButtons[i + 2].transform.position);

                return true;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (gridButtons[i].GetComponent<Image>().sprite == playerSymbol[turn] &&
                gridButtons[i + 3].GetComponent<Image>().sprite == playerSymbol[turn] &&
                gridButtons[i + 6].GetComponent<Image>().sprite == playerSymbol[turn])
            {
                winLine.SetPosition(0, gridButtons[i].transform.position);
                winLine.SetPosition(1, gridButtons[i + 6].transform.position);
                return true;
            }
        }

        if (gridButtons[0].GetComponent<Image>().sprite == playerSymbol[turn] &&
            gridButtons[4].GetComponent<Image>().sprite == playerSymbol[turn] &&
            gridButtons[8].GetComponent<Image>().sprite == playerSymbol[turn])
        {
            winLine.SetPosition(0, gridButtons[0].transform.position);
            winLine.SetPosition(1, gridButtons[8].transform.position);
            return true;
        }

        if (gridButtons[2].GetComponent<Image>().sprite == playerSymbol[turn] &&
            gridButtons[4].GetComponent<Image>().sprite == playerSymbol[turn] &&
            gridButtons[6].GetComponent<Image>().sprite == playerSymbol[turn])
        {
            winLine.SetPosition(0, gridButtons[2].transform.position);
            winLine.SetPosition(1, gridButtons[6].transform.position);
            return true;
        }
        return false;
    }

    public void WinnerDisplay(int index)
    {
        if(index == 0)
        {
            p1WinImage.SetActive(true);
            changeTurn[0].SetActive(true);
            changeTurn[1].SetActive(false);
        }
        else if(index == 1)
        {
            p2WinImage.SetActive(true);
            changeTurn[1].SetActive(true);
            changeTurn[0].SetActive(false);
        }
    }

    public void RestartGame()
    {
        isXFirst = !isXFirst;
        if (isXFirst)
        {
            if (turnCount % 2 == 0)
            {
                turn = 0;
            }
            else
            {
                turn = 1;
            }
        }
        else
        {
            if (turnCount % 2 == 0)
            {
                turn = 1;
            }
            else
            {
                turn = 0;
            }
        }
        Setup();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}