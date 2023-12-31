﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2023_02
{

    internal partial class Program
    {
       static List<string> d16_data =
        """
        \|.................................../....\\/........................-.....|.|..-.....|\...................../
        |-.....|...\........|........................../-...../...--.|..\..........|......./.......-......|......./.-.
        .....\..../......-......|.........................-............|..../.....|.......-........../................
        ........\..|.....|................/.......-\..............\...../....|............\....\......................
        -................\....-..........-................./.......//...............|..|...................-..........
        ...................|..................................|.....|.....-....................../....|..\...\..\.|-..
        .......-.............../......\...................................-......../.......-.\|.................../...
        ......\..................\.............-..........././....|-.....|......................../.-.....|......\....
        ......-............-..................|..................../......-...|.....................\.................
        .........\..........-.......-./...-.../..\...../............................|................/....|...........
        .\....../....|...............|..../.......................-...................................-......|..\.....
        ......................../...|.|........|.........|.....................-............../.-.../.-...............
        ..|..-.-......|.......\....................-|.......|............-............................................
        .........................|.............-..........................-.\\......|.......|........-.........\......
        .../....-....\............/...................|........./..\.......\...........||.|......\.......\..-./.......
        .../..\.|..-....../.......................-.......-.......|/...........-.......-.......\......................
        ..\...........-...-.......-.........|\....................../.-...........................\...........-.......
        ..../.....||.....................-.........................................../............././................
        ...................-..\................\................................................../.../.........../...
        \..................-.-..........//....|......./........-.............../.........\............................
        ................/...|................../......./..............|.....|..............|..-...........|...........
        ..../........-......\.|...-................./...................../|.........\................................
        ...../...................-........|.......................-...\.............................\.../.............
        ...../....-......................./..........................-............-........\......-.....\.../.\.\.....
        ..|......-.\..........\........................\........../.........|...|............./............../-.......
        ./..\-.......|.........|.\/........-........|.|...|..|....\...|.\.....|..|..-...............\.................
        ..|../..........\................./...\...........|.....................-\..................-..|.........-..-.
        ........./......||.....................................|..\.-|-................//..|-...|............-.\......
        ........\..........\..........\......................./......................./......|........-.......|...-...
        .........././.....-...-..................-.|..........................-.............-......................./.
        .\..........|...........--....................\...-.\.........\......-.................../....|./..\..........
        ......\.........\.|..||.....|...../........-..........|....|.....|....\..|..........\.........|...............
        ...........\....-.../......-............/.............-......../.-...................-....|.....-\.......|....
        /.....................-.../...............-./....................../|....|.-...|.................\......../...
        ................../.............|..........\..|..\....|....-...../...-.......\../......\....-...../.||........
        ./-|.......\..../........../-....../......./...-....-................|............|.................-..../....
        .....................\....................|\.-../.......................|.......|.....-........-./..|.........
        ..........|...\......\......................|./...........--................-..........-.......\...\-..\|.\..\
        .......\...-|................\....|......................../............................\..........-.\........
        ./.......|................\...\........./.........|..................|.....-.......-.....|..|-............-...
        ...|............/....-.\...........\...../..|..-...../..|.......-........\...-..............-.................
        .\-.....................-.........-.\.................|....-.....\....\.\\.//|.........\../.\.........../..-..
        ......................................|\-........../..-.........-.........................................-|..
        ........./.../..|.....-............./..\............./............../..............-\.......|....-....-.......
        .|............|......-/.-......-......-............-........|...../......\/....\../........|.......|..........
        .......|...........\.................-......................\..-...........................|..................
        |.........\..\.....-.../.........................-\.................|.........\...............................
        ....-..........\.....././.............../..........-|...|...../........................-......|....-..........
        ....................\.|.........\....|..........-.............\..........................|...........\........
        .............|.../......|.........../....................../....-..................../......................./
        .\...|....-../.............|............-..-.......||..........|............\/..................\...-.........
        ........\/....-...................../..............-.....................-................................-...
        /-..........-......\.....|.....\....\............./..../...........|........-....\...............\.........|..
        ...../..................\........................./.-...../.....\....-..-\.....\...|................./........
        ......../......./.......................-.......-..\//...........-|.............../...\.\./..\................
        ..............................|...................../......\..|.............\\......................./..-.....
        ....\.....|..............|..................|...|.............|............--.......-..-.................../..
        ....|.............../.\.............................................|...................../.........\.........
        .........\...../.../......\....................................................|.....-.../...\..........\.....
        -\............\.......................-...................-..../-........./.................................-.
        |.....\.....................\...................|..|..\....../.......\...|......\.............../-.-......|...
        .......|.........\.................|..-.............................\............................/............
        ........................-.-........./..../.|.|.../..............\../....-..........|............../.....\\....
        ...........\-...........||..........................................\.........|...........-...-...........-\..
        ..................|................................./.................||.|..........|.....................|.|.
        ....../.............................|.............................-/..|......./............................../
        ./................../.|................................-...\.......|..................|............\-..|.....|
        \.....|.-...........-|.................../............\................../......................./.......\....
        .\.....................\.............|...............-......\...-........-...............-..........|../......
        .../..\...|........\\....\............-..|.....|........-/..........|....|.........\..|/...-.....-.\.../.....\
        ......................../....-................................\..|.........................\..................
        ..................\....-............\\\/..........|.................|...../..|.............|..................
        ..|./.......|.....-............|...............|....|............-...\...........................\.\..........
        .....|-..\...\.........................|../...................../......\............\.........................
        -..-...........-............|......../................./......\.....-...|...............|.....................
        ........|.......|.......\.|...\...../..\.../......................\.......\...\..................\....-..../..
        ....-.-./.|...............-.-.............-.............../......./..........|.|../...............|.-.........
        ...........\..-............../..........-..-../........../.....-/-|.........................-.............\...
        ............................\.......//.|......../...........|\.....\...........-..-.......-/-.................
        ..................................\..-..........|./..................../\............./............../........
        .....\..........\.......-............/.....................-.|....-......................./.|.................
        ...............................|.......\........../.............-.....|.......\..\........................-...
        ......\.........\\............../..-..............\...|..--......-.....\......./................./............
        .\.......\.....-...........-......................\........................../......../..../.-./..\........../
        .......|.......|........|...\\................./..../..\....\...\...........-.....................\.........-/
        ....../........................................\..........-..|...............\....................|...-.......
        .|.|......./..........\........|......-......-............................|..|..\.........\.......-...........
        .....\.\-.-......\.........../.......\\..|..\........\/....\|.............................................\...
        .....................-.......................|............................................../.................
        ..................\...-|............|........................-....../...........||............................
        ........./............................./.....-....../.-...........-...........................................
        .........|...........-.........\......\.............\-.....|...-...|-.../..|.............\.....|....-.........
        .....|.....................................|.../........./|...|.|..................\..........................
        ..\...........-.............|................//.............|........|.........../.\..............\...........
        ..-\..-............|.\............../../.....||.../......-......|...-/...|..../../...................-/.\.....
        .......\.........../.\......\.........\...........-..............\..............|.........-..........-.-......
        ../...................\|............................../...................|.............../.............-/....
        ................/\.................|.....................\...|.\....................-.....-...................
        ......................................................|................................\..\../...|.......|.-./
        .........//..|.......\.-...............|............................................................-.|.......
        ........../.\........\............-./.\....../..//...........\..........|.\.\...|/\...-...\...................
        ...-................/............../.............................................-../.-........\........../...
        ...................-...............-.....-............/................./|.-.|...\.\...............-..-......|
        \....../.....|......\.........|../.....|......-..../....................-................/...\......\....../..
        ...../...-.......................\..|\...-...\.|.........|.......|.................\.............-............
        ...........................\/............................../...........-.......-..../......../......\...-.....
        .............................|.-......................-|\...--...............................\..............\.
        ..............\.|......./..........\..........\/.../......|.....-../......../.....\...|-...........\........./
        .............../........\...................../...|......................-...|.|......./.....-................
        ..-...............--..-...-............-..-\......................-.......|..............|........\.\.....-...
        """.Split("\r\n").ToList();
    }
}
