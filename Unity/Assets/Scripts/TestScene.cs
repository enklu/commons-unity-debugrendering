using UnityEngine;

namespace CreateAR.Commons.Unity.DebugRenderer
{
    public class TestScene : MonoBehaviour
    {
        private DebugRenderer _renderer;

        public DebugRendererMonoBehaviour Debug;

        private void Start()
        {
            _renderer = Debug.Renderer;
        }

        private void Update()
        {
            Draw();
            Draw2D();
        }

        private void Draw()
        {
            var handle = _renderer.Handle("Cubes");
            if (null != handle)
            {
                handle.Draw(ctx =>
                {
                    ctx.Cube(Vector3.zero, 1);
                    ctx.Cube(new Vector3(5, 0, 5), 2);
                    ctx.Cube(new Vector3(2, 5, 3), .5f);
                });
            }

            handle = _renderer.Handle("Pluses");
            if (null != handle)
            {
                handle.Draw(ctx =>
                {
                    ctx.Plus(Vector3.one, 1);
                    ctx.Plus(new Vector3(1, 1, 1), .25f);
                });
            }

            handle = _renderer.Handle("Line");
            if (null != handle)
            {
                handle.Draw(ctx =>
                {
                    ctx.Color(Color.green);
                    ctx.Line(Vector3.zero, Vector3.right);
                    ctx.Line(Vector3.zero, Vector3.up);
                    ctx.Line(Vector3.zero, Vector3.forward);
                });
            }

            handle = _renderer.Handle("Lines");
            if (null != handle)
            {
                handle.Draw(ctx =>
                {
                    ctx.Color(Color.red);
                    ctx.Lines(new[]
                    {
                        Vector3.zero, -Vector3.right,
                        Vector3.zero, -Vector3.up,
                        Vector3.zero, -Vector3.forward
                    });
                });
            }

            handle = _renderer.Handle("LineStrip");
            if (null != handle)
            {
                handle.Draw(ctx =>
                {
                    ctx.Color(Color.red);
                    ctx.LineStrip(new[]
                    {
                        new Vector3(-2, 0, -2),
                        new Vector3(-3, 1, -3),
                        new Vector3(-5, -4, 2)
                    });
                });
            }

            handle = _renderer.Handle("Prism");
            if (null != handle)
            {
                handle.Draw(ctx =>
                {
                    ctx.Prism(new Bounds(
                        new Vector3(3, 3, 3),
                        new Vector3(3, 2, 1)));
                });
            }
        }

        private void Draw2D()
        {
            var handle = _renderer.Handle2D("Line");
            if (null != handle)
            {
                handle.Draw(ctx =>
                {
                    ctx.Line(Vector2.zero, new Vector2(Screen.width, Screen.height));
                });
            }

            handle = _renderer.Handle2D("Lines");
            if (null != handle)
            {
                handle.Draw(ctx =>
                {
                    ctx.Lines(new []
                    {
                        new Vector2(10, 10), new Vector2(20, 10),
                        new Vector2(10, 10), new Vector2(10, 20)
                    });
                });
            }

            handle = _renderer.Handle2D("Square");
            if (null != handle)
            {
                handle.Draw(ctx =>
                {
                    ctx.Square(new Vector2(200, 40), 20);
                    ctx.Square(new Vector2(200, 40), 10);
                    ctx.Square(new Vector2(200, 40), 5);
                });
            }

            handle = _renderer.Handle2D("Rectangle");
            if (null != handle)
            {
                handle.Draw(ctx =>
                {
                    ctx.Rectangle(new Rect(
                        1, 1,
                        Screen.width - 2,
                        Screen.height - 2));
                });
            }
        }
    }
}