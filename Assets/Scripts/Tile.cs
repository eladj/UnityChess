using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public int row;
	public int column;
	public Piece piece;

	public enum TileType {White, Black};
	public Material whiteTileMaterial, blackTileMaterial;

	public TileType GetTileType(){
		// Determine the tile color based on the position. If the column and
		// the row is even or the column and the row is odd, it must be white.
		TileType res = (column % 2 == 0 && row % 2 == 0 || column % 2 == 1 && row % 2 == 1) ? TileType.Black : TileType.White;
		return res;
	}

	void LoadTileMaterial(TileType type){
		Debug.Log ("Loading Material");

		// Load Tile material according to its type (White / Black)
		MeshRenderer renderer = gameObject.GetComponent<MeshRenderer> ();
		if (type == TileType.White) {
			renderer.material = whiteTileMaterial;
		} else if (type == TileType.Black) {
			renderer.material = blackTileMaterial;
		}
	}

	// Use this for initialization
	void Start () {
		// Set position on Board based on column and row. We'll pretend that a tile is 1 unit long and high
		transform.localPosition = new Vector3((float)column, (float)row, 0f);

		// Load Tile material according to its type (White / Black)
		LoadTileMaterial (GetTileType ());
	}


//	public static Tile Create(Board board, int column, int row) {
//		if (!tilePrefab) {
//			if (GetTileType == TileType.White) {
//				tilePrefab = Resources.Load<GameObject>("Prefabs/Board/TileWhite");
//			} else if (GetTileType == TileType.Black) {
//				tilePrefab = Resources.Load<GameObject>("Prefabs/Board/TileBlack");
//			}
//		}
//
//		var tile = (Instantiate(tilePrefab) as GameObject).GetComponent<Tile>();
//
//		tile.transform.SetParent(board.transform);
//		tile.board = board;
//		tile.row = row;
//		tile.column = column;
//
//		return tile;
//
//	}

}
