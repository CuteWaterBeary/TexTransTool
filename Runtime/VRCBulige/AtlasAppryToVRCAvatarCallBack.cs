﻿#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VRC.SDKBase.Editor.BuildPipeline;
using System.Linq;
using Rs64.TexTransTool.TexturAtlas;

namespace Rs64.TexTransTool.VRCBulige
{
    [InitializeOnLoad]
    public class AtlasAppryToVRCAvatarCallBack : IVRCSDKPreprocessAvatarCallback, IVRCSDKPostprocessAvatarCallback
    {
        public int callbackOrder => -2048;//この値についてはもうすこし考えるべきだが -1024で IEditorOnlyは消滅するらしい。

        public bool OnPreprocessAvatar(GameObject avatarGameObject)
        {
            var AtlasSetAvatarTags = avatarGameObject.GetComponentsInChildren<TexTransAtlasSet>(true);
            foreach (var AtlasSetAvatarTag in AtlasSetAvatarTags)
            {
                AtlasSetAvatarTag.AtlasSet.Appry();
                MonoBehaviour.DestroyImmediate(AtlasSetAvatarTag);
            }
            return true;

        }
        public void OnPostprocessAvatar()
        {
        }

    }
}
#endif