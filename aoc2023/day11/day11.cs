﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace aoc2023_02
{
    internal partial class Program
    {
        class c11Coord
        {            
            public c11Coord(char isc, int ir, int ic)
            {
                this.s = isc;
                this.r = ir;
                this.c = ic;
            }

            public c11Coord Clone()
            {
                return new c11Coord(s, r, c) { 
                    ParentMap = this.ParentMap,
                    IsColExpansion = this.IsColExpansion,
                    IsRowExpansion = this.IsRowExpansion,
                    GalId = this.GalId,
                };
            }

            public char s { get; set; }
            public int r { get; set; }
            public int c { get; set; }

            public c11Map ParentMap { get; set; }
            public bool IsColExpansion { get; set; }
            public bool IsRowExpansion { get; set; }
            public int GalId { get; set; }





            class c6Coord
            {
                public c6Coord(char isc, int ir, int ic)
                {
                    this.s = isc;
                    this.r = ir;
                    this.c = ic;

                    if (IsGuard)
                        origionalGuardDir = isc;
                }


                public char s { get; set; }
                public int r { get; set; }
                public int c { get; set; }

                public readonly char origionalGuardDir;
                public bool IsOrigionalGuardCell => "^v<>".Any(x => x == origionalGuardDir);

                public string WasVisitedDirection = "";
                public bool WasVisited => WasVisitedDirection != "";
                public bool IsRepeatedMove;

                public c6Map ParentMap { get; set; }

                public bool IsGuard => "^v<>".Any(x => x == s);
                public bool IsObstacle => s == '#' || s == 'X';

                public void Reset()
                {
                    if (IsOrigionalGuardCell)
                        s = origionalGuardDir;
                    else
                        s = '.';

                    WasVisitedDirection = "";
                    IsRepeatedMove = false;

                }

                void rotateGuardDirectionClockWise()
                {
                    if (IsGuard)
                    {
                        s = s == '^' ? '>'
                          : s == '>' ? 'v'
                          : s == 'v' ? '<'
                          : s == '<' ? '^'
                          : s;
                    }
                }

                public c6Coord GuardNextMoveCell()
                {
                    if (!IsGuard) return null;

                    WasVisitedDirection += s;

                    var mc = s == '^' ? CellNorth
                            : s == 'v' ? CellSouth
                            : s == '>' ? CellEast
                            : s == '<' ? CellWest
                            : null;
                    if (mc == null)
                    {
                        return null;
                    }
                    else if (mc.IsObstacle)
                    {
                        rotateGuardDirectionClockWise();
                        return this;
                    }
                    else
                    {
                        mc.s = this.s;
                        if (!mc.WasVisitedDirection.Any(x => x == mc.s))
                            mc.WasVisitedDirection += mc.s;
                        else
                            mc.IsRepeatedMove = true;

                        this.s = '.';

                        return mc;
                    }

                }


                c6Coord CellNorth => NeighborCellsBase[0];
                c6Coord CellSouth => NeighborCellsBase[1];
                c6Coord CellEast => NeighborCellsBase[2];
                c6Coord CellWest => NeighborCellsBase[3];
                c6Coord CellNE => NeighborCellsBase[4];
                c6Coord CellNW => NeighborCellsBase[5];
                c6Coord CellSE => NeighborCellsBase[6];
                c6Coord CellSW => NeighborCellsBase[7];
                public List<c6Coord> NeighborCells => NeighborCellsBase.Where((x, index) => x != null && index < 4).ToList();
                public List<c6Coord> NeighborCellsBase
                {
                    get
                    {
                        {
                            var nc = new List<c6Coord>();
                            var rc = ParentMap.rowCnt - 1;
                            var cc = ParentMap.colCnt - 1;
                            if (r > 0) nc.Add(ParentMap[r - 1][c + 0]); else nc.Add(null);//N
                            if (r < rc) nc.Add(ParentMap[r + 1][c + 0]); else nc.Add(null);//S
                            if (c < cc) nc.Add(ParentMap[r + 0][c + 1]); else nc.Add(null);//E
                            if (c > 0) nc.Add(ParentMap[r + 0][c - 1]); else nc.Add(null);//W


                            if (r > 0 && c < cc) nc.Add(ParentMap[r - 1][c + 0]); else nc.Add(null);//NE
                            if (r > 0 && c > 0) nc.Add(ParentMap[r - 1][c + 0]); else nc.Add(null);//NW
                            if (r < rc && c < cc) nc.Add(ParentMap[r + 1][c + 0]); else nc.Add(null);//SE
                            if (r < rc && c > 0) nc.Add(ParentMap[r + 1][c + 0]); else nc.Add(null);//SW

                            return nc;
                        }
                    }
                }

                public bool Equals(c6Coord ic)
                {
                    if (ic is null) return false;
                    if (r == ic.r && c == ic.c && s == ic.s && WasVisitedDirection == ic.WasVisitedDirection) return true;
                    return false;
                }

                public override string ToString() => $"{s}[{r},{c}]";

            }


            class c6Map : List<List<c6Coord>>
            {
                public c6Map(string[] ls)
                {
                    for (int i = 0; i < ls.Count(); i++)
                    {
                        var ca = ls[i].ToCharArray();
                        var xcl = new List<c6Coord>();
                        for (int j = 0; j < ca.Length; j++)
                        {
                            xcl.Add(new c6Coord(ca[j], i, j) { ParentMap = this });
                        }
                        this.Add(xcl);
                    }

                }

                public int colCnt => this[0].Count;
                public int rowCnt => Count;
                public int cellCnt => colCnt * rowCnt;


                public void Reset()
                {
                    foreach (var cell in Cells.Where(c => c.WasVisited))
                    {
                        cell.Reset();
                    }
                }

                public int MoveGaurdTillOffMap()
                {
                    //PrintMap();
                    var mg = GuardPos.GuardNextMoveCell();
                    while (mg != null && !mg.IsRepeatedMove)
                    {
                        mg = mg.GuardNextMoveCell();
                        //PrintMap();
                    }

                    if (mg != null && mg.IsRepeatedMove)
                        return -9;
                    else
                        return Cells.Count(c => c.WasVisited);
                }

                c6Coord GuardPos => Cells.Where(x => x.IsGuard).FirstOrDefault();
                c6Coord GuardPosOrigional => Cells.Where(x => x.IsOrigionalGuardCell).FirstOrDefault();


                public IEnumerable<c6Coord> Cells => this.SelectMany(x => x);

                public void PrintMap()
                {
                    Console.WriteLine($"\n\n");
                    foreach (var r in this)
                    {
                        foreach (var c in r)
                            Console.Write(
                              c.IsOrigionalGuardCell ? '@'
                            : c.WasVisited ? 'O'
                            : c.s);
                        Console.Write("\n");
                    }
                    Console.WriteLine($"\n\n");
                }

            }


            c11Coord CellNorth => NeighborCellsBase[0];
            c11Coord CellSouth => NeighborCellsBase[1];
            c11Coord CellEast  => NeighborCellsBase[2];
            c11Coord CellWest  => NeighborCellsBase[3];
            c11Coord CellNE    => NeighborCellsBase[4];
            c11Coord CellNW    => NeighborCellsBase[5];
            c11Coord CellSE    => NeighborCellsBase[6];
            c11Coord CellSW    => NeighborCellsBase[7];
            public List<c11Coord> NeighborCells => NeighborCellsBase.Where((x,index) => x != null && index < 4).ToList();
            public List<c11Coord> NeighborCellsBase
            {
                get
                {
                    {
                        var nc = new List<c11Coord>();
                        var rc = ParentMap.rowCnt - 1;
                        var cc = ParentMap.colCnt - 1;
                        if (r >  0) nc.Add(ParentMap[r - 1][c + 0]); else nc.Add(null);//N
                        if (r < rc) nc.Add(ParentMap[r + 1][c + 0]); else nc.Add(null);//S
                        if (c < cc) nc.Add(ParentMap[r + 0][c + 1]); else nc.Add(null);//E
                        if (c >  0) nc.Add(ParentMap[r + 0][c - 1]); else nc.Add(null);//W


                        if (r >  0 && c < cc) nc.Add(ParentMap[r - 1][c + 0]); else nc.Add(null);//NE
                        if (r >  0 && c >  0) nc.Add(ParentMap[r - 1][c + 0]); else nc.Add(null);//NW
                        if (r < rc && c < cc) nc.Add(ParentMap[r + 1][c + 0]); else nc.Add(null);//SE
                        if (r < rc && c >  0) nc.Add(ParentMap[r + 1][c + 0]); else nc.Add(null);//SW

                        return nc;
                    }
                }
            }



            public bool Equals(c11Coord ic)
            {
                if (ic is null) return false;
                if (r == ic.r && c == ic.c && s == ic.s) return true;
                return false;
            }

            public override string ToString() => $"{s}[{r},{c}]" + (GalId > 0 ? $"-g{GalId}" : "");

        }


        class c11Map : List<List<c11Coord>>
        {
            public c11Map(string[] ls)
            {
                var xc = new c11Coord('.', -1, -1) { ParentMap = this, IsColExpansion=true };
                var gi = 1;
                for (int i = 0; i < ls.Count(); i++)
                {
                    var ca = ls[i].ToCharArray();
                    var xcl = new List<c11Coord>();
                    for (int j = 0; j < ca.Length; j++)
                    {
                        xcl.Add(new c11Coord(ca[j], -1, -1) { ParentMap = this, GalId = ca[j] == '#' ? gi : 0 });
                        if (ca[j] == '#') gi++;
                    }
                    this.Add(xcl);

                    //add expansion row
                    if (xcl.All(c => c.s == '.')) this.Add(xcl.Select(c => xc.Clone()).ToList());
                }

                //reNumber row col indices
                for (int i = 0; i < rowCnt; i++)
                {
                    for (int j = 0; j < colCnt; j++)
                    {
                        this[i][j].r = i;
                        this[i][j].c = j;
                    }
                }

                //add expansion columns
                var aci = new List<int>();
                for (int i = 0; i < colCnt; i++)
                {
                    if (this.SelectMany(x => x).Where(x => x.c== i).All(x => x.s == '.'))
                    {
                        aci.Add(i);
                    }
                }
                aci.Reverse();
                foreach (var r in this)
                {
                    for (int i = 0; i < aci.Count; i++)
                    {
                        if (r.Any(x => x.IsColExpansion))
                            r.Insert(aci[i], new c11Coord('.', -1, -1) { ParentMap = this, IsColExpansion = true, IsRowExpansion=true });
                        else
                            r.Insert(aci[i], new c11Coord('.', -1, -1) { ParentMap = this, IsRowExpansion = true });
                    }
                }

                //reNumber row col indices
                for (int i = 0; i < rowCnt; i++)
                {
                    for (int j = 0; j < colCnt; j++)
                    {
                        this[i][j].r = i;
                        this[i][j].c = j;
                    }
                }

            }
            

            public int colCnt => this[0].Count;
            public int rowCnt => Count;
            public int cellCnt => colCnt * rowCnt;


            public IEnumerable<c11Coord> Cells => this.SelectMany(x => x); 


        }

        class c11Pair 
        { 
            public c11Coord g1 { get; set; } 
            public c11Coord g2 { get; set; } 
            public double distanceBetween { get; set; }

            public override string ToString() => $"{g1} | {g2} | {distanceBetween.ToString("#0.0")}";
        }

        
        static void day11()
        {
            var map = new c11Map(d11_data);


            Console.WriteLine($"map size=: rows: {map.rowCnt}: columns: {map.colCnt}: cells: {map.cellCnt}");
            Console.WriteLine($"galaxy count: = {map.SelectMany(x=>x).Count(c=>c.s!='.')}");
            Console.WriteLine($"\n\n");


            var gal = map.Cells.Where(c=>c.GalId>0).ToList();
            //var galPairs = GetPowerSet<c11Coord>(gal).Where(l => l.Count() == 2).Select(x=>new cllPair() { g1 = x.ElementAt(0), g2 = x.ElementAt(1)}).ToList();
            var galPairs = gal.DifferentCombinations(2).Select(x => new c11Pair() { g1 = x.ElementAt(0), g2 = x.ElementAt(1) }).ToList();
            


            //Calc base distances
            foreach (var gp in galPairs)
            {
                gp.distanceBetween = gp.g1.CalcDistToGalaxyBlock(gp.g2);
            }

            Console.WriteLine($"Answer1: {galPairs.Sum(x => x.distanceBetween)}");



            //Calc dis with 1,000,000 expansion factor
            foreach (var gp in galPairs)
            {
                gp.distanceBetween = gp.g1.CalcDistToGalaxyBlock(gp.g2, 1000000);
            }

            Console.WriteLine($"Answer2: {galPairs.Sum(x => x.distanceBetween)}");



            //Print Map
            Console.WriteLine($"\n\n");
            foreach (var r in map) 
            {
                foreach (var c in r)
                    Console.Write(
                      c.IsColExpansion && c.IsRowExpansion? '+'
                    : c.IsColExpansion ? '-'
                    : c.IsRowExpansion ? '|'
                    : c.GalId > 0 ? c.GalId.ToString()[0]
                    : c.s);
                Console.Write("\n");
            }
            Console.WriteLine($"\n\n");


        }


        static string[] d11_data0 =
        """
        ...#......
        .......#..
        #.........
        ..........
        ......#...
        .#........
        .........#
        ..........
        .......#..
        #...#.....
        """.Split("\r\n");


    }
}
