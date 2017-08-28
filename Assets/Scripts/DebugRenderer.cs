using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CreateAR.Commons.Unity.DebugRenderer
{
    public interface IFilteredRendererHandle
    {
        void Draw(Action<RenderContext> render);
    }

    public interface IFilteredRendererHandle2D
    {
        void Draw(Action<RenderContext2D> render);
    }

    public class FilteredRendererHandle : IFilteredRendererHandle
    {
        public Action<RenderContext> Action;

        public void Draw(Action<RenderContext> render)
        {
            Action = render;
        }
    }

    public class FilteredRendererHandle2D : IFilteredRendererHandle2D
    {
        public Action<RenderContext2D> Action;

        public void Draw(Action<RenderContext2D> render)
        {
            Action = render;
        }
    }

    public class DebugRenderer
    {
        private static Color _defaultColor;

        private readonly List<FilteredRendererHandle> _renderers = new List<FilteredRendererHandle>();
        private readonly List<FilteredRendererHandle2D> _renderers2D = new List<FilteredRendererHandle2D>();

        private readonly RenderContext _context;
        private readonly RenderContext2D _context2D;

        private Material _material;
        private string _filter;
        private Regex _filterRegex;

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

        public DebugRenderer()
            : this(new RenderContext(), new RenderContext2D())
        {
            // 
        }

        public DebugRenderer(RenderContext context, RenderContext2D context2D)
        {
            _context = context;
            _context2D = context2D;
            
            Filter = ".*";

            PrepareMaterial();
        }

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