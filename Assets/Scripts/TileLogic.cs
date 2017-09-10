using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic {
	public int row;
	public int column;
	private PieceLogic piece = null;	// Which piece is on the tile

//	public TileLogic(){
//		row = 0;
//		column = 0;
//	}

	public TileLogic(int _row, int _col){
		row = _row;
		column = _col;
	}

	public TileLogic Copy(){
		TileLogic tileCopy = new TileLogic (row, column);
		if (piece != null) {
			tileCopy.piece = piece.Copy(tileCopy);
		}
		return tileCopy;
	}

	public Game.TileType GetTileType(){
		// Determine the tile color based on the position. If the column and
		// the row is even or the column and the row is odd, it must be white.
		Game.TileType res = (column % 2 == 0 && row % 2 == 0 || column % 2 == 1 && row % 2 == 1) ? Game.TileType.Black : Game.TileType.White;
		return res;
	}

	public bool IsInBoard(){
		if (row >= 0 && row < BoardLogic.Height && column >= 0 && column < BoardLogic.Width) {
			return true;
		} else {
			return false;
		}
	}

	public bool HasPiece(){
		if (piece == null) {
			return false;
		} else {
			return true;
		}
	}

	public bool HasPieceOfType(Game.PieceType pieceType, Game.SideColor pieceColor){
		if (piece != null) {
			if (piece.type == pieceType && piece.color == pieceColor) {
				return true;
			}
		} 
		return false;
	}

	public void SetPiece(PieceLogic newPiece){
		piece = newPiece;
	}

	public PieceLogic GetPiece(){
		return piece;
	}

	public string Name(){
		int c = 'a' + (int)column;
		return (((char)c).ToString() + (row + 1).ToString());
	}
}
