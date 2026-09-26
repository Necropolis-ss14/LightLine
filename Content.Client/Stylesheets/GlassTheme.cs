using System.Collections.Generic;
using System.Linq;
using Content.Client.Resources;
using Robust.Client.Graphics;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Maths;

namespace Content.Client.Stylesheets;

/// <summary>
/// Builds a frosted "liquid glass" variant of a stylesheet: opaque panel
/// backgrounds are replaced with a fine noise texture tinted translucent
/// (real backdrop blur is impossible without engine render-pipeline changes,
/// this grain is the closest content-side approximation).
/// Bordered flat boxes keep their shape and are only made translucent.
/// </summary>
public static class GlassTheme
{
    public const string NoiseTexturePath = "/Textures/Interface/Nano/glass_noise.png";

    private const float GlassBackgroundAlpha = 0.8f;
    private const float GlassBorderAlpha = 0.9f;
    private const float GlassBorderLighten = 0.15f;
    private const float GlassWhiten = 0.12f;

    public static Stylesheet MakeGlass(Stylesheet source, Texture noise, out int glassifiedBoxes)
    {
        var state = new FrostState { Noise = noise };
        var rules = new List<StyleRule>();
        foreach (var rule in source.Rules)
            rules.Add(CloneRule(rule, state));
        glassifiedBoxes = state.Count;
        return new Stylesheet(rules.ToArray());
    }

    public static Texture LoadNoise(IResourceCache resCache)
    {
        return resCache.GetTexture(NoiseTexturePath);
    }

    private sealed class FrostState
    {
        public Texture Noise = default!;
        public int Count;
    }

    private static StyleRule CloneRule(StyleRule rule, FrostState state)
    {
        var props = new List<StyleProperty>();
        foreach (var prop in rule.Properties)
            props.Add(CloneProperty(prop, state));

        var panelIdx = props.FindIndex(p => p.Name == PanelContainer.StylePropertyPanel
            && p.Value is StyleBox);
        if (panelIdx < 0)
            return new StyleRule(rule.Selector, props);

        var frosted = FrostBox((StyleBox) props[panelIdx].Value, state.Noise, out var tint);
        if (frosted == null || tint == null)
            return new StyleRule(rule.Selector, props);

        props[panelIdx] = new StyleProperty(PanelContainer.StylePropertyPanel, frosted);
        ApplyTint(props, tint.Value);
        state.Count++;
        return new StyleRule(rule.Selector, props);
    }

    /// <summary>
    /// Returns a frosted replacement box plus the tint color, or nulls when
    /// the box is already translucent and should be left alone.
    /// </summary>
    private static StyleBox? FrostBox(StyleBox box, Texture noise, out Color? tint)
    {
        tint = null;
        if (box is StyleBoxFlat flat)
        {
            var bg = flat.BackgroundColor;
            if (bg.A < 0.99f)
                return null;
            // Bordered boxes keep their shape, only go translucent.
            if (HasBorder(flat))
            {
                var shaped = new StyleBoxFlat(flat)
                {
                    BackgroundColor = Glassify(bg, GlassBackgroundAlpha, GlassWhiten),
                    BorderColor = Glassify(flat.BorderColor, GlassBorderAlpha, GlassBorderLighten),
                };
                tint = new Color(1f, 1f, 1f, 1f);
                return shaped;
            }
            tint = Glassify(bg, GlassBackgroundAlpha, GlassWhiten);
            return new StyleBoxTexture { Texture = noise };
        }
        if (box is StyleBoxTexture)
        {
            tint = new Color(1f, 1f, 1f, GlassBackgroundAlpha);
            return new StyleBoxTexture { Texture = noise };
        }
        return null;
    }

    private static void ApplyTint(List<StyleProperty> props, Color tint)
    {
        var modIdx = props.FindIndex(p => p.Name == Control.StylePropertyModulateSelf
            && p.Value is Color c && c.A >= 0.99f);
        if (modIdx >= 0)
        {
            var existing = (Color) props[modIdx].Value;
            props[modIdx] = new StyleProperty(Control.StylePropertyModulateSelf,
                Multiply(existing, tint));
        }
        else if (!props.Any(p => p.Name == Control.StylePropertyModulateSelf))
        {
            props.Add(new StyleProperty(Control.StylePropertyModulateSelf, tint));
        }
    }

    private static bool HasBorder(StyleBoxFlat box)
    {
        var (l, t, r, b) = box.BorderThickness;
        return l > 0f || t > 0f || r > 0f || b > 0f;
    }

    private static Color Multiply(Color a, Color b)
    {
        return new Color(a.R * b.R, a.G * b.G, a.B * b.B, a.A * b.A);
    }

    private static StyleProperty CloneProperty(StyleProperty property, FrostState state)
    {
        // Tinted modulate-self values on non-panel props are left alone;
        // panel tints are handled in CloneRule.
        return property;
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
