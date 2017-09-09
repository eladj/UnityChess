using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic {

//	public BoardLogic boardLogic;

//	public SideColor currentTurn = SideColor.White;
//	public bool whiteChecked = false;
//	public bool blackChecked = false;
//
//	public enum TileType {White, Black};
//	public enum SideColor {White, Black};
//	public enum PieceType {King, Queen, Bishop, Knight, Rook, Pawn};

//	public void AdvanceTurn(){
//		if (currentTurn == Game.SideColor.White) {
//			currentTurn = Game.SideColor.Black;
//		} else {
//			currentTurn = Game.SideColor.White;
//		}
//	}

//	public void HandleMove(Tile tileOrigin, Tile tileDestination){
//		PieceLogic piece = tileOrigin.logicGetPiece ();
//
//		// Check if we move piece out of bounds and got empty tile
//		if (tileDestination == null) {
//			Debug.Log ("Out of bounds");
//			piece.UpdatePosition();
//			return;
//		}
//
//		// Check if we didn't changed tile
//		if (tileDestination == tileOrigin) {
//			Debug.Log ("Same tile");
//			piece.UpdatePosition();
//			return;
//		}
//
//		// Check if move is legal
//		bool isLegal = IsMoveLegal (piece, tileDestination);
//		Debug.Log("Move: " + piece.name + tileOrigin.name + "-"+ tileDestination.name);
//
//		// If legal, update tile
//		if (isLegal) {
//			MakeMove (piece, tileDestination);
//		} else {
//			Debug.Log ("Illegal Move");
//		}
//
//		// Update object position (If move was not legal, it returns to previous tile)
//		piece.UpdatePosition();
//	}
//
//	// Check if specific move is legal
//	public bool IsMoveLegal(Piece piece, Tile targetTile){
//		// Check if this is this color turn
//		if (piece.color == this.currentTurn) {
//			// Check if the target tile is valid
//			List<Tile> validMovesForPiece = piece.GetValidMoves ();
//			if (validMovesForPiece.Contains (targetTile)) {
//				// Check that the king is not exposed to check due to this move
//				// For this we need to copy current board, make the move and check if it is valid
//				Board boardCopy = board.CopyBoard();
//				Tile targetTileCopy = boardCopy.GetTile (targetTile.row, targetTile.column);
//				MakeMove (piece, targetTileCopy);
//				Tile kings_tile = boardCopy.GetKingTile(this.currentTurn);
//				Debug.Log ("Kings tile: " + kings_tile.name);
//
//				if (this.isChecked (kings_tile, this.currentTurn, boardCopy)) {
//					Debug.Log ("King is checked!");
//					return false;
//				} else {
//					return true;
//				}
//			}
//		}
//		return false;
//	}
//
//	// If we had done a legal move, this function handles the move
//	public void MakeMove(Piece piece, Tile tileDestination){
//		// Check if destination tile has opponent piece. If yes, remove it
//		if (tileDestination.HasPiece ()) {
//			tileDestination.DestroyPiece ();
//		}
//		// Remove this piece from previous tile
//		piece.currentTile.SetPiece(null);
//		// Update this piece tile to the new one
//		piece.currentTile = tileDestination;
//		// Check if Pawn reached last row
//		if (piece is Pawn) {
//			if (piece.color == Game.SideColor.White) {
//				if (tileDestination.row == Board.Height - 1) {
//					Debug.Log("White Pawn Crowned!!!");
//					// TODO - Something...
//				}
//			}
//			if (piece.color == Game.SideColor.Black) {
//				if (tileDestination.row == 0) {
//					Debug.Log("Black Pawn Crowned!!!");
//					// TODO - Something...
//				}
//			}
//		}
//		// Update that this piece has moved (for castle future check)
//		piece.hasMoved = true;
//		// Update the tile with the new piece
//		tileDestination.SetPiece(piece);
//		// Advance turn
//		this.AdvanceTurn();
//	}
//
//	// Check if a given tile is threatened by opponent
//	public bool isChecked(Tile tile, Game.SideColor sideColor, Board board){
//		// Get opponenet color
//		Game.SideColor opponenetColor;
//		if (sideColor == Game.SideColor.White) {
//			opponenetColor = Game.SideColor.Black;
//		} else {
//			opponenetColor = Game.SideColor.White;
//		}
//
//		// Go over all opponenet pieces.
//		// Check if the given tile exist in one of their valid moves.
//		List<Piece> opponenetPieces = board.GetAllPiecesOfColor (opponenetColor);
//		foreach (Piece piece in opponenetPieces){
//			List<Tile> validMoves = piece.GetValidMoves ();
//			if (validMoves.Contains (tile)) {
//				return true;
//			}
//		}
//		return false;
//	}
}
