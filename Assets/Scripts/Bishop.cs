using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece {
	protected override void Start (){
		base.Start ();
		cname = 'B';
	}

	public override List<Tile> GetValidMoves(){
		List<Tile> validMoves = new List<Tile>();

		// Up-Left
		for (int row = currentTile.row + 1, col = currentTile.column - 1;
			row < Board.Height && col >= 0; row++, col--) {
			Tile tile = board.GetTile (row, col);
			if (tile.HasPiece()) {
				if (tile.GetPiece ().color != this.color) {
					validMoves.Add (tile);
					break;
				} else {
					break;
				}
			} else {  // Empty tile
				validMoves.Add (tile);
			}	
		}

		// Up-Right
		for (int row = currentTile.row + 1, col = currentTile.column + 1;
			row < Board.Height && col < Board.Width; row++, col++) {
			Tile tile = board.GetTile (row, col);
			if (tile.HasPiece()) {
				if (tile.GetPiece ().color != this.color) {
					validMoves.Add (tile);
					break;
				} else {
					break;
				}
			} else {  // Empty tile
				validMoves.Add (tile);
			}	
		}

		// Down-Left
		for (int row = currentTile.row - 1, col = currentTile.column - 1;
			row >= 0 && col >= 0; row--, col--) {
			Tile tile = board.GetTile (row, col);
			if (tile.HasPiece()) {
				if (tile.GetPiece ().color != this.color) {
					validMoves.Add (tile);
					break;
				} else {
					break;
				}
			} else {  // Empty tile
				validMoves.Add (tile);
			}	
		}

		// Down-Right
		for (int row = currentTile.row - 1, col = currentTile.column + 1;
			row >= 0 && col < Board.Width; row--, col++) {
			Tile tile = board.GetTile (row, col);
			if (tile.HasPiece()) {
				if (tile.GetPiece ().color != this.color) {
					validMoves.Add (tile);
					break;
				} else {
					break;
				}
			} else {  // Empty tile
				validMoves.Add (tile);
			}	
		}

		return validMoves;
	}
}
