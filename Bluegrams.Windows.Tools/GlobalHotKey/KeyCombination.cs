using System;
using System.Windows.Forms;

namespace Bluegrams.Windows.Tools
{
    /// <summary>
    /// Represents a key combination out of modifiers and keys.
    /// </summary>
    public struct KeyCombination
    {
        public static readonly KeyCombination None = new KeyCombination(ModifierKeys.None, Keys.None);

        /// <summary>
        /// Holds the modifier keys of the key combination.
        /// </summary>
        public readonly ModifierKeys Modifiers;
        /// <summary>
        /// Holds the non-modifier keys of the key combination.
        /// </summary>
        public readonly Keys Keys;

        /// <summary>
        /// Creates a new instance of class KeyCombination.
        /// </summary>
        /// <param name="modifiers">The modifier keys of the key combination.</param>
        /// <param name="keys">The non-modifier keys of the key combination.</param>
        public KeyCombination(ModifierKeys modifiers, Keys keys)
        {
            Modifiers = modifiers;
            Keys = keys;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is KeyCombination other))
                return false;
            return this.Modifiers == other.Modifiers && this.Keys == other.Keys;
        }

        public override int GetHashCode()
        {
            return Modifiers.GetHashCode() + Keys.GetHashCode();
        }

        public override string ToString()
        {
            var keyConv = new KeysConverter();
            return keyConv.ConvertToString((Keys)this);
        }

        public static bool operator ==(KeyCombination first, KeyCombination second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(KeyCombination first, KeyCombination second)
        {
            return !first.Equals(second);
        }

        public static explicit operator KeyCombination(Keys keys)
        {
            Keys stripped = keys & Keys.KeyCode;
            return new KeyCombination(ExtractModifiers(keys), stripped);
        }

        public static explicit operator Keys(KeyCombination keyCombination)
        {
            return CombineKeys(keyCombination.Modifiers, keyCombination.Keys);
        }

        public static ModifierKeys ExtractModifiers(Keys keys)
        {
            ModifierKeys modifiers = ModifierKeys.None;
            if (keys.HasFlag(Keys.Alt))
                modifiers |= ModifierKeys.Alt;
            if (keys.HasFlag(Keys.Control))
                modifiers |= ModifierKeys.Ctrl;
            if (keys.HasFlag(Keys.Shift))
                modifiers |= ModifierKeys.Shift;
            if (keys.HasFlag(Keys.LWin))
                modifiers |= ModifierKeys.Win;
            return modifiers;
        }

        public static Keys CombineKeys(ModifierKeys modifiers, Keys keys)
        {
            if (modifiers.HasFlag(ModifierKeys.Alt))
                keys |= Keys.Alt;
            if (modifiers.HasFlag(ModifierKeys.Ctrl))
                keys |= Keys.Control;
            if (modifiers.HasFlag(ModifierKeys.Shift))
                keys |= Keys.Shift;
            if (modifiers.HasFlag(ModifierKeys.Win))
                keys |= Keys.LWin;
            return keys;
        }
    }

    /// <summary>
    /// Enumeration of possible modifiers for a global hot key.
    /// </summary>
    [Flags]
    public enum ModifierKeys : uint
    {
        None = 0x0000,
        Alt = 0x0001,
        Ctrl = 0x0002,
        Shift = 0x0004,
        Win = 0x0008
    }
}
