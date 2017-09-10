using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : PieceLogic {

	public Knight (Game.SideColor sideColor) : base(sideColor){
		type = Game.PieceType.Knight;
	}

	// Returns 
	public override List<TileLogic> GetMoves(){
		List<TileLogic> validMoves = new List<TileLogic>();
		TileLogic tile;

		// Left-up 1
		tile = new TileLogic(currentTile.row + 1, currentTile.column - 2);
		if (tile.IsInBoard()) { validMoves.Add (tile); }

		// Left-up 2
		tile = new TileLogic(currentTile.row + 2, currentTile.column - 1);
		if (tile.IsInBoard()) { validMoves.Add (tile); }

		// Left-down 1
		tile = new TileLogic(currentTile.row -1, currentTile.column - 2);
		if (tile.IsInBoard()) { validMoves.Add (tile); }

		// Left-down 2
		tile = new TileLogic(currentTile.row - 2, currentTile.column - 1);
		if (tile.IsInBoard()) { validMoves.Add (tile); }

		// Right-up 1
		tile = new TileLogic(currentTile.row + 1, currentTile.column +2);
		if (tile.IsInBoard()) { validMoves.Add (tile); }

		// Right-up 2
		tile = new TileLogic(currentTile.row + 2, currentTile.column + 1);
		if (tile.IsInBoard()) { validMoves.Add (tile); }

		// Right-down 1
		tile = new TileLogic(currentTile.row -1, currentTile.column + 2);
		if (tile.IsInBoard()) { validMoves.Add (tile); }

		// Right-down 2
		tile = new TileLogic(currentTile.row -2, currentTile.column + 1);
		if (tile.IsInBoard()) { validMoves.Add (tile); }

		return validMoves;
	}
}
