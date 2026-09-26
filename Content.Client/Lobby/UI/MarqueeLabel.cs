using System.Globalization;
using System.Linq;
using Robust.Client.UserInterface.Controls;
using Timer = Robust.Shared.Timing.Timer;

namespace Content.Client.Lobby.UI;

/// <summary>
/// Label with old-player-style marquee scrolling when the text overflows.
/// </summary>
public sealed partial class MarqueeLabel : Label
{
    private const int ScrollMs = 250;
    private const string Gap = "   •   ";

    private string _fullText = string.Empty;
    private int _offset;
    private bool _running;

    /// <summary>
    /// Full text. Scrolls automatically when it does not fit.
    /// </summary>
    public string FullText
    {
        get => _fullText;
        set
        {
            _fullText = value ?? string.Empty;
            _offset = 0;
            UpdateScroll();
        }
    }

    protected override void EnteredTree()
    {
        base.EnteredTree();
        _running = true;
        Tick();
    }

    protected override void ExitedTree()
    {
        base.ExitedTree();
        _running = false;
    }

    private void Tick()
    {
        if (!_running)
            return;
        UpdateScroll();
        Timer.Spawn(ScrollMs, Tick);
    }

    private void UpdateScroll()
    {
        if (!NeedsScroll())
        {
            if (Text != _fullText)
                Text = _fullText;
            return;
        }

        var elements = new List<string>();
        var enumerator = StringInfo.GetTextElementEnumerator(_fullText + Gap);
        while (enumerator.MoveNext())
            elements.Add(enumerator.GetTextElement());

        if (elements.Count == 0)
        {
            Text = _fullText;
            return;
        }

        _offset %= elements.Count;
        var rotated = elements.Skip(_offset).Concat(elements.Take(_offset));
        Text = string.Concat(rotated);
        _offset = (_offset + 1) % elements.Count;
    }

    private bool NeedsScroll()
    {
        if (string.IsNullOrEmpty(_fullText) || Size.X <= 0)
            return false;
        // Rough average glyph width for the default UI font.
        return _fullText.Length * 6.6f > Size.X;
    }
}
