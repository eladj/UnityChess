using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic {

	public string p_name;
	private Game.SideColor p_color;

	public PlayerLogic(string name, Game.SideColor sideColor){
		p_name = name;
		p_color = sideColor;
	}

	public override string ToString ()
	{
		return p_name + " (" + p_color.ToString() + ")";
	}
}
