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

    List<string> instructions;
    int edit_line = 0;
    Interpreter interpreter;
    
    string iterate_result;

    public override float Action(float input)
    {
        return input;
    }
    float pointer;
    public void Action(Dictionary<string, ComponentController> components)
    {
        if (interpreter == null) {
            SetInstructions("LOOP\ncom Thruster1 -1\ncom Thruster2 -1\ncom Thruster1 ina\ncom Thruster1 ina\ncom Thruster2 ind\ncom Thruster2 ind\njum LOOP");
            return;
        }
        iterate_result = interpreter.Iterate(components, edit_line);
        // print (iterate_result);
    }
    public void SetInstructions(string instructions_string)
    {
        SetInstructions(new List<string>(instructions_string.Split('\n')));
    }
    public void SetInstructions(List<string> instructions_list)
    {
        this.instructions = instructions_list;
        interpreter = new Interpreter(instructions.ToArray());

        Scroll(0);
    }
    public override string GetDescription() 
    {
        return "\n <b>Processors</b> run \n assembly code \n to control \n components;";
    }
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(4, 4);
    }

    public void Scroll(int direction)
    {
        edit_line += direction;
        if (edit_line < 0) edit_line = instructions.Count - 1;
        edit_line %= instructions.Count;
    }
    public void AddLine(int direction)
    {
        instructions.Insert((edit_line + direction) % (instructions.Count + 1), "/");
        Scroll(direction);
        SetInstructions(instructions);
    }
    public void DeleteLine()
    {
        instructions.RemoveAt(edit_line);
        SetInstructions(instructions);
    }
    public void SetOperand(string op_code)
    {
        instructions[edit_line] = op_code;
        SetInstructions(instructions);
    }
    public string GetEditInstruction()
    {
        return interpreter.GetInstruction(edit_line).ToString();
    }
    public string GetEditInstructionCategory()
    {
        return Interpreter.GetInstructionCategory(interpreter.GetInstruction(edit_line));
    }
    public string GetEditInstructionText()
    {
        return Interpreter.GetInstructionText(interpreter.GetInstruction(edit_line));
    }
    public string GetNextLabel()
    {
        return interpreter.GetNextLabel();
    }
    public string GetNextVariable()
    {
        return interpreter.GetNextVariable();
    }
    public override string ToString()
    {
        // string output = this.name + "\n║ Instruction Set:"; 
        // foreach (var instruction in instructions) output += "\n│ " + instruction ;
        // output += "║ Variables:";
        // foreach (var variable in variables) output += "\n│ " + variable.Key + ":" +  Plot("Marker", variable.Value.value, variable.Value.min, variable.Value.max, 10) ;
        return "\n" + iterate_result;
    }
}