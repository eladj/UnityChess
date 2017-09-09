using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	// Tile GameObjects
	// We connect each of them to the TileLogic inside BoardLogic
	Tile[,] tiles = new Tile[BoardLogic.Height, BoardLogic.Width];

	// List of all pieces GameObjects
	private List<Piece> pieces = new List<Piece>();

	// Logic
	public BoardLogic logic;

	// Graphics
	public GameObject tilePrefab;
	public static float z_height = 10f;

	void Start () {
		logic =  new BoardLogic ();
		logic.InitializePieces ();
		CreateBoard ();
		CreatePieces ();
	}

	public Tile GetTile(int row, int column){
		return tiles [row, column];
	}

	// Get Piece GameObject from row and column
	public Piece GetPiece(int row, int column){
		foreach (Piece p in pieces) {
			if (p.logic.IsActive ()) {
				if (p.logic.currentTile.row == row && p.logic.currentTile.column == column) {
					return p;
				}
			}
		}
		return null;
	}

	// Get Piece GameObject from tile
	public Piece GetPiece(Tile tile){
		foreach (Piece p in pieces) {
			if (p.logic.IsActive ()) {
				if (p.logic.currentTile.row == tile.logic.row && p.logic.currentTile.column == tile.logic.column) {
					return p;
				}
			}
		}
		return null;
	}

	public Piece GetPiece(TileLogic tile){
		foreach (Piece p in pieces) {
			if (p.logic.IsActive ()) {
				if (p.logic.currentTile.row == tile.row && p.logic.currentTile.column == tile.column) {
					return p;
				}
			}
		}
		return null;
	}

	void CreateBoard(){
		GameObject tiles_child = this.transform.Find("Tiles").gameObject;

		// Create all the tiles for this board.
		for (var row = 0; row < BoardLogic.Height; row++) {
			for (var column = 0; column < BoardLogic.Width; column++) {
				Tile cur_tile = (Instantiate (tilePrefab) as GameObject).GetComponent<Tile> ();
				// Put the Tile under the "Tiles" sub group in the hierarchy
				cur_tile.transform.SetParent (tiles_child.transform, true);
				// Connect GameObject Tile to TileLogic item in array
				cur_tile.logic = logic.GetTile (row, column);	
				// Update Graphics position
				cur_tile.UpdatePosition();
				// Debug.Log ("Generate Tile: " + cur_tile.name);
				tiles[row, column] = cur_tile;
			}
		}
	}

	void CreatePieces(){
		GameObject pieces_child = this.transform.Find("Pieces").gameObject;

		// White pieces
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.King, Game.SideColor.White, pieces_child, logic.GetTile (0, 4).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Queen, Game.SideColor.White, pieces_child, logic.GetTile (0, 3).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Bishop, Game.SideColor.White, pieces_child, logic.GetTile (0, 2).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Bishop, Game.SideColor.White, pieces_child, logic.GetTile (0, 5).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Knight, Game.SideColor.White, pieces_child, logic.GetTile (0, 1).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Knight, Game.SideColor.White, pieces_child, logic.GetTile (0, 6).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Rook, Game.SideColor.White, pieces_child, logic.GetTile (0, 0).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Rook, Game.SideColor.White, pieces_child, logic.GetTile (0, 7).GetPiece ()));
		for (int col = 0; col < 8; col++) {
			pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Pawn, Game.SideColor.White, pieces_child, logic.GetTile (1, col).GetPiece ()));
		}
		// Black Pieces
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.King, Game.SideColor.Black, pieces_child, logic.GetTile (7, 4).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Queen, Game.SideColor.Black, pieces_child, logic.GetTile (7, 3).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Bishop, Game.SideColor.Black, pieces_child, logic.GetTile (7, 2).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Bishop, Game.SideColor.Black, pieces_child, logic.GetTile (7, 5).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Knight, Game.SideColor.Black, pieces_child, logic.GetTile (7, 1).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Knight, Game.SideColor.Black, pieces_child, logic.GetTile (7, 6).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Rook, Game.SideColor.Black, pieces_child, logic.GetTile (7, 0).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Rook, Game.SideColor.Black, pieces_child, logic.GetTile (7, 7).GetPiece ()));
		for (int col = 0; col < 8; col++) {
			pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Pawn, Game.SideColor.Black, pieces_child, logic.GetTile (6, col).GetPiece ()));
		}
	}

	void OnDrawGizmos(){
		float width = 8;
		float height = 8;
		Vector3 offset = new Vector3(width/2f - 0.5f, height/2f - 0.5f, 0f);
		Gizmos.DrawWireCube (transform.position + offset, new Vector3(width, height, 0));
	}

	// Remove a piece when it has been taken by opponent
	public void RemovePiece(TileLogic tile){
		if (tile.HasPiece ()) {
			Piece p = GetPiece(tile);
			Debug.Log("Remove piece: " + p.logic.NameChar() + " at " + tile.Name());
			tile.SetPiece (null);
			p.logic.currentTile = null;
			p.gameObject.SetActive (false);
//			pieces.Remove (p);
//			Destroy (p.gameObject);
		}
	}
}
