// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global
#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation.WindowsAPI
{
    using System;

    [Flags]
    public enum TOUCH_MASK
    {
        /// <summary>
        /// Default. None of the optional fields are valid.
        /// </summary>
        NONE = 0x00000000,

        /// <summary>
        /// <see cref="POINTER_TOUCH_INFO.Contact"/> of the <see cref="POINTER_TOUCH_INFO"/> structure is valid.
        /// </summary>
        CONTACTAREA = 0x00000001,

        /// <summary>
        /// <see cref="POINTER_TOUCH_INFO.Orientation"/> of the <see cref="POINTER_TOUCH_INFO"/> structure is valid.
        /// </summary>
        ORIENTATION = 0x00000002,

        /// <summary>
        /// <see cref="POINTER_TOUCH_INFO.Pressure"/> of the <see cref="POINTER_TOUCH_INFO"/> structure is valid.
        /// </summary>
        PRESSURE = 0x00000004,
    }
}
