using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionOne : MonoBehaviour
{
    //INPUT VARIABLES
    public string characterName = "Pikachu";
    public int characterLevel = 10;
    public string className = "Paladin";
    public int conScore = 18;
    public bool isHillDwarf = false; 
    public bool hasToughFeat = true;
    public bool isAveraged = false; //true is average false is rolled

    // ADDITIONAL VARIABLES / STRUCTURES
    Dictionary<string, int> classes = new Dictionary<string, int>
    {
        {"Artificer", 8},
        {"Barbarian", 12},
        {"Bard", 8},
        {"Cleric", 8},
        {"Druid", 8},
        {"Fighter", 10},
        {"Monk", 8},
        {"Ranger", 10},
        {"Rogue", 8},
        {"Paladin", 10},
        {"Sorcerer", 6},
        {"Wizard", 6},
        {"Warlock", 8}
    };

    Dictionary<int, int> conMods = new Dictionary<int, int>
    {
        {1, -5},
        {2, -4},
        {3, -4},
        {4, -3},
        {5, -3},
        {6, -2},
        {7, -2},
        {8, -1},
        {9, -1},
        {10, 0},
        {11, 0},
        {12, 1},
        {13, 1},
        {14, 2},
        {15, 2},
        {16, 3},
        {17, 3},
        {18, 4},
        {19, 4},
        {20, 5},
        {21, 5},
        {22, 6},
        {23, 6},
        {24, 7},
        {25, 7},
        {26, 8},
        {27, 8},
        {28, 9},
        {29, 9},
        {30, 10}
    };

    private int hitDie;
    private int totalHealth;
	
    void Start()
    {
        hitDie = FindHitdie(className);
		
		if(isAveraged)
			totalHealth += RollHpAveraged(hitDie, characterLevel);
		else
			totalHealth += RollHp(hitDie, characterLevel);
		
		//checking hillDwarf and toughFeat 
		
		if(isHillDwarf)
			totalHealth += characterLevel;
		if(hasToughFeat)
			totalHealth += characterLevel * 2;
		
		totalHealth += ConstitutionCheck(conScore);	//checking conScore chart
		
		if(totalHealth <= 0)	//incase total health ends up negative set to 1
			totalHealth = 1;
		
		Debug.LogFormat("My character {0} is a level {1} {2} with a CON score of {3} and {4} a Hill Dwarf and {5} Tough feat. I want the HP {6}", characterName, characterLevel, className, conScore, isHillDwarf ? "is" : "is not", hasToughFeat ? "has" : "doesn't have", isAveraged ? "averaged" : "rolled");
		Debug.Log("Total health is: " + totalHealth);
    }

    private int FindHitdie(string className)	//returns the hit die based on the class
    {
        if (classes.ContainsKey(className))
			return classes[className];
        else
            return 0;
    }
	
	private int RollHp(int hitDie, int characterLevel)	//rolls for hp
	{
		int counter = 0;
		for(int i =0; i <= characterLevel; i++)
		{
			counter += Random.Range(1, hitDie);
		}
		return counter;
	}
	
	private int RollHpAveraged(int hitDie, int characterLevel)	//gets average for hp
	{
		float average = (hitDie / 2f + 0.5f) * characterLevel;
		return Mathf.CeilToInt(average);
	}
	
	private int ConstitutionCheck(int conScore)
	{
		if (conMods.ContainsKey(conScore))
			return conMods[conScore];
        else
            return 0;
	}
}