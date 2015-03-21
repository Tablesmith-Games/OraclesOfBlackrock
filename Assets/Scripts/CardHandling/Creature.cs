using UnityEngine;
using System.Collections;

public class Creature  : Card {

	public enum CreatureType{Azal, Elf, Undead, Human, Dragon, Beast};

	public int attack;
	public int endurance;
	public bool isDead;
	public CreatureType creatureType;


	public Creature(string cardName, int blackrockCost, Affinty cardAffinty, CreatureType creatureType, bool isInHand, int attack, int endurance, bool isDead): 
		base(cardName, blackrockCost,cardAffinty, isInHand)
	{
		this.attack = attack;
		this.endurance = endurance;
		this.creatureType = creatureType;
		isDead = false;
		this.isDead = isDead;
	}

	public void Attack(Creature target)
	{
		target.endurance -= this.attack;
		this.endurance -= target.attack;

		if(this.endurance <= 0)
		{
			this.isDead = true;
		}
	}
}
