//using UnityEngine;
//using UnityEditor;
//using System.IO;

//public class CardGenerator : EditorWindow
//{

//    [MenuItem("Tools/Generate Cards")]
//    public static void GenerateCards()
//    {
//        string path = "Assets/Data/cards.json";
//        if (!File.Exists(path))
//        {
//            Debug.LogError("File not found: " + path);
//            return;
//        }

//        string json = File.ReadAllText(path);
//        CardTemplate[] cardArray = JsonHelper.FromJson<CardTemplate>(json);

//        string outputFolder = "Assets/Cards";
//        if (!AssetDatabase.IsValidFolder(outputFolder))
//        {
//            AssetDatabase.CreateFolder("Assets", "Cards");
//        }

//        foreach (var cardData in cardArray)
//        {
//            CardTemplate card = ScriptableObject.CreateInstance<Card>();
//            card.cardName = cardData.cardName;

//            string assetPath = $"{outputFolder}/{card.cardName}.asset";
//            AssetDatabase.CreateAsset(card, assetPath);
//        }

//        AssetDatabase.SaveAssets();
//        AssetDatabase.Refresh();
//    }
//}
