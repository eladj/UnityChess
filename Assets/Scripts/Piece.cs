using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

	public PieceLogic logic;
	protected Game game;	// Reference to Game GameObject - to handle moves
	private float drag_z_height = Board.z_height - 0.2f;	// The height where we drag the piece
	private Vector3 distance;	// utility variable to calculate the distance we drag the piece

	protected void Start () {
		// Get the position of the tile
		transform.position = Tile.GetPositionFromTileLogic(logic.currentTile);
		game = GameObject.FindWithTag ("Game").GetComponent<Game>();
	}

	// Update piece position according to tile location
	public void UpdatePosition(){
		transform.position = Tile.GetPositionFromTileLogic(logic.currentTile);
	}

	void OnMouseDown(){
		distance = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,Camera.main.WorldToScreenPoint(transform.position).z)) - transform.position;
	}

	void OnMouseDrag(){
		Vector3 distance_to_screen = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen.z ));
		transform.position = new Vector3( pos_move.x - distance.x , pos_move.y - distance.y, drag_z_height );
	}

	void OnMouseUp(){
		// Get destination tile
		int row = Mathf.RoundToInt (transform.position.y);
		int column = Mathf.RoundToInt (transform.position.x);
		game.HandleMove (logic.currentTile, row, column);
	}
}

