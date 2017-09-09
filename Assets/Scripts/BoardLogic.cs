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

	// Copy board while creating copies of tiles and pieces
	// Useful for checking check on next move
	public BoardLogic Copy(){
		BoardLogic boardCopy = new BoardLogic();
		for (var row = 0; row < BoardLogic.Height; row++) {
			for (var column = 0; column < BoardLogic.Width; column++) {
				boardCopy.tiles [row, column] = tiles[row, column].Copy();
			}
		}
		return boardCopy;
	}

	// Get piece from row and column
	public PieceLogic GetPiece(int row, int column){
		return tiles [row, column].GetPiece ();
	}

	// Get piece from tile
	public PieceLogic GetPiece(TileLogic tileLogic){
		return tiles [tileLogic.row, tileLogic.column].GetPiece ();
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

	// Is tile has the piece type and color
	public bool isTileHasPiece(int row, int column, Game.PieceType pieceType, Game.SideColor pieceColor){
		TileLogic tile = GetTile (row, column);
		if (tile != null) {
			if (tile.HasPieceOfType(pieceType, pieceColor)){
				return true;
			}
		}
		return false;
	}

	// Is tile has any of the pieces type in the list and in the correct color
	public bool isTileHasPiece(int row, int column, List<Game.PieceType> pieceTypeList, Game.SideColor pieceColor){
		TileLogic tile = GetTile (row, column);
		if (tile != null) {
			foreach (Game.PieceType p in pieceTypeList) {
				if (tile.HasPieceOfType(p, pieceColor)){
					return true;
				}
			}
		}
		return false;
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

	// Check if a given tile is threatened by an opponent
	public bool isTileChecked(TileLogic tile, Game.SideColor sideColor){
		// Get opponenet color
		Game.SideColor opponenetColor;
		if (sideColor == Game.SideColor.White) {
			opponenetColor = Game.SideColor.Black;
		} else {
			opponenetColor = Game.SideColor.White;
		}

		// Go over all opponenet pieces.
		// Check if the given tile exist in one of their valid moves.
		TileLogic t;
		PieceLogic p;
		List<Game.PieceType> straightPieces = new List<Game.PieceType>{Game.PieceType.Queen, Game.PieceType.Rook};
		List<Game.PieceType> diagonalPieces = new List<Game.PieceType>{Game.PieceType.Queen, Game.PieceType.Bishop};

		// We go to all directions and check what is the first piece we see
		// If it is opponent and can move to this tile, we return true

		// Up
		for (int row = tile.row + 1; row < BoardLogic.Height; row++) {
			t = GetTile (row, tile.column);
			if (t.HasPiece()) {
				p = t.GetPiece ();
				if (straightPieces.Contains(p.type) && p.color == opponenetColor){
					return true;
				}
				break;
			}
		}

		// Down
		for (int row = tile.row - 1; row >= 0; row--) {
			t = GetTile (row, tile.column);
			if (t.HasPiece()) {
				p = t.GetPiece ();
				if (straightPieces.Contains(p.type) && p.color == opponenetColor){
					return true;
				}
				break;
			}
		}

		// Right
		for (int col = tile.column + 1; col < BoardLogic.Width; col++) {
			t = GetTile (tile.row, col);
			if (t.HasPiece()) {
				p = t.GetPiece ();
				if (straightPieces.Contains(p.type) && p.color == opponenetColor){
					return true;
				}
				break;
			}
		}

		// Left
		for (int col = tile.column - 1; col >= 0; col--) {
			t = GetTile (tile.row, col);
			if (t.HasPiece()) {
				p = t.GetPiece ();
				if (straightPieces.Contains(p.type) && p.color == opponenetColor){
					return true;
				}
				break;
			}
		}

		// Diagonal up-right
		for (int row = tile.row + 1, col = tile.column + 1; row < BoardLogic.Height && col < BoardLogic.Width; row++, col++) {
			t = GetTile (row, col);
			if (t.HasPiece()) {
				p = t.GetPiece ();
				if (diagonalPieces.Contains(p.type) && p.color == opponenetColor){
					return true;
				}
				break;
			}
		}

		// Diagonal up-left
		for (int row = tile.row + 1, col = tile.column - 1; row < BoardLogic.Height && col >= 0; row++, col--) {
			t = GetTile (row, col);
			if (t.HasPiece()) {
				p = t.GetPiece ();
				if (diagonalPieces.Contains(p.type) && p.color == opponenetColor){
					return true;
				}
				break;
			}
		}

		// Diagonal down-right
		for (int row = tile.row - 1, col = tile.column + 1; row >= 0 && col < BoardLogic.Width; row--, col++) {
			t = GetTile (row, col);
			if (t.HasPiece()) {
				p = t.GetPiece ();
				if (diagonalPieces.Contains(p.type) && p.color == opponenetColor){
					return true;
				}
				break;
			}
		}

		// Diagonal down-left
		for (int row = tile.row - 1, col = tile.column - 1; row >= 0 && col >= 0; row--, col--) {
			t = GetTile (row, col);
			if (t.HasPiece()) {
				p = t.GetPiece ();
				if (diagonalPieces.Contains(p.type) && p.color == opponenetColor){
					return true;
				}
				break;
			}
		}

		// Check Pawn
		if (opponenetColor == Game.SideColor.Black) {
			if (isTileHasPiece(tile.row + 1, tile.column + 1, Game.PieceType.Pawn, Game.SideColor.Black)) return true;
			if (isTileHasPiece(tile.row + 1, tile.column - 1, Game.PieceType.Pawn, Game.SideColor.Black)) return true;
		}
		if (opponenetColor == Game.SideColor.White) {
			if (isTileHasPiece(tile.row - 1, tile.column + 1, Game.PieceType.Pawn, Game.SideColor.White)) return true;
			if (isTileHasPiece(tile.row - 1, tile.column - 1, Game.PieceType.Pawn, Game.SideColor.White)) return true;
		}

		// Check knight moves
		if (isTileHasPiece(tile.row + 2, tile.column + 1, Game.PieceType.Knight, opponenetColor)) return true;
		if (isTileHasPiece(tile.row + 2, tile.column - 1, Game.PieceType.Knight, opponenetColor)) return true;
		if (isTileHasPiece(tile.row + 1, tile.column + 2, Game.PieceType.Knight, opponenetColor)) return true;
		if (isTileHasPiece(tile.row + 1, tile.column - 2, Game.PieceType.Knight, opponenetColor)) return true;
		if (isTileHasPiece(tile.row - 2, tile.column + 1, Game.PieceType.Knight, opponenetColor)) return true;
		if (isTileHasPiece(tile.row - 2, tile.column - 1, Game.PieceType.Knight, opponenetColor)) return true;
		if (isTileHasPiece(tile.row - 1, tile.column + 2, Game.PieceType.Knight, opponenetColor)) return true;
		if (isTileHasPiece(tile.row - 1, tile.column - 2, Game.PieceType.Knight, opponenetColor)) return true;

		// Check King
		if (GetKingTile (opponenetColor).GetPiece ().GetValidMoves ().Contains (tile)) {
			return true;
		}

		return false;
	}

	// Remove a piece (only logical representation of piece - not the GameObject)
	public void RemovePiece(TileLogic tile){
		if (tile.HasPiece ()) {
			PieceLogic p = GetPiece(tile);
			tile.SetPiece (null);
			p.currentTile = null;
		}
	}
		
	// Move piece from origin to destination
	public void MovePiece(Move move){
		// Remove this piece reference from previous tile
		move.origin.SetPiece(null);

		// Update this piece tile to the new one
		move.piece.currentTile = move.destination;

		// Update that this piece has moved (for castle future check)
		move.piece.hasMoved = true;

		// Update the tile with the new piece
		move.destination.SetPiece(move.piece);
	}
}
