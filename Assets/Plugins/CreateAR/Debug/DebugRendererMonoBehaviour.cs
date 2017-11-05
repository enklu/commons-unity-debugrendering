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
		/// True iff debug rendering is enabled.
		/// </summary>
		public bool Enabled = false;
        
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
			Renderer.Enabled = Enabled;
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