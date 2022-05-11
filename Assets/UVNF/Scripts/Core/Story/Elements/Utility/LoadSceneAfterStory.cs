using System.Collections;
using UnityEngine;
using UnityEditor;
using UVNF.Core.UI;
using UVNF.Extensions;

namespace UVNF.Core.Story.Utility
{
    public class LoadSceneAfterStory : StoryElement
    {
        public override string ElementName => "Load Scene";

        public override Color32 DisplayColor => _displayColor;
        private Color32 _displayColor = new Color32().Utility();

        public override StoryElementTypes Type => StoryElementTypes.Utility;

        public string SceneName;

#if UNITY_EDITOR
        public override void DisplayLayout(Rect layoutRect, GUIStyle label)
        {
            SceneName = EditorGUILayout.TextField("Load Scene", SceneName);
        }
#endif

        public override IEnumerator Execute(UVNFManager managerCallback, UVNFCanvas canvas)
        {
            LoadScene loadScene = FindObjectOfType<LoadScene>();
            loadScene.LoadSceneName(SceneName);
            yield return null;
        }
    }
}