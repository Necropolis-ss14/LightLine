using System.Collections.Generic;
using System.Linq;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Maths;

namespace Content.Client.Stylesheets;

/// <summary>
/// Builds a light "liquid glass" (iPhone-style white frosted) variant of a stylesheet:
/// panel backgrounds become translucent white, light text is darkened for readability.
/// Textured buttons and icons are left untouched to preserve their look.
/// </summary>
public static class GlassTheme
{
    private const float GlassWhiteMix = 0.8f;
    private const float GlassBackgroundAlpha = 0.87f;
    private const float GlassTextDarken = 0.75f;
    private const float GlassTextMinLuminance = 0.55f;

    private static readonly Color GlassBorder = new(0.82f, 0.82f, 0.84f, 0.9f);

    public static Stylesheet MakeGlass(Stylesheet source, out int glassifiedBoxes)
    {
        var count = 0;
        var rules = new List<StyleRule>();
        foreach (var rule in source.Rules)
            rules.Add(CloneRule(rule, ref count));
        glassifiedBoxes = count;
        return new Stylesheet(rules.ToArray());
    }

    private static StyleRule CloneRule(StyleRule rule, ref int count)
    {
        var props = new List<StyleProperty>();
        foreach (var prop in rule.Properties)
            props.Add(CloneProperty(prop, ref count));
        return new StyleRule(rule.Selector, props);
    }

    private static StyleProperty CloneProperty(StyleProperty property, ref int count)
    {
        // Flat panel backgrounds -> translucent white.
        if (property.Value is StyleBoxFlat box)
        {
            var bg = box.BackgroundColor;
            if (bg.A >= 0.99f)
            {
                var glass = new StyleBoxFlat(box)
                {
                    BackgroundColor = Whiten(bg),
                    BorderColor = GlassBorder,
                };
                count++;
                return new StyleProperty(property.Name, glass);
            }
            return property;
        }

        // Textured panel backgrounds (windows, chat, ...) -> white flat glass.
        if (property.Name == PanelContainer.StylePropertyPanel && property.Value is StyleBoxTexture)
        {
            var glass = new StyleBoxFlat
            {
                BackgroundColor = new Color(GlassTint.R, GlassTint.G, GlassTint.B, GlassBackgroundAlpha),
                BorderColor = GlassBorder,
                BorderThickness = new Thickness(2f),
            };
            count++;
            return new StyleProperty(property.Name, glass);
        }

        // Light text -> dark for readability on white glass.
        if (property.Value is Color color
            && property.Name.Contains("font")
            && Luminance(color) > GlassTextMinLuminance
            && color.A >= 0.99f)
        {
            var dark = Darken(color);
            count++;
            return new StyleProperty(property.Name, dark);
        }

        return property;
    }

    /// <summary>
    /// Translucent whitened variant of an opaque panel color, for hardcoded backgrounds.
    /// </summary>
    public static Color GlassifyPanel(Color color)
    {
        if (color.A < 0.99f)
            return color;
        return Whiten(color);
    }

    // Light gray frosted tint (iPhone-style, but not eye-searing white).
    private static readonly Color GlassTint = new(0.82f, 0.82f, 0.85f);

    private static Color Whiten(Color color)
    {
        var r = color.R + (GlassTint.R - color.R) * GlassWhiteMix;
        var g = color.G + (GlassTint.G - color.G) * GlassWhiteMix;
        var b = color.B + (GlassTint.B - color.B) * GlassWhiteMix;
        return new Color(r, g, b, GlassBackgroundAlpha);
    }

    private static Color Darken(Color color)
    {
        var r = color.R * (1f - GlassTextDarken);
        var g = color.G * (1f - GlassTextDarken);
        var b = color.B * (1f - GlassTextDarken);
        return new Color(r, g, b, color.A);
    }

    private static float Luminance(Color color)
    {
        return 0.299f * color.R + 0.587f * color.G + 0.114f * color.B;
    }
}
