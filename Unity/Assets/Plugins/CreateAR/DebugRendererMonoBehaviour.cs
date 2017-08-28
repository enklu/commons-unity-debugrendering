using UnityEngine;

namespace CreateAR.Commons.Unity.DebugRenderer
{
    /// <summary>
    /// MonoBehaviour that can motor the DebugRenderer.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class DebugRendererMonoBehaviour : MonoBehaviour
    {
        /// <summary>
        /// DebugRenderer implementation.
        /// </summary>
        public DebugRenderer Renderer { get; private set; }

        /// <summary>
        /// Reflects Renderer filter.
        /// </summary>
        public string Filter = ".*";
        
        /// <summary>
        /// Called to initialize the object.
        /// </summary>
        private void Awake()
        {
            Renderer = new DebugRenderer();
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        private void Update()
        {
            Renderer.Filter = Filter;
        }

        /// <summary>
        /// Called to make GL calls.
        /// </summary>
        private void OnPostRender()
        {
            Renderer.Update(Time.deltaTime);
        }
    }
}