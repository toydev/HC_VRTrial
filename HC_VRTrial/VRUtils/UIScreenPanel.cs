using UnityEngine;

namespace HC_VRTrial.VRUtils
{
    public class UIScreenPanel
    {
        public UIScreenPanel(RenderTexture texture)
        {
            Texture = texture;
        }

        public UIScreenPanel(RenderTexture texture, Vector3 offset, Vector3 scale)
        {
            Texture = texture;
            Offset = offset;
            Scale = scale;
        }

        public RenderTexture Texture { get; private set; }
        public Vector3 Offset { get; private set; } = Vector3.zero;
        public Vector3 Scale { get; private set; } = Vector3.one;
    }
}
