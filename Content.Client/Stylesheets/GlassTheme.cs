using System.Collections.Generic;
using System.Linq;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Maths;

namespace Content.Client.Stylesheets;

/// <summary>
/// Builds a translucent "liquid glass" variant of a stylesheet by cloning its
/// rules and lowering the alpha of opaque <see cref="StyleBoxFlat"/> panels.
/// Textured window backgrounds (which have no alpha of their own) are made
/// translucent via modulate-self. Already-translucent boxes are kept as is.
/// </summary>
public static class GlassTheme
{
    private const float GlassBackgroundAlpha = 0.8f;
    private const float GlassBorderAlpha = 0.9f;
    private const float GlassBorderLighten = 0.15f;
    private const float GlassPanelAlpha = 0.8f;
    private const float GlassWhiten = 0.12f;

    public static Stylesheet MakeGlass(Stylesheet source, out int glassifiedBoxes)
    {
        var count = 0;
        var rules = source.Rules.Select(rule => CloneRule(rule, ref count));
        var sheet = new Stylesheet(rules.ToArray());
        glassifiedBoxes = count;
        return sheet;
    }

    private static StyleRule CloneRule(StyleRule rule, ref int count)
    {
        var props = new List<StyleProperty>();
        foreach (var prop in rule.Properties)
            props.Add(CloneProperty(prop, ref count));
        // Textured panels (window backgrounds, chat panels, ...) have no alpha of
        // their own, so make them translucent via modulate-self instead.
        // Existing modulate-self tints (e.g. PDA) get their alpha scaled, not skipped.
        var panelIdx = props.FindIndex(p => p.Name == PanelContainer.StylePropertyPanel
            && p.Value is StyleBoxTexture);
        if (panelIdx >= 0)
        {
            var modIdx = props.FindIndex(p => p.Name == Control.StylePropertyModulateSelf
                && p.Value is Color c && c.A >= 0.99f);
            if (modIdx >= 0)
            {
                var tint = (Color) props[modIdx].Value;
                props[modIdx] = new StyleProperty(Control.StylePropertyModulateSelf,
                    Glassify(tint, GlassPanelAlpha, GlassWhiten));
                count++;
            }
            else if (!props.Any(p => p.Name == Control.StylePropertyModulateSelf))
            {
                props.Add(new StyleProperty(Control.StylePropertyModulateSelf,
                    new Color(1f, 1f, 1f, GlassPanelAlpha)));
                count++;
            }
        }
        return new StyleRule(rule.Selector, props);
    }

    private static StyleProperty CloneProperty(StyleProperty property, ref int count)
    {
        if (property.Value is not StyleBoxFlat box)
            return property;

        var glass = new StyleBoxFlat(box);
        var bg = Glassify(glass.BackgroundColor, GlassBackgroundAlpha, GlassWhiten);
        if (bg.A < glass.BackgroundColor.A)
            count++;
        glass.BackgroundColor = bg;
        glass.BorderColor = Glassify(glass.BorderColor, GlassBorderAlpha, GlassBorderLighten);
        return new StyleProperty(property.Name, glass);
    }

    /// <summary>
    /// Translucent whitened variant of an opaque panel color, for hardcoded backgrounds.
    /// </summary>
    public static Color GlassifyPanel(Color color)
    {
        return Glassify(color, GlassBackgroundAlpha, GlassWhiten);
    }

    private static Color Glassify(Color color, float alpha, float lighten)
    {
        if (color.A < 0.99f)
            return color;

        var r = color.R + (1f - color.R) * lighten;
        var g = color.G + (1f - color.G) * lighten;
        var b = color.B + (1f - color.B) * lighten;
        return new Color(r, g, b, alpha);
    }
}
