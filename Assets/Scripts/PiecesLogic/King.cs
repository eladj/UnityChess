using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : PieceLogic {
	
	public King (Game.SideColor sideColor) : base(sideColor){
		type = Game.PieceType.King;
	}

	public override List<TileLogic> GetMoves(){
		List<TileLogic> validMoves = new List<TileLogic>();

		// Standard moves
		for (int row = Mathf.Max (0, currentTile.row - 1);
			row <= Mathf.Min (BoardLogic.Height - 1, currentTile.row + 1); row++) {
			for (int col = Mathf.Max (0, currentTile.column - 1);
				col <= Mathf.Min (BoardLogic.Width - 1, currentTile.column + 1); col++) {
				validMoves.Add (new TileLogic (row, col));
			}
		}

		// Castle
		// IMPORTANT: These moves are also checked in the Game level, to make sure they are valid.
		if (this.color == Game.SideColor.White && currentTile.row == 0) {
			validMoves.Add (new TileLogic (0, 6));
			validMoves.Add (new TileLogic (0, 2));
		}
		if (this.color == Game.SideColor.Black && currentTile.row == 7) {
			validMoves.Add (new TileLogic (7, 6));
			validMoves.Add (new TileLogic (7, 2));
		}

		return validMoves;
	}
}