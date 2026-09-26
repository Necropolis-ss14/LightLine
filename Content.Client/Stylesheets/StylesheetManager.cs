using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Content.Client.Stylesheets.Stylesheets;
using Content.Shared._Starlight.CCVar; // Starlight glass theme toggle
using Robust.Client.Graphics;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Configuration;
using Robust.Shared.Maths;
using Robust.Shared.Reflection;
using Timer = Robust.Shared.Timing.Timer;

#region Starlight
using Content.Client._Starlight;
#endregion Starlight

namespace Content.Client.Stylesheets
{
    public sealed partial class StylesheetManager : IStylesheetManager
    {
        [Dependency] private ILogManager _logManager = default!;
        [Dependency] private IUserInterfaceManager _userInterfaceManager = default!;
        [Dependency] private IReflectionManager _reflection = default!;
        [Dependency] private IConfigurationManager _cfg = default!; // Starlight glass theme toggle

        [Dependency]
        private IResourceCache
            _resCache = default!; // TODO: REMOVE (obsolete; used to construct StyleNano/StyleSpace)

        public Stylesheet SheetNanotrasen { get; private set; } = default!;
        public Stylesheet SheetSystem { get; private set; } = default!;

        public Stylesheet SheetGlassNanotrasen { get; private set; } = default!; // Starlight glass theme

        [Obsolete("Update to use SheetNanotrasen instead")]
        public Stylesheet SheetNano { get; private set; } = default!;

        [Obsolete("Update to use SheetSystem instead")]
        public Stylesheet SheetSpace { get; private set; } = default!;

        [Obsolete("Update to use SheetSystem instead")] // Starlight
        public Stylesheet Starlight { get; private set; } = default!;  //🌟Starlight🌟

        private Dictionary<string, Stylesheet> Stylesheets { get; set; } = default!;

        public bool TryGetStylesheet(string name, [MaybeNullWhen(false)] out Stylesheet stylesheet)
        {
            return Stylesheets.TryGetValue(name, out stylesheet);
        }

        /// <summary>
        /// Starlight: live swap between the opaque and translucent ("liquid glass") interface theme.
        /// The swap is wrapped in a quick fade so the change feels smooth.
        /// </summary>
        public void SetGlassTheme(bool enabled)
        {
            // Instant apply during startup, faded swap afterwards.
            if (!_glassFadeReady)
            {
                ApplyGlassTheme(enabled);
                return;
            }
            var id = ++_glassFadeId;
            FadeGlassStep(id, enabled, 0, true);
        }

        private bool _glassFadeReady;
        private int _glassFadeId;
        private const int GlassFadeSteps = 5;
        private const int GlassFadeStepMs = 1000; // TODO test: 5s per phase, restore 30ms

        private void ApplyGlassTheme(bool enabled)
        {
            _userInterfaceManager.Stylesheet = enabled ? SheetGlassNanotrasen : SheetNanotrasen;
        }

        private void FadeGlassStep(int id, bool enabled, int step, bool fadingOut)
        {
            if (id != _glassFadeId)
                return;

            var root = _userInterfaceManager.RootControl;
            if (root == null)
            {
                ApplyGlassTheme(enabled);
                return;
            }

            var overlay = GetFadeOverlay(root);
            // Fade out: blackout 0 -> 1. Fade in: blackout 1 -> 0.
            var t = (step + 1) / (float) (GlassFadeSteps + 1);
            var alpha = fadingOut ? t : 1f - t;
            overlay.PanelOverride = new StyleBoxFlat(new Color(0f, 0f, 0f, alpha));
            overlay.Visible = true;
            if (step >= GlassFadeSteps - 1)
            {
                if (fadingOut)
                {
                    ApplyGlassTheme(enabled);
                    FadeGlassStep(id, enabled, 0, false);
                }
                else
                {
                    overlay.Visible = false;
                }
                return;
            }
            Timer.Spawn(GlassFadeStepMs, () => FadeGlassStep(id, enabled, step + 1, fadingOut));
        }

        private PanelContainer? _glassFadeOverlay;

        /// <summary>
        /// Fullscreen click-through blackout rect used for the theme fade.
        /// </summary>
        private PanelContainer GetFadeOverlay(Control root)
        {
            if (_glassFadeOverlay is { Disposed: false })
                return _glassFadeOverlay;

            _glassFadeOverlay = new PanelContainer
            {
                MouseFilter = Control.MouseFilterMode.Ignore,
                Visible = false,
            };
            LayoutContainer.SetAnchorPreset(_glassFadeOverlay, LayoutContainer.LayoutPreset.Wide);
            root.AddChild(_glassFadeOverlay);
            return _glassFadeOverlay;
        }

        public HashSet<Type> UnusedSheetlets { get; private set; } = [];

        public void Initialize()
        {
            var sawmill = _logManager.GetSawmill("style");
            sawmill.Debug("Initializing Stylesheets...");
            var sw = Stopwatch.StartNew();

            // add all sheetlets to the hashset
            var tys = _reflection.FindTypesWithAttribute<CommonSheetletAttribute>();
            UnusedSheetlets = [..tys];

            Stylesheets = new Dictionary<string, Stylesheet>();
            SheetNanotrasen = Init(new NanotrasenStylesheet(new BaseStylesheet.NoConfig(), this));
            SheetSystem = Init(new SystemStylesheet(new BaseStylesheet.NoConfig(), this));
            SheetNano = new StyleNano(_resCache).Stylesheet; // TODO: REMOVE (obsolete)
            SheetSpace = new StyleSpace(_resCache).Stylesheet; // TODO: REMOVE (obsolete)
            Starlight = new StyleStarlight(_resCache).Stylesheet; //🌟Starlight🌟 TODO: REMOVE (obsolete)
            SheetGlassNanotrasen = GlassTheme.MakeGlass(SheetNanotrasen, out var glassified);
            sawmill.Debug($"Glass theme variant: {glassified} panels glassified.");
            // Applies the saved theme immediately and live-swaps on CVar change.
            _cfg.OnValueChanged(StarlightCCVars.UIGlassTheme, SetGlassTheme, true);
            _glassFadeReady = true;

            // warn about unused sheetlets
            if (UnusedSheetlets.Count > 0)
            {
                var sheetlets = UnusedSheetlets.AsEnumerable()
                    .Take(5)
                    .Select(t => t.FullName ?? "<could not get FullName>")
                    .ToArray();
                sawmill.Error($"There are unloaded sheetlets: {string.Join(", ", sheetlets)}");
            }

            sawmill.Debug($"Initialized {_styleRuleCount} style rules in {sw.Elapsed}");
        }

        private int _styleRuleCount;

        private Stylesheet Init(BaseStylesheet baseSheet)
        {
            Stylesheets.Add(baseSheet.StylesheetName, baseSheet.Stylesheet);
            _styleRuleCount += baseSheet.Stylesheet.Rules.Count;
            return baseSheet.Stylesheet;
        }
    }
}
