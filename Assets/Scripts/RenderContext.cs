using UnityEngine;

namespace CreateAR.Commons.Unity.DebugRenderer
{
    public class RenderContext
    {
        protected Color _color;

        protected virtual void Setup(int mode)
        {
            GL.Begin(mode);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Color(_color);
        }

        protected virtual void Teardown()
        {
            GL.PopMatrix();
            GL.End();
        }

        public RenderContext Color(Color color)
        {
            _color = color;

            return this;
        }

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

        public RenderContext Cube(Vector3 center, float size)
        {
            return Prism(new Bounds(center, size * Vector3.one));
        }

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