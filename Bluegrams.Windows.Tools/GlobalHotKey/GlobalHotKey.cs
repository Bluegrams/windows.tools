using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Bluegrams.Windows.Tools
{
    /// <summary>
    /// Wraps a global Windows hotkey.
    /// </summary>
    public class GlobalHotKey : IDisposable
    {
        private static Dictionary<int, GlobalHotKey> hotKeyDict;

        #region Win Api methods
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, UInt32 fsModifiers, UInt32 vlc);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public const int WM_HOTKEY = 0x0312;
        #endregion

        private bool _disposed = false;

        /// <summary>
        /// The key combination associated with the hot key.
        /// </summary>
        public KeyCombination KeyCombination { get; private set; }
        /// <summary>
        /// The action be executed when the hot key is pressed.
        /// </summary>
        public Action<GlobalHotKey> Action { get; private set; }
        /// <summary>
        /// Identifier of the hot key.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the hot key is registered.
        /// </summary>
        public bool Registered { get; private set; }

        /// <summary>
        /// Creates a new instance of class GlobalHotKey.
        /// </summary>
        /// <param name="keyCombination">The key combination associated with the hot key.</param>
        /// <param name="action">The action be executed when the hot key is pressed.</param>
        /// <param name="register">If set to true, directly registers the hot key.</param>
        public GlobalHotKey(KeyCombination keyCombination, Action<GlobalHotKey> action, bool register = false)
        {
            KeyCombination = keyCombination;
            Action = action;
            if (register)
            {
                Register();
            }
        }

        /// <summary>
        /// Globally registers a hot key.
        /// </summary>
        public bool Register()
        {
            if (hotKeyDict == null)
            {
                hotKeyDict = new Dictionary<int, GlobalHotKey>();
                Application.AddMessageFilter(new HotKeyMessageFilter());
            }
            Id = hotKeyDict.Count;
            Registered = RegisterHotKey(IntPtr.Zero, Id, (uint)KeyCombination.Modifiers, (uint)KeyCombination.Keys);
            hotKeyDict.Add(Id, this);
            return Registered;
        }

        /// <summary>
        /// Unregisters a registered hot key.
        /// </summary>
        public void Unregister()
        {
            if (hotKeyDict.ContainsKey(Id))
            {
                Registered = !UnregisterHotKey(IntPtr.Zero, Id);
                hotKeyDict.Remove(Id);
            }
            else Registered = false;
        }

        private class HotKeyMessageFilter : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {
                    if (hotKeyDict.TryGetValue((int)m.WParam, out GlobalHotKey hotKey))
                    {
                        hotKey.Action?.Invoke(hotKey);
                        return true;
                    }
                }
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Unregister();
                }
                _disposed = true;
            }
        }
    }
}
