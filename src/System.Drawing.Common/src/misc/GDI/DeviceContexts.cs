// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Drawing.Internal;

/// <summary>
/// Keeps a cache of some graphics primitives. Created to improve performance of TextRenderer.MeasureText methods
/// that don't receive a WindowsGraphics. This class maintains a cache of MRU WindowsFont objects in the process.
/// </summary>
internal static class DeviceContexts
{
    [ThreadStatic]
    private static ClientUtils.WeakRefCollection? t_activeDeviceContexts;

    /// <summary>
    /// WindowsGraphicsCacheManager needs to track DeviceContext objects so it can ask them if a font is in use
    /// before they it's deleted.
    /// </summary>
    internal static void AddDeviceContext(DeviceContext dc)
    {
        ClientUtils.WeakRefCollection wrc = t_activeDeviceContexts ??= new ClientUtils.WeakRefCollection()
        {
            RefCheckThreshold = 20
        };

        if (!wrc.Contains(dc))
        {
            dc.Disposing += new EventHandler(OnDcDisposing);
            wrc.Add(dc);
        }
    }

    private static void OnDcDisposing(object? sender, EventArgs e)
    {
        if (sender is DeviceContext dc)
        {
            dc.Disposing -= new EventHandler(OnDcDisposing);
            RemoveDeviceContext(dc);
        }
    }

    internal static void RemoveDeviceContext(DeviceContext dc)
    {
        t_activeDeviceContexts?.RemoveByHashCode(dc);
    }
}
