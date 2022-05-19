using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct RegisteredKeybind
{
    public readonly KeyCode key;
    bool isPressed;
    public readonly string KeyDwonEvent;
    public readonly string KeyUpEvent;

    public bool IsPressed()
    {
        return isPressed;
    }
    public void SetPressed(bool pressed)
    {
        isPressed = pressed;
    }
    public RegisteredKeybind(KeyCode key)
    {
        this.key = key;
        isPressed = false;
        KeyDwonEvent = this.key.ToString() + "Down";
        KeyUpEvent = this.key.ToString() + "Up";
    }
}

/*
public class InputManager : MonoBehaviour
{
    private static Hashtable registeredKeyBinds = new Hashtable();

    void Awake()
    {
        RegisterKeybinds();
    }

    void RegisterKeybinds()
    {
        KeyBinds[] keyBinds = (KeyBinds[])System.Enum.GetValues(typeof(KeyBinds));
        foreach (KeyBinds key in keyBinds)
        {
            if (registeredKeybinds.Contains(key)) continue;

            RegisteredKeybind k = new RegisteredKeybind((KeyCode)key);
            registeredKeybinds.Add(key, k);
        }
    }
    void Update()
    {
        foreach (DictionaryEntry entry in registeredKeyBinds)
        {
            RegisteredKeyBinds keybind = (RegisteredKeyBinds)entry.Value;
        }
    }
}


public enum KeyBinds
{
    JUMP_KEY = KeyCode.Space,
    ATTACK_KEY = KeyCode.J,
    INTERACT_KEY = KeyCode.C,
    MENU_KEY = KeyCode.Escape,

    UP_KEY1 = KeyCode.UpArrow,
    UP_KEY2 = KeyCode.W,
    DOWN_KEY = KeyCode.DownArrow,
    CROWCH_DASH_KEY = KeyCode.LeftShift,
    // Armour piece keys for debugging.
    CHEST_PIECE = KeyCode.Alpha1,
    GAUNTLET = KeyCode.Alpha2,
    LEGGINGS = KeyCode.Alpha3,
    SWORD = KeyCode.Alpha4,

    // This is for debug only
    DEBUG_TRIGGER = KeyCode.Alpha0
}
*/