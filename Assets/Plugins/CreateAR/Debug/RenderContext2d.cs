using UnityEngine;

namespace CreateAR.Commons.Unity.DebugRenderer
{
    /// <summary>
    /// An object that actually makes the GL calls for 2D primitives.
    /// </summary>
    public class RenderContext2D
    {
        /// <summary>
        /// A regular RenderContext we prefix with a different matrix.
        /// </summary>
        private class RenderContext2DInternal : RenderContext
        {
            /// <inheritdoc cref="RenderContext"/>
            protected override void Setup(int mode)
            {
                GL.PushMatrix();
                GL.LoadPixelMatrix();
                GL.Begin(mode);
                GL.Color(_color);
            }
        }

        /// <summary>
        /// The internal context to draw with.
        /// </summary>
        private readonly RenderContext2DInternal _context = new RenderContext2DInternal();

        /// <summary>
        /// Sets the color of primitives.
        /// </summary>
        /// <param name="color">The color to set.</param>
        /// <returns></returns>
        public RenderContext2D Color(Color color)
        {
            _context.Color(color);

            return this;
        }

        /// <summary>
        /// Draws a line segment.
        /// </summary>
        /// <param name="from">Starting point.</param>
        /// <param name="to">Ending point.</param>
        /// <returns></returns>
        public RenderContext2D Line(Vector2 from, Vector2 to)
        {
            _context.Line(from, to);

            return this;
        }

        /// <summary>
        /// Draws a list of line segments.
        /// </summary>
        /// <param name="lines">List of pairs of start, end points.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Draws a line strip.
        /// </summary>
        /// <param name="lines">A list of waypoints to draw along.</param>
        /// <returns></returns>
        public RenderContext2D LineStrip(Vector2[] lines)
        {
            var len = lines.Length;
            var cast = new Vector3[len];
            for (var i = 0; i < len; i++)
            {
                cast[i] = lines[i];
            }

            _context.LineStrip(cast);

            return this;
        }

        /// <summary>
        /// Draws a square.
        /// </summary>
        /// <param name="center">Center of the square.</param>
        /// <param name="size">The size of the square.</param>
        /// <returns></returns>
        public RenderContext2D Square(Vector2 center, float size)
        {
            return Rectangle(new Rect(
                center.x - size / 2f,
                center.y - size / 2f,
                size,
                size));
        }

        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="rect">The rect to draw.</param>
        /// <returns></returns>
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