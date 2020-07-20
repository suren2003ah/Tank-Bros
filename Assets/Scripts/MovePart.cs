using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePart : MonoBehaviour
{
    public GameObject MovingPlayer1;
    public GameObject MovingPlayer2;
    public void EmitPlayer1()
    {
        MovingPlayer1.SetActive(true);
    }
    public void EmitPlayer2()
    {
        MovingPlayer2.SetActive(true);
    }
}
