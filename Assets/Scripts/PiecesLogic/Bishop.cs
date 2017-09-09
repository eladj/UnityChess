using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : PieceLogic {
	
	public Bishop (Game.SideColor sideColor) : base(sideColor) {
		type = Game.PieceType.Bishop;
	}

	public override List<TileLogic> GetValidMoves(){
		List<TileLogic> validMoves = new List<TileLogic>();

		// Up-Left
		for (int row = currentTile.row + 1, col = currentTile.column - 1;
			row < boardHeight && col >= 0; row++, col--) {
			validMoves.Add (new TileLogic (row, col));
		}

		// Up-Right
		for (int row = currentTile.row + 1, col = currentTile.column + 1;
			row < boardHeight && col < boardWidth; row++, col++) {
			validMoves.Add (new TileLogic (row, col));
		}

		// Down-Left
		for (int row = currentTile.row - 1, col = currentTile.column - 1;
			row >= 0 && col >= 0; row--, col--) {
			validMoves.Add (new TileLogic (row, col));
		}

		// Down-Right
		for (int row = currentTile.row - 1, col = currentTile.column + 1;
			row >= 0 && col < boardWidth; row--, col++) {
			validMoves.Add (new TileLogic (row, col));
		}

		return validMoves;
	}
}
