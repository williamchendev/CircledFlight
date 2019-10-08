using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
                        .-`                                                                            
                        /./                                                                             
                        +.:-                                                                            
                        `:/+/:..--....-..-.`   ``..``                                                   
                            `.-+:.``       `.--`.-.....`                                                  
                            -/`       `    `..//```   `                                                  
                            -+`        ```  `:/-s-.-.                                                     
                        `` `o:.        ..`  -oydso `..                                                    
                        -//-.`       :/:- `.dyshs   `                                                    
                        `+/.`.``  .:osys..`+s:os                                                        
                            .o--.```+dyddo:-.`.:--+/                                                       
                            //--::.`-h:oy- ````...:+                                                       
                            s--//::``-.:/-`.`````.+-                                                       
                    `-..:/+:::++/-.......```.--++                      ~ Written by @mermaid_games ~                                  
                    `/+/o+::+++++oo:-----//++oo/                               aka William Spelman bc that's my Twitter handle                         
                        `-::-/+++/+ss/:-.:oysys++/`                                                       
            `      `-::..-:++++:odhyss/-ysoyyso+-                                                       
            `----.://----:/++/+/:dddyssyhyysyssyy+--                                                     
                `//o++oo/-:+oo:-+:-dhhyhyhdysosyhhhhhy                                                     
                    :o:/+:+/-/+:.ssydddhdhdyydhhhhd`                                                     
                    `+//o`+/-:+o+..yhdhhhmhdyhhhys+d`                                                     
                    .y+s-++:/++oo-`/yysyddhhhhhyso+h`                                                     
                    .y+y+o++oo+shho.:oshmh+oso+osoo-                                                      
                    .y+so++oso+ymmyyo+oymdoo++//o+                                                        
                    :s+++osyssmdmhssshdds///o+od                                                         
                    `+o+oyyyyhmmddssyddy/:/+/shd                                                         
                    `+osyyyhhydmdysshyhs/:::yhhd        ....`                                            
            ..```-:ooyhyyhhhhhddysyyshs/+oohhhs   `./ooo++++o-                                          
                .ooss+/ohhhhysshddhyyysyhy++s:yhd/:o+ssoooo++///s:                                         
                `.. `odhysssyddmhhhyshhsyo:syosysooooooo+oyso+os:                                        
                    :shyssssyyddmhdhsyhhds-:+oys++oooooo+++ooo++os:                                       
                -shhysssssyhddmdhyshhhy/++hhs++ooooooosyy+////+os`                                      
                .yhhhyssssyhhsmdhhsshdds-.odyssooooooosydd+::////oo                                      
                `dhhhysssssyyo+mdhysshdds/:ydhhysooosshd/`so//////+s:                                     
                +dhhyssssss//omhyo++syd+.+dhhhhhyyyhddm  .y+//////+o.                                    
                -o:ooyyss+///odo+---/oyoods+osyyhhddddo   /y+//////+s.                                   
                -+--`-os+///+hs++::::+ohds////++shhddm-   `sso++////oy`                                  
                ./--`  ./+oosdo++/:::++hh/////////+syh:.    /sooo+///oo`                                 
                /o:--.`---.oyo///--:oosd///////o++//+++/.`  .oyso+///o+`                                
                `-+++o//:-.so:-::-..://d+//////++++++ssso+//-.-oyo+/osd:                                
                    `...-::osyo:----/yodhyysssoooossssssoo+++++/+ddddddd:`                              
                            ``s/--.:.-hyyyyyyyyssysso++++///////++oyhhhdddo`                             
                            :s//./o+-o         `-/+so+++////////////+oyhdmo.                            
                            :so+:/-`+:o.            .:+oooo+++//////////+smdho/-.`                       
                            `.::-    ..`                 ..-///+syso+++++yhyyhhhys+/:..`                 
                                                                -dmh/:ooydhhyoosyyyyssssoo:-/oos/`       
                                                                hm+   `+ddh+oo+oyyso++++oyhhyhdm.       
                                                                oy:     ydhyyysoshyysssosyhhhhy+`       
                                                                        `odhhhhhhhhyhhhhhhhhh/`         
                                                                            .ydddddy:` .::::::.            
                                                                            `/ohdddo.                     
                                                                            -:ydh                     
                                                                                ```       
    
                  ~ This Utility is provided to you by you local Socialist Code Witches! ~
*/

public class Grid : MonoBehaviour {

    //Settings
    [SerializeField] private float size;
    [SerializeField] private LayerMask space_layer;
    [SerializeField] private LayerMask solids_layer;

    private Node[,] grid;
    private Vector2 grid_position;
    private PolygonCollider2D space;
    private GameObject solidspace;

    //Init Grid
    void Start() {
        if (size == 0){
            size = 2f / 32f;
        }
        space = GetComponent<PolygonCollider2D>();

        Vector2 xy_point = new Vector2(space.points[0].x + transform.position.x, space.points[0].y + transform.position.y);
        float x1 = xy_point.x;
        float y1 = xy_point.y;
        float x2 = xy_point.x;
        float y2 = xy_point.y;

        for (int i = 0; i < space.points.Length; i++){
            Vector2 check_point = new Vector2(space.points[i].x + transform.position.x, space.points[i].y + transform.position.y);
            if (check_point.x < x1){
                x1 = check_point.x;
            }
            if (check_point.y > y1){
                y1 = check_point.y;
            }
            if (check_point.x > x2){
                x2 = check_point.x;
            }
            if (check_point.y < y2){
                y2 = check_point.y;
            }
        }

        int width = (int)((x2 - x1) / size);
        int hight = (int)((y1 - y2) / size);
        grid_position = new Vector2(x1, y1);
        createGrid(width, hight);
    }

    private void createGrid (int width, int height) {
        //Create Grid
        float start_x = grid_position.x;
        float start_y = grid_position.y;
        
        grid = new Node[width, height];
        if (solidspace != null){
            Destroy(solidspace.gameObject);
        }

        //Create Grid Check
        int i = 0;
        for (int y = 0; y < height; y++){
            for (int x = 0; x < width; x++){
                bool is_empty = false;
                Vector2 node_position = new Vector2(start_x + (x * size), start_y - (y * size));
                RaycastHit2D node_check = Physics2D.BoxCast(node_position, new Vector2(size, size), 0f, new Vector2(0f, 0f), Mathf.Infinity, space_layer, -Mathf.Infinity, Mathf.Infinity);
                if (node_check.collider != null){
                    is_empty = true;
                }
                node_check = Physics2D.BoxCast(node_position, new Vector2(size, size), 0f, new Vector2(0f, 0f), Mathf.Infinity, solids_layer, -Mathf.Infinity, Mathf.Infinity);
                if (node_check.collider != null){
                    is_empty = false;
                }

                grid[x, y] = new Node(is_empty, node_position.x, node_position.y, i);
                i++;
            }
        }

        //Create Solid Space
        solidspace = new GameObject("pGridSolids", typeof(PolygonCollider2D));
        solidspace.transform.position = transform.position;
        int layerNumber = 0;
        int layer = solids_layer.value;
        while(layer > 0) {
            layer = layer >> 1;
            layerNumber++;
        }
        solidspace.layer = layerNumber - 1;

        //Add Vertices to Solid Space
        List<Vector2> solidspace_vertices = new List<Vector2>();
        Vector2 xy_point = new Vector2(space.points[0].x, space.points[0].y);
        float x1 = xy_point.x;
        float y1 = xy_point.y;
        float x2 = xy_point.x;
        float y2 = xy_point.y;

        for (int k = 0; k < space.points.Length; k++){
            Vector2 check_point = new Vector2(space.points[k].x, space.points[k].y);
            if (check_point.x < x1){
                x1 = check_point.x;
            }
            if (check_point.y > y1){
                y1 = check_point.y;
            }
            if (check_point.x > x2){
                x2 = check_point.x;
            }
            if (check_point.y < y2){
                y2 = check_point.y;
            }
        }
        solidspace_vertices.Add(new Vector2(x1, y1));
        solidspace_vertices.Add(new Vector2(x2, y1));
        solidspace_vertices.Add(new Vector2(x2, y2));
        solidspace_vertices.Add(new Vector2(x1, y2));
        solidspace_vertices.Add(new Vector2(x1, y1));
        for (int l = 0; l < space.points.Length; l++){
            solidspace_vertices.Add(new Vector2(space.points[l].x, space.points[l].y));
        }
        solidspace_vertices.Add(new Vector2(space.points[0].x, space.points[0].y));
        solidspace.GetComponent<PolygonCollider2D>().points = solidspace_vertices.ToArray();
    }

    public List<Node> getNeighbors(Node node){
        List<Node> neighbors = new List<Node>();

        int grid_width = grid.GetLength(0);
        int grid_height = grid.GetLength(1);
        int node_check_x = node.getGridNum() % grid_width;
        int node_check_y = node.getGridNum() / grid_width;

        for (int w = -1; w <= 1; w++){
            for (int h = -1; h <= 1; h++){
                if (w == 0 && h == 0){
                    continue;
                }

                if (node_check_x + w >= 0 && node_check_x + w < grid_width){
                    if (node_check_y + h >= 0 && node_check_y + h < grid_height){
                        neighbors.Add(grid[node_check_x + w, node_check_y + h]);
                    }
                }
            }
        }

        return neighbors;
    }

    public int getDistance(Node nodeA, Node nodeB){
        int temp_distance;

        int grid_width = grid.GetLength(0);
        int w = Mathf.Abs((nodeA.getGridNum() % grid_width) - (nodeB.getGridNum() % grid_width));
        int h = Mathf.Abs((nodeA.getGridNum() / grid_width) - (nodeB.getGridNum() / grid_width));

        if (w > h){
            temp_distance = (h * 14) + ((w - h) * 10);
        }
        else {
            temp_distance = (w * 14) + ((h - w) * 10);
        }
        return temp_distance;
    }

    public Node getNodeFromWorld(float x, float y){
        Node start_node = grid[0, 0];
        Vector2 start_position = start_node.getPosition();

        float world_node_x = (x - start_position.x) / size;
        float world_node_y = (start_position.y - y) / size;

        int node_x = Mathf.RoundToInt(Mathf.Clamp(world_node_x, 0, grid.GetLength(0) - 1));
        int node_y = Mathf.RoundToInt(Mathf.Clamp(world_node_y, 0, grid.GetLength(1) - 1));

        return grid[node_x, node_y]; 
    }

    public Vector2 gridVector(int index){
        index = Mathf.Clamp(index, 0, maxSize());
        int x_val = index % grid.GetLength(0);
        int y_val = index / grid.GetLength(0);
        return grid[x_val, y_val].getPosition();
    }

    public int maxSize(){
        return (grid.GetLength(0) * grid.GetLength(1));
    }

    public LayerMask spaceLayer {
        get {
            return space_layer;
        }
    }

    public LayerMask solidLayer {
        get {
            return solids_layer;
        }
    }

    //Debug
    private void OnDrawGizmos() {
        if (space != null){
            Gizmos.color = Color.blue;
            for (int i = 1; i < space.points.Length; i++){
                Gizmos.DrawLine(new Vector3(space.points[i - 1].x + transform.position.x, space.points[i - 1].y + transform.position.y, 25), new Vector3(space.points[i].x + transform.position.x, space.points[i].y + transform.position.y, 25));
            }
            Gizmos.DrawLine(new Vector3(space.points[space.points.Length - 1].x + transform.position.x, space.points[space.points.Length - 1].y + transform.position.y, 25), new Vector3(space.points[0].x + transform.position.x, space.points[0].y + transform.position.y, 25));
        }
    }

}
