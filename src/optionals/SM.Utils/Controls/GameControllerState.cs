using System;
using OpenTK;
using SharpDX.XInput;

namespace SM.Utils.Controls
{
    public struct GameControllerState
    {
        public GameControllerStateThumbs Thumbs;
        public GameControllerStateTriggers Triggers;
        public GameControllerStateDPad DPad;
        public GameControllerStateButtons Buttons;

        public bool FromConnected { get; }
        public bool AnyInteraction => Buttons.AnyInteraction || DPad.AnyInteraction || Triggers.AnyInteraction || Thumbs.AnyInteraction;
        internal GameControllerState(bool empty)
        {
            FromConnected = false;

            Thumbs = GameControllerStateThumbs.Default;
            Triggers = GameControllerStateTriggers.Default;
            DPad = GameControllerStateDPad.Default;
            Buttons = GameControllerStateButtons.Default;
        }
        internal GameControllerState(Gamepad state, GameController controller)
        {
            FromConnected = true;

            Thumbs = new GameControllerStateThumbs(controller, state);
                
            Triggers = new GameControllerStateTriggers()
            {
                Left = (float)state.LeftTrigger / byte.MaxValue,
                Right = (float)state.RightTrigger / byte.MaxValue
            };

            DPad = new GameControllerStateDPad(state.Buttons);
            Buttons = new GameControllerStateButtons(state.Buttons, controller);
        }

        public override string ToString()
        {
            return !FromConnected ? "[From a disconnected controller]" : $"Thumbs: [{Thumbs}]; Trigger: [{Triggers}]; DPad: [{DPad}]; Buttons: [{Buttons}]";
        }
    }
}