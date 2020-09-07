/*
|| All components/subcomponents can be of radius 1, 2, 4, 8, 16, ...
|| A subcomponent's radius must be <= parent component's radius
|| A component must connect to other components of similiar relative radii  
||
|| Components:
||   Building blocks of ships and stations
|| = = = = = = = 
|| - Processor, runs code to control all other components (one open direction, all others sides can mount subcomponents)
|| - Scaffold, can control item flow between connected Bulkheads (all open directions, or can mount subcomponents)
|| - Bulkhead, holds items (two open sides, other sides can mount subcomponents)
|| - Factory, converts items into other items (two open sides [input, output], other sides can mount subcomponents)
|| - Constructor, materializes items into concrete shapes
||
|| Subcomponents:
||   Attaches to sides of things, giving better effects and abilities
|| = = = = = = =
|| - Gimbal, holds a second subcomponent, giving rotational control to it
|| - Collector, absorbs heat from sunlight
|| - Radiator, disperses heat
|| - Dehumidifier, absorbs water from space humidity
|| - Gun, breaks apart asteroids and ships alike
|| - Engine, provides thrust from steam (or water... or explosives...)
|| - Shield, protects against abrasive space dust and enemies alike
|| - Dock, links ships together
|| 
|| Perfect damage visualization system with marching cubes updating on taking damage? each matrix value is -1 -> 1 (health) As damaged, chunks of ship are eaten away.
||

*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

public abstract class ComponentController : MonoBehaviour
{

    // public ComponentController[] connected_components;
    protected float temperature = .5f;
    protected float hitpoints;
    protected float action_speed = .1f, action_cooldown = 0f;

    protected string prefab_path;
    public Color material_color, debug_color;
    

    UIController component_panel;

    // Visualized via marching cubes... On update, update cubes to any granular damage

    public Material m_material;

    public int seed = 0;

    List<GameObject> meshes = new List<GameObject>();

    void Start()
    {
        debug_color = new UnityEngine.Color(UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f));
        // var panel = GameObject.Find("ComponentPanel");
        component_panel = GameObject.Find("ComponentPanel")?.GetComponent<UIController>();
        //Set the mode used to create the mesh.
        //Cubes is faster and creates less verts, tetrahedrons is slower and creates more verts but better represents the mesh surface.
        Marching marching = null;
        // if (mode == MARCHING_MODE.TETRAHEDRON)
        // marching = new MarchingTertrahedron();
        // else
        marching = new MarchingCubes();

        //Surface is the value that represents the surface of mesh
        //For example the perlin noise has a range of -1 to 1 so the mid point is where we want the surface to cut through.
        //The target value does not have to be the mid point it can be any value with in the range.
        marching.Surface = 0.0f; // TODO: test how this affects small details on 32x32x32 detail, e.g. structural components on ships

        //The size of voxel array.
        int width = 9;
        int height = 9;
        int length = 9;

        float[] voxels = new float[width * height * length];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                for (int z = 0; z < height; z++)
                {
                    voxels[x + y * length + z * length * height] = -1f;
                    if (x > 0 && x < width - 1 && y > 0 && y < length - 1 && z > 0 && z < height - 1)
                    {
                        //TODO:: Work on a more refined way of defining custom shapes for controllers...
                        //       Clean would be each child component controller has own "formula" to define shape...? Allows for destruction also (if within visual range, so not overly computationally expensive )
                        // if (x == z || x == width - 1 - z || y == z || y == length - 1 - z || z == 1)
                        voxels[x + y * length + z * length * height] = 1f;
                    }
                }
            }
        }

        List<Vector3> verts = new List<Vector3>();
        List<int> indices = new List<int>();

        //The mesh produced is not optimal. There is one vert for each index.
        //Would need to weld vertices for better quality mesh.
        marching.Generate(voxels, width, height, length, verts, indices);

        //A mesh in unity can only be made up of 65000 verts.
        //Need to split the verts between multiple meshes.

        int maxVertsPerMesh = 30000; //must be divisible by 3, ie 3 verts == 1 triangle
        int numMeshes = verts.Count / maxVertsPerMesh + 1;

        for (int i = 0; i < numMeshes; i++)
        {

            List<Vector3> splitVerts = new List<Vector3>();
            List<int> splitIndices = new List<int>();

            for (int j = 0; j < maxVertsPerMesh; j++)
            {
                int idx = i * maxVertsPerMesh + j;

                if (idx < verts.Count)
                {
                    splitVerts.Add(verts[idx]);
                    splitIndices.Add(j);
                }
            }

            if (splitVerts.Count == 0) continue;

            Mesh mesh = new Mesh();
            mesh.SetVertices(splitVerts);
            mesh.SetTriangles(splitIndices, 0);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            GameObject go = new GameObject("Mesh");
            go.transform.parent = transform;
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            go.GetComponent<Renderer>().material = m_material;
            go.GetComponent<MeshFilter>().mesh = mesh;
            go.transform.localPosition = new Vector3(-.5f,.5f,.5f);//-width / 2, -height / 2, -length / 2);
            go.transform.localScale = new Vector3(1 / 7f, 1 / 7f, 1 / 7f);

            meshes.Add(go);
        }
    }
    
    public Transform GetTransform()
    {
        return this.transform;
    }

    // void Update () {

    //     if (action_cooldown > 0) { /* Updates Component's action cooldown timer */
    //         action_cooldown = Mathf.Clamp (action_cooldown, 0, action_cooldown - Time.deltaTime);
    //     } else {
    //         action_cooldown = action_speed;
    //         Action();
    //     }
    //     //  print (this.temperature + ", " + action_cooldown + ", " + action_speed);
    //     // Visualize (); /* Updates Component's visual representation to current frame */
    // }
    public abstract float Action(float input);

    public void Remove()
    {
        // foreach (var part in parts) Destroy (part.Value);
        Destroy(this.gameObject);
    }

    void OnMouseUp()
    {
        component_panel.Set(this);
        // if (componentPanel == null) 
        // var structure_controller = GetComponentInParent(typeof(StructureController)) as StructureController;

    }

    public string Plot(string type, float value, float min, float max, int length)
    {
        string output = "";
        string style = "░▒▓█" + "─┼├┤" + "╔╚";
        float marker;
        int i = 0;
        switch (type)
        {
            case "ProgressBar":
                marker = Mathf.Clamp(value.Remap(min, max, 0, length), 0, length);
                if (float.IsNaN(marker)) return "";
                // if (marker == length) return style[3].ToString().Repeat(length);
                return (marker % 1 > 0) ? 
                    style[3].ToString().Repeat(Mathf.FloorToInt(marker)) + style[(int) (4 * (marker % 1))] + ' '.ToString().Repeat(Mathf.FloorToInt(length - marker)) : 
                    style[3].ToString().Repeat(Mathf.FloorToInt(marker)) + ' '.ToString().Repeat(Mathf.FloorToInt(length - marker));
            case "Marker":
                marker = Mathf.Clamp(value.Remap(min, max, 0, length), 0, length - .01f);
                if (float.IsNaN(marker)) return "";
                return new StringBuilder(style[4].ToString().Repeat(length)) 
                { 
                    [(int)marker] = style[5] 
                }.ToString();
        }
        return "";
    }
}