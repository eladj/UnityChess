using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public Board board;
	public PlayerLogic player1, player2;
	public SideColor currentTurn = SideColor.White;
	public bool isWhiteChecked = false;
	public bool isBlackChecked = false;

	// Public enumeration which can be accessed globally
	public enum TileType {White, Black};
	public enum SideColor {White, Black};
	public enum PieceType {King, Queen, Bishop, Knight, Rook, Pawn};

	private List<Move> moves = new List<Move>();	// List of all the moves

	public static Game Instance;

	void Awake(){
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void HandleMove(TileLogic origin, int row, int col){
		TileLogic destination = board.logic.GetTile (row, col);
		Move move = new Move (origin, destination);
		CheckMove (move);
	}

	private void CheckMove(Move move){
		Tile tile = board.GetTile (move.origin.row, move.origin.column);
		Piece piece = board.GetPiece (tile);

		// Check if we move piece out of bounds and got empty tile
		if (move.destination == null) {
			Debug.Log ("Out of bounds");
			// Find the piece which was on the origin tile and move it back
			piece.UpdatePosition ();
			return;
		}

		// Check if we didn't changed tile
		if (move.origin == move.destination) {
			Debug.Log ("Same tile");
			piece.UpdatePosition();
			return;
		}

		// Check if move is legal
		bool isLegal = IsMoveLegal (move);
		Debug.Log("Move: " + move.ToString());

		// If legal, update tile
		if (isLegal) {
			MakeMove (move);
		}

		// Update object position (If move was not legal, it returns to previous tile)
		piece.UpdatePosition();
	}

	private void MakeMove(Move move){
		Piece piece = board.GetPiece(move.origin);
		// Check if destination tile has opponent piece. If yes, remove it
		if (move.destination.HasPiece ()) {
			board.RemovePiece (move.destination);
		}

		// Special case of Pawn and EnPassant move (going diagonal to an empty tile)
		// Notice that we already checked this move is legal, so this is the only option.
		if (move.piece is Pawn && !move.destination.HasPiece () && move.origin.column != move.destination.column) {
			Debug.Log ("MakeMove: EnPassant");
			if (this.currentTurn == Game.SideColor.White) {
				board.RemovePiece (board.logic.GetTile (move.destination.row - 1, move.destination.column));
			} else {
				board.RemovePiece (board.logic.GetTile(move.destination.row + 1, move.destination.column));
			}
		}

		// Remove this piece reference from previous tile
		move.origin.SetPiece(null);

		// Update this piece tile to the new one
		piece.logic.currentTile = move.destination;

		// Check if Pawn reached last row
		if (piece.logic is Pawn) {
			if (piece.logic.color == Game.SideColor.White) {
				if (move.destination.row == BoardLogic.Height - 1) {
					Debug.Log("White Pawn Crowned!!!");
					// TODO - Something...
				}
			}
			if (piece.logic.color == Game.SideColor.Black) {
				if (move.destination.row == 0) {
					Debug.Log("Black Pawn Crowned!!!");
					// TODO - Something...
				}
			}
		}
		// Update that this piece has moved (for castle future check)
		piece.logic.hasMoved = true;
		// Update the tile with the new piece
		move.destination.SetPiece(piece.logic);

		// Add move to list
		moves.Add (move);

		// Advance turn
		this.AdvanceTurn();
	}

	// Check all scenarios where a move can be illegal:
	//  - Whos turn is it
	//  - The piece can do this kind of move
	//    - In case of Pawn, diagonal or En-passant are only legal in specific cases
	//  - If the destination has piece, that it is opponent piece
	//  - The king is not in check due to this move
	private bool IsMoveLegal(Move move){
		Piece piece = board.GetPiece(move.origin);

		// Check if this is this color turn
		if (piece.logic.color != this.currentTurn) {
			Debug.Log ("Illegal move: this is " + this.currentTurn.ToString() + " turn");
			return false;
		}

		// Check if the target tile is valid for this kind of piece
		List<TileLogic> validMovesForPiece = piece.logic.GetValidMoves ();
		bool contains = false;
		foreach (TileLogic t in validMovesForPiece){
			if (t.row == move.destination.row && t.column == move.destination.column) {
				contains = true;
				break;
			}
		}
		if (!contains){
			Debug.Log ("Illegal move: not a valid move for this kind of piece");
			return false;
		}

		// Special case of Pawn
		if (piece.logic is Pawn) {
			// Check if this is a diagonal move
			if (piece.logic.currentTile.column != move.destination.column) {
				// Check that destination tile is not empty
				if (move.destination.GetPiece() == null) {
					// Check private case of En passant 
					Move lastMove = moves [moves.Count - 1];
					if (!(lastMove.piece is Pawn && lastMove.destination.column == move.destination.column &&
						Mathf.Abs (lastMove.origin.row - lastMove.destination.row) == 2)) {
						Debug.Log ("Illegal move: Pawn cannot move diagonally to an empty tile (excluding EnPassant)");
						return false;
					} else {
						Debug.Log ("EnPassant !");
					}
				}
			}
		}

		// Check that the destination tile is not occupied with same color piece
		if (move.destination.GetPiece() != null) {
			if (move.destination.GetPiece ().color == this.currentTurn) {
				Debug.Log ("Illegal move: destination has piece with the same color");
				return false;
			}
		}
			
		// Check that the king is not exposed to check due to this move
		// For this we need to copy current board, make the move and check if it is valid
//			Board boardCopy = board.CopyBoard ();
//			Tile targetTileCopy = boardCopy.GetTile (targetTile.row, targetTile.column);
//			MakeMove (piece, targetTileCopy);
//			Tile kings_tile = boardCopy.GetKingTile (this.currentTurn);
//			Debug.Log ("Kings tile: " + kings_tile.name);
//
//			if (this.isChecked (kings_tile, this.currentTurn, boardCopy)) {
//				Debug.Log ("King is checked!");
//				return false;
//			} else {
//				return true;
//			}
//		}
		return true;
	}
		
	private void AdvanceTurn(){
		if (currentTurn == Game.SideColor.White) {
			currentTurn = Game.SideColor.Black;
		} else {
			currentTurn = Game.SideColor.White;
		}
	}
}
