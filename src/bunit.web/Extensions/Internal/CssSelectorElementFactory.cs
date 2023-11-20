using AngleSharp.Dom;

namespace Bunit;

internal sealed class CssSelectorElementFactory : Bunit.Web.AngleSharp.IElementFactory
{
	private readonly IRenderedFragment testTarget;
	private readonly string cssSelector;

	public Action? OnElementReplaced { get; set; }

	public CssSelectorElementFactory(IRenderedFragment testTarget, string cssSelector)
	{
		this.testTarget = testTarget;
		this.cssSelector = cssSelector;

		// Is it a problem this subscription is not being disposed of?
		testTarget.OnMarkupUpdated += FragmentsMarkupUpdated;
	}

	public TElement GetElement<TElement>() where TElement : class, INode
	{
		var queryResult = testTarget.Nodes.QuerySelector(cssSelector);
		var element = queryResult as TElement;
		return element ?? throw new ElementRemovedFromDomException(cssSelector);
	}

	private void FragmentsMarkupUpdated(object? sender, EventArgs args)
		=> OnElementReplaced?.Invoke();
}
