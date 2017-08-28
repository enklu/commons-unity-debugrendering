using UnityEngine;

namespace CreateAR.Commons.Unity.DebugRenderer
{
    [RequireComponent(typeof(Camera))]
    public class DebugRendererMonoBehaviour : MonoBehaviour
    {
        public DebugRenderer Renderer;
        public string Filter = ".*";
        
        private void Awake()
        {
            Renderer = new DebugRenderer();
        }

        private void Update()
        {
            Renderer.Filter = Filter;
        }

        private void OnPostRender()
        {
            Renderer.Update(Time.deltaTime);
        }
    }
}