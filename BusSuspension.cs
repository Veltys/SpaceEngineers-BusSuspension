using Sandbox.ModAPI.Ingame;


/// <file>Suspensión autobús.cs</file>
/// <summary>Autobus vehicle suspension manager</summary>
/// <author>Veltys</author>
/// <date>2022-05-23</date>
/// <version>1.1.0</version>
/// <note>Made just for internal use</note>


namespace ScriptingClass {

    /// <summary>
    /// Program class
    /// Provides the container for a programmable block program
    /// Needs to be called "Program" because ingame programmable block assume that
    /// </summary>
    class Program {
        // Some stuff for avoiding some environment errors
        IMyGridTerminalSystem? GridTerminalSystem = null;
        IMyGridProgramRuntimeInfo? Runtime = null;
        Action<string>? Echo = null;
        IMyTerminalBlock? Me = null;

        // Start copying to game after this text


        private bool _mode;                                                                 // Current mode: true to driving, false to picking-up

        private const float _maxHeight = -0.3200F;                                          // Maximum suspension height
        private const float _minHeight = 0.0500F;                                           // Minimum suspension height
        private const float _normalHeight = -0.1600F;                                       // Normal suspension height

        private const string _nameAllWheelSuspensionGroup = "Suspensiones bus";             // Group of all wheel suspensions
        private const string _nameLeftWheelSuspensionGroup = "Suspensiones izq. bus";       // Group of all left wheel suspensions
        private const string _nameRightWheelSuspensionGroup = "Suspensiones der. bus";      // Group of all right wheel suspensions

        private const string _verbDown = "bajar";                                           // Verb for picking-up passengers mode
        private const string _verbUp = "subir";                                             // Verb for driving mode
        private const string _verbInvert = "invertir";                                      // Verb for invert mode


        /// <summary>
        /// Class constructor
        /// Set-up all variables and programmable block screens
        /// </summary>
        public Program() {
            _mode = true;
        }


        /// <summary>
        /// ModifySuspension private method
        /// Modify suspension height
        /// </summary>
        /// <param name="suspension">Suspension to modify</param>
        /// <param name="height">Height to set suspension</param>
        private void ModifySuspension(List<IMyMotorSuspension> suspension, float height) {
            ushort i;

            for(i = 0; i < suspension.Count; i++) {
                suspension[i].Height = height;
            }
        }


        /// <summary>
        /// Main public method
        /// Maing execution
        /// </summary>
        /// <param name="argument">Argument given</param>
        /// <param name="updateSource">"Who" ran the programmable block</param>
        public void Main(string argument, UpdateType updateSource) {
            List<IMyMotorSuspension> allSuspensions = new List<IMyMotorSuspension>();       // Wheel suspensions group
            List<IMyMotorSuspension> leftSuspensions = new List<IMyMotorSuspension>();      // Wheel suspensions group
            List<IMyMotorSuspension> rightSuspensions = new List<IMyMotorSuspension>();     // Wheel suspensions group

            GridTerminalSystem.GetBlockGroupWithName(_nameAllWheelSuspensionGroup).GetBlocksOfType<IMyMotorSuspension>(allSuspensions);
            GridTerminalSystem.GetBlockGroupWithName(_nameLeftWheelSuspensionGroup).GetBlocksOfType<IMyMotorSuspension>(leftSuspensions);
            GridTerminalSystem.GetBlockGroupWithName(_nameRightWheelSuspensionGroup).GetBlocksOfType<IMyMotorSuspension>(rightSuspensions);


            switch(argument.ToLowerInvariant()) {                                           // Switch to choose the action
                case _verbDown:                                                             // Picking-up passengers mode
                    ModifySuspension(leftSuspensions, _maxHeight);
                    ModifySuspension(rightSuspensions, _minHeight);

                    _mode = false;

                    break;
                case _verbUp:                                                               // Driving mode
                    ModifySuspension(allSuspensions, _normalHeight);

                    _mode = true;

                    break;
                case _verbInvert:                                                           // Invert current mode
                    if(_mode) {                                                             // Switch to picking-up passengers mode
                        ModifySuspension(leftSuspensions, _maxHeight);
                        ModifySuspension(rightSuspensions, _minHeight);
                    }
                    else {                                                                  // Switch to driving mode
                        ModifySuspension(allSuspensions, _normalHeight);
                    }

                    _mode = !_mode;

                    break;
                default:                                                                    // Just-in-case
                    Echo("Please, run this program with an appropiate argument");
                    break;
            }
        }


        // Stop copying to game before this text
    }
}

