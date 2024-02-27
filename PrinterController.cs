using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;
public class PrinterController : ComponentController {   
    public GameObject Head, Beam1, Beam2;
    public int print_index = -1;
    public string[] components_declarations;    
    public override float GetCost() {
        return 420; //4 metal 2 silicon
    }
    public override void Focus() {
            Beam1.transform.localPosition = new Vector2(Head.transform.localPosition.x, 0);
            Beam1.GetComponent<SpriteRenderer>().size = new Vector2(1, GetComponent<SpriteRenderer>().size.y);
            Beam2.transform.localPosition = new Vector2(0, Head.transform.localPosition.y);
            Beam2.GetComponent<SpriteRenderer>().size = new Vector2(1, GetComponent<SpriteRenderer>().size.x);
        // Head.GetComponent<SpriteRenderer>().size = GetComponent<SpriteRenderer>().size;
    }
    public bool GoTo(Vector2 pos) {
        Head.transform.Translate(
            new Vector2(
                Mathf.Clamp(pos.x - Head.transform.localPosition.x, -.1f, .1f),
                Mathf.Clamp(pos.y - Head.transform.localPosition.y, -.1f, .1f)
            )
        );
        if (Mathf.Abs(Head.transform.localPosition.x - pos.x) < .2f && Mathf.Abs(Head.transform.localPosition.y - pos.y) < .2f) {
            Head.transform.localPosition = pos;
            Focus();
            print_index++;
            return true;
        }
        Focus();
        return false;
    }
    public void Edit() {
        Head.SetActive(false);
        Beam1.SetActive(false);
        Beam2.SetActive(false);
    }
    public void Print() {
        Head.SetActive(true);
        Beam1.SetActive(true);
        Beam2.SetActive(true);
    }
    public override void Ping() {
        // if (interpreter == null) interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("▦")});
    }
    public override float Action () 
    {
        return 0;
    }
    public override float Action (float input) 
    {
        return input;
    }

    public override void Launch() {
        launched = true;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }
    public override string GetIcon() { return "▦"; }
    public override string ToString()
    { //▤<b>≣ Data</b>
        var output = "";
        // var components = new string[]{"Processor Process = new Processor (0, 3, 4, 5);","Bulkhead Bulk = new Bulkhead (0, 0, 4, 4);","Booster Right = new Booster (4, 3, 2, 1);", "Booster Left = new Booster (1, 2, 3, 4);", "Thruster Engine = new Thruster (3, 4, 5, 6);"};
        for (int i = 0; i < components_declarations.Length; i++) {
            if (i == print_index) {
                output += "  " + Formatter.Red(components_declarations[i].Replace(" ", "_")) + "\n";
            }
            else {
                output += "  " + components_declarations[i] + "\n";
            }
        }
        return $"{GetIcon()} {this.name}\n{base.ToString()}\n blueprint = Component [{components_declarations.Length}] {{\n  new Processor (),\n  new Bulkhead (),\n  new Thruster (),\n  new Booster (),\n  new Booster ()\n }};\n\n /*_Construct_Ship_*/\n void Print_() {{\n  for (int i = 0; i < blueprint.Length; i++)\n  {{\n   Instantiate (blueprint[i]);\n  }}\n }}\n\n /*_Modify_Ship_*/\n void Edit_() {{ }}\n}}\n\n<b>Exit</b>\n\n<b>Delete</b>";
    }
}