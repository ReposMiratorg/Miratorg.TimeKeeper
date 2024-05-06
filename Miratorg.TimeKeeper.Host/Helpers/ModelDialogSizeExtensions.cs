using Miratorg.TimeKeeper.Host.Components.Shared;

namespace Miratorg.TimeKeeper.Host.Helpers;

public static class ModelDialogSizeExtensions
{
    public static string ToStyleCSS(this ModalDialogSize modelDialogSize)
    {
        switch (modelDialogSize)
        {
            case ModalDialogSize.Standard:
                return string.Empty;
            case ModalDialogSize.Large:
                return "modal-lg";
            case ModalDialogSize.Small:
                return "modal-sm";
            case ModalDialogSize.Full:
                return "modal-full-width";
            case ModalDialogSize.Scrolable:
                return "modal-dialog-scrollable";

            default:
                throw new Exception($"NoDefinedValue: {modelDialogSize}");
        }
    }
}
