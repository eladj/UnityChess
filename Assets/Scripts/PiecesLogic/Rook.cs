using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : PieceLogic {
	
	public Rook (Game.SideColor sideColor) : base(sideColor){
		type = Game.PieceType.Rook;
	}

	public override List<TileLogic> GetValidMoves(){
		List<TileLogic> validMoves = new List<TileLogic>();

		// Down
		for (int row = currentTile.row - 1; row >= 0; row--) {
			validMoves.Add(new TileLogic (row, currentTile.column));
		}

		// Up
		for (int row = currentTile.row + 1; row < BoardLogic.Height; row++) {
			validMoves.Add(new TileLogic (row, currentTile.column));
		}

		// Left
		for (int col = currentTile.column - 1; col >= 0; col--) {
			validMoves.Add(new TileLogic (currentTile.row, col));
		}

		// Right
		for (int col = currentTile.column + 1; col < BoardLogic.Width; col++) {
			validMoves.Add(new TileLogic (currentTile.row, col));
		}
		return validMoves;
	}
}
