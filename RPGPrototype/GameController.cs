using MonoGameLibrary;
using MonoGameLibrary.Input;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;

namespace WizardsVsWirebacks;

public static class GameController
{
    private static KeyboardInfo s_keyboard => Core.Input.Keyboard;
    private static MouseInfo s_mouse => Core.Input.Mouse;

    public static bool MoveUp()
    {
        return s_keyboard.IsKeyDown(Keys.Up) ||
            s_keyboard.IsKeyDown(Keys.W);
    }

    public static bool MoveDown()
    {
        return s_keyboard.IsKeyDown(Keys.Down) ||
            s_keyboard.IsKeyDown(Keys.S);
    }

    public static bool MoveLeft()
    {
        return s_keyboard.IsKeyDown(Keys.Left) ||
               s_keyboard.IsKeyDown(Keys.A);
    }

    public static bool MoveRight()
    {
        return s_keyboard.IsKeyDown(Keys.Right) ||
               s_keyboard.IsKeyDown(Keys.D);
    }
    //public static bool MoveUp()
    //{
    //    return s_keyboard.WasKeyJustPressed(Keys.Up) ||
    //        s_keyboard.WasKeyJustPressed(Keys.W);
    //}

    //public static bool MoveDown()
    //{
    //    return s_keyboard.WasKeyJustPressed(Keys.Down) ||
    //        s_keyboard.WasKeyJustPressed(Keys.S);
    //}

    //public static bool MoveLeft()
    //{
    //    return s_keyboard.WasKeyJustPressed(Keys.Left) ||
    //           s_keyboard.WasKeyJustPressed(Keys.A);
    //}

    //public static bool MoveRight()
    //{
    //    return s_keyboard.WasKeyJustPressed(Keys.Right) ||
    //           s_keyboard.WasKeyJustPressed(Keys.D);
    //}

    public static bool Pause()
    {
        return s_keyboard.WasKeyJustPressed(Keys.Escape);
    }

    public static bool Action()
    {
        return s_keyboard.WasKeyJustPressed(Keys.Enter);
    }
    
    public static bool Exit()
    {
        return s_keyboard.WasKeyJustPressed(Keys.Escape);
    }

    public static bool ZoomIn()
    {
        return s_mouse.ScrollWheelDelta > 0;
    }
    public static bool ZoomOut()
    {
        return s_mouse.ScrollWheelDelta < 0;
    }
    
    public static bool M1Released()
    {
        return s_mouse.WasButtonJustReleased(MouseButton.Left);
    }
    public static bool M1Clicked()
    {
        return s_mouse.WasButtonJustPressed(MouseButton.Left);
    }
    public static bool IsM1Down()
    {
        return s_mouse.IsButtonDown(MouseButton.Left);
    }
    public static Vector2 MousePosition()
    {
        return s_mouse.Position.ToVector2();
    }
    
    public static bool ToggleDebug()
    {
        return s_keyboard.WasKeyJustPressed(Keys.F11);
    }
}

