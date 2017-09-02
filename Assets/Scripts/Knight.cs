using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece {
	protected override void Start (){
		base.Start ();
		cname = 'N';
	}

	bool CheckTile(Tile tile){
		if (tile != null) {
			if (tile.HasPiece ()) {
				if (tile.GetPiece ().color != this.color) {
					return true;
				}
			} else {
				return true;
			}
		}
		return false;
	}

	public override List<Tile> GetValidMoves(){
		List<Tile> validMoves = new List<Tile>();

		// Left-up 1
		Tile tile;
		tile = board.GetTile (currentTile.row + 1, currentTile.column - 2);
		if (CheckTile (tile)) { validMoves.Add (tile); }

		// Left-up 2
		tile = board.GetTile (currentTile.row + 2, currentTile.column - 1);
		if (CheckTile (tile)) { validMoves.Add (tile); }

		// Left-down 1
		tile = board.GetTile (currentTile.row - 1, currentTile.column - 2);
		if (CheckTile (tile)) { validMoves.Add (tile); }

		// Left-down 2
		tile = board.GetTile (currentTile.row - 2, currentTile.column - 1);
		if (CheckTile (tile)) { validMoves.Add (tile); }

		// Right-up 1
		tile = board.GetTile (currentTile.row + 1, currentTile.column + 2);
		if (CheckTile (tile)) { validMoves.Add (tile); }
		// Right-up 2
		tile = board.GetTile (currentTile.row + 2, currentTile.column + 1);
		if (CheckTile (tile)) { validMoves.Add (tile); }

		// Right-down 1
		tile = board.GetTile (currentTile.row - 1, currentTile.column + 2);
		if (CheckTile (tile)) { validMoves.Add (tile); }

		// Right-down 2
		tile = board.GetTile (currentTile.row - 2, currentTile.column + 1);
		if (CheckTile (tile)) { validMoves.Add (tile); }

		return validMoves;
	}
}
