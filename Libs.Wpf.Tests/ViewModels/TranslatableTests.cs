namespace Libs.Wpf.Tests.ViewModels;

using System.Globalization;
using Libs.Wpf.Localization;
using Libs.Wpf.ViewModels;

public class TranslatableTests
{
    [Fact]
    public void Ctor_de()
    {
        var translatable = new Translatable(
            Translation.ResourceManager,
            nameof(Translation.Label),
            nameof(Translation.ToolTip),
            nameof(Translation.Watermark));

        TranslationSource.Instance.CurrentCulture = new CultureInfo("de-DE");

        Assert.Equal(
            null,
            translatable.ErrorResourceKey);
        Assert.Equal(
            false,
            translatable.HasError);
        Assert.Equal(
            "LabelText (de)",
            translatable.LabelTranslation);
        Assert.Equal(
            "ToolTipText (de)",
            translatable.ToolTipTranslation);
        Assert.Equal(
            "WatermarkText (de)",
            translatable.WatermarkTranslation);
    }

    [Fact]
    public void Ctor_en()
    {
        var translatable = new Translatable(
            Translation.ResourceManager,
            nameof(Translation.Label),
            nameof(Translation.ToolTip),
            nameof(Translation.Watermark));

        TranslationSource.Instance.CurrentCulture = new CultureInfo("en-US");

        Assert.Equal(
            null,
            translatable.ErrorResourceKey);
        Assert.Equal(
            false,
            translatable.HasError);
        Assert.Equal(
            "LabelText (en)",
            translatable.LabelTranslation);
        Assert.Equal(
            "ToolTipText (en)",
            translatable.ToolTipTranslation);
        Assert.Equal(
            "WatermarkText (en)",
            translatable.WatermarkTranslation);
    }
}
