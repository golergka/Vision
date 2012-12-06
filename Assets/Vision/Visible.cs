using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Don't remove or add components with this interface in runtime.
interface IVisibleListener {

	void OnNoticedBy(Vision observer);
	void OnLostBy(Vision observer);

}

public class Visible : MonoBehaviour {

	// The cell of the VisibleGrid I belong to.
	// It's Visible's responsibility to register at corresponding cell and keeping the reference to it here.
	private List<Visible> myCell = null;

	// All visions that have me in range. They may actually not see me if I'm invisible.
	// We need it to send them messages about becoming invisible & visible.
	public List<Vision> inRangeOfVisions = new List<Vision>();

	// Here we message all the visibles about our change of visibility whenever it happens.
	private bool _visible;
	public bool visible {

		get { return _visible; }
		set {

			if ( _visible == value )
				return;

			_visible = value;

			foreach(Vision vision in inRangeOfVisions)
				vision.ChangedVisibility(this);

		}

	}

	// Defines the visibility of visible at start.
	public bool visibleOnStart = true;

	void Start() {

		visible = visibleOnStart;
		myCell = VisibleGrid.instance.GetCell(transform.position);
		myCell.Add(this);

	}

	void Update() {

		// Did we change our cell?
		// TODO: This checking could possibly be optimized. Maybe.
		List<Visible> myNewCell = VisibleGrid.instance.GetCell(transform.position);

		if (myCell == myNewCell)
			return;

		// If we're here, we're changing our cell.
		if (myCell != null)
			myCell.Remove(this);

		myNewCell.Add(this);
		myCell = myNewCell;


	}

	#region Automatic visibility changing

	// Everyone will get chance to react that we're going invisible
	void OnDestroy() {

		visible = false;

	}

	// You should remove this methods if you're going to making object enabled and disabled AND change it's visilibity at the same time
	void OnDisable() {

		visible = false;

	}

	void OnEnable() {

		visible = true;

	}

	#endregion

}
