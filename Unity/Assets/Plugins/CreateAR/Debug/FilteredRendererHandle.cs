using System;

namespace CreateAR.Commons.Unity.DebugRenderer
{
    /// <summary>
    /// Provides an interface for calling a render action.
    /// </summary>
    public interface IFilteredRendererHandle
    {
        /// <summary>
        /// Takes an action that is executed with drawing methods.
        /// </summary>
        /// <param name="render">Called when rendering is ready.</param>
        void Draw(Action<RenderContext> render);
    }

    /// <summary>
    /// Provides an interface for calling a 2D render action.
    /// </summary>
    public interface IFilteredRendererHandle2D
    {
        /// <summary>
        /// Takes an action that is executed with drawing methods.
        /// </summary>
        /// <param name="render">Called when rendering is ready.</param>
        void Draw(Action<RenderContext2D> render);
    }

    /// <summary>
    /// Implementation which hides the Action.
    /// </summary>
    public class FilteredRendererHandle2D : IFilteredRendererHandle2D
    {
        /// <summary>
        /// The action to draw with.
        /// </summary>
        public Action<RenderContext2D> Action;

        /// <inheritdoc cref="IFilteredRendererHandle"/>
        public void Draw(Action<RenderContext2D> render)
        {
            Action = render;
        }
    }

    /// <summary>
    /// Implementation which hides the Action.
    /// </summary>
    public class FilteredRendererHandle : IFilteredRendererHandle
    {
        /// <summary>
        /// The action to draw with.
        /// </summary>
        public Action<RenderContext> Action;

        /// <inheritdoc cref="IFilteredRendererHandle"/>
        public void Draw(Action<RenderContext> render)
        {
            Action = render;
        }
    }
}