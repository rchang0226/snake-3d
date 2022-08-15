# Description
A simple 3-dimensional snake game created in Unity.

## Controls
Left click and drag with the mouse to move the camera. 

The red hat at the top of the snake's head shows where the top of the snake's head is, which otherwise would be difficult to track.
Going left and right by pressing the left and right arrow keys means moving left and right relative to where the snake is going. For example, in our global view, if the snake is moving forward and facing upright (the hat is on top), then pressing left means the snake will go left. However, if the snake were upside down (the hat is below), then pressing left means the snake moves right in the global view, since the "left arrow" means that the snake moves left in its local view, which when it's upside-down, will be flipped compared to the global view.  

Likewise, you can go up and down by pressing the up and down arrows. Again, these are localized. In other words, "going up" really means going in the direction the hat was facing, and "going down" means going in the direction opposite to the hat.
