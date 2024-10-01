using Microsoft.Xna.Framework.Input;

namespace vampire;

public class CInput
{
    public Axis CreateAxis(Keys one, Keys two)
    {
        return new Axis(one, two);
    }

    public class Axis
    {
        public Axis(Keys one, Keys two)
        {
            KeyOne = one;
            KeyTwo = two;
        }

        public int Value = 0;

        public Keys KeyOne;
        public Keys KeyTwo;

        public KeyState CurrentKeyState;
        public KeyState PreviousKeyState;

        public struct KeyState
        {
            public bool KeyOnePressed;
            public bool KeyTwoPressed;
        }

        public void Update()
        {
            PreviousKeyState.KeyOnePressed = CurrentKeyState.KeyOnePressed;
            PreviousKeyState.KeyTwoPressed = CurrentKeyState.KeyTwoPressed;
            CurrentKeyState.KeyOnePressed = Keyboard
                .GetState()
                .IsKeyDown(KeyOne);
            CurrentKeyState.KeyTwoPressed = Keyboard
                .GetState()
                .IsKeyDown(KeyTwo);

            if (CurrentKeyState.KeyOnePressed && CurrentKeyState.KeyTwoPressed)
            {
                if (
                    PreviousKeyState.KeyOnePressed
                    && !PreviousKeyState.KeyTwoPressed
                )
                {
                    Value = 1;
                }
                else if (
                    !PreviousKeyState.KeyOnePressed
                    && PreviousKeyState.KeyTwoPressed
                )
                {
                    Value = -1;
                }
                // if prev && prev then keep the value the same
            }
            else if (CurrentKeyState.KeyOnePressed)
            {
                Value = -1;
            }
            else if (CurrentKeyState.KeyTwoPressed)
            {
                Value = 1;
            }
            else
            {
                Value = 0;
            }
        }
    }
}
