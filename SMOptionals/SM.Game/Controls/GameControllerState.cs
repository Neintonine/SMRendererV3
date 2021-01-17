using System;
using OpenTK;
using SharpDX.XInput;

namespace SM.Game.Controls
{
    public struct GameControllerState
    {
        public GameControllerStateThumbs Thumbs;
        public GameControllerStateTriggers Triggers;
        public GameControllerStateDPad DPad;
        public GameControllerStateButtons Buttons;

        public bool FromConnected { get; }

        internal GameControllerState(bool empty)
        {
            FromConnected = false;

            Thumbs = GameControllerStateThumbs.Default;
            Triggers = GameControllerStateTriggers.Default;
            DPad = GameControllerStateDPad.Default;
            Buttons = GameControllerStateButtons.Default;
        }
        internal GameControllerState(Gamepad state, ref GameController controller)
        {
            FromConnected = true;

            Thumbs = new GameControllerStateThumbs
            {
                Left = new Vector2(
                    Math.Abs((float)state.LeftThumbX) < controller.Deadband ? 0 : (float)state.LeftThumbX / short.MaxValue,
                    Math.Abs((float)state.LeftThumbY) < controller.Deadband ? 0 : (float)state.LeftThumbY / short.MaxValue),
                Right = new Vector2(
                    Math.Abs((float)state.RightThumbX) < controller.Deadband ? 0 : (float)state.RightThumbX / short.MaxValue,
                    Math.Abs((float)state.RightThumbY) < controller.Deadband ? 0 : (float)state.RightThumbY / short.MaxValue),

                PressedLeft = state.Buttons.HasFlag(GamepadButtonFlags.LeftThumb),
                PressedRight = state.Buttons.HasFlag(GamepadButtonFlags.RightThumb)
            };
                
            Triggers = new GameControllerStateTriggers()
            {
                Left = (float)state.LeftTrigger / byte.MaxValue,
                Right = (float)state.RightTrigger / byte.MaxValue
            };

            DPad = new GameControllerStateDPad(state.Buttons);
            Buttons = new GameControllerStateButtons(state.Buttons);
        }

        public override string ToString()
        {
            return !FromConnected ? "[From a disconnected controller]" : $"Thumbs: [{Thumbs}]; Trigger: [{Triggers}]; DPad: [{DPad}]; Buttons: [{Buttons}]";
        }
    }
}