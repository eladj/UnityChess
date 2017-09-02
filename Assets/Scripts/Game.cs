using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public Board board;
	public Player player1, player2;

	public SideColor currentTurn = SideColor.White;
	public bool whiteChecked = false;
	public bool blackChecked = false;

	public enum SideColor {White, Black};

	public void AdvanceTurn(){
		if (currentTurn == Game.SideColor.White) {
			currentTurn = Game.SideColor.Black;
		} else {
			currentTurn = Game.SideColor.White;
		}
	}

	// Check if a given tile is threatened by opponent
	public bool isChecked(Tile tile, Game.SideColor sideColor){
		// Get opponenet color
		Game.SideColor opponenetColor;
		if (sideColor == Game.SideColor.White) {
			opponenetColor = Game.SideColor.Black;
		} else {
			opponenetColor = Game.SideColor.White;
		}

		// Go over all opponenet pieces.
		// Check if the given tile exist in one of their valid moves.
		List<Piece> opponenetPieces = board.GetAllPiecesOfColor (opponenetColor);
		foreach (Piece piece in opponenetPieces){
			List<Tile> validMoves = piece.GetValidMoves ();
			if (validMoves.Contains (tile)) {
				return true;
			}
		}
		return false;
	}
}
