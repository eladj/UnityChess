using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour {

	public Tile currentTile;
	public ColorType color;
	public bool visible = true;
	protected Board board;	// Reference to Board GameObject
	protected char cname;	// Charcter name representation

	public enum ColorType {White, Black};

	// Is specific move legal
	public bool IsMoveLegal(Tile targetTile){
		List<Tile> validMoves = GetValidMoves();
		if (validMoves.Contains(targetTile)) {
			return true;
		} else {
			return false;
		}
	}

	// Get all valid moves of the piece as a list of destination tiles
	public abstract List<Tile> GetValidMoves();

	protected virtual void Start () {
		// Get the position of the tile
		transform.position = currentTile.transform.position;

		board = GameObject.FindWithTag ("Board").GetComponent<Board>();
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
		Tile tile = board.GetTile (row, column);

		// Check if we move piece out of bounds and got empty tile
		if (tile == null) {
			Debug.Log ("Out of bounds");
			transform.position = currentTile.transform.position;
			return;
		}

		// Check if we didn't changed tile
		if (tile == this.currentTile) {
			Debug.Log ("Same tile");
			transform.position = currentTile.transform.position;
			return;
		}

		// Check if move is legal
		bool isLegal = this.IsMoveLegal (tile);
		Debug.Log("Move: " + this.cname + this.currentTile.name + "-"+ tile.name);

		// If legal, update tile
		if (isLegal) {
			// Rmove piece from previous tile
			currentTile.SetPiece(null);
			// Update piece tile to the new one
			currentTile = tile;
			// Update the tile with the new piece
			tile.SetPiece(this);
		} else {
			Debug.Log ("Illegal Move");
		}

		// Update object position (If move was not legal, it returns to previous tile)
		transform.position = currentTile.transform.position;
	}
}

