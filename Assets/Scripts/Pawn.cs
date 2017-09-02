using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {
	protected override void Start (){
		base.Start ();
		cname = 'P';
	}

	public override List<Tile> GetValidMoves(){
		List<Tile> validMoves = new List<Tile>();
		// Check white
		if (this.color == ColorType.White) {
			int nextRow = currentTile.row + 1;
			// TODO - Check crowned pawn when reach row 8 - not here...
			Tile nextTile = board.GetTile (nextRow, currentTile.column);

			if (nextTile.GetPiece() == null) {
				validMoves.Add (nextTile);
			}

			// Check first row for 2 jump
			if (currentTile.row == 1) {
				Tile nextTwoTiles = board.GetTile (nextRow + 1, currentTile.column);
				if (nextTile.GetPiece() == null) {
					validMoves.Add (nextTwoTiles);
				}

			}

			// Check diagonal takes
			Tile takeLeftTile = board.GetTile (nextRow, currentTile.column - 1);
			if (takeLeftTile != null) { // Check we didn't exceed bounds
				if (takeLeftTile.HasPiece()){
					if (takeLeftTile.GetPiece().color != this.color) {
						validMoves.Add (takeLeftTile);
					}
				}
			}
			Tile takeRightTile = board.GetTile (nextRow, currentTile.column + 1);
			if (takeRightTile != null) {
				if (takeRightTile.HasPiece ()) {
					if (takeRightTile.GetPiece ().color != this.color) {
						validMoves.Add (takeRightTile);
					}
				}
			}
		}

		// Check black
		if (this.color == ColorType.Black) {
			int nextRow = currentTile.row - 1;
			// TODO - Check crowned pawn when reach row 0 - not here...
			Tile nextTile = board.GetTile (nextRow, currentTile.column);

			if (nextTile.GetPiece() == null) {
				validMoves.Add (nextTile);
			}

			// Check first row for 2 jump
			if (currentTile.row == 6) {
				Tile nextTwoTiles = board.GetTile (nextRow - 1, currentTile.column);
				if (nextTile.GetPiece() == null) {
					validMoves.Add (nextTwoTiles);
				}

			}

			// Check diagonal takes
			Tile takeLeftTile = board.GetTile (nextRow, currentTile.column - 1);
			if (takeLeftTile != null) {
				if (takeLeftTile.HasPiece ()) {
					if (takeLeftTile.GetPiece ().color != this.color) {
						validMoves.Add (takeLeftTile);
					}
				}
			}
			Tile takeRightTile = board.GetTile (nextRow, currentTile.column + 1);
			if (takeRightTile != null) {
				if (takeRightTile.HasPiece ()) {
					if (takeRightTile.GetPiece ().color != this.color) {
						validMoves.Add (takeRightTile);
					}
				}
			}
		}

		return validMoves;
	}
}