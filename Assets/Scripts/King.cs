using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece {
	
	protected override void Start (){
		base.Start ();
		cname = 'K';
	}

//	public override bool IsMoveLegal(Tile targetTile){
//		List<Tile> validMoves = GetValidMoves();
//		if (validMoves.Contains(targetTile)) {
//			return true;
//		} else {
//			return false;
//		}
//	}

	public override List<Tile> GetValidMoves(){
		List<Tile> validMoves = new List<Tile>();

		for (int row = Mathf.Max (0, currentTile.row - 1);
			row <= Mathf.Min (Board.Height - 1, currentTile.row + 1); row++) {
			for (int col = Mathf.Max (0, currentTile.column - 1);
				col <= Mathf.Min (Board.Width - 1, currentTile.column + 1); col++) {
				Tile tile = board.GetTile (row, col);
				if (!tile.HasPiece()) {  //TODO: Also check if tile is not checked
					validMoves.Add (tile);
				} else if (tile.GetPiece().color != this.color){
					validMoves.Add (tile);
				}					
			}
		}
		return validMoves;
	}
}