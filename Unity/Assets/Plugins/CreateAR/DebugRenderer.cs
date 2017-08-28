using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CreateAR.Commons.Unity.DebugRenderer
{
    /// <summary>
    /// An API for rendering debug primitives.
    /// </summary>
    public class DebugRenderer
    {
        /// <summary>
        /// The default color of controls.
        /// </summary>
        private static Color _defaultColor = Color.white;

        /// <summary>
        /// List of actions that can render.
        /// </summary>
        private readonly List<FilteredRendererHandle> _renderers = new List<FilteredRendererHandle>();

        /// <summary>
        /// List of actions that can render in 2D.
        /// </summary>
        private readonly List<FilteredRendererHandle2D> _renderers2D = new List<FilteredRendererHandle2D>();

        /// <summary>
        /// The context with which to draw.
        /// </summary>
        private readonly RenderContext _context;

        /// <summary>
        /// The context with which to draw 2D primitives.
        /// </summary>
        private readonly RenderContext2D _context2D;

        /// <summary>
        /// The material to draw with, programmatically generated.
        /// </summary>
        private Material _material;

        /// <summary>
        /// Backing variable for Filter property.
        /// </summary>
        private string _filter;

        /// <summary>
        /// Lazily constructed Regex object for filtering.
        /// </summary>
        private Regex _filterRegex;

        /// <summary>
        /// Gets/sets the category filter.
        /// </summary>
        public string Filter
        {
            get { return _filter; }
            set
            {
                value = value ?? string.Empty;

                if (value == _filter)
                {
                    return;
                }

                _filter = value;
                _filterRegex = new Regex(_filter);
            }
        }

        /// <summary>
        /// Creates a new DebugRenderer.
        /// </summary>
        public DebugRenderer()
            : this(new RenderContext(), new RenderContext2D())
        {
            // 
        }

        /// <summary>
        /// Creates a new DebugRenderer with custom contexts.
        /// </summary>
        /// <param name="context">The context for 3D actions.</param>
        /// <param name="context2D">The context for 2D actions.</param>
        public DebugRenderer(RenderContext context, RenderContext2D context2D)
        {
            _context = context;
            _context2D = context2D;
            
            Filter = ".*";

            PrepareMaterial();
        }

        /// <summary>
        /// Retrieves a handle for drawing.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>A handle, or null if the current category is filtered out.</returns>
        public IFilteredRendererHandle Handle(string category)
        {
#if !DEBUG_RENDERING
            return null;
#endif

            if (_filterRegex.IsMatch(category))
            {
                var renderer = new FilteredRendererHandle();
                _renderers.Add(renderer);

                return renderer;
            }

            return null;
        }

        /// <summary>
        /// Retrieves a handle for drawing.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>A handle, or null if the current category is filtered out.</returns>
        public IFilteredRendererHandle2D Handle2D(string category)
        {
#if !DEBUG_RENDERING
            return null;
#endif

            if (_filterRegex.IsMatch(category))
            {
                var renderer = new FilteredRendererHandle2D();
                _renderers2D.Add(renderer);

                return renderer;
            }

            return null;
        }

        /// <summary>
        /// Must be called every OnPostRender.
        /// </summary>
        /// <param name="dt">The time that has passed since last Update.</param>
        public void Update(float dt)
        {
            _material.SetPass(0);
            
            for (int i = 0, len = _renderers.Count; i < len; i++)
            {
                _renderers[i].Action(_context);

                _defaultColor = Color.white;
                _context.Color(_defaultColor);
            }
            _renderers.Clear();

            for (int i = 0, len = _renderers2D.Count; i < len; i++)
            {
                _renderers2D[i].Action(_context2D);

                _defaultColor = Color.white;
                _context2D.Color(_defaultColor);
            }
            _renderers2D.Clear();
        }
        
        /// <summary>
        /// Prepares a material for drawing.
        /// </summary>
        protected void PrepareMaterial()
        {
            _material = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = HideFlags.HideAndDontSave
            };

            _material.SetInt("_Cull", (int) UnityEngine.Rendering.CullMode.Off);
            _material.SetInt("_ZWrite", 0);
        }
    }
}