using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public TileLogic logic;
	public Material whiteTileMaterial, blackTileMaterial;

	void LoadTileMaterial(Game.TileType type){
		// Load Tile material according to its type (White / Black)
		MeshRenderer renderer = gameObject.GetComponent<MeshRenderer> ();
		if (type == Game.TileType.White) {
			renderer.material = whiteTileMaterial;
		} else if (type == Game.TileType.Black) {
			renderer.material = blackTileMaterial;
		}
	}

	// Use this for initialization
	void Start () {
		// Set position on Board based on column and row. We'll pretend that a tile is 1 unit long and high
		UpdatePosition();

		// Load Tile material according to its type (White / Black)
		LoadTileMaterial (logic.GetTileType ());

		name = logic.Name ();
	}

	public void UpdatePosition(){
		transform.position = GetPositionFromTileLogic(logic);
	}

	public static Vector3 GetPositionFromTileLogic(TileLogic tile){
		return new Vector3((float)tile.column, (float)tile.row, Board.z_height);
	}

	public bool HasPiece(){
		return logic.HasPiece ();
	}

//	public void SetPiece(Piece newPiece){
//		// TODO
//		logic.SetPiece(newPiece.logic);
//	}
//
//	public Piece GetPiece(){
//		// TODO
//
//	}
}
