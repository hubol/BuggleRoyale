using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Recieves input from PlayerSoul and relays it to movements
public class InputReciever : MonoBehaviour
{
	public bool[] actions = new bool[]{false,false,false};
	public bool action1{get{return actions[0];} set{actions[0]=value;}}
	public bool action2{get{return actions[1];} set{actions[1]=value;}}
	public bool action3{get{return actions[2];} set{actions[2]=value;}}
	public bool left=false, right=false, up=false, down=false;
}
