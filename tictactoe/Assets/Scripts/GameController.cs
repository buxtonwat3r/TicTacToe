using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[System.Serializable]
public class Player {
   public Image panel;
   public Text text;
   public Button button;
}


[System.Serializable]
public class PlayerColor {
   public Color panelColor;
   public Color textColor;
}

public class GameController : MonoBehaviour {
    
    public Text[] buttonList;
    private string playerSide;

    public GameObject restartButton;
    public GameObject startInfo;
    public GameObject gameOverPanel; 

    public Text gameOverText;

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    
    private int[,] rows = new int[8, 3] { 
        { 0, 1, 2 }, 
        { 3, 4, 5 }, 
        { 6, 7, 8 }, 
        { 0, 3, 6 },
        { 1, 4, 7 }, 
        { 2, 5, 8 }, 
        { 0, 4, 8 }, 
        { 2, 4, 6 } 
    };

    private int moveCount;

    void Awake () { 
        SetGameControllerReferenceOnButtons(); 
        
        //playerSide = "X";
        //SetPlayerColors(playerX, playerO);

        moveCount = 0;

        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);

        
    }

    void SetGameControllerReferenceOnButtons () 
    {
        for (int i = 0; i < buttonList.Length; i++) 
        {
            buttonList[i].GetComponentInParent< GridSpace>().SetGameControllerReference(this);
        }
    }


    public void SetStartingSide (string startingSide) 
    { 
        playerSide = startingSide; 


        SetPlayerColors(); 
        /*
        if (playerSide == "X") 
        { 
            SetPlayerColors(playerX, playerO); 
        } else { 
            SetPlayerColors(playerO, playerX); 
        } 
        */

        StartGame();
    }

    public void StartGame()
    {

        SetBoardInteractable(true);

        SetPlayerButtons(true);

        startInfo.SetActive(false);

    }

    public string GetPlayerSide () 
    { 
        //return "?"; 
        return playerSide;
    }

    public void EndTurn ()
    { 
        //Debug.Log("EndTurn is not implemented!"); 

        moveCount ++;


        
        for (int i = 0; i < rows.GetLength(0); i++)
        {
            if (buttonList[rows[i, 0]].text == playerSide && buttonList[rows[i, 1]].text
            == playerSide && buttonList[rows[i, 2]].text == playerSide)
            {
            GameOver(playerSide);
            return;
            }
        }

        /*
        if (buttonList [0].text == playerSide && buttonList [1].text == playerSide && buttonList [2].text == playerSide) 
        {
            GameOver(playerSide);
        }

        else if (buttonList [3].text == playerSide && buttonList [4].text == playerSide && buttonList [5].text == playerSide) 
        {
            GameOver(playerSide);
        }

        else if (buttonList [6].text == playerSide && buttonList [7].text == playerSide && buttonList [8].text == playerSide) 
        {
            GameOver(playerSide);
        }

        else if (buttonList [0].text == playerSide && buttonList [3].text == playerSide && buttonList [6].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList [1].text == playerSide && buttonList [4].text == playerSide && buttonList [7].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList [2].text == playerSide && buttonList [5].text == playerSide && buttonList [8].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList [2].text == playerSide && buttonList [4].text == playerSide && buttonList [6].text == playerSide)
        {
            GameOver(playerSide);
        }
        
        else if (moveCount >= 9) 
        { 
            //gameOverPanel.SetActive(true); gameOverText.text = "It's a draw!"; 
            //SetGameOverText("It's a draw!");
            GameOver("draw");
        }

        else 
        {

            ChangeSides();
        }

        */

        if (moveCount >= 9) 
        { 
            //gameOverPanel.SetActive(true); gameOverText.text = "It's a draw!"; 
            //SetGameOverText("It's a draw!");
            GameOver("draw");
        }

        else 
        {

            ChangeSides();
        }
        

    }


    void ChangeSides () 
    {
        playerSide = (playerSide == "X") ? "O" : "X"; // Note: Capital Letters for "X" and "O"
        SetPlayerColors();
        /*
        if (playerSide == "X") 
        {
            SetPlayerColors(playerX, playerO);
            
        } else {
            SetPlayerColors(playerO, playerX);
        }
        */
        
    }

    /*
    void SetPlayerColors (Player newPlayer, Player oldPlayer) 
    {
        newPlayer.panel.color = activePlayerColor.panelColor; 
        newPlayer.text.color = activePlayerColor.textColor; 
        oldPlayer.panel.color = inactivePlayerColor.panelColor; 
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }
    */

    void SetPlayerColors()
    {
        Player activePlayer = (playerSide == "X") ? playerX : playerO;
        Player inactivePlayer = (playerSide == "X") ? playerO : playerX;
        activePlayer.panel.color = activePlayerColor.panelColor;
        activePlayer.text.color = activePlayerColor.textColor;
        inactivePlayer.panel.color = inactivePlayerColor.panelColor;
        inactivePlayer.text.color = inactivePlayerColor.textColor;
    }





    void GameOver (string winningPlayer) 
    {
        /*
        for (int i = 0; i < buttonList.Length; i++) 
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
            
        }
        */

        SetBoardInteractable(false);

        if (winningPlayer == "draw") 
        { 
            SetGameOverText("It's a Draw!"); 
            SetPlayerColorsInactive();
        } else { 
            SetGameOverText(winningPlayer + " Wins!");
        }

        

        //gameOverPanel.SetActive(true); gameOverText.text = playerSide + " Wins!"; 
        // Note the space after the first " and Wins!"

        restartButton.SetActive(true);

    }
  

    void SetGameOverText(string value) 
    {
        gameOverPanel.SetActive(true); 
        gameOverText.text = value;
    }


    void RestartGame() 
    {
        //playerSide = "X"; 
        moveCount = 0; 

        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        startInfo.SetActive(true);

        SetPlayerColorsInactive();

        
        //SetPlayerColors(playerX, playerO);
        
        for (int i = 0; i < buttonList.Length; i++) 
        { 
            //buttonList[i].GetComponentInParent<Button>().interactable = true; 
            buttonList [i].text = ""; 
        }



    }

    void SetBoardInteractable (bool toggle) 
    {
        for (int i = 0; i < buttonList.Length; i++) 
        { 
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;  
        }
    }




    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle; 
        playerO.button.interactable = toggle;

    }

    void SetPlayerColorsInactive() 
    { 
        playerX.panel.color = inactivePlayerColor.panelColor; 
        playerX.text.color = inactivePlayerColor.textColor; 
        playerO.panel.color = inactivePlayerColor.panelColor; 
        playerO.text.color = inactivePlayerColor.textColor; 
    }


}

