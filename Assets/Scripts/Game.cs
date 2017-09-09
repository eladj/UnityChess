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
	public enum MoveType {Regular, CastleShort, CastleLong, Promote};

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

		// Check if we move piece out of bounds and got empty tile
		if (destination == null) {
			// Find the piece which was on the origin tile and move it back
			Debug.Log ("Out of bounds");
			board.GetPiece(origin).UpdatePosition ();
			return;
		}

		Move move = new Move (origin, destination);
		Debug.Log ("Handling a move: " + move.ToString());
		ProcessMove (move);
	}

	private void ProcessMove(Move move){
		Tile tile = board.GetTile (move.origin.row, move.origin.column);
		Piece piece = board.GetPiece (tile);

		// Check if we didn't changed tile
		if (move.origin == move.destination) {
			Debug.Log ("Same tile");
			piece.UpdatePosition();
			return;
		}

		// Check if move is legal
		bool isLegal = IsMoveLegal (move);

		// If legal, update tile
		if (isLegal) {
			MakeMove (move);
		}

		if (move.type != Game.MoveType.Promote) {
			// Update object position (If move was not legal, it returns to previous tile)
			piece.UpdatePosition ();
		}
	}

	// Handles making a move
	private void MakeMove(Move move, BoardLogic _boardLogic=null, bool updateMainGame=true){
		if (_boardLogic == null) {
			_boardLogic = board.logic;
		}
//		PieceLogic piece = _board.GetPiece(move.origin);
		PieceLogic piece = move.piece;

		// Special case of Pawn and EnPassant move (going diagonal to an empty tile)
		// Notice that we already checked this move is legal, so this is the only option.
		if (move.piece is Pawn && !move.destination.HasPiece () && move.origin.column != move.destination.column) {
			Debug.Log ("MakeMove: EnPassant");
			if (this.currentTurn == Game.SideColor.White) {
				if (updateMainGame) {
					board.RemovePiece (_boardLogic.GetTile (move.destination.row - 1, move.destination.column));
				} else {
					_boardLogic.RemovePiece (_boardLogic.GetTile (move.destination.row - 1, move.destination.column));
				}
			} else {
				if (updateMainGame) {
					board.RemovePiece (_boardLogic.GetTile (move.destination.row + 1, move.destination.column));
				} else {
					_boardLogic.RemovePiece (_boardLogic.GetTile (move.destination.row + 1, move.destination.column));
				}
			}
		}

		// Check if destination tile has opponent piece. If yes, remove it
		if (move.destination.HasPiece ()) {
			if (updateMainGame) {
				board.RemovePiece (move.destination);
			} else {
				_boardLogic.RemovePiece (move.destination);
			}
		}
			
//		// Remove this piece reference from previous tile
//		move.origin.SetPiece(null);
//
//		// Update this piece tile to the new one
//		piece.currentTile = move.destination;
//
//		// Update that this piece has moved (for castle future check)
//		piece.hasMoved = true;
//		// Update the tile with the new piece
//		move.destination.SetPiece(piece);
		_boardLogic.MovePiece(move);

		// Check if Pawn reached last row and promote it
		if (piece is Pawn) {
			if ((piece.color == Game.SideColor.White && move.destination.row == BoardLogic.Height - 1) ||
				(piece.color == Game.SideColor.Black && move.destination.row == 0)){
				Debug.Log("Pawn reached the end!!!");
				move.piecePromotedType = Game.PieceType.Queen;
				move.type = Game.MoveType.Promote;
				// Remove pawn and add new piece
				if (updateMainGame) {
					board.RemovePiece (move.destination);
					board.AddPiece (move.piecePromotedType, move.color, move.destination.row, move.destination.column);
				} else {
					_boardLogic.RemovePiece (move.origin);
					_boardLogic.CreateNewPiece (move.piecePromotedType, move.color, move.destination.row, move.destination.column);
				}
			}
		}

		if (move.type == Game.MoveType.CastleShort || move.type == Game.MoveType.CastleLong) {
			Move moveRook = null;
			if (move.type == Game.MoveType.CastleShort) {
				moveRook = new Move (_boardLogic.GetTile (move.destination.row, move.destination.column + 1),
					_boardLogic.GetTile (move.destination.row, move.destination.column - 1));
			} else if (move.type == Game.MoveType.CastleLong){
				moveRook = new Move (_boardLogic.GetTile (move.destination.row, move.destination.column - 2),
					_boardLogic.GetTile (move.destination.row, move.destination.column + 1));
			}
			if (updateMainGame) {
				board.MovePiece (moveRook);
			} else {
				_boardLogic.MovePiece (moveRook);
			}
		}

		if (move.type == Game.MoveType.CastleLong) {
		}

		if (updateMainGame) {
			// Add move to list (if we are not working on copy of board)
			moves.Add (move);

			// Advance turn
			this.AdvanceTurn();
		}
	}

	// Check all scenarios where a move can be illegal:
	//  - Whos turn is it
	//  - The piece can do this kind of move
	//    - In case of Pawn, Rook, Bishop and queen: We don't pass through other pieces
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

		// Check that we dont go through pieces
		Debug.Log ("Route Check");
		List<TileLogic> route = move.GetRoute();
		foreach (TileLogic t in route) {
			Debug.Log (t.Name());
			if (board.logic.GetTile(t.row, t.column).HasPiece()) {
				Debug.Log ("Illegal move: Cannot move through pieces");
				return false;
			}
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
			
		// Check that the king is not exposed to check after this move
		// For this we need to copy current board, make the move and check if it is valid
		BoardLogic boardCopy = board.logic.Copy();
		Move moveCopy = new Move (boardCopy.GetTile(move.origin.row, move.origin.column),
			boardCopy.GetTile(move.destination.row, move.destination.column), false);
		MakeMove (moveCopy, boardCopy, false);
		TileLogic kingTile = boardCopy.GetKingTile (this.currentTurn);
		Debug.Log ("Kings tile: " + kingTile.Name());
		if (boardCopy.isTileChecked (kingTile, this.currentTurn)) {
			Debug.Log ("Illegal move: " + this.currentTurn.ToString() + " King is checked");
			return false;
		}

		// Check castle
		if (piece.logic is King) {
			if (move.type == Game.MoveType.CastleShort || move.type == Game.MoveType.CastleLong){
				// Check the king wasn't threatend
				if (board.logic.isTileChecked (move.origin, move.color)) {
					Debug.Log ("Illegal move: Cannot castle while checked");
					return false;
				}

				// Check we didn't pass through check
				TileLogic t = move.GetRoute()[0];
				if (board.logic.isTileChecked (t, move.color)) {
					Debug.Log ("Illegal move: Cannot castle while passing through check");
					return false;
				}

				// Check the king didn't move from the start of game
				if (piece.logic.hasMoved) {
					Debug.Log ("Illegal move: Cannot castle after the king has moved");
					return false;
				}

				// Check the relevant rook didn't moved
				bool flagRookMoved = false;
				if (move.type == Game.MoveType.CastleShort) {
					PieceLogic p = board.logic.GetPiece (move.destination.row, move.destination.column + 1);
					if (p == null) {
						flagRookMoved = true;
					} else {
						if (p.type == Game.PieceType.Rook && p.hasMoved == true) {
							flagRookMoved = true;
						}
						if (p.type != Game.PieceType.Rook) {
							flagRookMoved = true;
						}
					}
				}
				if (move.type == Game.MoveType.CastleLong) {
					PieceLogic p = board.logic.GetPiece (move.destination.row, move.destination.column - 2);
					if (p == null) {
						flagRookMoved = true;
					} else {
						if (p.type == Game.PieceType.Rook && p.hasMoved == true) {
							flagRookMoved = true;
						}
						if (p.type != Game.PieceType.Rook) {
							flagRookMoved = true;
						}
					}
				}
				if (flagRookMoved) {
					Debug.Log ("Illegal move: Cannot castle after the rook has moved");
					return false;
				}
			}
		}

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
