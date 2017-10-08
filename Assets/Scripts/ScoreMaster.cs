using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreMaster {

	//returns a list of cumulative scores, lika a normal score card
	public static List<int> ScoreCumulative (List<int> rolls)
	{
		List<int> cumulativeScores = new List<int> ();
		int runningTotal = 0;

		foreach (int frameScore in ScoreFrames(rolls)) {
			runningTotal += frameScore;
			cumulativeScores.Add (runningTotal);
		}

		return cumulativeScores;
	}

	// returns a list of individual frame scores, NOT cumulative
	public static List<int> ScoreFrames (List<int> rolls)
	{
		int counter = 0;				// help loop counter
		int lastRoll = -1;				// value for holding last roll
		int strikeValue = 0;			// value for summing strike values
		bool lastRollSpare = false;		// trigger for last roll spare
		bool lastRollStrike = false;	// trigger for last roll strike

		List<int> frameList = new List<int> ();	

		foreach (int roll in rolls) {

			if (counter % 2 == 0) {

				if (lastRollSpare) {				//spare bonus (last frame = 10)
					frameList.Add (10 + roll);
					lastRollSpare = false;
				}
				if (strikeValue == 20 && roll != 10) {	// two last strike in a row, and current not strike
					frameList.Add (strikeValue + roll);
				}
				if (roll == 10) {						//strike
					if (strikeValue == 20) {			// three strikes in a row
						frameList.Add (30);
					}
					if (lastRollStrike) {				// 2 strikes in a row
						strikeValue = 20;
					}
					lastRollStrike = true;				
					counter++;							// move to next frame
				}
				if (roll != 10) {						
					if (lastRollStrike) {				// sum bonus for last strike (first strike bonus bowl)
						strikeValue = 10 + roll;
					}
				}
			}
			else if (counter % 2 == 1) {
				
				if (lastRollStrike) {					// sum bonus for last strike (second strike bonus bowl)
					strikeValue += roll;
					frameList.Add (strikeValue);
					lastRollStrike = false;
					if (frameList.Count == 10) {		// if 10 frames, finish algorithm
						break;
					}
				}
				if ((lastRoll + roll) != 10) {			//frame not cleared
					frameList.Add (lastRoll + roll);
				} 
				else if ((lastRoll + roll) == 10) {		// frame cleared, trigger spare and reset bonus values for strikes		
					lastRollSpare = true;
					strikeValue = 0;
				}		
			}
			lastRoll = roll;
			counter++;
		}
		return frameList;
	}


}
