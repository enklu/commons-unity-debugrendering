# Configuration

The idea is that in Handle2D, you provide an object with default configiration values.

```
var handle = _renderer.Handle2D(
	"Input.Points",
	new object
	{
		Color: Color.blue,
		Size: 10f
	});
if (null != handle)
{
	handle.Draw((context, config) =>
	{
		context.Color(config.Color);
		context.Square(Input.mousePosition, config.Size);
	});
}
```

# Timer

Here the idea is that we compose a `Timer` function on top of render.

```
var handle = _renderer.Handle2D("Input.Points");
handle = _renderer.Timer(5f, handle);
if (null != handle)
{
	handle.Draw(ctx => ...);
}
```
