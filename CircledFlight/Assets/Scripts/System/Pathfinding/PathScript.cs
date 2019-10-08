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

public class PathScript : MonoBehaviour {

    //Settings
    private Grid grid;
    protected Vector2[] path;

    //Path Functions
    private void createPath(Vector2 start_position, Vector2 end_position){
        Node start_node = grid.getNodeFromWorld(start_position.x, start_position.y);
        Node end_node = grid.getNodeFromWorld(end_position.x, end_position.y);

        Heap<Node> open = new Heap<Node>(grid.maxSize());
        HashSet<Node> closed = new HashSet<Node>();
        open.Add(start_node);

        while (open.Count > 0){
            Node current_node = open.removeFirst();
            closed.Add(current_node);

            if (current_node == end_node){
                setPath(start_node, end_node);
                return;
            }

            foreach (Node neighbour in grid.getNeighbors(current_node)) {
                if (!neighbour.isEmpty() || closed.Contains(neighbour)){
                    continue;
                }

                int movecost = current_node.gcost + grid.getDistance(current_node, neighbour);
                if (movecost < neighbour.gcost || !open.Contains(neighbour)){
                    neighbour.gcost = movecost;
                    neighbour.hcost = grid.getDistance(neighbour, end_node);
                    neighbour.parent = current_node;

                    if (!open.Contains(neighbour)){
                        open.Add(neighbour);
                    }
                    else {
					    open.UpdateData(neighbour);
					}
                }
            }
        }
    }

    private void setPath(Node start_node, Node end_node){
        List<Node> node_path = new List<Node>();
        Node current_node = end_node;
        
        while (current_node != start_node){
            node_path.Add(current_node);
            current_node = current_node.parent;
        }
        node_path.Reverse();

        path = new Vector2[node_path.Count];
        for (int i = 0; i < node_path.Count; i++){
            path[i] = node_path[i].getPosition();
        }
    }

    private void cleanPath(){
        List<Vector2> waypoints = new List<Vector2>();
        int pointA = 0;
        int pointB = 2;
        waypoints.Add(path[pointA]);
        while (pointB < path.Length){
            if (!checkLineRay(path[pointA], path[pointB])){
                pointB++;
            }
            else {
                waypoints.Add(path[pointB - 1]);
                pointA = pointB;
                pointB = pointA + 2;
            }
        }
        waypoints.Add(path[path.Length - 1]);
        path = waypoints.ToArray();
    }

    private bool checkLine(Vector2 pointA, Vector2 pointB, float size){
        float clean_length = Vector2.Distance(pointA, pointB);
        float clean_angle = Vector2.Angle(pointA, pointB);
        int clean_count = (int) (clean_length / (1f / 32f));
        bool can_clean = true;
        for (int k = 0; k < clean_count; k++){
            Vector2 clean_point = new Vector2(pointA.x + (((1f / 32f) * k) * Mathf.Cos(clean_angle)), pointA.y + (((1f / 32f) * k) * Mathf.Sin(clean_angle)));
            Node clean_node = grid.getNodeFromWorld(clean_point.x, clean_point.y);
            if (!clean_node.isEmpty()){
                can_clean = false;
                break;
            }
        }
        return can_clean;
    }

    private bool checkLineRay(Vector2 pointA, Vector2 pointB){
        RaycastHit2D ray_check = Physics2D.Raycast(pointA, pointB - pointA, Vector2.Distance(pointA, pointB), grid.solidLayer, -Mathf.Infinity, Mathf.Infinity);
        bool hit = false;
        if (ray_check.collider != null){
            hit = true;
        }
        return hit;
    }

    public Vector2[] getPath(Vector2 start_position, Vector2 end_position){
        createPath(start_position, end_position);
        if (path != null){
            if (path.Length > 0){
                cleanPath();
            }
            else {
                path = new Vector2[1];
                path[0] = start_position;
            }
        }
        return path;
    }

    public Vector2 closestPoint(Vector2 position){
        Vector2 point = position;
        float distance = -1;
        bool first = false;
        for (int i = 0; i < grid.maxSize(); i++){
            if (grid.getNodeFromWorld(grid.gridVector(i).x, grid.gridVector(i).y).isEmpty()){
                if (!first){
                    first = true;
                    point = grid.gridVector(i);
                    distance = Vector2.Distance(position, grid.gridVector(i));
                }
                else {
                    float new_distance = Vector2.Distance(position, grid.gridVector(i));
                    if (new_distance < distance){
                        distance = new_distance;
                        point = grid.gridVector(i);
                    }
                }
            }
        }
        return point;
    }

    public Grid Grid {
        set {
            grid = value;
        }
        get {
            return grid;
        }
    }

}