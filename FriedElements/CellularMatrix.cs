using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FriedElements.Elements;
using SFML.Graphics;

namespace FriedElements
{
    public class CellularMatrix
    {
        private int gridSize = 32;
        public byte[] data = new byte[1024];

        public static bool Debug = false;
        private Dictionary<(int,int),Element> matrix;

        public Color BackgroundColor = Color.Black;

        public CellularMatrix(int size = 32)
        {
            matrix = new Dictionary<(int, int), Element> ();
            gridSize = size;
            data = new byte[(size * size) * 4];
        }


        public Color GetValueC(int x, int y) 
        {
            (byte R, byte G, byte B, byte A) = GetValue(x,y);
            return new Color(R, G, B, A);
        }
        private (byte, byte, byte, byte) GetValue(int x, int y)
        {
            // Calculate the index in the 1D array
            int index = (x * gridSize + y) * 4;

            // Access the value in the 1D array
            return (data[index], data[index + 1], data[index + 2], data[index + 3]);
        }

        public void SetValueC(int x, int y, Color c) => SetValue(x,y,(c.R, c.G, c.B, c.A));
        private void SetValue(int x, int y, (byte R, byte G, byte B, byte A) value)
        {
            // Calculate the index in the 1D array
            int index = (x * gridSize + y) * 4;

            // Set the value in the 1D array
            data[index] = value.R;
            data[index + 1] = value.G;
            data[index + 2] = value.B;
            data[index + 3] = value.A;
        }


        public Element Get(int x, int y) 
        {
            var key = (x,y);
            if (matrix.TryGetValue(key, out Element? element))
                return element!;
            else
                return new Empty();
        }
        public void Set(int x, int y, Element element)
        {
            if (x < 0 || y < 0 || x > gridSize || y > gridSize)
            { 
                matrix.Remove((x,y));
                return;
            }

            var key = (x, y);
            if (element is Empty)
            {
                matrix.Remove(key);
                SetValueC(x, y, BackgroundColor);
            }
            else
            { 
                element.MatrixX = x;
                element.MatrixY = y;
                matrix[key] = element;
                SetValueC(x,y,element.Color);
            }
        }
        public void StepAll() 
        {
            for (int i = 0; i < matrix.Values.Count; i++)
            {
                var element = matrix.Values.ElementAt(i);
                element.Step(this);
            }
        }
    }
}
