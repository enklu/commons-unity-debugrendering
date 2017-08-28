using UnityEngine;

namespace CreateAR.Commons.Unity.DebugRenderer
{
    public class RenderContext2DInternal : RenderContext
    {
        protected override void Setup(int mode)
        {
            GL.PushMatrix();
            GL.LoadPixelMatrix();
            GL.Begin(mode);
            GL.Color(_color);
        }
    }

    public class RenderContext2D
    {
        private readonly RenderContext2DInternal _context = new RenderContext2DInternal();

        public RenderContext2D Color(Color color)
        {
            _context.Color(color);

            return this;
        }

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

        public RenderContext2D Square(Vector2 center, float size)
        {
            return Rectangle(new Rect(
                center.x - size / 2f,
                center.y - size / 2f,
                size,
                size));
        }

        public RenderContext2D Rectangle(Rect rect)
        {
            _context.LineStrip(new[]
            {
                new Vector3(rect.xMin, rect.yMin, 0),
                new Vector3(rect.xMax, rect.yMin, 0),
                new Vector3(rect.xMax, rect.yMax, 0),
                new Vector3(rect.xMin, rect.yMax, 0),
                new Vector3(rect.xMin, rect.yMin, 0)
            });

            return this;
        }
    }
}