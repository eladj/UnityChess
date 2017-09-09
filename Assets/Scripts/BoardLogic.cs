using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLogic {
	// Logic
	public static int Width = 8;
	public static int Height = 8;
	TileLogic[,] tiles;

	public BoardLogic(){
		tiles =  new TileLogic[Height, Width];
		for (var row = 0; row < BoardLogic.Height; row++) {
			for (var column = 0; column < BoardLogic.Width; column++) {
				tiles [row, column] = new TileLogic (row, column);
			}
		}
	}

	public void CreateNewPiece(Game.PieceType pieceType, Game.SideColor side, int row, int col){
		PieceLogic p = PieceLogicFactory.CreatePiece (pieceType, side);
		p.currentTile = tiles [row, col];
		tiles[row, col].SetPiece(p);
	}
		
	public void InitializePieces(){
		CreateNewPiece(Game.PieceType.King, Game.SideColor.White, 0, 4);
		CreateNewPiece(Game.PieceType.Queen, Game.SideColor.White, 0, 3);
		CreateNewPiece(Game.PieceType.Bishop, Game.SideColor.White, 0, 2);
		CreateNewPiece(Game.PieceType.Bishop, Game.SideColor.White, 0, 5);
		CreateNewPiece(Game.PieceType.Knight, Game.SideColor.White, 0, 1);
		CreateNewPiece(Game.PieceType.Knight, Game.SideColor.White, 0, 6);
		CreateNewPiece(Game.PieceType.Rook, Game.SideColor.White, 0, 0);
		CreateNewPiece(Game.PieceType.Rook, Game.SideColor.White, 0, 7);
		for (int col = 0; col < 8; col++) {
			CreateNewPiece(Game.PieceType.Pawn, Game.SideColor.White, 1, col);
		}
		CreateNewPiece(Game.PieceType.King, Game.SideColor.Black, 7, 4);
		CreateNewPiece(Game.PieceType.Queen, Game.SideColor.Black, 7, 3);
		CreateNewPiece(Game.PieceType.Bishop, Game.SideColor.Black, 7, 2);
		CreateNewPiece(Game.PieceType.Bishop, Game.SideColor.Black, 7, 5);
		CreateNewPiece(Game.PieceType.Knight, Game.SideColor.Black, 7, 1);
		CreateNewPiece(Game.PieceType.Knight, Game.SideColor.Black, 7, 6);
		CreateNewPiece(Game.PieceType.Rook, Game.SideColor.Black, 7, 0);
		CreateNewPiece(Game.PieceType.Rook, Game.SideColor.Black, 7, 7);
		for (int col = 0; col < 8; col++) {
			CreateNewPiece(Game.PieceType.Pawn, Game.SideColor.Black, 6, col);
		}
	}

	public TileLogic GetTile(int row, int column){
		// Debug.Log ("Get Tile: " + row.ToString () + "," + column.ToString ());
		if (row >= 0 && row < Width && column >= 0 && column < Height) {
			return tiles [row, column];
		} else {
			return null;
		}
	}

	// Get all pieces
	public List<PieceLogic> GetAllPieces(){
		List<PieceLogic> piecesList = new List<PieceLogic>();
		foreach (TileLogic tile in tiles){
			if (tile.HasPiece ()) {
				piecesList.Add (tile.GetPiece ());
			}
		}
		return piecesList;
	}

	// Get all pieces from specific color
	public List<PieceLogic> GetAllPiecesOfColor(Game.SideColor colorType){
		//		Debug.Log ("GetAllPiecesOfColor: " + colorType.ToString());
		List<PieceLogic> piecesList = new List<PieceLogic>();
		foreach (TileLogic tile in tiles){
			if (tile.HasPiece ()) {
				if (tile.GetPiece ().color == colorType) {
					piecesList.Add (tile.GetPiece ());
				}
			}
		}
		return piecesList;
	}

	public TileLogic GetKingTile(Game.SideColor sideColor){
		List<PieceLogic> pieces = GetAllPiecesOfColor(sideColor);
		foreach (PieceLogic p in pieces){
			if (p is King) {
				return p.currentTile;
			}
		}
		return null;
	}

}
