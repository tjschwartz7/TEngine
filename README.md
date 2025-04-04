#TEngine2.0

##What is it
A game engine currently only designed for Text-Based games. It uses an architecture highly familiar to anyone who has used Unity before.

##What can it do
The intention of the engine is that it will have a feature-set similar to Unity, but without the graphics engine. Like I said, it is intended currently for only Text-Based games, but it could be updated 
to contain an actual graphics renderer. There is a 'camera', which is simply the command prompt for the text-based engine. GameObjects can be attached to the camera, and will have physics, collisions, et cetera.
TEngine also handles scenes for you, and the loading and unloading of GameObjects that occurs when you switch scenes. 
In order to render things, you essentially attach GameObjects to the renderer, which will handle everything related to them. GameObjects contain a set of 'components', which can be things like Behaviors,
Colliders, Transforms, and RigidBodies. 
This is an architecture which allows for modularity, and makes it incredibly easy to add new features in the form of components. 
The background architecture of the engine uses Events, with a custom built EventManager class. This is how the different parts of the engine communicate with each other.
This engine uses these primary design patterns:
Singleton
Observer
Component
