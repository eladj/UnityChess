using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : PieceLogic {
	
	public Queen (Game.SideColor sideColor) : base(sideColor){
		type = Game.PieceType.Queen;
	}

	public override List<TileLogic> GetValidMoves(){
		List<TileLogic> validMoves = new List<TileLogic>();

		// Up-Left
		for (int row = currentTile.row + 1, col = currentTile.column - 1;
			row < BoardLogic.Height && col >= 0; row++, col--) {
			validMoves.Add (new TileLogic (row, col));
		}

		// Up-Right
		for (int row = currentTile.row + 1, col = currentTile.column + 1;
			row < BoardLogic.Height && col < BoardLogic.Width; row++, col++) {
			validMoves.Add (new TileLogic (row, col));
		}

		// Down-Left
		for (int row = currentTile.row - 1, col = currentTile.column - 1;
			row >= 0 && col >= 0; row--, col--) {
			validMoves.Add (new TileLogic (row, col));
		}

		// Down-Right
		for (int row = currentTile.row - 1, col = currentTile.column + 1;
			row >= 0 && col < BoardLogic.Width; row--, col++) {
			validMoves.Add (new TileLogic (row, col));
		}

		// Down
		for (int row = currentTile.row - 1; row >= 0; row--) {
			validMoves.Add (new TileLogic (row, currentTile.column));
		}

		// Up
		for (int row = currentTile.row + 1; row < BoardLogic.Height; row++) {
			validMoves.Add (new TileLogic (row, currentTile.column));
		}

		// Left
		for (int col = currentTile.column - 1; col >= 0; col--) {
			validMoves.Add (new TileLogic (currentTile.row, col));
		}

		// Right
		for (int col = currentTile.column + 1; col < BoardLogic.Width; col++) {
			validMoves.Add (new TileLogic (currentTile.row, col));
		}

		return validMoves;
	}
}
