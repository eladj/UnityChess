using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : PieceLogic {
	
	public King (Game.SideColor sideColor) : base(sideColor){
		type = Game.PieceType.King;
	}

	public override List<TileLogic> GetValidMoves(){
		List<TileLogic> validMoves = new List<TileLogic>();

		// Standard moves
		for (int row = Mathf.Max (0, currentTile.row - 1);
			row <= Mathf.Min (boardHeight - 1, currentTile.row + 1); row++) {
			for (int col = Mathf.Max (0, currentTile.column - 1);
				col <= Mathf.Min (boardWidth - 1, currentTile.column + 1); col++) {
				validMoves.Add (new TileLogic (row, col));
			}
		}
		return validMoves;
	}
}