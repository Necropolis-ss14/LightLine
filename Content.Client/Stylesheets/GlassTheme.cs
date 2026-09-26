using System.Linq;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Shared.Maths;

namespace Content.Client.Stylesheets;

/// <summary>
/// Builds a translucent "liquid glass" variant of a stylesheet by cloning its
/// rules and lowering the alpha of opaque <see cref="StyleBoxFlat"/> panels.
/// Textured boxes are left untouched, already-translucent boxes are kept as is.
/// </summary>
public static class GlassTheme
{
    private const float GlassBackgroundAlpha = 0.8f;
    private const float GlassBorderAlpha = 0.9f;
    private const float GlassBorderLighten = 0.15f;

    public static Stylesheet MakeGlass(Stylesheet source)
    {
        var rules = source.Rules.Select(rule =>
            new StyleRule(rule.Selector, rule.Properties.Select(CloneProperty).ToArray()));
        return new Stylesheet(rules.ToArray());
    }

    private static StyleProperty CloneProperty(StyleProperty property)
    {
        if (property.Value is not StyleBoxFlat box)
            return property;

        var glass = new StyleBoxFlat(box);
        glass.BackgroundColor = Glassify(glass.BackgroundColor, GlassBackgroundAlpha, 0f);
        glass.BorderColor = Glassify(glass.BorderColor, GlassBorderAlpha, GlassBorderLighten);
        return new StyleProperty(property.Name, glass);
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
