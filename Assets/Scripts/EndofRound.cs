using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndofRound : MonoBehaviour {

    public int overallScore = 0;
    public int overallKillCount = 0;

    public int killComboGun = 0;
    public int killComboTrap = 0;
    public int highestKillCombo = 0;

    public int resourceCount = 0;

    public int playerLevel = 0;
    public int playerCurrency = 0;

    public Text scoreText;
    public Text killText;
    public Text gunText;
    public Text trapText;
    public Text comboText;
    public Text resourceText;

    public Text level;
    public Text currency;

    void Start()
    {
        DisplayLevel();
        DisplayScore();
        DisplayKills();
        DisplayGunCombo();
        DisplayTrapCombo();
        DisplayHighestCombo();
        
    }

    void FixedUpdate()
    {
        if (resourceCount > 0){
            resourceCount -= (int)Time.time;
            playerCurrency += (int)Time.time * Random.Range(1,5);
            
        }

        DisplayResources();
        DisplayCurrency();

    }

    void DisplayLevel()
    {
        level.text = "Level: " + playerLevel.ToString();
    }

    void DisplayCurrency()
    {
        currency.text = "Currency: " + playerCurrency.ToString() + " gems";
    }

    void DisplayScore()
    {
        scoreText.text = "Score: \t" + overallScore.ToString();
    }

    void DisplayKills()
    {
        killText.text = "Total Kills: \t" + overallKillCount.ToString();
    }

    void DisplayGunCombo()
    {
        gunText.text = "Combos with Gun: \t" + killComboGun.ToString();
    }

    void DisplayTrapCombo()
    {
        trapText.text = "Combos with Traps: \t" + killComboTrap.ToString();
    }

    void DisplayHighestCombo()
    {
        comboText.text = "Highest Combo: \t" + highestKillCombo.ToString();
    }

    void DisplayResources()
    {
        resourceText.text = "Resources: \t" + resourceCount.ToString();
    }

}
