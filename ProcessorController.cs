using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ProcessorController : ComponentController
{
    
    
    /* Thruster Processor Example
        com left_sensor
        sub res 20
        com left_thruster res
        com right_sensor
        sub res 20
        com right_thruster res
    */

    /* Turret Processor Example
        STR set rotation 1
        --- com turret rotation
        --- ji

        com turret_left_sensor
        
    */
        // new string[] {
        //     "RST set dir rnd",
        //     "STR com sensor",
        //     "--- set dist res",
        //     "--- sub res 100",
        //     "--- set brake res",
        //     "--- jil dist 100 TRL",
        //     "--- com l_thr 100",
        //     "--- com r_thr 100",
        //     "n_l jig dist 100 RST",
        //     "--- jum STR",
        //     "TRL jig dir 0 TRR",
        //     "--- com l_thr brake",
        //     "--- jum STR",
        //     "TRR com r_thr brake",
        //     "--- jum STR",

        //     // Gimbal sensor back and forth ~10 degrees. If something "close" on either side, thrust vector other way.
        // }

    public string[] instructions;
    Interpreter interpreter;
    
    public override float Action(float input)
    {
        return input;
    }
    float pointer;
    public void Action(Dictionary<string, ComponentController> components)
    {
        if (interpreter == null) {
            interpreter = new Interpreter(instructions);
        }
        interpreter.Iterate(components);
    }
    
        // if (instructions == null) Init(debug_instructions);
        // );


   
    public override string ToString()
    {
        // string output = this.name + "\n║ Instruction Set:"; 
        // foreach (var instruction in instructions) output += "\n│ " + instruction ;
        // output += "║ Variables:";
        // foreach (var variable in variables) output += "\n│ " + variable.Key + ":" +  Plot("Marker", variable.Value.value, variable.Value.min, variable.Value.max, 10) ;
        return "Processor\n";
    }
}