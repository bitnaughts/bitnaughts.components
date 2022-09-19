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
    public string object_class_string = "class Object {\n\tfloat x, y, w, h;\n}";
    public string class_string = "class Processor : Object {\n\tvoid main() {\n\t\tprint(\"Hello\");\n\t}\n}";
    public string[] override_instructions;
    List<string> instructions;
    int edit_line = 1;
    float speed = 1;
    Interpreter interpreter;

    Dictionary<string, ComponentController> components;
    
    string iterate_result;    
    public const int SPEED_MIN = 0, SPEED_MAX = 999;

    public override void Focus() {
        speed = Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * .25f, SPEED_MIN, SPEED_MAX);
    }
    public override void Ping() {
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
            // if (override_instructions.Length == 0) 
                // SetInstructions(new string[]{"class Processor {","void Start() { }","}"});
            // else 
                // SetInstructions(override_instructions);
        }
    }
    float timer;
    public void Update() {
        timer += Time.deltaTime;
        if (timer > 1f/speed && interpreter != null) {
            timer -= 1f/speed;
            // interpreter.step();
        }
    }
    public void SetInstructions(string instructions_string)
    {
        SetInstructions(instructions_string.Split('\n'));
    }
    public void SetInstructions(List<string> instructions_list)
    {
        this.instructions = instructions_list;
        interpreter = new Interpreter(instructions.ToArray());
        Scroll(0);
    }
    public void SetInstructions(string[] instructions)
    {
        this.instructions = instructions.ToList<string>();
        interpreter = new Interpreter(instructions);
        Scroll(0);
    }
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(4, 4);
    }

    public void Scroll(int direction)
    {
        if (instructions.Count == 0) return;
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
    public override string GetIcon() { return "▩"; }
    public override string ToString()
    {    
        return $"{name}\nclass {name} : Component {{\n  /*_Constructor_*/\n  public class {name} () {{\n{ComponentToString()}\n  }}\n  /*_Main_method_*/\n  public static void Main () {{\n    <b>$</b>\n  }}\n}}\n\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
            // "⋅  var ship = System.Read (@\"example\");\n" +
            // "⋅  if (size < ship.size) break;\n" +
            // "⋅  foreach (c in ship.components) {\n" +
            // "⋅   nozzle.GoTo (c.position);\n" +
            // "⋅   nozzle.Place (c.type);\n" +
            // "⋅   nozzle.Rotate (c.rotation);\n" +
            // "⋅   nozzle.Resize (c.size);\n" +
            // "⋅  }\n" +   
        // string output = "\n  ▥ <b>" + name + "</b>\n  ┣ ↧ " + new Vector2(transform.localPosition.x, transform.localPosition.y).ToString() + "\n  ┗ ↹ " + GetComponent<SpriteRenderer>().size.ToString();// + "\n  ┗ ↺ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°";
        // if (instructions.Count == 0) return output;
        // output += "\n  <b>◬ Code</b>";
        // int active_line = 0;//interpreter.GetPointer();
        // for (int i = 0; i < instructions.Count - 1; i++) {
        //     if (i == edit_line && i == active_line) 
        //         output += "\n  ┝ <b>" + instructions[i] + "</b>";
        //     else if (i == edit_line)
        //         output += "\n  ├ <b>" + instructions[i] + "</b>";
        //     else if (i == active_line)
        //         output += "\n  ┣ " + instructions[i];
        //     else 
        //         output += "\n  ┠ " + instructions[i];
        // }
        // var last = instructions.Count - 1;
        // if (last == edit_line && last == active_line) 
        //     output += "\n  ┕ <b>" + instructions[last] + "</b>";
        // else if (last == edit_line)
        //     output += "\n  └ <b>" + instructions[last] + "</b>";
        // else if (last == active_line)
        //     output += "\n  ┗ " + instructions[last];
        // else 
        //     output += "\n  ┖ " + instructions[last];
        // // output += "\n  <b>≣ R.A.M.</b>" + interpreter.GetVariablesString();
        // return output;
    }
}