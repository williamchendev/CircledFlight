using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawUtil : MonoBehaviour {

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
                    `/+/o+::+++++oo:-----//++oo/                                                        
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

    /// <summary>
    /// drawPoint: draws a point with the x and y coordinates to the given texture2D with the given color.
    /// </summary>
    /// <param name="tex">Texture2D texture</param>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    /// <param name="color">color of the point</param>
    /// <returns>the edited Texture2D, make sure to use texture.Apply(); after calling this method!!!</returns>
	public static Texture2D drawPoint(Texture2D tex, int x, int y, Color color){
        tex.SetPixel(x, y, color);
        return tex;
    }

    /// <summary>
    /// drawLine: draws a line between the two given coordinates to the given texture2D with the given color.
    /// </summary>
    /// <param name="tex">Texture2D texture</param>
    /// <param name="x1">the x position of the first coordinate</param>
    /// <param name="y1">the y position of the first coordinate</param>
    /// <param name="x2">the x position of the second coordinate</param>
    /// <param name="y2">the y position of the second coordinate</param>
    /// <param name="color">color of the line</param>
    /// <returns>the edited Texture2D, make sure to use texture.Apply(); after calling this method!!!</returns>
    public static Texture2D drawLine(Texture2D tex, int x1, int y1, int x2, int y2, Color color){
        int dy = (int)(y2-y1);
	    int dx = (int)(x2-x1);
 	    int stepx, stepy;
 
	    if (dy < 0) {dy = -dy; stepy = -1;}
	    else {stepy = 1;}
	    if (dx < 0) {dx = -dx; stepx = -1;}
	    else {stepx = 1;}
	    dy <<= 1;
	    dx <<= 1;
 
	    float fraction = 0;
 
	    tex.SetPixel(x1, y1, color);
	    if (dx > dy) {
		    fraction = dy - (dx >> 1);
		    while (Mathf.Abs(x1 - x2) > 1) {
			    if (fraction >= 0) {
				    y1 += stepy;
				    fraction -= dx;
			    }
			    x1 += stepx;
			    fraction += dy;
			    tex.SetPixel(x1, y1, color);
		    }
	    }
	    else {
		    fraction = dx - (dy >> 1);
		    while (Mathf.Abs(y1 - y2) > 1) {
			    if (fraction >= 0) {
				    x1 += stepx;
				    fraction -= dy;
			    }
			    y1 += stepy;
			    fraction += dx;
			    tex.SetPixel(x1, y1, color);
		    }
	    }
        return tex;
    }

    /// <summary>
    /// drawRect: draws a rectangle between the two given coordinates to the given texture2D with the given color.  Can be filled with color or not.
    /// </summary>
    /// <param name="tex">Texture2D texture</param>
    /// <param name="x1">the x position of the first coordinate</param>
    /// <param name="y1">the y position of the first coordinate</param>
    /// <param name="x2">the x position of the second coordinate</param>
    /// <param name="y2">the y position of the second coordinate</param>
    /// <param name="color">color of rectangle</param>
    /// <param name="fill">Choice to fill entire shape with the given color, true will fill shape, false will not</param>
    /// <returns>the edited Texture2D, make sure to use texture.Apply(); after calling this method!!!</returns>
    public static Texture2D drawRect(Texture2D tex, int x1, int y1, int x2, int y2, Color color, bool fill){
        if (fill){
            int h = y2 - y1;
            while (h != 0){
                int w = x2 - x1;
                while (w != 0){
                    tex.SetPixel(w + x1, h + y1, color);
                    w -= Math.Sign(w);
                }
                h -= Math.Sign(h);
            }
        }
        else {
            int[] temp_coor = new int[10];
            temp_coor[0] = x1;
            temp_coor[1] = y1;
            temp_coor[2] = x1;
            temp_coor[3] = y2;
            temp_coor[4] = x2;
            temp_coor[5] = y2;
            temp_coor[6] = x2;
            temp_coor[7] = y1;
            temp_coor[8] = x1;
            temp_coor[9] = y1;
            for (int c = 0; c < 4; c++){
                x1 = temp_coor[c * 2];
                y1 = temp_coor[(c * 2) + 1];
                x2 = temp_coor[(c * 2) + 2];
                y2 = temp_coor[(c * 2) + 3];

                int dy = (int)(y2-y1);
	            int dx = (int)(x2-x1);
 	            int stepx, stepy;
 
	            if (dy < 0) {dy = -dy; stepy = -1;}
	            else {stepy = 1;}
	            if (dx < 0) {dx = -dx; stepx = -1;}
	            else {stepx = 1;}
	            dy <<= 1;
	            dx <<= 1;
 
	            float fraction = 0;
 
	            tex.SetPixel(x1, y1, color);
	            if (dx > dy) {
		            fraction = dy - (dx >> 1);
		            while (Mathf.Abs(x1 - x2) > 1) {
			            if (fraction >= 0) {
				            y1 += stepy;
				            fraction -= dx;
			            }
			            x1 += stepx;
			            fraction += dy;
			            tex.SetPixel(x1, y1, color);
		            }
	            }
	            else {
		            fraction = dx - (dy >> 1);
		            while (Mathf.Abs(y1 - y2) > 1) {
			            if (fraction >= 0) {
				            x1 += stepx;
				            fraction -= dy;
			            }
			            y1 += stepy;
			            fraction += dx;
			            tex.SetPixel(x1, y1, color);
		            }
	            }
            }
        }
        return tex;
    }

    /// <summary>
    /// drawCircle: draws a circle with the radius and the x and y coordinates given to the given texture2D with the given color.  Can be filled with color or not.
    /// </summary>
    /// <param name="tex">Texture2D texture</param>
    /// <param name="x">x coordinate of the circle</param>
    /// <param name="y">y coordinate of the circle</param>
    /// <param name="r">radius of the circle</param>
    /// <param name="color">color of circle</param>
    /// <param name="fill">Choice to fill entire shape with the given color, true will fill shape, false will not</param>
    /// <returns>the edited Texture2D, make sure to use texture.Apply(); after calling this method!!!</returns>
    public static Texture2D drawCircle(Texture2D tex, int x, int y, int r, Color color, bool fill){
        if (fill){
            for (int h = 0; h <= r; h++){
                for (int w = 0; w <= r; w++){
                    if (((w * w) + (h * h)) <= (r * r)){
                        tex.SetPixel(x + w, y + h, color);
                        tex.SetPixel(x + w, y - h, color);
                        tex.SetPixel(x - w, y + h, color);
                        tex.SetPixel(x - w, y - h, color);
                    }
                }
            }
        }
        for (int l = 0; l <= r; l++){
            int cx = l;
            int cy = (int) Math.Round(Math.Sqrt((r * r) - (l * l)));

            tex.SetPixel(x - cx, y + cy, color); //0a
            tex.SetPixel(x + cx, y + cy, color); //0b

            tex.SetPixel(x + cy, y + cx, color); //1a
            tex.SetPixel(x + cy, y - cx, color); //1b
                
            tex.SetPixel(x + cx, y - cy, color); //2a
            tex.SetPixel(x - cx, y - cy, color); //2b

            tex.SetPixel(x - cy, y - cx, color); //3a
            tex.SetPixel(x - cy, y + cx, color); //3b
        }
        return tex;
    }

    /// <summary>
    /// drawRoundRect: a Rectangle with round edges drawn between the two given coordinates and the radius drawn on to the given texture2D with the given color.  Can be filled with color or not.
    /// </summary>
    /// <param name="tex">Texture2D texture</param>
    /// <param name="x1">the x position of the first coordinate</param>
    /// <param name="y1">the y position of the first coordinate</param>
    /// <param name="x2">the x position of the second coordinate</param>
    /// <param name="y2">the y position of the second coordinate</param>
    /// <param name="r">radius of the round edges of the rectangle, will not be any larger than half the length of the smallest side of the rectangle</param>
    /// <param name="color">color of the Rounded Rectangle</param>
    /// <param name="fill">Choice to fill entire shape with the given color, true will fill shape, false will not</param>
    /// <returns>the edited Texture2D, make sure to use texture.Apply(); after calling this method!!!</returns>
    public static Texture2D drawRoundRect(Texture2D tex, int x1, int y1, int x2, int y2, int r, Color color, bool fill){

        int[] rrxy = new int[8];
        rrxy[0] = Math.Max(x1, x2);
        rrxy[1] = Math.Max(y1, y2);
        rrxy[2] = Math.Max(x1, x2);
        rrxy[3] = Math.Min(y1, y2);
        rrxy[4] = Math.Min(x1, x2);
        rrxy[5] = Math.Min(y1, y2);
        rrxy[6] = Math.Min(x1, x2);
        rrxy[7] = Math.Max(y1, y2);

        int l = Math.Min(Math.Abs(x1 - x2) , Math.Abs(y1 - y2));
        r = Clamp(r, 0, l / 2);

        for (int t = 0; t <= r; t++){
            int cx = t;
            int cy = (int) Math.Round(Math.Sqrt((r * r) - (t * t)));

            tex.SetPixel((rrxy[0] - r) + cx, (rrxy[1] - r) + cy, color); //0b
            tex.SetPixel((rrxy[0] - r) + cy, (rrxy[1] - r) + cx, color); //1a
            
            tex.SetPixel((rrxy[2] - r) + cy, (rrxy[3] + r) - cx, color); //1b
            tex.SetPixel((rrxy[2] - r) + cx, (rrxy[3] + r) - cy, color); //2a
            
            tex.SetPixel((rrxy[4] + r) - cx, (rrxy[5] + r) - cy, color); //2b
            tex.SetPixel((rrxy[4] + r) - cy, (rrxy[5] + r) - cx, color); //3a

            tex.SetPixel((rrxy[6] + r) - cx, (rrxy[7] - r) + cy, color); //0a
            tex.SetPixel((rrxy[6] + r) - cy, (rrxy[7] - r) + cx, color); //3b
        }

        if (fill){
            for (int w = 0; w <= r; w++){
                for (int h = 0; h <= (Math.Abs(y1 - y2) - (r * 2)); h++){
                    tex.SetPixel(rrxy[4] + w, rrxy[5] + r + h, color);
                    tex.SetPixel(rrxy[2] - w, rrxy[3] + r + h, color);
                }
            }
            for (int w = 0; w <= (Math.Abs(x1 - x2) - (r * 2)); w++){
                for (int h = 0; h <= Math.Abs(y1 - y2); h++){
                    tex.SetPixel(rrxy[4] + w + r, rrxy[5] + h, color);
                }
            }
            for (int w = 0; w <= r; w++){
                for (int h = 0; h <= r; h++){
                    if ((w * w) + (h * h) <= (r * r)){
                        tex.SetPixel((rrxy[0] - r) + w, (rrxy[1] - r) + h, color);
                        tex.SetPixel((rrxy[2] - r) + w, (rrxy[3] + r) - h, color);
                        tex.SetPixel((rrxy[4] + r) - w, (rrxy[5] + r) - h, color);
                        tex.SetPixel((rrxy[6] + r) - w, (rrxy[7] - r) + h, color);
                    }
                }
            }
        }
        else {
            drawLine(tex, rrxy[0], rrxy[1] - r, rrxy[2], rrxy[3] + r, color);
            drawLine(tex, rrxy[2] - r, rrxy[3], rrxy[4] + r, rrxy[5], color);
            drawLine(tex, rrxy[4], rrxy[5] + r, rrxy[6], rrxy[7] - r, color);
            drawLine(tex, rrxy[6] + r, rrxy[7], rrxy[0] - r, rrxy[1], color);
        }

        return tex;
    }

    public static int Clamp(int value, int num1, int num2){
        if (value < Math.Min(num1, num2)){
            value = Math.Min(num1, num2);
        }
        else if (value > Math.Max(num1, num2)){
            value = Math.Max(num1, num2);
        }
        return value;
    }
}
