using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is a dummy script that unlocks weapons when the player approaches it. 
 * In the actual game, this will be handled through menus.
 * 
 */

public class Unlockables : MonoBehaviour {

	public bool[] currUnlockWeapons = new bool[15];
	public int[] weaponLevelReq = new int[15];

	public int[] gunDamageCost = new int[4];
	public int[] gunAmmoCost = new int[4];
	public int[] gunReloadCost = new int[4];
	public int[] gunFireRateCost = new int[4];

	private GameObject player;

	void Awake()
	{		
		// When the "menu screen" boots up, any weapon that the player can get will be unlocked.
		player = GameObject.FindGameObjectWithTag("Player");
		for(int i = 0; i < currUnlockWeapons.Length; i++)
		{
			// We check if the player has reached the level requirement for a weapon. If so, the weapon is unlocked.
			if(player.GetComponent<PlayerState>().currLevel >= weaponLevelReq[i] && currUnlockWeapons[i] == false)
			{
				currUnlockWeapons[i] = true;
				print("Congrats! At level " + weaponLevelReq[i] + ", you got a new weapon!");
			}
		}
	}
		
	/* 
	 * This simulate seeing what the player can upgrade. This will automatically select which upgrades the player can afford. 
	 * To prevent "reupgrading", the cost of the item will be 0 in order to do a check if they already upgraded to that.
	 */

	void Update()
	{
		if(Input.GetKeyUp(KeyCode.C))
		{
			for(int i = 0; i < 4; i++)
			{
				int newLevel = i+2;

				float purch1 = DeductCost(gunDamageCost[i]);
				if(gunDamageCost[i] != 0 && player.GetComponent<PlayerState>().currCurrency != purch1)
				{
					print("Upgraded the gun's damage power to lv " + newLevel);
					player.GetComponent<PlayerState>().currWeaponDamage += 5;
					player.GetComponent<PlayerState>().currCurrency = purch1;
					gunDamageCost[i] = 0;
				}
				float purch2 = DeductCost(gunAmmoCost[i]);
				if(gunAmmoCost[i] != 0 && player.GetComponent<PlayerState>().currCurrency != purch2)
				{
					print("Upgraded the gun's ammo count to lv " + newLevel);
					player.GetComponent<PlayerState>().currWeaponAmmo += 20;
					player.GetComponent<PlayerState>().currCurrency = purch2;
					gunAmmoCost[i] = 0;
				}
				float purch3 = DeductCost(gunFireRateCost[i]);
				if(gunFireRateCost[i] != 0 && player.GetComponent<PlayerState>().currCurrency != purch3)
				{
					print("Upgraded the gun's fire rate to lv " + newLevel);
					player.GetComponent<PlayerState>().currWeaponShotRate += 0.5f;
					player.GetComponent<PlayerState>().currCurrency = purch3;
					gunFireRateCost[i] = 0;
				}
				float purch4 = DeductCost(gunReloadCost[i]);
				if(gunReloadCost[i] != 0 && player.GetComponent<PlayerState>().currCurrency != purch4)
				{
					print("Upgraded the gun's reload speed to lv " + newLevel);
					player.GetComponent<PlayerState>().currWeaponReloadRate += 0.5f;
					player.GetComponent<PlayerState>().currCurrency = purch4;
					gunReloadCost[i] = 0;
				}
			}
		}
	}

	// If the player has enough money, returns the amount they have left after the purchange. Else, returns the current amount they have.
	float DeductCost(float cost)
	{
		float cashOnHand = player.GetComponent<PlayerState>().currCurrency;
		if(cashOnHand - cost >= 0f)
			return cashOnHand - cost;
		else
			return cashOnHand;
	}
}
