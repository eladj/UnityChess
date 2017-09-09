using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic {
	public int row;
	public int column;
	protected int boardWidth;
	protected int boardHeight;
	private PieceLogic piece = null;	// Which piece is on the tile

//	public TileLogic(){
//		row = 0;
//		column = 0;
//	}

	public TileLogic(int _row, int _col, PieceLogic piece=null){
		row = _row;
		column = _col;
		boardWidth = BoardLogic.Width;
		boardHeight = BoardLogic.Height;
	}

	public Game.TileType GetTileType(){
		// Determine the tile color based on the position. If the column and
		// the row is even or the column and the row is odd, it must be white.
		Game.TileType res = (column % 2 == 0 && row % 2 == 0 || column % 2 == 1 && row % 2 == 1) ? Game.TileType.Black : Game.TileType.White;
		return res;
	}

	public bool IsInBoard(){
		if (row > 0 && row < boardHeight && column > 0 && column < boardWidth) {
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
