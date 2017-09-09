using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceLogicFactory {

	public static PieceLogic CreatePiece(Game.PieceType pieceType, Game.SideColor sideColor){

		PieceLogic ObjSelector = null;

		switch (pieceType){
		case Game.PieceType.King:
			ObjSelector = new King(sideColor);
			break;
		case Game.PieceType.Queen:
			ObjSelector = new Queen(sideColor);
			break;
		case Game.PieceType.Bishop:
			ObjSelector = new Bishop(sideColor);
			break;
		case Game.PieceType.Knight:
			ObjSelector = new Knight(sideColor);
			break;
		case Game.PieceType.Rook:
			ObjSelector = new Rook(sideColor);
			break;
		case Game.PieceType.Pawn:
			ObjSelector = new Pawn(sideColor);
			break;
		}
		return ObjSelector;
	}
}
