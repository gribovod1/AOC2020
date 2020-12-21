using AOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AOC2020
{
    public class Tile
    {
        public Dictionary<BoardSide,Tile> Neirb = new Dictionary<BoardSide, Tile>();
        public ulong Id;
        public List<string> Data = new List<string>();
        public int Rotate;
        public bool Flip;

        public enum BoardSide
        {
            Right = 1,
            Up,
            Left,
            Down
        }

        public Tile(string text)
        {
            var ss = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var idText = ss[0].Split(new string[] { " ", ":" }, StringSplitOptions.RemoveEmptyEntries);
            Id = ulong.Parse(idText[1]);

            Data = new List<string>();
            for (var s = 1; s < ss.Length; ++s)
                Data.Add(ss[s]);
        }

        List<KeyValuePair<string,BoardSide>> UpBoards()
        {
            var result = new List<KeyValuePair<string, BoardSide>>();
            result.Add(new KeyValuePair<string, BoardSide>(Data[0],BoardSide.Up));
            var left = new StringBuilder();
            for (var i = Data.Count - 1; i >= 0; --i)
                left.Append(Data[i][0]);
            result.Add(new KeyValuePair<string, BoardSide>(left.ToString(),BoardSide.Left));
            var down = new StringBuilder();
            for (var i = Data.Count - 1; i >= 0; --i)
                down.Append(Data[Data.Count - 1][i]);
            result.Add(new KeyValuePair<string, BoardSide>(down.ToString(),BoardSide.Down));
            var right = new StringBuilder();
            for (var i = 0; i < Data.Count; ++i)
                right.Append(Data[i][Data[i].Length - 1]);
            result.Add(new KeyValuePair<string, BoardSide>(right.ToString(),BoardSide.Right));
            return result;
        }

        List<KeyValuePair<string, BoardSide>> DownBoards()
        {
            var result = new List<KeyValuePair<string, BoardSide>>();
            result.Add(new KeyValuePair<string, BoardSide>(Data[Data.Count - 1],BoardSide.Down));
            var left = new StringBuilder();
            for (var i = Data.Count - 1; i >= 0; --i)
                left.Append(Data[i][Data[i].Length - 1]);
            result.Add(new KeyValuePair<string, BoardSide>(left.ToString(),BoardSide.Left));
            var up = new StringBuilder();
            for (var i = Data.Count - 1; i >= 0; --i)
                up.Append(Data[0][i]);
            result.Add(new KeyValuePair<string, BoardSide>(up.ToString(),BoardSide.Up));
            var right = new StringBuilder();
            for (var i = 0; i < Data.Count; ++i)
                right.Append(Data[i][0]);
            result.Add(new KeyValuePair<string, BoardSide>(right.ToString(),BoardSide.Right));
            return result;
        }

        public bool IsNeirb(Tile other)
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
                    if (otherUp[oU].Key == db[dIndex].Key)
                    {
                        Neirb.Add(db[dIndex].Value, other);
                        other.Neirb.Add(calcOtherSide(dIndex, db[dIndex].Value, oU),this);
                        return true;
                    }
                for (var oD = 0; oD < otherDown.Count; ++oD)
                    if (otherDown[oD].Key == db[dIndex].Key)
                    {
                        Neirb.Add(db[dIndex].Value, other);
                        other.Neirb.Add(calcOtherSide(dIndex, db[dIndex].Value, oD), this);
                        other.Flip = true;
                        return true;
                    }
            }
            return false;
        }

        BoardSide calcOtherSide(int board, BoardSide side, int otherBoard)
        {
            throw new Exception("");
        }

        int calcRotate()
        {
            return 0;
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
            var newData = new List<string>();
            for (var row = 0; row < Data.Count; ++row)
            {
                var sb = new StringBuilder();
                for (var col = 0; col < Data[0].Length; ++col)
                    if (Flip)
                    {

                    }
                    else
                    {

                    }
                newData.Add(sb.ToString());
            }
        }

        public void addTile(char[,] image, int x, int y)
        {
            for (var yData = 0; yData < Data.Count; ++yData)
                for (var xData = 0; xData < Data[yData].Length; ++xData)
                    image[y * Data.Count + yData,x * Data[0].Length + xData] = Data[yData][xData];
        }

        public bool isLeftUp()
        {
            return !Neirb.ContainsKey(BoardSide.Up) && !Neirb.ContainsKey(BoardSide.Left);
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
                    Tiles[i].IsNeirb(Tiles[j]);

            for (var i = 0; i < Tiles.Count; ++i)
                if (Tiles[i].Neirb.Count == 2)
                    result *= Tiles[i].Id;

            return result;
        }

        ulong PartTwo()
        {
            ulong result = 0;
            var image = GetImage();
            var height = CalcHeight() * Tiles[0].Data.Count;
            var width = CalcWidth() * Tiles[0].Data[0].Length;
            Console.WriteLine("IMAGE:");
            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < height; ++x)
                    Console.Write(image[x,y]);
                Console.WriteLine();
            }
                    return result;
        }

        char[,] GetImage()
        {
            for (var i = 0; i < Tiles.Count; ++i)
            {
                Tiles[i].TrimBoards();
                Tiles[i].Transform();
            }
            var height = CalcHeight();
            var width = CalcWidth();
            var result = new char[width * Tiles[0].Data[0].Length, height * Tiles[0].Data.Count];

            var startRowTile = FindLeftUp();
            for (var row = 0; row < height; ++row)
            {
                Tile currTile = startRowTile;
                for (var col = 0; col < width; ++col)
                {
                    currTile.addTile(result, row, col);
                    currTile = currTile.Neirb[Tile.BoardSide.Right];
                }
                startRowTile = startRowTile.Neirb[Tile.BoardSide.Down];
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
            while(tile.Neirb.ContainsKey(Tile.BoardSide.Right))
            {
                ++result;
                tile = tile.Neirb[Tile.BoardSide.Right];
            }
            return result;
        }

        int CalcHeight()
        {
            var tile = FindLeftUp();
            var result = 0;
            while (tile.Neirb.ContainsKey(Tile.BoardSide.Down))
            {
                ++result;
                tile = tile.Neirb[Tile.BoardSide.Down];
            }
            return result;
        }
    }
}