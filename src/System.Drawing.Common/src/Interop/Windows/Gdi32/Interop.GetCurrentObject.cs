// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Gdi32
    {
#if NET7_0_OR_GREATER
        [LibraryImport(Libraries.Gdi32)]
        public static partial IntPtr GetCurrentObject(
#else
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern IntPtr GetCurrentObject(
#endif
            IntPtr hdc,
            ObjectType type);

        public static IntPtr GetCurrentObject(HandleRef hdc, ObjectType type)
        {
            IntPtr result = GetCurrentObject(hdc.Handle, type);
            GC.KeepAlive(hdc.Wrapper);
            return result;
        }
    }
}
