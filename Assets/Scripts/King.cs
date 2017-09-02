using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece {
	public override bool IsMoveLegal(Tile targetTile){
		if (Mathf.Abs (targetTile.row - currentTile.row) == 1 &&
			Mathf.Abs (targetTile.column - currentTile.column) == 1 &&
			targetTile.row >= 0 && targetTile.row < 8 &&
			targetTile.column >= 0 && targetTile.column < 8) {
			return true;
		} else {
			return false;
		}
	}
}