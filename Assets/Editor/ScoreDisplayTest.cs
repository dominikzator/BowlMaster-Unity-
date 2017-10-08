using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

[TestFixture]
public class ScoreDisplayTest {

	[Test]
	public void T00PassingTest () {
		Assert.AreEqual (1, 1);
	}

	[Test]
	public void T01Bowl23 () {
		int[] rolls = {2,3};
		string rollsString = "23";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T02Bowl234 () {
		int[] rolls = {2,3,4};
		string rollsString = "234";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T03Bowl2345 () {
		int[] rolls = {2,3,4,5};
		string rollsString = "2345";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T04Bowl23456 () {
		int[] rolls = {2,3,4,5,6};
		string rollsString = "23456";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T05Bowl234561 () {
		int[] rolls = {2,3,4,5,6,1};
		string rollsString = "234561";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T06Bowl2345612 () {
		int[] rolls = {2,3, 4,5, 6,1, 2};
		string rollsString = "2345612";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T07BowlX1 () {
		int[] rolls = {10, 1};
		string rollsString = "X 1";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T08Bowl19 () {
		int[] rolls = {1, 9};
		string rollsString = "1/";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T09Bowl123455 () {
		int[] rolls = {1,2, 3,4, 5,5};
		string rollsString = "12345/";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T10SpareBonus () {
		int[] rolls = {1,2, 3,5, 5,5, 3,3};
		string rollsString = "12355/33";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T11SpareBonus2 () {
		int[] rolls = {1,2, 3,5, 5,5, 3,3, 7,1, 9,1, 6};
		string rollsString = "12355/33719/6";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T12StrikeBonus () {
		int[] rolls = { 10, 3,4};
		string rollsString = "X 34";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T13StrikeBonus3 () {
		int[] rolls = { 1,2, 3,4, 5,4, 3,2, 10, 1,3, 3,4};
		string rollsString = "12345432X 1334";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T14MultiStrikes () {
		int[] rolls = { 10, 10, 2,3};
		string rollsString = "X X 23";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T15MultiStrikes3 () {
		int[] rolls = { 10, 10, 2,3, 10, 5,3};
		string rollsString = "X X 23X 53";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T16TestGutterGame () {
		int[] rolls = { 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0};
		string rollsString = "--------------------";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T17TestAllOnes () {
		int[] rolls = { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1};
		string rollsString = "11111111111111111111";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T18TestAllStrikes () {
		int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10,10,10};
		string rollsString = "X X X X X X X X X XXX";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T19TestImmediateStrikeBonus() {
		int[] rolls = {5,5, 3};
		string rollsString = "5/3";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T20SpareInLastFrame () {
		int[] rolls = { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,9,7};
		string rollsString = "1111111111111111111/7";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void T21StrikeInLastFrame () {
		int[] rolls = { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10,2,3};
		string rollsString = "111111111111111111X23";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	// http://slocums.homestead.com/gamescore.html
	[Test]
	[Category ("Verification")]
	public void TG02GoldenCopyA () {
		int[] rolls = { 10, 7,3, 9,0, 10, 0,8, 8,2, 0,6, 10, 10, 10,8,1};
		string rollsString = "X 7/9-X -88/-6X X X81";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	//http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
	[Category ("Verification")]
	[Test]
	public void TG03GoldenCopyB1of3 () {
		int[] rolls = { 10, 9,1, 9,1, 9,1, 9,1, 7,0, 9,0, 10, 8,2, 8,2,10};
		string rollsString = "X 9/9/9/9/7-9-X 8/8/X";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	//http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
	[Category ("Verification")]
	[Test]
	public void TG03GoldenCopyB2of3 () {
		int[] rolls = { 8,2, 8,1, 9,1, 7,1, 8,2, 9,1, 9,1, 10, 10, 7,1};
		string rollsString = "8/819/718/9/9/X X 71";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	//http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
	[Category ("Verification")]
	[Test]
	public void TG03GoldenCopyB3of3 () {
		int[] rolls = { 10, 10, 9,0, 10, 7,3, 10, 8,1, 6,3, 6,2, 9,1,10};
		string rollsString = "X X 9-X 7/X 8163629/X";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	// http://brownswick.com/wp-content/uploads/2012/06/OpenBowlingScores-6-12-12.jpg
	[Category ("Verification")]
	[Test]
	public void TG03GoldenCopyC1of3 () {
		int[] rolls = { 7,2, 10, 10, 10, 10, 7,3, 10, 10, 9,1, 10,10,9};
		string rollsString = "72X X X X 7/X X 9/XX9";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	// http://brownswick.com/wp-content/uploads/2012/06/OpenBowlingScores-6-12-12.jpg
	[Category ("Verification")]
	[Test]
	public void TG03GoldenCopyC2of3 () {
		int[] rolls = { 10, 10, 10, 10, 9,0, 10, 10, 10, 10, 10,9,1};
		string rollsString = "X X X X 9-X X X X X9/";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Category ("Verification")]
	[Test]
	public void TZ01MyTest () {
		int[] rolls = {5,5, 10, 10, 7,3, 0,0, 0,10, 10, 5,5, 9,1, 10, 10, 10 };
		string rollsString = "5/X X 7/---/X 5/9/XXX";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

	[Test]
	public void TZ02Bowl010 () {
		int[] rolls = {0,10};
		string rollsString = "-/";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
	}

//	[Test]
//	public void T01Bowl1 () {
//		int[] rolls =  {1};
//		string rollsString = "1";
//		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
//	}
//	
//	[Test]
//	public void T02Bowl15 () {
//		int[] rolls =  {1,5};
//		string rollsString = "15";
//		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
//	}
//	
//	[Test]
//	public void T03BowlX5 () {
//		int[] rolls =  {10,5};
//		string rollsString = "X5";
//		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
//	}
//	
//	[Test]
//	public void T04BowlXX5 () {
//		int[] rolls =  {10,5};
//		string rollsString = "X5";
//		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
//	}
//	
//	[Test]
//	public void T05BowlZeroTen () {
//		int[] rolls =  {0,10};
//		string rollsString = "0/";
//		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
//	}
//	
//	[Test]
//	public void T06AllStrikes () {
//		int[] rolls =  {10,10,10,10,10,10,10,10,10,10,10,10};
//		string rollsString = "XXXXXXXXXXXX";
//		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
//	}
//	
//	[Test]
//	public void T07SomeSpares () {
//		int[] rolls =  {4,6, 8,2, 5,5};
//		string rollsString = "4/8/5/";
//		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
//	}
//	
}
