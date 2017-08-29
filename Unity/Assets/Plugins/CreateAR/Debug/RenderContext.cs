using UnityEngine;

namespace CreateAR.Commons.Unity.DebugRenderer
{
    /// <summary>
    /// An object that actually makes the GL calls.
    /// </summary>
    public class RenderContext
    {
        /// <summary>
        /// The color to draw with.
        /// </summary>
        protected Color _color;

        /// <summary>
        /// Sets up for drawing. Must be followed with a Tearfown.
        /// </summary>
        /// <param name="mode">The GL mode to draw with.</param>
        protected virtual void Setup(int mode)
        {
            GL.PushMatrix();
            GL.LoadProjectionMatrix(Camera.main.projectionMatrix);
            GL.Begin(mode);
            GL.Color(_color);
        }

        /// <summary>
        /// Must be paired with a preceeding Setup.
        /// </summary>
        protected virtual void Teardown()
        {
            GL.End();
            GL.PopMatrix();
        }

        /// <summary>
        /// Sets the color to draw with.
        /// </summary>
        /// <param name="color">The color to draw with.</param>
        /// <returns></returns>
        public RenderContext Color(Color color)
        {
            _color = color;

            return this;
        }

        /// <summary>
        /// Draws a line segment.
        /// </summary>
        /// <param name="from">The starting point.</param>
        /// <param name="to">The end point.</param>
        /// <returns></returns>
        public RenderContext Line(Vector3 from, Vector3 to)
        {
            Setup(GL.LINES);
            {
                GL.Vertex(from);
                GL.Vertex(to);
            }
            Teardown();

            return this;
        }

        /// <summary>
        /// Draws a series of lines
        /// </summary>
        /// <param name="lines">A list of pairs of line segments.</param>
        /// <returns></returns>
        public RenderContext Lines(Vector3[] lines)
        {
            Setup(GL.LINES);
            {
                for (int i = 0, len = lines.Length; i < len; i += 2)
                {
                    GL.Vertex(lines[i]);
                    GL.Vertex(lines[i + 1]);
                }
            }
            Teardown();

            return this;
        }

        /// <summary>
        /// Draws a line strip.
        /// </summary>
        /// <param name="lines">A list of waypoints to draw along.</param>
        /// <returns></returns>
        public RenderContext LineStrip(Vector3[] lines)
        {
            Setup(GL.LINE_STRIP);
            {
                for (int i = 0, len = lines.Length; i < len; i++)
                {
                    GL.Vertex(lines[i]);
                }
            }
            Teardown();

            return this;
        }

        /// <summary>
        /// Draws a cube.
        /// </summary>
        /// <param name="center">The center of the cube.</param>
        /// <param name="size">The size of the cube.</param>
        /// <returns></returns>
        public RenderContext Cube(Vector3 center, float size)
        {
            return Prism(new Bounds(center, size * Vector3.one));
        }

        /// <summary>
        /// Draws a prism.
        /// </summary>
        /// <param name="bounds">The bounds to draw.</param>
        /// <returns></returns>
        public RenderContext Prism(Bounds bounds)
        {
            var min = bounds.min;
            var max = bounds.max;
            Setup(GL.LINES);
            {
                // bottom rect
                {
                    GL.Vertex3(min.x, min.y, min.z);
                    GL.Vertex3(min.x, min.y, max.z);

                    GL.Vertex3(min.x, min.y, max.z);
                    GL.Vertex3(max.x, min.y, max.z);

                    GL.Vertex3(max.x, min.y, max.z);
                    GL.Vertex3(max.x, min.y, min.z);

                    GL.Vertex3(max.x, min.y, min.z);
                    GL.Vertex3(min.x, min.y, min.z);
                }

                // render flat prisms differently
                if (Mathf.Abs(min.y - max.y) > Mathf.Epsilon)
                {
                    // top rect
                    {
                        GL.Vertex3(min.x, max.y, min.z);
                        GL.Vertex3(min.x, max.y, max.z);

                        GL.Vertex3(min.x, max.y, max.z);
                        GL.Vertex3(max.x, max.y, max.z);

                        GL.Vertex3(max.x, max.y, max.z);
                        GL.Vertex3(max.x, max.y, min.z);

                        GL.Vertex3(max.x, max.y, min.z);
                        GL.Vertex3(min.x, max.y, min.z);
                    }

                    // connect rects
                    {
                        GL.Vertex3(min.x, min.y, min.z);
                        GL.Vertex3(min.x, max.y, min.z);

                        GL.Vertex3(min.x, min.y, max.z);
                        GL.Vertex3(min.x, max.y, max.z);

                        GL.Vertex3(max.x, min.y, max.z);
                        GL.Vertex3(max.x, max.y, max.z);

                        GL.Vertex3(max.x, min.y, min.z);
                        GL.Vertex3(max.x, max.y, min.z);
                    }
                }
            }
            Teardown();

            return this;
        }

        /// <summary>
        /// Draws a plus sign.
        /// </summary>
        /// <param name="center">The center of the plus.</param>
        /// <param name="size">The size of the plus' extents.</param>
        /// <returns></returns>
        public RenderContext Plus(Vector3 center, float size)
        {
            Setup(GL.LINES);
            {
                GL.Vertex(center);
                GL.Vertex(center + size * Vector3.up);

                GL.Vertex(center);
                GL.Vertex(center + size * Vector3.right);

                GL.Vertex(center);
                GL.Vertex(center + size * Vector3.forward);

                GL.Vertex(center);
                GL.Vertex(center - size * Vector3.up);

                GL.Vertex(center);
                GL.Vertex(center - size * Vector3.right);

                GL.Vertex(center);
                GL.Vertex(center - size * Vector3.forward);
            }
            Teardown();

            return this;
        }
    }
}