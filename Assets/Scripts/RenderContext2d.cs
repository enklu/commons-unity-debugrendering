using UnityEngine;

namespace CreateAR.Commons.Unity.DebugRenderer
{
    public class RenderContext2DInternal : RenderContext
    {
        protected override void Setup(int mode)
        {
            GL.Begin(mode);
            GL.Color(_color);
            GL.LoadPixelMatrix();
        }
    }

    public class RenderContext2D
    {
        private readonly RenderContext2DInternal _context = new RenderContext2DInternal();

        public RenderContext2D Line(Vector2 from, Vector2 to)
        {
            _context.Line(from, to);

            return this;
        }

        public RenderContext2D Lines(Vector2[] lines)
        {
            var len = lines.Length;
            var cast = new Vector3[len];
            for (var i = 0; i < len; i++)
            {
                cast[i] = lines[i];
            }

            _context.Lines(cast);

            return this;
        }
    }
}