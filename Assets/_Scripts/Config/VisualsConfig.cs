//using System.Drawing;
using UnityEngine;

static class VisualsConfig
{
    #region attack_animations
    public static float AttackAnimTime = 0.2f;
    public static float DmgFlashTime = 0.1f;
    public static Color DmgFlashColor = Color.red;
    #endregion

    #region highlights
    public static Color DefaultHighlight = Color.white;
    public static Color ValidTargetHighlight = Color.white;
    public static Color MouseTargetHighlight = Color.green;
    public static Color InvalidMouseTargetHighlight = Color.red;
    #endregion

    #region card_view
    public static float CardDragHeight = 8f;
    #endregion
}