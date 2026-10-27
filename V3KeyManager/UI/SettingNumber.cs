using System.Globalization;

namespace V3KeyManager.UI;

/// <summary>Commits typed edits before commands, without displaying 28 trailing decimal places.</summary>
internal sealed class SettingNumber : NumericUpDown
{
    internal bool TrimFraction;
    internal decimal CommittedValue;

    // Reading NumericUpDown.Value itself commits text; compare with our model snapshot instead.
    internal bool HasPendingEdit => !decimal.TryParse(Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var number) || number != CommittedValue;

    internal void Commit()
    {
        ValidateEditText();
        // The base control leaves an empty string or a lone minus sign visible.
        // Normalize those unfinished edits before saving or leaving this page.
        ChangingText = true;
        Text = Value.ToString(TrimFraction ? "0.0###########################" : "0", CultureInfo.CurrentCulture);
    }

    protected override void UpdateEditText()
    {
        base.UpdateEditText();
        if (TrimFraction)
        {
            ChangingText = true;
            Text = Value.ToString("0.0###########################", CultureInfo.CurrentCulture);
        }
    }
}
