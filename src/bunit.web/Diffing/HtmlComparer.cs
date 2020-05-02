using System.Collections.Generic;
using AngleSharp.Diffing.Strategies;
using AngleSharp.Diffing;
using AngleSharp.Dom;
using AngleSharp.Diffing.Core;
using AngleSharpWrappers;
using Bunit.Rendering;

namespace Bunit.Diffing
{
    /// <summary>
    /// Represents a test HTML comparer, that is configured to work with markup generated by the <see cref="TestRenderer"/> and <see cref="Htmlizer"/> classes.
    /// </summary>
    public sealed class HtmlComparer
    {
        private readonly HtmlDiffer _differenceEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlComparer"/> class.
        /// </summary>
        public HtmlComparer()
        {
            var strategy = new DiffingStrategyPipeline();
            strategy.AddDefaultOptions();
            strategy.AddFilter(BlazorDiffingHelpers.BlazorEventHandlerIdAttrFilter, StrategyType.Specialized);
            _differenceEngine = new HtmlDiffer(strategy);
        }

        /// <summary>
        /// Compares the <paramref name="controlHtml"/> with the <paramref name="testHtml"/> and returns any differences found.
        /// </summary>
        public IEnumerable<IDiff> Compare(INode controlHtml, INode testHtml)
        {
            return _differenceEngine.Compare(controlHtml.Unwrap(), testHtml.Unwrap());
        }

        /// <summary>
        /// Compares the <paramref name="controlHtml"/> with the <paramref name="testHtml"/> and returns any differences found.
        /// </summary>
        public IEnumerable<IDiff> Compare(IEnumerable<INode> controlHtml, IEnumerable<INode> testHtml)
        {
            return _differenceEngine.Compare(controlHtml.Unwrap(), testHtml.Unwrap());
        }
    }
}