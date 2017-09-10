using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : PieceLogic {

	public Pawn (Game.SideColor sideColor) : base(sideColor){
		type = Game.PieceType.Pawn;
	}

	public override List<TileLogic> GetMoves(){
		List<TileLogic> validMoves = new List<TileLogic>();
		int nextRow = 0;

		// Check white
		if (this.color == Game.SideColor.White) {
			nextRow = currentTile.row + 1;
			validMoves.Add (new TileLogic(nextRow, currentTile.column));

			// Check first row for 2 jump
			if (currentTile.row == 1) {
				validMoves.Add (new TileLogic(nextRow + 1, currentTile.column));
			}
		}

		// Check black
		if (this.color == Game.SideColor.Black) {
			nextRow = currentTile.row - 1;
			validMoves.Add (new TileLogic(nextRow, currentTile.column));

			// Check first row for 2 jump
			if (currentTile.row == 6) {
				validMoves.Add (new TileLogic(nextRow - 1, currentTile.column));
			}
		}

		// Check diagonal takes
		// IMPORTANT: These move are also checked in the Game level, to make sure they
		//            are valid.
		validMoves.Add (new TileLogic(nextRow, currentTile.column - 1));
		validMoves.Add (new TileLogic(nextRow, currentTile.column + 1));

		return validMoves;
	}
}