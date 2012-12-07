Vision
======

Vision is simple system to realize a spherical AI vision in your projects.

* Create single gameObject with VisibleGrid in the scene for the system to work.
* Put Visible component on the gameObjects that you want to be visible.
* Use IVisibleListener interface on other components of this object to know when you're being seen.
* Put Vision component on the gameObjects taht you want to see.
* Use IVisionListener interface on other components of this object to know what you see.
* Or just use VisiblesInSight().

Future plans:

* Further optimizaitons where neccessary.
* Cone-shaped vision.
* Raycasting.