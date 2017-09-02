using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece {
	protected override void Start (){
		base.Start ();
		cname = 'R';
	}

	public override List<Tile> GetValidMoves(){
		List<Tile> validMoves = new List<Tile>();

		// Down
		for (int row = currentTile.row - 1; row >= 0; row--) {
			Tile tile = board.GetTile (row, currentTile.column);
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

		// Up
		for (int row = currentTile.row + 1; row < Board.Height; row++) {
			Tile tile = board.GetTile (row, currentTile.column);
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

		// Left
		for (int col = currentTile.column - 1; col >= 0; col--) {
			Tile tile = board.GetTile (currentTile.row, col);
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

		// Right
		for (int col = currentTile.column + 1; col < Board.Width; col++) {
			Tile tile = board.GetTile (currentTile.row, col);
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
