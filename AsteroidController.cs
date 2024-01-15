using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point 
{
    public float x, y;
    public Point(float x, float y) {
        this.x = x;
        this.y = y;
    }
    public static Point operator +(Point a, Point b) {
        return new Point(a.x + b.x, a.y + b.y);
    }
    public static Point operator -(Point a, Point b) {
        return new Point(a.x - b.x, a.y - b.y);
    }
   public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Point p = (Point)obj;
        return (x == p.x) && (y == p.y);
    }
    
    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 31 + x.GetHashCode();
        hash = hash * 31 + y.GetHashCode();
        return hash;
    }
    public override string ToString()
    {
        return x + "," + y;
    }
}
public enum TerrainType {
    Carbonaceous,
    Siliceous,
    Metallic,
    Empty
}

public class AsteroidController : MonoBehaviour
{
    public GameObject prefab;
    public Sprite[] AsteroidTileset;

    Dictionary<Point, TerrainType> terrain;
    // Start is called before the first frame update
    void Start()
    {
        terrain = new Dictionary<Point, TerrainType>();
        while (transform.childCount > 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        System.Random rand = new System.Random();
        terrain = new Dictionary<Point, TerrainType>();
        for (int x = -6; x <= 6; x += 2)
        {
            for (int y = -6; y <= 6; y += 2)
            {
                int z = rand.Next(0, 3);
                if (z== 0) if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Carbonaceous);
                if (z == 1) if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Siliceous); 
                if (z == 2) if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Metallic); 
                // if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Metallic);
            }
        }
        for (int x = -4; x <= 4; x += 2)
        {
            for (int y = -10; y <= 8; y += 2)
            {
                int z = rand.Next(0, 3);
                if (z== 0) if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Carbonaceous);
                if (z == 1) if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Siliceous); 
                if (z == 2) if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Metallic); 
                // if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Metallic);
            }
        }
        for (int x = -4; x <= 4; x += 2)
        {
            for (int y = -12; y <= 10; y += 2)
            {
                int z = rand.Next(0, 7);
                if (z== 0) if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Carbonaceous);
                if (z == 1) if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Siliceous); 
                if (z == 2) if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Metallic); 
                // if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Metallic);
            }
        }
        // System.Random rand = new System.Random();
        // for (int r = 0; r < 750; r++) 
        // {
        //     int x = rand.Next(-20, 10); // Generates a random integer between -5 and 5
        //     int y = rand.Next(-20, 10); // Generates a random integer between -5 and 5
        //     if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Carbonaceous);
        // }
        // for (int r = 0; r < 500; r++) 
        // {
        //     int x = rand.Next(-10, 20); // Generates a random integer between -5 and 5
        //     int y = rand.Next(-10, 20); // Generates a random integer between -5 and 5
        //     if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Siliceous);
        // }
        // for (int r = 0; r < 250; r++) 
        // {
        //     int x = rand.Next(-20, 20); // Generates a random integer between -5 and 5
        //     int y = rand.Next(-20, 20); // Generates a random integer between -5 and 5
        //     if (!terrain.ContainsKey(new Point(x, y))) terrain.Add(new Point(x, y), TerrainType.Metallic);
        // }
    }

    public void Hit(string name) {
        Point p = new Point(float.Parse(name.Split(',')[0]), float.Parse(name.Split(',')[1]));
        Point top_left_shift = new Point(-1f, 1f);
        Point top_right_shift = new Point(1f, 1f);
        Point bottom_left_shift = new Point(-1f, -1f);
        Point bottom_right_shift = new Point(1f, -1f);
        if (terrain.ContainsKey(p + top_left_shift)) {
            // print ("destroy " + name);
            terrain.Remove(p + top_left_shift);
            rendered = false;
            // Destroy(GameObject.Find((p + top_left_shift).ToString()));
        }
        if (terrain.ContainsKey(p + top_right_shift)) {
            // print ("destroy " + name);
            terrain.Remove(p + top_right_shift);
            rendered = false;
            // Destroy(GameObject.Find((p + top_right_shift).ToString()));
        }
        if (terrain.ContainsKey(p + bottom_left_shift)) {
            // print ("destroy " + name);
            terrain.Remove(p + bottom_left_shift);
            rendered = false;
            // Destroy(GameObject.Find((p + bottom_left_shift).ToString()));
        }
        if (terrain.ContainsKey(p + bottom_right_shift)) {
            // print ("destroy " + name);
            terrain.Remove(p + bottom_right_shift);
            rendered = false;
            // Destroy(GameObject.Find((p + bottom_right_shift).ToString()));
        }
        else {
            // print ("not found  " + name + p.ToString());
        }
    
        // foreach (var point in terrain.Keys) 
        // {
        //     print (point + " " + name);
        //     if (point.ToString() == name)
        //     {
        //         print ("YES destroy " + name);
        //         terrain.Remove(point);
        //     }
        // }
    }

    public TerrainType GetTerrainKey(Point point) 
    {
        // point = new Point((int)point.x, (int)point.y);
        if (terrain.ContainsKey(point)) return terrain[point];
        return TerrainType.Empty;
    }
    bool rendered = false;
    int delay_count = 0;
    // Update is called once per frame
    void FixedUpdate()
    {

        //     if (delay_count >= 10) {
        //         delay_count = 0;
        //         rendered = false;
        //     }
        //     else {
        //         delay_count++;
        //     }
            
        // }
        // else {

        if (!rendered) {
            
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            foreach (var point in terrain.Keys) 
            {
                Point top_left_shift = new Point(-1f, 1f);
                Point top_right_shift = new Point(1f, 1f);
                Point bottom_left_shift = new Point(-1f, -1f);
                Point bottom_right_shift = new Point(1f, -1f);

                Point top_left = point + top_left_shift;
                Point top_right = point + top_right_shift;
                Point bottom_left = point + bottom_left_shift;
                Point bottom_right = point + bottom_right_shift;

                InstantiateTile(top_left, GetTerrainSprite(
                    GetTerrainKey(top_left + top_right_shift), 
                    GetTerrainKey(top_left + bottom_right_shift), 
                    GetTerrainKey(top_left + top_left_shift), 
                    GetTerrainKey(top_left + bottom_left_shift)));            
                InstantiateTile(top_right, GetTerrainSprite(
                    GetTerrainKey(top_right + top_right_shift), 
                    GetTerrainKey(top_right + bottom_right_shift), 
                    GetTerrainKey(top_right + top_left_shift), 
                    GetTerrainKey(top_right + bottom_left_shift)));
                InstantiateTile(bottom_left, GetTerrainSprite(
                    GetTerrainKey(bottom_left + top_right_shift), 
                    GetTerrainKey(bottom_left + bottom_right_shift), 
                    GetTerrainKey(bottom_left + top_left_shift), 
                    GetTerrainKey(bottom_left + bottom_left_shift)));
                InstantiateTile(bottom_right, GetTerrainSprite(
                    GetTerrainKey(bottom_right + top_right_shift), 
                    GetTerrainKey(bottom_right + bottom_right_shift), 
                    GetTerrainKey(bottom_right + top_left_shift), 
                    GetTerrainKey(bottom_right + bottom_left_shift)));
            }
            rendered = true;
        }
    }

    public Sprite GetTerrainSprite (TerrainType type_top_right, TerrainType type_bottom_right, TerrainType type_top_left, TerrainType type_bottom_left)
    {
        switch (type_top_right) {
            case TerrainType.Carbonaceous:
                switch (type_bottom_right) {
                    case TerrainType.Carbonaceous:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[0];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[64];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[128];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[192];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[1];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[65];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[129];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[193];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[2];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[66];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[130];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[194];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[3];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[67];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[131];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[195];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                    case TerrainType.Siliceous:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[16];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[80];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[144];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[208];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[17];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[81];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[145];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[209];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[18];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[82];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[146];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[210];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[19];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[83];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[147];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[211];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                    case TerrainType.Metallic:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[32];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[96];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[160];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[224];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[33];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[97];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[161];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[225];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[34];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[98];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[162];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[226];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[35];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[99];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[163];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[227];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                    case TerrainType.Empty:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[48];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[112];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[176];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[240];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[49];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[113];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[177];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[241];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[50];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[114];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[178];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[242];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[51];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[115];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[179];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[243];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                }
                return AsteroidTileset[0];
            case TerrainType.Siliceous:
                switch (type_bottom_right) {
                    case TerrainType.Carbonaceous:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[4];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[68];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[132];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[196];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[5];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[69];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[133];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[197];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[6];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[70];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[134];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[198];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[7];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[71];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[135];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[199];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                    case TerrainType.Siliceous:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[20];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[84];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[148];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[212];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[21];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[85];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[149];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[213];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[22];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[86];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[150];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[214];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[23];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[87];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[151];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[215];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                    case TerrainType.Metallic:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[36];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[100];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[164];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[228];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[37];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[101];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[165];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[229];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[38];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[102];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[166];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[230];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[39];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[103];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[167];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[231];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                    case TerrainType.Empty:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[52];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[116];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[180];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[244];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[53];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[117];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[181];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[245];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[54];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[118];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[182];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[246];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[55];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[119];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[183];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[247];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                }
                return AsteroidTileset[0];
            case TerrainType.Metallic:
                switch (type_bottom_right) {
                    case TerrainType.Carbonaceous:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[8];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[72];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[136];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[200];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[9];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[73];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[137];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[201];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[10];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[74];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[138];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[202];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[11];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[75];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[139];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[203];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                    case TerrainType.Siliceous:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[24];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[88];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[152];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[216];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[25];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[89];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[153];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[217];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[26];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[90];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[154];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[218];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[27];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[91];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[155];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[219];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                    case TerrainType.Metallic:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[40];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[104];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[168];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[232];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[41];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[105];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[169];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[233];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[42];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[106];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[170];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[234];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[43];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[107];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[171];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[235];
                                }
                                return AsteroidTileset[0];
                        }            
                        return AsteroidTileset[0];       
                    case TerrainType.Empty:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[56];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[120];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[184];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[248];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[57];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[121];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[185];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[249];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[58];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[122];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[186];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[250];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[59];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[123];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[187];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[251];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                }
                return AsteroidTileset[0];
            case TerrainType.Empty:
                switch (type_bottom_right) {
                    case TerrainType.Carbonaceous:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[12];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[76];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[140];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[204];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[13];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[77];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[141];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[205];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[14];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[78];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[142];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[206];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[15];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[79];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[143];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[207];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                    case TerrainType.Siliceous:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[28];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[92];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[156];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[220];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[29];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[93];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[157];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[221];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[30];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[94];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[158];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[222];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[31];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[95];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[159];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[223];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                    case TerrainType.Metallic:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[44];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[108];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[172];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[236];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[45];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[109];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[173];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[237];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[46];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[110];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[174];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[238];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[47];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[111];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[175];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[239];
                                }
                                return AsteroidTileset[0];
                        }       
                        return AsteroidTileset[0];            
                    case TerrainType.Empty:
                        switch (type_top_left) {
                            case TerrainType.Carbonaceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[60];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[124];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[188];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[252];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Siliceous:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[61];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[125];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[189];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[253];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Metallic:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[62];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[126];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[190];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[254];
                                }
                                return AsteroidTileset[0];
                            case TerrainType.Empty:
                                switch (type_bottom_left) {
                                    case TerrainType.Carbonaceous:
                                        return AsteroidTileset[63];
                                    case TerrainType.Siliceous:
                                        return AsteroidTileset[127];
                                    case TerrainType.Metallic:
                                        return AsteroidTileset[191];
                                    case TerrainType.Empty:
                                        return AsteroidTileset[0];
                                }
                                return AsteroidTileset[0];
                        }
                        return AsteroidTileset[0];
                }
                return AsteroidTileset[0];
        }
        return AsteroidTileset[0];
    }

    void InstantiateTile(Point point, Sprite sprite)
    {
        // Sprite sprite = carbonaceous;
        // switch (type) {
        //     case TerrainType.Carbonaceous:
        //         sprite = carbonaceous;
        //         break;
        //     case TerrainType.Siliceous:
        //         sprite = siliceous;
        //         break;
        //     case TerrainType.Metallic:
        //         sprite = metallic;
        //         break;  
        // }
        if (GameObject.Find(point.ToString()) != null) {
            GameObject.Find(point.ToString()).SetActive(true);
            GameObject.Find(point.ToString()).GetComponent<SpriteRenderer>().sprite = sprite;
            return;
        }
        var object_instance = Instantiate(prefab, new Vector3(point.x, 0, point.y), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        object_instance.GetComponent<SpriteRenderer>().sprite = sprite;
        object_instance.name = point.ToString();
        print ("Instantiated " + object_instance.name);
        // object_instance.GetComponent<SpriteRenderer>().size = new Vector2(float.Parse(size.Split(',')[0]), float.Parse(size.Split(',')[1]));
        object_instance.transform.SetParent(this.transform);//.parent.GameObject);//structure.transform.Find("Rotator"));
        object_instance.transform.localPosition = new Vector2(
            point.x,
            point.y
        );
        object_instance.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
