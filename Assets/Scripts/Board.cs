using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	public GameObject tilePrefab;
	public GameObject pawnWhitePrefab;
	public GameObject rookWhitePrefab;
	public GameObject knightWhitePrefab;
	public GameObject bishopWhitePrefab;
	public GameObject queenWhitePrefab;
	public GameObject kingWhitePrefab;
	public GameObject pawnBlackPrefab;
	public GameObject rookBlackPrefab;
	public GameObject knightBlackPrefab;
	public GameObject bishopBlackPrefab;
	public GameObject queenBlackPrefab;
	public GameObject kingBlackPrefab;

	public static int Width = 8;
	public static int Height = 8;
	Tile[,] tiles = new Tile[Height, Width];

	void Start () {
		CreateBoard ();
		CreatePieces ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateBoard(){
		GameObject tiles_child = this.transform.Find("Tiles").gameObject;

		//Create all the tiles for this board.
		for (var row = 0; row < 8; row++) {
			for (var column = 0; column < 8; column++) {
				Tile cur_tile = (Instantiate (tilePrefab) as GameObject).GetComponent<Tile> ();
				cur_tile.transform.SetParent (tiles_child.transform, true);
				int c = 'a' + (int)column;
				cur_tile.name = ((char)c).ToString() + (row + 1).ToString();
				// Debug.Log ("Generate Tile: " + cur_tile.name);
				cur_tile.row = row;
				cur_tile.column = column;
				tiles [row, column] = cur_tile;
			}
		}
	}

	void CreatePieces(){
		// White pieces
		InstantiateKing (kingWhitePrefab, Piece.ColorType.White, tiles[0,4]);
		InstantiateQueen (queenWhitePrefab, Piece.ColorType.White, tiles[0,3]);
		InstantiateBishop (bishopWhitePrefab, Piece.ColorType.White, tiles[0,2]);
		InstantiateBishop (bishopWhitePrefab, Piece.ColorType.White, tiles[0,5]);
		InstantiateKnight (knightWhitePrefab, Piece.ColorType.White, tiles[0,1]);
		InstantiateKnight (knightWhitePrefab, Piece.ColorType.White, tiles[0,6]);
		InstantiateRook (rookWhitePrefab, Piece.ColorType.White, tiles[0,0]);
		InstantiateRook (rookWhitePrefab, Piece.ColorType.White, tiles[0,7]);
		for (int col = 0; col < 8; col++) {
			InstantiatePawn (pawnWhitePrefab, Piece.ColorType.White, tiles[1,col]);
		}

		InstantiateKing (kingBlackPrefab, Piece.ColorType.Black, tiles[7,4]);
		InstantiateQueen (queenBlackPrefab, Piece.ColorType.Black, tiles[7,3]);
		InstantiateBishop (bishopBlackPrefab, Piece.ColorType.Black, tiles[7,2]);
		InstantiateBishop (bishopBlackPrefab, Piece.ColorType.Black, tiles[7,5]);
		InstantiateKnight (knightBlackPrefab, Piece.ColorType.Black, tiles[7,1]);
		InstantiateKnight (knightBlackPrefab, Piece.ColorType.Black, tiles[7,6]);
		InstantiateRook (rookBlackPrefab, Piece.ColorType.Black, tiles[7,0]);
		InstantiateRook (rookBlackPrefab, Piece.ColorType.Black, tiles[7,7]);
		for (int col = 0; col < 8; col++) {
			InstantiatePawn (pawnBlackPrefab, Piece.ColorType.Black, tiles[6,col]);
		}
	}

	public Tile GetTile(int row, int column){
		// Debug.Log ("Get Tile: " + row.ToString () + "," + column.ToString ());
		if (row >= 0 && row < Width && column >= 0 && column < Height) {
			return tiles [row, column];
		} else {
			return null;
		}
	}

	King InstantiateKing(GameObject prefab, Piece.ColorType color, Tile tile){
		King piece;
		piece = (Instantiate (prefab) as GameObject).GetComponent<King> ();
		GameObject pieces_child = this.transform.Find("Pieces").gameObject;
		piece.transform.SetParent (pieces_child.transform, true);
		piece.currentTile = tile;
		piece.color = color;
		if (color == Piece.ColorType.White) {
			piece.name = "king_white";
		} else if (color == Piece.ColorType.Black) {
			piece.name = "king_black";
		}
		tile.SetPiece(piece);
		return piece;
	}

	Queen InstantiateQueen(GameObject prefab, Piece.ColorType color, Tile tile){
		Queen piece;
		piece = (Instantiate (prefab) as GameObject).GetComponent<Queen> ();
		GameObject pieces_child = this.transform.Find("Pieces").gameObject;
		piece.transform.SetParent (pieces_child.transform, true);
		piece.currentTile = tile;
		piece.color = color;
		if (color == Piece.ColorType.White) {
			piece.name = "queen_white";
		} else if (color == Piece.ColorType.Black) {
			piece.name = "queen_black";
		}
		tile.SetPiece(piece);
		return piece;
	}

	Bishop InstantiateBishop(GameObject prefab, Piece.ColorType color, Tile tile){
		Bishop piece;
		piece = (Instantiate (prefab) as GameObject).GetComponent<Bishop> ();
		GameObject pieces_child = this.transform.Find("Pieces").gameObject;
		piece.transform.SetParent (pieces_child.transform, true);
		piece.currentTile = tile;
		piece.color = color;
		if (color == Piece.ColorType.White) {
			piece.name = "bishop_white";
		} else if (color == Piece.ColorType.Black) {
			piece.name = "bishop_black";
		}
		if (tile.column < 3) {
			piece.name += "_0";
		} else {
			piece.name += "_1";
		}
		tile.SetPiece(piece);
		return piece;
	}

	Knight InstantiateKnight(GameObject prefab, Piece.ColorType color, Tile tile){
		Knight piece;
		piece = (Instantiate (prefab) as GameObject).GetComponent<Knight> ();
		GameObject pieces_child = this.transform.Find("Pieces").gameObject;
		piece.transform.SetParent (pieces_child.transform, true);
		piece.currentTile = tile;
		piece.color = color;
		if (color == Piece.ColorType.White) {
			piece.name = "knight_white";
		} else if (color == Piece.ColorType.Black) {
			piece.name = "knight_black";
		}
		if (tile.column < 3) {
			piece.name += "_0";
		} else {
			piece.name += "_1";
		}
		tile.SetPiece(piece);
		return piece;
	}

	Rook InstantiateRook(GameObject prefab, Piece.ColorType color, Tile tile){
		Rook piece;
		piece = (Instantiate (prefab) as GameObject).GetComponent<Rook> ();
		GameObject pieces_child = this.transform.Find("Pieces").gameObject;
		piece.transform.SetParent (pieces_child.transform, true);
		piece.currentTile = tile;
		piece.color = color;
		if (color == Piece.ColorType.White) {
			piece.name = "rook_white";
		} else if (color == Piece.ColorType.Black) {
			piece.name = "rook_black";
		}
		if (tile.column < 3) {
			piece.name += "_0";
		} else {
			piece.name += "_1";
		}
		tile.SetPiece(piece);
		return piece;
	}

	Pawn InstantiatePawn(GameObject prefab, Piece.ColorType color, Tile tile){
		Pawn piece;
		piece = (Instantiate (prefab) as GameObject).GetComponent<Pawn> ();
		GameObject pieces_child = this.transform.Find("Pieces").gameObject;
		piece.transform.SetParent (pieces_child.transform, true);
		piece.currentTile = tile;
		piece.color = color;
		if (color == Piece.ColorType.White) {
			piece.name = "pawn_white_" + tile.column.ToString();
		} else if (color == Piece.ColorType.Black) {
			piece.name = "pawn_black_" + tile.column.ToString();
		}
		tile.SetPiece(piece);
		return piece;
	}

	void OnDrawGizmos(){
		float width = 8;
		float height = 8;
		Vector3 offset = new Vector3(width/2f - 0.5f, height/2f - 0.5f, 0f);
		Gizmos.DrawWireCube (transform.position + offset, new Vector3(width, height, 0));
	}

}
