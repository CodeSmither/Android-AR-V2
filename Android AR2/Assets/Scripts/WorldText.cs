using System;
using System.Collections.Generic;
using UnityEngine;

//Create World Text and Text Pop-up Inspired by https://www.youtube.com/watch?v=dulosHPl82A&ab_channel=CodeMonkey
namespace Utilities.Ulility
{
    public static class Utility
    {
        public const int sortingOrderDefault = 5000;

        // Creates a Text Mesh in the World and constantly updates it
        public static FunctionUpdater CreateWorldTextUpdater(Func<string> GetTextFunc, Vector3 localPosition, Transform parent = null, int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
        {
            TextMesh textMesh = CreateWorldText(GetTextFunc(), parent, localPosition, fontSize, color, textAnchor, textAlignment, sortingOrder);
            return FunctionUpdater.Create(() => {
                textMesh.text = GetTextFunc();
                return false;
            }, "WorldTextUpdater");
        }

        // Create Text in the World
        public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
        {
            if (color == null) color = Color.white;
            return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
        }

        // Create Text in the World
        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            textMesh.gameObject.transform.localScale = new Vector3(1 / 33.3333f, 1 / 33.3333f, 1 / 33.3333f);
            textMesh.gameObject.layer = 7;
            return textMesh;
        }

        // Create a Text Popup in the World, no parent
        public static void CreateWorldTextPopup(string text, Vector3 localPosition)
        {
            CreateWorldTextPopup(null, text, localPosition, 20, Color.white, localPosition + new Vector3(0, 0.303f), 1f);
        }

        // Create a Text Popup in the World
        public static void CreateWorldTextPopup(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, Vector3 finalPopupPosition, float popupTime)
        {
            TextMesh textMesh = CreateWorldText(parent, text, localPosition, fontSize, color, TextAnchor.LowerLeft, TextAlignment.Left, sortingOrderDefault);
            Transform transform = textMesh.transform;
            Vector3 moveAmount = (finalPopupPosition - localPosition) / popupTime;
            FunctionUpdater.Create(delegate () {
                transform.position += moveAmount * Time.deltaTime;
                popupTime -= Time.deltaTime;
                if (popupTime <= 0f)
                {
                    UnityEngine.Object.Destroy(transform.gameObject);
                    return true;
                }
                else
                {
                    return false;
                }
            }, "WorldTextPopup");
        }
        // Collects one touch within the world using a ray cast from the camera and the first finger a player places
        public static Vector3 GetTouchWorldPosition()
        {
            Vector3 vec = GetTouchWorldPositionWithZ(Input.GetTouch(0).position, Camera.main);
            vec.z = 0f;
            return vec;
        }
        // converts the players tap into a position along the Z axis
        public static Vector3 GetTouchWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
        // caculates the direction that the ray is pointing
        public static Vector3 GetDirToMouse(Vector3 fromPosition)
        {
            Vector3 mouseWorldPosition = GetTouchWorldPosition();
            return (mouseWorldPosition - fromPosition).normalized;
        }


    }
    



}
