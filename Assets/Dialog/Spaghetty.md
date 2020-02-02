# Spaghetty Dialog System
## About
### _Spaghetty is easy to make, and it tastes real good. You games could be that spaghetty._
The spaghetty dialog system is built on top of the [ink scripting language](https://www.inklestudios.com/ink/) and TextMeshPro for use with the Unity game engine. The goal of spaghetty is to make text based story games quick to put together and easy to finish. Spaghetty provides a high level of polish with a low level of investment, and builds games that are easily scalable to include their own unique gameplay sauce on top.

_(?) indicates something that is soon to change or needs more design discussion_


# Custom Line Tags
[_See Ink Docs On Tags_]((https://github.com/inkle/ink/blob/master/Documentation/RunningYourInk.md#marking-up-your-ink-content-with-tags))
## Scene Related Tags
> `#BG : [arg1] : [arg2]`<br>
  ### Description
 The background tag changes the the background image of the scene, with an optional transition.
 > ### Arguments
 > Use | Name | Description
 > --------|---------|---------
 > _Optional_ | Transition Effect | The name of a transition effect, as found in  the ScreenTransitionLibrary
 > _REQUIRED_ | Background Name | The name of the background, as found in the BackgroundLibrary

> `#MUSIC : [arg1]`<br>
 ### Description
 The music tag changes the music playing in the scene.  
> ### Arguments
> Use | Name | Description
> --------|---------|---------
> _REQUIRED_ | Track Name | The name of the music track, as found in the MusicLibrary

> `#IN : [arg1]`<br>
 ### Description
 Sets the dialog box to be used for the current dialog. Once set, this will remain the dialog box for all subsequent dialog until a new #IN tag is called. 
> ### Arguments
> Use | Name | Description
> --------|---------|---------
> _REQUIRED_ | DialogBox Name | The name GameObject where the chosen dialog box component resides. 

> `#WAIT : [arg1]`<br>
 ### Description
 Holds Displaying the dialog text or choices of the line this tag is on for a specified amount of time.
> ### Arguments
> Use | Name | Description
> --------|---------|---------
> _REQUIRED_ | Time (seconds) | The number of seconds to wait before display this line of dialog. All tags on this line will still be processed.

## Character Related Tags
> `#Show : [arg1] : [arg2] : [arg3] : [arg4]`<br>
 ### Description
 Brings a character onto screen with a specified sprite and entrance animation. At the end of a show call, the renderer component on the specified character will be enabled.
> ### Arguments
> Use | Name | Description
> --------|---------|---------
> _REQUIRED_ | Character Name | The name of the GameObject where this DialogCharacter resides.
> _REQUIRED_(?) | Emotion Sprite | The name of the emotion sprite to display. <br>(?) If left out, the sprite will remain the same as the last #show tag. (?)
> _Optional_ | Animation | The name of the animation to play. This can be for entry from off screen or any other special animation. If left out, no animation will play.
> _Optional_ | Change Sprite After Animation | TRUE or FALSE, defaults to FALSE if unused. If TRUE, the character's new sprite as specified in this tag will not change until after the animation specified in this tag has finished playing.

> `#Hide : [arg1] : [arg2] : [arg3]`<br>
 ### Description
 Brings a character off screen with a specified sprite and exit animation. At the end of a hide call, the renderer component on the specified character will be disabled.
> ### Arguments
> Use | Name | Description
> --------|---------|---------
> _REQUIRED_ | Character Name | The name of the GameObject where this DialogCharacter resides.
> _REQUIRED_(?) | Emotion Sprite | The name of the emotion sprite to display. <br>(?) If left out, the sprite will remain the same as the last #show tag. (?)
> _REQUIRED_(?) | Animation | The name of the animation to play. This animations are specificly for hiding the character, as this tag will result in the renderer being disabled.

## Gameplay Related Tags
> `#INVOKE : [arg1]`<br>
### Description
Invokes a DialogEvent, as defined in the DialogEvents component.
> ### Arguments
> Use | Name | Description
> --------|---------|---------
> _REQUIRED_ | Event Name | The name of the event to invoke. This are System,Action events that any component can subscribe to.


# Dialog Lines
> ` Doug : neutral : Welcome to Potato Palazzo. We fry ‘em, you buy ‘em.`

Dialog Lines take the form of: 
<br>
<br>
__[Character] : [Emote] : [Line]__
<br><br>
Where Character is the name of the game object where the Dialog Character component resides, the emote is the name of the sprite as listed in that characters emotion library, and the line is the content you wish for that character to say. 
<p>
By default, lines are drawn to the screen over time with a typing like effect, which can be cancel by pressing the continue button. After being fully displayed, the line will stay in the dialog box until the user presses the continue button. This button is space bar and also left click by default.

# Dialog Box
 Dialog box takes reference to two `TextMeshProUGUI` components, one for the character name/title and one for the actual dialog text. <p>The name component works as any normal `TextMeshProUGUI` would, but for the dialog, be sure to set the color of the text to an alpha of 0.0. To set the text color as it would appear in game, set the "Text Color" field in the `DialogBox` component. "Color Tint" and "Fade Speed" control the color of the fade in effect as the text appears on screen and the speed the text appears, respectively. <p> If this Dialog box is the starting dialog box, and you would like your dialog lines to go to this box by default, check "is default". Other dialog boxes can be switched to at runtime using the `#IN` tag. <p>set "Keep Text" if you would like to keep the dialog box visible when it is not the dialog box the the dialog lines are being displayed through.<p>
 The `DialogBox` component requires both a button and image component on the same GameObject, which it uses to determine the clickable area for continuing when the "click anywhere" field is set to `false`. In that case, clicking anywhere but the image of the dialog box will not advance the story. If "click anywhere" is set to true, however, the user can click anywhere on screen to advance.
 <p>
 "Hide When Inactive", if true, will make sure this dialog box is not rendered at all if it is not the active dialog box.
 <p>
 HACKY -> "Hide on awake" will hide the dialog box after the scene has loaded. Currently it is required that all dialog boxes be active in the scene at first, and you can use this to make sure they are inactive once the user starts to see the scene.

### Future Considerations
1) Make the dialog boxes able to be disabled in the scene by default
2) Make the text fade in effect configureable
3) Consider something for moving the name around on top of the box, like under the character art when a character is speaking
4) Consider support for portraits in the dialog box, like in monster prom.

# Dialog Choices
Dialog choices are written no differently than in basic ink. Once the DialogDirector comes to a group of choices, it will spawn that many instances of the `DialogChoicePrefab`, all as children to the `DialogChoiceParent`. These prefabs have a button component, that onClick selects that choice and continues with the story. Change the visuals of this group of buttons by changing the `DialogChoicePrefab` and `DialogChoiceParent` references in `DialogDirector`.

### Future Considerations
1. What if a choice didn't have to be a button, but could be any event that eventually called back to the story to continue? (that might mean the DialogGame component could be replaced with just DialogChoice)

# Dialog Games

# Dialog Director

# Dialog Character

oohhhh fuck this is the part that sucks.
>### outline mask (Highlight effects?)
>### Emotes
>### Animations

# Dialog Events

# Jukebox

# Background

# Scene Management

# Post Processing (cause why not)

# Libraries
>### Backgrounds
>### Screen Transitions
>### Character Animations
>### Character Emotes
>### Music