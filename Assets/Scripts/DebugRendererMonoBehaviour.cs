using UnityEngine;

namespace CreateAR.Commons.Unity.DebugRenderer
{
    [RequireComponent(typeof(Camera))]
    public class DebugRendererMonoBehaviour : MonoBehaviour
    {
        public DebugRenderer Renderer;

        private void Awake()
        {
            Renderer = new DebugRenderer();
        }

        private void OnPostRender()
        {
            Renderer.Update(Time.deltaTime);
        }
    }
}