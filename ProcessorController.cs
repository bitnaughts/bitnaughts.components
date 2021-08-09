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
    List<string> instructions;
    int edit_line = 1;
    float speed = 1;
    Interpreter interpreter;

    Dictionary<string, ComponentController> components;
    
    string iterate_result;    
    public const int SPEED_MIN = 0, SPEED_MAX = 999;

    public override void Focus() {
        speed = Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 1f, SPEED_MIN, SPEED_MAX);
    }

    public override float Action(float input)
    {
        return input;
    }
    float pointer;
    public void Action(Dictionary<string, ComponentController> components)
    {
        this.components = components;
        if (interpreter == null) {
            // SetInstructions("START\ncom Cannon1 ptr\ncom Cannon1 ptr\ncom Cannon1 ptr\ncom Cannon1 ptr\ncom Cannon1 ptr\ncom Cannon1 ptr\njum START");
            SetInstructions("START\n/\njum START");
            return;
        }
        
        // print (iterate_result);
    }
    float timer;
    public void Update() {
        timer += Time.deltaTime;
        if (timer > 1f/speed) {
            timer -= 1f/speed;
            iterate_result = interpreter.Iterate(components, edit_line);
        }
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
    public void AddOperand(string op_code)
    {
        instructions[edit_line] += op_code;
        SetInstructions(instructions);
    }
    public void ModifyConstantOperand(float delta)
    {
        string[] parts = instructions[edit_line].Split(' ');
        float old_constant = float.Parse(parts[parts.Length - 1]);
        parts[parts.Length - 1] = Mathf.Clamp(old_constant + delta, -999, 999).ToString();
        instructions[edit_line] = String.Join(" ", parts);
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
    public string[] GetLabels()
    {
        return interpreter.GetLabels();
    }
    public string[] GetVariables()
    {
        return interpreter.GetVariables();
    }
    public bool IsVariable(string variable)
    {
        return interpreter.isVariable(variable);
    }
    public bool IsLabel (string label)
    {
        return interpreter.isLabel(label);
    }
    public override string ToString()
    {       
        string output = "\n ▥ <b>" + name + "</b>\n ┣ " + new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y).ToString() + "\n ┣ " + GetComponent<SpriteRenderer>().size.ToString() + "\n ┗ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°";
        // string output = base.ToString();
        output += "\n ┏ <b>Assembly</b>";
        int active_line = interpreter.GetPointer();
        for (int i = 0; i < instructions.Count - 1; i++) {
            if (i == edit_line && i == active_line) 
                output += "\n ┝ <b>" + instructions[i] + "</b>";
            else if (i == edit_line)
                output += "\n ├ <b>" + instructions[i] + "</b>";
            else if (i == active_line)
                output += "\n ┣ " + instructions[i];
            else 
                output += "\n ┠ " + instructions[i];
        }
        var last = instructions.Count - 1;
        if (last == edit_line && last == active_line) 
            output += "\n ┕ <b>" + instructions[last] + "</b>";
        else if (last == edit_line)
            output += "\n └ <b>" + instructions[last] + "</b>";
        else if (last == active_line)
            output += "\n ┗ " + instructions[last];
        else 
            output += "\n ┖ " + instructions[last];
        return output;
    }
}