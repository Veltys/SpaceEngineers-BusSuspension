using Sandbox.ModAPI.Ingame;


/// <file>Suspensión autobús.cs</file>
/// <summary>Autobus vehicle suspension manager</summary>
/// <author>Veltys</author>
/// <date>2022-05-22</date>
/// <version>0.1.0</version>
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


        bool mode;                                                                  // Current mode: true to driving, false to picking-up

        /// <summary>
        /// Class constructor
        /// Set-up all variables and programmable block screens
        /// </summary>
        public Program() {
            mode = true;
        }

        /// <summary>
        /// Main public method
        /// Maing execution
        /// </summary>
        /// <param name="argument">Argument given</param>
        /// <param name="updateSource">"Who" ran the programmable block</param>
        public void Main(string argument, UpdateType updateSource) {
            switch(argument.ToLower()) {                                            // Switch to choose the action
                case "bajar":                                                       // Picking-up passengers mode
                    Echo("Bajando suspensión");
                    break;
                case "invertir":                                                    // Invert current mode
                    if(mode) {
                        Echo("Bajando suspensión");
                    }
                    else {
                        Echo("Subiendo suspensión");
                    }

                    mode = !mode;
                    break;
                case "subir":                                                       // Driving mode
                    Echo("Subiendo suspensión");
                    break;
                default:                                                            // Just-in-case
                    break;
            }
        }


        // Stop copying to game before this text
    }
}

