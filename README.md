# Animlist Transition Tool
FNIS Animlist to Nemesis patch tool, for Skyrim Special Edition.

Originally made for Ostim NG, allows users to port FNIS behaviour to Nemesis patches.

## Requirements:

.NET Desktop Runtime 6.0.11+

## Quickstart:

Run > load animlist.txt file > fill required fields > set output folder > launch

## Documentation:

Functionally, the abbreviation format for the end user remains the same as it was in FNIS, albeit with less options.

`<AnimType> [-<option,option,...>] <AnimEvent> <AnimFile> [<AnimObject> ...]`

### Supported AnimTypes:

`b`: basic.

### Supported options:

(Animations play at looping mode if no other mode is specified)

`a`: acyclic play mode

`p`: ping pong play mode (plays to end, reverse plays back to start, repeats)

`o`: one or more AnimObjects, define at end of line separated by spaces between each object. Must match plugin defined animobjects that are set to unload on `AnimObjectUnequip`.


`Tn`: already default on in all animations, no need to include this explicitly in the abbreviation


### Binding variables:
Every converted animation is bound in behaviour to special graph variables you can change with [`SetAnimationVariableFloat`](https://www.creationkit.com/index.php?title=SetAnimationVariableFloat_-_ObjectReference) and get with [`GetAnimationVariableFloat`](https://www.creationkit.com/index.php?title=GetAnimationVariableFloat_-_ObjectReference).

The naming is automatically assigned in the following format to avoid conflicts with other patches:
`<modPrefix><variablename>`

The following variables are available for ported animlist behaviour, and their function is self-evident by naming:

`<modPrefix>_CropAnimStart` 0.0 by default

`<modPrefix>_CropAnimEnd` 0.0 by default

`<modPrefix>_AnimStartTime`: 0.0 by default

`<modPrefix>_AnimationSpeed`: 1.0 by default



### Exit events
Use event `OST_ExitAnim` to exit an anim instantly.

## Planned
Option for gender specific animations, transition time customization. Addition of more options to make it as capable as fnis.
