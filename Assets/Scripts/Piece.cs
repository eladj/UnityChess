using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour {

	public Tile currentTile;
	public Game.SideColor color;
	public bool hasMoved = false;	// Useful for castle in case of King and Rook
	public bool visible = true;
	protected Board board;	// Reference to Board GameObject
	protected Game game;	// Reference to Board GameObject
	protected char cname;	// Charcter name representation

//	public enum PieceColor {White, Black};

	// Is specific move legal
	public bool IsMoveLegal(Tile targetTile){
		// Check if this is this color turn
		if (this.color == game.currentTurn) {
			// Check if the target tile is valid
			List<Tile> validMoves = GetValidMoves ();
			if (validMoves.Contains (targetTile)) {
				// Check that the king is not exposed to check due to this move
				// TODO - We need to check given that the move has already been done....current is mistake
				Tile kings_tile = board.GetKingTile(game.currentTurn);
				Debug.Log ("Kings tile: " + kings_tile.name);

				if (game.isChecked (kings_tile, game.currentTurn)) {
					Debug.Log ("King is checked!");
					return false;
				} else {
					return true;
				}
			}
		}
		return false;
	}

	// Get all valid moves of the piece as a list of destination tiles
	public abstract List<Tile> GetValidMoves();

	protected virtual void Start () {
		// Get the position of the tile
		transform.position = currentTile.transform.position;

		board = GameObject.FindWithTag ("Board").GetComponent<Board>();
		game = GameObject.FindWithTag ("Game").GetComponent<Game>();
	}

	// If we had done a legal move, this function handles the move
	private void ProcessMove(Tile tileDestination){
		// Check if destination tile has opponent piece. If yes, remove it
		if (tileDestination.HasPiece ()) {
			tileDestination.DestroyPiece ();
		}
		// Remove this piece from previous tile
		currentTile.SetPiece(null);
		// Update this piece tile to the new one
		currentTile = tileDestination;
		// Check if Pawn reached last row
		if (this is Pawn) {
			if (this.color == Game.SideColor.White) {
				if (tileDestination.row == Board.Height - 1) {
					Debug.Log("White Pawn Crowned!!!");
					// TODO - Something...
				}
			}
			if (this.color == Game.SideColor.Black) {
				if (tileDestination.row == 0) {
					Debug.Log("Black Pawn Crowned!!!");
					// TODO - Something...
				}
			}
		}
		// Update that this piece has moved (for castle future check)
		this.hasMoved = true;
		// Update the tile with the new piece
		tileDestination.SetPiece(this);
		// Advance turn
		game.AdvanceTurn();
	}

	private float drag_z_height = 9.8f;
	private Vector3 distance;

	void OnMouseDown()
	{
		distance = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,Camera.main.WorldToScreenPoint(transform.position).z)) - transform.position;
	}

	void OnMouseDrag()
	{
		Vector3 distance_to_screen = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen.z ));
		transform.position = new Vector3( pos_move.x - distance.x , pos_move.y - distance.y, drag_z_height );
	}

	void OnMouseUp(){
		// Get destination tile
		int row = Mathf.RoundToInt (transform.position.y);
		int column = Mathf.RoundToInt (transform.position.x);
		Board board = GameObject.FindWithTag ("Board").GetComponent<Board>();//gameObject.GetComponentsInParent<Board> ();
		Tile tileDestination = board.GetTile (row, column);

		// Check if we move piece out of bounds and got empty tile
		if (tileDestination == null) {
			Debug.Log ("Out of bounds");
			transform.position = currentTile.transform.position;
			return;
		}

		// Check if we didn't changed tile
		if (tileDestination == this.currentTile) {
			Debug.Log ("Same tile");
			transform.position = currentTile.transform.position;
			return;
		}

		// Check if move is legal
		bool isLegal = this.IsMoveLegal (tileDestination);
		Debug.Log("Move: " + this.cname + this.currentTile.name + "-"+ tileDestination.name);

		// If legal, update tile
		if (isLegal) {
			ProcessMove (tileDestination);
		} else {
			Debug.Log ("Illegal Move");
		}

		// Update object position (If move was not legal, it returns to previous tile)
		transform.position = currentTile.transform.position;
	}
}

