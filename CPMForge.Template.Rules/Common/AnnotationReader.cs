// <copyright file="AnnotationReader.cs" company="CPMForge">
// Copyright (c) 2026 Christian Prior-Mamulyan. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

using UiPath.Studio.Analyzer.Models;

namespace CPMForge.Template.Rules.Common
{
    /// <summary>
    /// Parses <c>@tag</c> and <c>@tag:VALUE</c> directives from UiPath activity/workflow annotation text.
    /// Tokenizes on whitespace and newlines; all tag name and value matching is case-insensitive.
    /// </summary>
    public static class AnnotationReader
    {
        // ── Core string-level methods ──────────────────────────────────────────

        /// <summary>True if the annotation contains <c>@suppress:RULE-ID</c> for the given rule ID (all tokens checked).</summary>
        public static bool IsSuppressed(string? annotation, string ruleId)
            => Tokenize(annotation).Any(t =>
                t.StartsWith("@suppress:", StringComparison.OrdinalIgnoreCase) &&
                t.Substring("@suppress:".Length).Equals(ruleId, StringComparison.OrdinalIgnoreCase));

        /// <summary>True if the annotation contains <c>@nocheck</c> (suppresses all rules).</summary>
        public static bool IsNoCheck(string? annotation)
            => HasTag(annotation, "@nocheck");

        /// <summary>
        /// True if the annotation contains the given tag.
        /// Matches both bare form (<c>@unit</c>) and value form (<c>@unit:VALUE</c>).
        /// </summary>
        public static bool HasTag(string? annotation, string tag)
        {
            var bare = tag.TrimEnd(':');
            var prefix = bare + ":";
            return Tokenize(annotation).Any(t =>
                t.Equals(bare, StringComparison.OrdinalIgnoreCase) ||
                t.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Returns the value after the colon for <c>@tag:VALUE</c>, or <c>null</c> if the tag is absent or has no value.
        /// </summary>
        public static string? GetTagValue(string? annotation, string tag)
        {
            var prefix = tag.TrimEnd(':') + ":";
            var tokens = Tokenize(annotation).ToList();
            for (var i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    var value = tokens[i].Substring(prefix.Length);
                    if (value.Length == 0 && i + 1 < tokens.Count)
                    {
                        return tokens[i + 1];
                    }

                    return value;
                }
            }

            return null;
        }

        // ── Model-aware convenience overloads ──────────────────────────────────

        /// <summary>True if the activity's annotation contains <c>@suppress:RULE-ID</c>.</summary>
        public static bool IsSuppressed(IActivityModel? activity, string ruleId)
            => IsSuppressed(activity?.AnnotationText, ruleId);

        /// <summary>True if the activity's annotation contains <c>@nocheck</c>.</summary>
        public static bool IsNoCheck(IActivityModel? activity)
            => IsNoCheck(activity?.AnnotationText);

        // ── Internal ──────────────────────────────────────────────────────────
        private static IEnumerable<string> Tokenize(string? annotation)
            => annotation?.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
               ?? Enumerable.Empty<string>();
    }
}
