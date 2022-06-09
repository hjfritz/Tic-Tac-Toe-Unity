using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

public enum TicTacToeState{none, cross, circle}

[System.Serializable]
public class WinnerEvent : UnityEvent<int>
{
}

public class TicTacToeAI : MonoBehaviour
{

	int _aiLevel;

	//Code representation for board
	[SerializeField]
	private string One = "e";

	[SerializeField]
	private string Two = "e";

	[SerializeField]
	private string Three = "e";

	[SerializeField]
	private string Four = "e";

	[SerializeField]
	private string Five = "e";

	[SerializeField]
	private string Six = "e";

	[SerializeField]
	private string Seven = "e";

	[SerializeField]
	private string Eight = "e";

	[SerializeField]
	private string Nine = "e";


	TicTacToeState[,] boardState;

	[SerializeField]
	private bool _isPlayerTurn;

	[SerializeField]
	private int _gridSize = 3;
	
	[SerializeField]
	private TicTacToeState playerState = TicTacToeState.cross;
	TicTacToeState aiState = TicTacToeState.circle;

	[SerializeField]
	private GameObject _xPrefab;

	[SerializeField]
	private GameObject _oPrefab;

	//Keeps track of turns
	[SerializeField]
	public int _turn = 1;

	public UnityEvent onGameStarted;

	//Call This event with the player number to denote the winner
	public WinnerEvent onPlayerWin;

	ClickTrigger[,] _triggers;
	
	private void Awake()
	{
		if(onPlayerWin == null){
			onPlayerWin = new WinnerEvent();
		}
	}

	public void StartAI(int AILevel){
		_aiLevel = AILevel;
		StartGame();
	}

	public void RegisterTransform(int myCoordX, int myCoordY, ClickTrigger clickTrigger)
	{
		_triggers[myCoordX, myCoordY] = clickTrigger;
	}

	private void StartGame()
	{
		_triggers = new ClickTrigger[3,3];
		onGameStarted.Invoke();
	}

	public void PlayerSelects(int coordX, int coordY){

		SetVisual(coordX, coordY, playerState);
		ChangeBoardState(coordX, coordY, "p");
		AiTurn();
	}

	public void AiSelects(int coordX, int coordY){

		SetVisual(coordX, coordY, aiState);
		ChangeBoardState(coordX, coordY, "a");
	}

	private void SetVisual(int coordX, int coordY, TicTacToeState targetState)
	{
		Instantiate(
			targetState == TicTacToeState.circle ? _oPrefab : _xPrefab,
			_triggers[coordX, coordY].transform.position,
			Quaternion.identity
		);
		_turn++;
	}

	private void Pick()
	{
		if (One == "e")
		{
			AiSelects(0,0);
		}else if (Two == "e")
		{
			AiSelects(0,1);
		}else if (Three == "e")
		{
			AiSelects(0,2);
		}else if (Four == "e")
		{
			AiSelects(1,0);
		}else if (Five == "e")
		{
			AiSelects(1,1);
		}else if (Six == "e")
		{
			AiSelects(1,2);
		}else if (Seven == "e")
		{
			AiSelects(2,0);
		}else if (Eight == "e")
		{
			AiSelects(2,1);
		}else if (Nine == "e")
		{
			AiSelects(2,2);
		}
	}

	private bool CheckBoardState(int coordX, int coordY)
    {
		if(coordX == 0 && coordY == 0)
        {
			if(One == "e")
            {
				return false;
            }
            else
            {
				return true;
            }
        }else if (coordX == 1 && coordY == 0)
		{
			if (Two == "e")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (coordX == 2 && coordY == 0)
		{
			if (Three == "e")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (coordX == 0 && coordY == 1)
		{
			if (Four == "e")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (coordX == 1 && coordY == 1)
		{
			if (Five == "e")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (coordX == 2 && coordY == 1)
		{
			if (Six == "e")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (coordX == 0 && coordY == 2)
		{
			if (Seven == "e")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (coordX == 1 && coordY == 2)
		{
			if (Eight == "e")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (coordX == 2 && coordY == 2)
		{
			if (Nine == "e")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
        else
        {
			return true;
        }
	}

	private void ChangeBoardState(int coordX, int coordY, string player)
    {
		if (coordX == 0 && coordY == 0)
		{
			One = player;
		}
		else if (coordX == 0 && coordY == 1)
		{
			Two = player;
		}
		else if (coordX == 0 && coordY == 2)
		{
			Three = player;
		}
		else if (coordX == 1 && coordY == 0)
		{
			Four = player;
		}
		else if (coordX == 1 && coordY == 1)
		{
			Five = player;
		}
		else if (coordX == 1 && coordY == 2)
		{
			Six = player;
		}
		else if (coordX == 2 && coordY == 0)
		{
			Seven = player;
		}
		else if (coordX == 2 && coordY == 1)
		{
			Eight = player;
		}
		else if (coordX == 2 && coordY == 2)
		{
			Nine = player;
		}
	}

	private void CheckForWin()
    {
		if(One != "e" && One == Two && Two == Three)
        {
			if(One == "a")
			{
				onPlayerWin.Invoke(1);
			}else if(One == "p")
			{
				onPlayerWin.Invoke(0);
			}
        }else if (Four != "e" && Four == Five && Five == Six)
		{
			if(Four == "a")
			{
				onPlayerWin.Invoke(1);
			}else if(Four == "p")
			{
				onPlayerWin.Invoke(0);
			}
		}else if (Seven != "e" && Seven == Eight && Eight == Nine)
		{
			if(Seven == "a")
			{
				onPlayerWin.Invoke(1);
			}else if(Seven == "p")
			{
				onPlayerWin.Invoke(0);
			}
		}else if (One != "e" && One == Four && Four == Seven)
		{
			if(Four == "a")
			{
				onPlayerWin.Invoke(1);
			}else if(Four == "p")
			{
				onPlayerWin.Invoke(0);
			}
		}else if (Two != "e" && Two == Five && Five == Eight)
		{
			if(Two == "a")
			{
				onPlayerWin.Invoke(1);
			}else if(Two == "p")
			{
				onPlayerWin.Invoke(0);
			}
		}else if (Three != "e" && Three == Six && Nine == Six)
		{
			if(Three == "a")
			{
				onPlayerWin.Invoke(1);
			}else if(Three == "p")
			{
				onPlayerWin.Invoke(0);
			}
		}else if (One != "e" && One == Five && Five == Nine)
		{
			if(One == "a")
			{
				onPlayerWin.Invoke(1);
			}else if(One == "p")
			{
				onPlayerWin.Invoke(0);
			}
		}else if (Four != "e" && Four == Five && Five == Six)
		{
			if(Four == "a")
			{
				onPlayerWin.Invoke(1);
			}else if(Four == "p")
			{
				onPlayerWin.Invoke(0);
			}
		}else if (Three != "e" && Three == Five && Five == Seven)
		{
			if(Three == "a")
			{
				onPlayerWin.Invoke(1);
			}else if(Three == "p")
			{
				onPlayerWin.Invoke(0);
			}
		}
    }

	private void AiTurn()
    {
        if (_turn == 2)
        {
            if (CheckBoardState(1,1))
            {
				AiSelects(0, 2);
			}
            else
            {
				AiSelects(1, 1);
            }
        }else if(_turn == 4 || _turn == 6 || _turn == 8){
		        if (One == "p" && Three == "p" && Two == "e")
		        {
			        AiSelects(0,1);
		        }else if (One == "p" && Two == "p" && Three == "e")
		        {
			        AiSelects(0,2);
		        }else if (Two == "p" && Three == "p" && One == "e")
		        {
			        AiSelects(0,0);
		        }else if (One == "p" && Four == "p" && Seven == "e")
		        {
			        AiSelects(2,0);
		        }else if (One == "p" && Seven == "p" && Four == "e")
		        {
			        AiSelects(1,0);
		        }else if (Four == "p" && Seven == "p" && One == "e")
		        {
			        AiSelects(0,0);
		        }else if (One == "p" && Five == "p" && Nine == "e")
		        {
			        AiSelects(2,2);
		        }else if (One == "p" && Nine == "p" && Five == "e")
		        {
			        AiSelects(1,1);
		        }else if (Five == "p" && Nine == "p" && One == "e")
		        {
			        AiSelects(0,0);
		        }else if (Three == "p" && Seven == "p" && Five == "e")
		        {
			        AiSelects(1,1);
		        }else if (Five == "p" && Seven == "p" && Three == "e")
		        {
			        AiSelects(0,2);
		        }else if (Three == "p" && Five == "p" && Seven == "e")
		        {
			        AiSelects(2,0);
		        }else if (Seven == "p" && Eight == "p" && Nine == "e")
		        {
			        AiSelects(2,2);
		        }else if (Eight == "p" && Nine == "p" && Seven == "e")
		        {
			        AiSelects(2,0);
		        }else if (Seven == "p" && Nine == "p" && Eight == "e")
		        {
			        AiSelects(2,1);
		        }else if (Three == "p" && Nine == "p" && Six == "e")
		        {
			        AiSelects(1,2);
		        }else if (Six == "p" && Nine == "p" && Three == "e")
		        {
			        AiSelects(0,2);
		        }else if (Three == "p" && Six == "p" && Nine == "e")
		        {
			        AiSelects(2,2);
		        }else if (Four == "p" && Five == "p" && Six == "e")
		        {
			        AiSelects(1,2);
		        }else if (Five == "p" && Six == "p" && Four == "e")
		        {
			        AiSelects(1,0);
		        }else if (Four == "p" && Six == "p" && Five == "e")
		        {
			        AiSelects(1,1);
		        }else if (Two == "p" && Five == "p" && Eight == "e")
		        {
			        AiSelects(2,1);
		        }else if (Two == "p" && Eight == "p" && Five == "e")
		        {
			        AiSelects(1,1);
		        }else if (Five == "p" && Eight == "p" && Two == "e")
		        {
			        AiSelects(0,1);
		        }
		        else
		        {
			        Pick();
		        }
        }else if (_turn == 10)
        {
	        onPlayerWin.Invoke(-1);
        }
        else
        {
	        Pick();
        }
        CheckForWin();
    }
}
