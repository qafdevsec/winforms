// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Runtime.InteropServices;
#if NET7_0_OR_GREATER
using System.Runtime.InteropServices.Marshalling;
#endif

internal static partial class Interop
{
    internal static partial class Gdi32
    {
#if NET7_0_OR_GREATER
        [LibraryImport(Libraries.Gdi32, EntryPoint = "ResetDCW", SetLastError = true)]
        internal static partial IntPtr /*HDC*/ ResetDC(
            [MarshalUsing(typeof(HandleRefMarshaller))]
#else
        [DllImport(Libraries.Gdi32, SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr /*HDC*/ ResetDC(
#endif
            HandleRef hDC,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef /*DEVMODE*/ lpDevMode);
    }
}
