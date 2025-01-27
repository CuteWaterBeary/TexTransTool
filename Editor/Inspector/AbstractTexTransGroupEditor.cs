#if UNITY_EDITOR
using System.Net.Mime;
using UnityEngine;
using UnityEditor;
using net.rs64.TexTransTool.Decal;
using net.rs64.TexTransTool.Editor.Decal;
using net.rs64.TexTransTool.MatAndTexUtils;
using net.rs64.TexTransTool.Editor.MatAndTexUtils;
using net.rs64.TexTransTool.TextureAtlas;
using net.rs64.TexTransTool.TextureAtlas.Editor;

namespace net.rs64.TexTransTool.Editor
{
    [CustomEditor(typeof(AbstractTexTransGroup), true)]
    public class AbstractTexTransGroupEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var thisTarget = target as AbstractTexTransGroup;

            DrawerSummaryList(thisTarget);

            PreviewContext.instance.DrawApplyAndRevert(thisTarget);
        }

        private static void DrawerSummaryList(AbstractTexTransGroup thisTarget)
        {

            foreach (var tf in thisTarget.Targets)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.BeginHorizontal();

                var sObj = new SerializedObject(tf.gameObject);
                EditorGUILayout.LabelField(new GUIContent("Enabled"), GUILayout.Width(50));
                EditorGUILayout.PropertyField(sObj.FindProperty("m_IsActive"), GUIContent.none, GUILayout.Width(EditorGUIUtility.singleLineHeight));
                sObj.ApplyModifiedProperties();

                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField(tf, typeof(TextureTransformer), true);
                EditorGUI.EndDisabledGroup();

                EditorGUILayout.EndHorizontal();

                switch (tf)
                {
                    case SimpleDecal simpleDecal:
                        {
                            SimpleDecalEditor.DrawerSummary(simpleDecal);
                            break;
                        }
                    case AtlasTexture atlasTexture:
                        {
                            AtlasTextureEditor.DrawerSummary(atlasTexture);
                            break;
                        }
                    case TextureBlender textureBlender:
                        {
                            TextureBlenderEditor.DrawerSummary(textureBlender);
                            break;
                        }
                    case NailEditor nailEditor:
                        {
                            NailEditorEditor.DrawerSummary(nailEditor);
                            break;
                        }
                    case AbstractTexTransGroup abstractTexTransGroup:
                        {
                            EditorGUILayout.LabelField("Group Children");
                            DrawerSummaryList(abstractTexTransGroup);
                            break;
                        }
                    case MatAndTexAbsoluteSeparator matAndTexAbsoluteSeparator:
                        {
                            MatAndTexAbsoluteSeparatorEditor.DrawerSummary(matAndTexAbsoluteSeparator);
                            break;
                        }
                    case MatAndTexRelativeSeparator matAndTexRelativeSeparator:
                        {
                            MatAndTexRelativeSeparatorEditor.DrawerSummary(matAndTexRelativeSeparator);
                            break;
                        }
                    default:
                        {
                            EditorGUILayout.LabelField("Summary None");
                            break;
                        }
                }
                EditorGUILayout.EndVertical();
            }

        }
    }
}
#endif
