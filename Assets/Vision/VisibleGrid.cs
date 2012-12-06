using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The purpose of the VisibleGrid is to cache position of all visibles in the game.
// It optimizes distance checks. No Vision can have range more than gridStep.
// However, the optimization only works for 2d or 2d-ish games (like RTS or RPGs).
// If you have a lot of objects dispersed along Y-axis, you should rewrite it.
public class VisibleGrid : MonoBehaviour {	

	#region Grid setup

	// That's only public for the sake of the editor. Don't mess with it in runtime.
	public float gridStep = 10f;
	public int gridSize = 10;
	public List<Visible>[,] grid;
	public static VisibleGrid instance;

	void Awake() {

		instance = this;
		grid = new List<Visible> [gridSize, gridSize];
		for(int x=0; x<gridSize; x++)
			for(int y=0; y<gridSize; y++)
				grid[x,y] = new List<Visible>();

	}

	#endregion

	#region Public

	private bool CheckCoordinates(int x, int y) {

		if ( x < 0 || x >= gridSize ||
			 y < 0 || y >= gridSize ) {

			Debug.LogError("Coordinates beyond limits for vision grid, x: " + x + " y: " + y );
			return false;

		} else
			return true;

	}

	public List<Visible> GetCell(Vector3 position) {

		int x = Mathf.FloorToInt( ( position.x / gridStep ) + gridSize / 2 );
		int y = Mathf.FloorToInt( ( position.y / gridStep ) + gridSize / 2 );

		if (CheckCoordinates(x,y))
			return grid[x, y];
		else
			return null;

	}

	public List<Visible> GetNeighbors(Vector3 position) {

		int x = Mathf.FloorToInt( ( position.x / gridStep ) + gridSize / 2 );
		int y = Mathf.FloorToInt( ( position.y / gridStep ) + gridSize / 2 );

		if (CheckCoordinates(x,y)) {

			List<Visible> result = new List<Visible>();

			for (int loopX = x-1; loopX <= x+1; loopX++)
				for (int loopY = y-1; loopY <= y+1; loopY++)
					if (loopX >= 0 && loopX < gridSize &&
						loopY >= 0 && loopY < gridSize)
						result.AddRange(grid[loopX, loopY]);

			return result;

		} else
			return null;

	}

	#endregion

	void OnDrawGizmosSelected() {

		Gizmos.color = Color.blue;
		
		float lineStart = -gridSize*gridStep/2;
		float lineEnd = gridSize*gridStep/2;

		// Lines parallel to x-axis
		for(int i = 0; i <= gridSize; i++) {

			float z = ((float) i - gridSize/2)*gridStep;
			Vector3 start = new Vector3(lineStart,0,z);
			Vector3 end = new Vector3(lineEnd,0,z);
			Gizmos.DrawLine(start,end);

		}

		// Lines parallel to z-axis
		for(int i = 0; i <= gridSize; i++) {

			float x = ((float) i - gridSize/2)*gridStep;
			Vector3 start = new Vector3(x,0,lineStart);
			Vector3 end = new Vector3(x,0,lineEnd);
			Gizmos.DrawLine(start,end);

		}


	}

}