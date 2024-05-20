# Animated Text

![In Progress](https://img.shields.io/badge/Project%20In%20Progress-No-red?style=flat-square)
![Project Type](https://img.shields.io/badge/Project_Type-Complete-blue?style=flat-square)

## Example

![TextParsing_MultiField](https://github.com/NathanThus/Animated-Text/assets/99728206/3c4e7dfb-693a-4118-9adb-dda7b3688c0f)

## Introduction
Animated Text is a project I created to learn more about meshes, which eventually turned into me creating a set of extendible classes, allowing you to animate text in fun ways!

## Features
- Asynchronous Animation
- Multi-Field Animation
- Add Text Fields on the fly
- Easily extendible ScriptableObjects
- (Semi)-Automatic animation detection
- Includes an easy to use demo

## Using the System
### Setup
To use the system, create a `TextAnimator` gameobject, and add the `TextMeshAnimator` Component. This object will take care of all the animation, and can be controlled by another class.

### Creating a new animation
Duplicate the `TemplateAnimation` class, and adjust the code as required. Several animations already exist in the `BaseAnimationObject`, so feel free to peek at both the examples and that class to get started.

#### Important
The `Regex` used in `TextMeshAnimator` expects the following format: `:XYZ123:`. For example: `:glow3:` or `:rainbow:`. Not using this format (or adjusting the regex yourself) will lead to issues.

You can always add more `SerializedFields` to the animation class, as it only cares for the `DoEffect(Mesh mesh)` function. Make sure to override the function with your own implementation, returning the newly modified mesh.

Several fields have already been provided, and act more akin to a guideline than a hard list of properties.

### Adding the Animation
Under the `TextMeshAnimator` component, press the `Refresh` button. This will search the asset database for any new animation objects, and add them to the list.

### Animating your text
Once the animations are setup, make sure the `TMP_Text.text` property contains an animation before passing it to the `TextAnimator`. It will find and parse any animation found, creating a `TextAnimationPair` for internal use.

You can `Play` and `Stop` the animations altogether, add new animations while other are playing, or remove it from the list (though it does **NOT** destroy the gameObject).

## Dependencies
Please checkout [dependencies](https://github.com/NathanThus/Animated-Text/blob/develop/DEPENDENCIES.md) for more information, though both `TextMeshPro` and `UniTask` are included in the package.

## Contribution
Please checkout the [contribution](https://github.com/NathanThus/Animated-Text/blob/develop/CONTRIBUTING.md) page for more info! Thanks in advance!

## License
This project is [licensed](https://github.com/NathanThus/Animated-Text/blob/develop/LICENSE.md) under Creative Commons 4.0 BY.
