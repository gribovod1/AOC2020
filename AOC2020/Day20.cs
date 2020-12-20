using AOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AOC2020
{
    public class Tile
    {
        public Dictionary<int,Tile> Neirb = new Dictionary<int, Tile>();
        public ulong Id;
        public List<string> Data = new List<string>();
        public int Rotate;
        public int Flip;

        public Tile(string text)
        {
            var ss = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var idText = ss[0].Split(new string[] { " ", ":" }, StringSplitOptions.RemoveEmptyEntries);
            Id = ulong.Parse(idText[1]);

            Data = new List<string>();
            for (var s = 1; s < ss.Length; ++s)
                Data.Add(ss[s]);
        }

        List<string> UpBoards()
        {
            var result = new List<string>();
            result.Add(Data[0]);
            var left = new StringBuilder();
            for (var i = Data.Count - 1; i >= 0; --i)
                left.Append(Data[i][0]);
            result.Add(left.ToString());
            var right = new StringBuilder();
            for (var i = 0; i < Data.Count; ++i)
                right.Append(Data[i][Data[i].Length - 1]);
            result.Add(right.ToString());
            var down = new StringBuilder();
            for (var i = Data.Count - 1; i >= 0; --i)
                down.Append(Data[Data.Count - 1][i]);
            result.Add(down.ToString());
            return result;
        }

        List<string> DownBoards()
        {
            var result = new List<string>();
            result.Add(Data[Data.Count - 1]);
            var left = new StringBuilder();
            for (var i = Data.Count - 1; i >= 0; --i)
                left.Append(Data[i][Data[i].Length - 1]);
            result.Add(left.ToString());
            var right = new StringBuilder();
            for (var i = 0; i < Data.Count; ++i)
                right.Append(Data[i][0]);
            result.Add(right.ToString());
            var down = new StringBuilder();
            for (var i = Data.Count - 1; i >= 0; --i)
                down.Append(Data[0][i]);
            result.Add(down.ToString());
            return result;
        }

        public bool isNeirb(Tile other)
        {
            if (other == this) return false;
            if (Neirb.ContainsValue(other))
                return true;
            var db = DownBoards();
            var otherUp = other.UpBoards();
            var otherDown = other.DownBoards();
            for (var dIndex = 0; dIndex < db.Count; ++dIndex)
            {
                for (var oU = 0; oU < otherUp.Count; ++oU)
                    if (otherUp[oU] == db[dIndex])
                    {
                        Neirb.Add(other);
                        other.Neirb.Add(this);
                        return true;
                    }
                for (var oD = 0; oD < otherDown.Count; ++oD)
                    if (otherDown[oD] == db[dIndex])
                    {
                        Neirb.Add(other);
                        other.Neirb.Add(this);
                        return true;
                    }
            }
            return false;
        }

        public void TrimBoards()
        {
            Data.RemoveAt(0);
            Data.RemoveAt(Data.Count - 1);
            for (var i = 0; i < Data.Count - 1; ++i)
                Data[i] = Data[i].Substring(1, Data[i].Length - 2);
        }

        public void Transform()
        {

        }

        void addTile(List<List<char>> image, int x, int y)
        {
            for (var yData = 0; yData < Data.Count; ++yData)
            {
                var s = image[y * Data.Count + yData];
                for (var xData = 0; xData < Data[yData].Length; ++xData)
                {
                    s[x * Data[0].Length + xData] = Data[yData][xData];
                }
            }
        }
        public bool isLeftUp()
        {
            return !Neirb.ContainsKey(2) && !Neirb.ContainsKey(3);
        }
    }


    public class Day20
    {
        public List<Tile> Tiles = new List<Tile>();

        public void Exec()
        {
            var text = File.ReadAllText(@"Data\day20.txt");
            var splited = text.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in splited)
                Tiles.Add(new Tile(s));
            var one = PartOne();
            var two = PartTwo();
            Console.WriteLine($"partOne: {one} partTwo: {two}");
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                Clipboard.SetText(one.ToString());
        }

        ulong PartOne()
        {
            ulong result = 1;
            for (var i = 0; i < Tiles.Count-1; ++i)
                for (var j = i+1; j < Tiles.Count; ++j)
                    Tiles[i].isNeirb(Tiles[j]);

            for (var i = 0; i < Tiles.Count; ++i)
                if (Tiles[i].Neirb.Count == 2)
                    result *= Tiles[i].Id;

            return result;
        }

        ulong PartTwo()
        {
            ulong result = 0;
            var image = GetImage();


            return result;
        }

        List<string> GetImage()
        {
            for (var i = 0; i < Tiles.Count; ++i)
            {
                Tiles[i].TrimBoards();
                Tiles[i].Transform();
            }
            var result = new List<List<char>>();
            var height = CalcHeight();
            var width = CalcWidth();
            for (var row = 0; row < height * Tiles[0].Data.Count; ++row)
                result.Add(new string('.', width * Tiles[0].Data[0].Length));

            var startRowTile = FindLeftUp();
            for (var row = 0; row < height; ++row)
            {
                Tile currTile = startRowTile;
                for (var col = 0; col < width; ++col)
                {
                    addTile(result, currTile, row, col);
                    currTile = currTile.Neirb[1];
                }
                startRowTile = startRowTile.Neirb[4];
            }
            return result;
        }

        Tile FindLeftUp()
        {
            foreach (var t in Tiles)
                if (t.isLeftUp())
                    return t;
            throw new Exception("Error tile placement");
        }

        int CalcWidth()
        {
            var tile = FindLeftUp();
            var result = 0;
            while(tile.Neirb.ContainsKey(1))
            {
                ++result;
                tile = tile.Neirb[1];
            }
            return result;
        }

        int CalcHeight()
        {
            var tile = FindLeftUp();
            var result = 0;
            while (tile.Neirb.ContainsKey(4))
            {
                ++result;
                tile = tile.Neirb[4];
            }
            return result;
        }
    }
}