using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFactory : MonoBehaviour {

	// Make this class Singelton
	public static PieceFactory Instance;
	void Awake(){
		Instance = this;
	}

	public static GameObject pawnWhitePrefab = Resources.Load("Prefabs/Pieces/White Pawn") as GameObject;
	public static GameObject rookWhitePrefab = Resources.Load("Prefabs/Pieces/White Rook") as GameObject;
	public static GameObject knightWhitePrefab = Resources.Load("Prefabs/Pieces/White Knight") as GameObject;
	public static GameObject bishopWhitePrefab = Resources.Load("Prefabs/Pieces/White Bishop") as GameObject;
	public static GameObject queenWhitePrefab = Resources.Load("Prefabs/Pieces/White Queen") as GameObject;
	public static GameObject kingWhitePrefab = Resources.Load("Prefabs/Pieces/White King") as GameObject;
	public static GameObject pawnBlackPrefab = Resources.Load("Prefabs/Pieces/Black Pawn") as GameObject;
	public static GameObject rookBlackPrefab = Resources.Load("Prefabs/Pieces/Black Rook") as GameObject;
	public static GameObject knightBlackPrefab = Resources.Load("Prefabs/Pieces/Black Knight") as GameObject;
	public static GameObject bishopBlackPrefab = Resources.Load("Prefabs/Pieces/Black Bishop") as GameObject;
	public static GameObject queenBlackPrefab = Resources.Load("Prefabs/Pieces/Black Queen") as GameObject;
	public static GameObject kingBlackPrefab = Resources.Load("Prefabs/Pieces/Black King") as GameObject;

	public static Piece CreatePiece(Game.PieceType pieceType, Game.SideColor sideColor,
		GameObject parent=null, PieceLogic pieceLogic=null){
		Piece ObjSelector = null;

		switch (pieceType){
		case Game.PieceType.King:
			if (sideColor == Game.SideColor.White) {
				ObjSelector = (Instantiate (kingWhitePrefab) as GameObject).GetComponent<Piece> ();
				ObjSelector.name = "king_white";
			} else {
				ObjSelector = (Instantiate (kingBlackPrefab) as GameObject).GetComponent<Piece> ();
				ObjSelector.name = "king_black";
			}
			break;
		case Game.PieceType.Queen:
			if (sideColor == Game.SideColor.White) {
				ObjSelector = (Instantiate (queenWhitePrefab) as GameObject).GetComponent<Piece> ();
				ObjSelector.name = "queen_white";
			} else {
				ObjSelector = (Instantiate (queenBlackPrefab) as GameObject).GetComponent<Piece> ();
				ObjSelector.name = "queen_black";
			}
			break;
		case Game.PieceType.Bishop:
			if (sideColor == Game.SideColor.White) {
				ObjSelector = (Instantiate (bishopWhitePrefab) as GameObject).GetComponent<Piece> ();
				ObjSelector.name = "bishop_white";
			} else {
				ObjSelector = (Instantiate (bishopBlackPrefab) as GameObject).GetComponent<Piece> ();
				ObjSelector.name = "bishop_black";
			}
			break;
		case Game.PieceType.Knight:
			if (sideColor == Game.SideColor.White) {
				ObjSelector = (Instantiate (knightWhitePrefab) as GameObject).GetComponent<Piece> ();
				ObjSelector.name = "knight_white";
			} else {
				ObjSelector = (Instantiate (knightBlackPrefab) as GameObject).GetComponent<Piece> ();
				ObjSelector.name = "knight_black";
			}
			break;
		case Game.PieceType.Rook:
			if (sideColor == Game.SideColor.White) {
				ObjSelector = (Instantiate (rookWhitePrefab) as GameObject).GetComponent<Piece> ();
				ObjSelector.name = "rook_white";
			} else {
				ObjSelector = (Instantiate (rookBlackPrefab) as GameObject).GetComponent<Piece> ();
				ObjSelector.name = "rook_black";
			}
			break;
		case Game.PieceType.Pawn:
			if (sideColor == Game.SideColor.White) {
				ObjSelector = (Instantiate (pawnWhitePrefab) as GameObject).GetComponent<Piece> ();
				ObjSelector.name = "pawn_white";
			} else {
				ObjSelector = (Instantiate (pawnBlackPrefab) as GameObject).GetComponent<Piece> ();
				ObjSelector.name = "pawn_black";
			}
			break;
		default:
			ObjSelector = new Piece();
			break;
		}

		if (parent != null) {
			ObjSelector.transform.SetParent (parent.transform, true);
		}
		if (pieceLogic != null) {
			ObjSelector.logic = pieceLogic;
			pieceLogic.currentTile.SetPiece (ObjSelector.logic);
		}
		return ObjSelector;
	}
}
