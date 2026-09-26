using Content.Client.Stylesheets;
using Content.Shared._Starlight.CCVar;
using Robust.Client.Graphics;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Analyzers;
using Robust.Shared.Configuration;
using Robust.Shared.IoC;
using Robust.Shared.Maths;

namespace Content.Client.UserInterface.Controls;

/// <summary>
/// PanelContainer whose background follows the liquid glass theme toggle:
/// opaque <see cref="GlassColor"/> when glass is off, translucent whitened
/// version when it is on. Updates live on CVar change.
/// Use in place of hardcoded PanelOverride boxes in XAML.
/// </summary>
[Virtual]
public partial class GlassPanel : PanelContainer
{
    [Dependency] private IConfigurationManager _cfg = default!;

    private Color _glassColor = Color.Transparent;
    private Action<bool>? _onGlassChanged;

    /// <summary>
    /// Base (opaque) background color, settable from XAML.
    /// </summary>
    public Color GlassColor
    {
        get => _glassColor;
        set
        {
            _glassColor = value;
            UpdateBox();
        }
    }

    private float _glassContentMargin = -1f;

    /// <summary>
    /// Optional content margin applied to all sides (mirrors StyleBoxFlat ContentMargin*Override).
    /// </summary>
    public float GlassContentMargin
    {
        get => _glassContentMargin;
        set
        {
            _glassContentMargin = value;
            UpdateBox();
        }
    }

    public GlassPanel()
    {
        IoCManager.InjectDependencies(this);
        _onGlassChanged = _ => UpdateBox();
        _cfg.OnValueChanged(StarlightCCVars.UIGlassTheme, _onGlassChanged, true);
    }

    protected override void ExitedTree()
    {
        base.ExitedTree();
        if (_onGlassChanged != null)
            _cfg.UnsubValueChanged(StarlightCCVars.UIGlassTheme, _onGlassChanged);
    }

    private void UpdateBox()
    {
        if (_cfg == null!)
            return;

        var box = _cfg.GetCVar(StarlightCCVars.UIGlassTheme)
            ? new StyleBoxFlat(GlassTheme.GlassifyPanel(_glassColor))
            : new StyleBoxFlat(_glassColor);
        if (_glassContentMargin >= 0f)
        {
            box.ContentMarginTopOverride = _glassContentMargin;
            box.ContentMarginBottomOverride = _glassContentMargin;
            box.ContentMarginLeftOverride = _glassContentMargin;
            box.ContentMarginRightOverride = _glassContentMargin;
        }
        PanelOverride = box;
    }
}
