### *Work in progress*

# Purpose

The goal of this project is to create a solid basic structure for various card game designs, while following most fitting programming patterns to keep it readible and adaptable to more specific projects. In this particular project I limit myself in terms of designing gameplay and visuals, in favor of technical aspects.

As a jump-start I used following tutorial serie: https://www.youtube.com/watch?v=rgsp9pb0Oi0

As said tutorial is designed specifically for single-player asymetrical roguelike deckbuilding game, many changes were made to make it more generalized, and many new functionalities were added with even more to come.

# Used packages

- [Serialize Reference Editor](https://assetstore.unity.com/packages/tools/utilities/serialize-reference-editor-297559)
- [DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)

# Overview


# Editor Features

### Battlefield placement gizmo

Allows quick changes to battlefield layout, using Editor only you can change the number of fields and whether you want horizontal or vertical split between players.

![BFgizmo2](https://github.com/user-attachments/assets/ad4220bd-037e-4e53-a178-29a9c214ecb4)

### Complex scriptable data structures (via Serialize Reference Editor) 

<table border="0">
 <tr>
    <td><b style="font-size:30px">Card with effect that choses it's targets automatically</b></td>
    <td><b style="font-size:30px">Card with passive effect triggered by certain conditions</b></td>
 </tr>
 <tr>
    <td>
      <img width="487" height="612" alt="AutoTargetEffectsOnCard" src="https://github.com/user-attachments/assets/781748c4-90b7-4348-be54-24b4242347f4" />
    </td>
    <td>
      <img width="485" height="733" alt="PassiveEffectOnCard" src="https://github.com/user-attachments/assets/ce0ad00a-cebc-4b3a-bfcf-7ac102bcf5aa" />
    </td>
 </tr>
</table>

### Simplified, clearer inspectors (Custom Property Drawers)

<table border="0">
 <tr>
    <td><b style="font-size:30px">Defualt</b></td>
    <td><b style="font-size:30px">Custom</b></td>
 </tr>
 <tr>
    <td>
      <img width="512" height="178" alt="image" src="https://github.com/user-attachments/assets/e07f5f22-50ed-49d2-b450-c99f046c3833" />
    </td>
    <td>
      <img width="501" height="119" alt="image" src="https://github.com/user-attachments/assets/0bc9030e-e713-488c-ac4a-36c6fe609199" />
    </td>
 </tr>
</table>
[Related code](https://github.com/Priescik/CardGameBase/blob/5ca187bab10bfc876ce1daa320d573815b06c35e/Assets/Editor)
