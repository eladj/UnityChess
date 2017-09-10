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
				cur_tile.logic = logic.GetTileLogic (row, column);	
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
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.King, Game.SideColor.White, pieces_child, logic.GetTileLogic (0, 4).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Queen, Game.SideColor.White, pieces_child, logic.GetTileLogic (0, 3).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Bishop, Game.SideColor.White, pieces_child, logic.GetTileLogic (0, 2).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Bishop, Game.SideColor.White, pieces_child, logic.GetTileLogic (0, 5).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Knight, Game.SideColor.White, pieces_child, logic.GetTileLogic (0, 1).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Knight, Game.SideColor.White, pieces_child, logic.GetTileLogic (0, 6).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Rook, Game.SideColor.White, pieces_child, logic.GetTileLogic (0, 0).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Rook, Game.SideColor.White, pieces_child, logic.GetTileLogic (0, 7).GetPiece ()));
		for (int col = 0; col < 8; col++) {
			pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Pawn, Game.SideColor.White, pieces_child, logic.GetTileLogic (1, col).GetPiece ()));
		}
		// Black Pieces
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.King, Game.SideColor.Black, pieces_child, logic.GetTileLogic (7, 4).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Queen, Game.SideColor.Black, pieces_child, logic.GetTileLogic (7, 3).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Bishop, Game.SideColor.Black, pieces_child, logic.GetTileLogic (7, 2).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Bishop, Game.SideColor.Black, pieces_child, logic.GetTileLogic (7, 5).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Knight, Game.SideColor.Black, pieces_child, logic.GetTileLogic (7, 1).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Knight, Game.SideColor.Black, pieces_child, logic.GetTileLogic (7, 6).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Rook, Game.SideColor.Black, pieces_child, logic.GetTileLogic (7, 0).GetPiece ()));
		pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Rook, Game.SideColor.Black, pieces_child, logic.GetTileLogic (7, 7).GetPiece ()));
		for (int col = 0; col < 8; col++) {
			pieces.Add(PieceFactory.CreatePiece(Game.PieceType.Pawn, Game.SideColor.Black, pieces_child, logic.GetTileLogic (6, col).GetPiece ()));
		}
	}

	void OnDrawGizmos(){
		float width = 8;
		float height = 8;
		Vector3 offset = new Vector3(width/2f - 0.5f, height/2f - 0.5f, 0f);
		Gizmos.DrawWireCube (transform.position + offset, new Vector3(width, height, 0));
	}

	// Add a new Piece GameObject and corresponding PieceLogic
	public void AddPiece(Game.PieceType pieceType, Game.SideColor pieceColor, int row, int col){
		GameObject pieces_child = this.transform.Find("Pieces").gameObject;
		logic.CreateNewPiece(pieceType, pieceColor, row, col);
		pieces.Add(PieceFactory.CreatePiece(pieceType, pieceColor, pieces_child, logic.GetTileLogic (row, col).GetPiece ()));
	}

	// Remove a piece when it has been taken by opponent
	public void RemovePiece(TileLogic tile){
		if (tile.HasPiece ()) {
			Piece p = GetPiece(tile);
			Debug.Log("Remove piece: " + p.logic.NameChar() + " at " + tile.Name());
			logic.RemovePiece (tile);
			p.gameObject.SetActive (false);
			pieces.Remove (p);
//			Destroy (p.gameObject);
		}
	}

	// Move piece from origin to destination
	public void MovePiece(Move move){
		logic.MovePiece (move);
		Piece p = GetPiece(move.destination);
		p.UpdatePosition ();
	}
}
