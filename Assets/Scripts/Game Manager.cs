using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject storeGO;
    public GameObject nextWaveGO;
    public GameObject winningGO;
    public EnemySpawner2 leftSpawner;
    public EnemySpawner2 rightSpawner;
    public HP playerHP;
    public HP houseHP;
    bool waveDone = false;
    bool gameDone = false;
    // Start is called before the first frame update
    void Start()
    {
        storeGO.SetActive(false);
        nextWaveGO.SetActive(false);
        winningGO.SetActive(false);
        //Time.timeScale = 1f;
    }

    // Update is called once per frame
    public void LateUpdate()
    {
        if (leftSpawner.spawning == false && rightSpawner.spawning == false)
        {
            if (Main.enemyCounter == 0)
            {
                nextWaveGO.SetActive(true);
            }            
        }
        else
        {
            nextWaveGO.SetActive(false);
        }


        if (leftSpawner.currentlvl > 3)
        {
            winningGO.SetActive(true);
        }
        else
        {
            winningGO.SetActive(false);
        }
                
    }

    void checkifNextWave()
    {
        nextWaveGO.SetActive(true);               
    }

    void checkIfWin()
    {
        winningGO.SetActive(true);
        gameDone = false;
    }

    public void nextWave()
    {
        nextWaveGO.SetActive(false);
        Main.lvl++;
        print(Main.lvl);
    }

    public void Done()
    {
        storeGO.SetActive(false);
        Main.lvl++;
        print(Main.lvl);
    }

    public void Store()
    {
        storeGO.SetActive(true);
        nextWaveGO.SetActive(false);
    }


    public void Heal()
    {
        playerHP.AddHealth(10);
    }

    public void Repair()
    {
        houseHP.AddHealth(10);
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("LevelOne");
        winningGO.SetActive(false);
        Main.lvl = 0;

    }

    public void OnClickHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
