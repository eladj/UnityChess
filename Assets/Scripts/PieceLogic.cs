using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceLogic {

	public TileLogic currentTile;	// if null, that means that piece was taken and is not active any move
	public Game.PieceType type;
	public Game.SideColor color;
	public bool hasMoved = false;	// Useful for castle in case of King and Rook
	protected int boardWidth;
	protected int boardHeight;

	// Get all valid moves of the piece as a list of destination tiles
	public abstract List<TileLogic> GetValidMoves();

	public PieceLogic (Game.SideColor sideColor) {
		boardWidth = BoardLogic.Width;
		boardHeight = BoardLogic.Height;
		color = sideColor;
	}

	public bool IsActive(){
		return currentTile != null;
	}

	// Charcter name representation: K, Q, B, K, R, P
	public char NameChar(){
		char c = '\0';
		switch (type){
		case Game.PieceType.King:
			c = 'K';
			break;
		case Game.PieceType.Queen:
			c = 'Q';
			break;
		case Game.PieceType.Bishop:
			c = 'B';
			break;
		case Game.PieceType.Knight:
			c = 'N';
			break;
		case Game.PieceType.Rook:
			c = 'R';
			break;
		case Game.PieceType.Pawn:
			c = 'P';
			break;
		}
		return c;
	}
}
