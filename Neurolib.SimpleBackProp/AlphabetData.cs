using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.SimpleBackProp
{
    public class AlphabetData
    {
        public static Sample[] Samples = new Sample[]
        {
            //new Sample()
            //{
            //    Input = new double[] 
            //    { 
            //        0, 0, 0, 
            //        0, 0, 0, 
            //        0, 0, 0, 
            //        0, 0, 0, 
            //        0, 0, 0
            //    },
            //    Output = 0.0,
            //    Letter = ' ',
            //},
            //new Sample()
            //{
            //    Input = new double[] 
            //    { 
            //        1, 1, 1, 
            //        1, 1, 1, 
            //        1, 1, 1, 
            //        1, 1, 1, 
            //        1, 1, 1
            //    },
            //    Output = 0.30,
            //    Letter = '!',
            //},
            new Sample()
            {
                Input = new double[] 
                { 
                    0, 1, 0, 
                    1, 0, 1, 
                    1, 1, 1, 
                    1, 0, 1, 
                    1, 0, 1
                },
                Output = 0.01,
                Letter = 'A',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 0,
                    1, 0, 1,
                    1, 1, 0,
                    1, 0, 1,
                    1, 1, 0
                },
                Output = 0.02,
                Letter = 'B',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    1, 0, 0,
                    1, 0, 0,
                    1, 0, 0,
                    1, 1, 1
                },
                Output = 0.03,
                Letter = 'C',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 0,
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 0
                },
                Output = 0.04,
                Letter = 'D',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    1, 0, 0,
                    1, 1, 1,
                    1, 0, 0,
                    1, 1, 1
                },
                Output = 0.05,
                Letter = 'E',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    1, 0, 0,
                    1, 1, 1,
                    1, 0, 0,
                    1, 0, 0
                },
                Output = 0.06,
                Letter = 'F',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    1, 0, 0,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1
                },
                Output = 0.07,
                Letter = 'G',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1,
                    1, 0, 1,
                    1, 0, 1
                },
                Output = 0.26,
                Letter = 'H',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    0, 1, 0,
                    0, 1, 0,
                    0, 1, 0,
                    1, 1, 1
                },
                Output = 0.08,
                Letter = 'I',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    0, 1, 0,
                    0, 1, 0,
                    0, 1, 0,
                    1, 1, 0
                },
                Output = 0.09,
                Letter = 'J',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 0,
                    1, 0, 1,
                    1, 0, 1
                },
                Output = 0.10,
                Letter = 'K',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 0, 0,
                    1, 0, 0,
                    1, 0, 0,
                    1, 0, 0,
                    1, 1, 1
                },
                Output = 0.11,
                Letter = 'L',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 0, 1,
                    1, 1, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1
                },
                Output = 0.12,
                Letter = 'M',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1,
                    1, 1, 1,
                    1, 0, 1
                },
                Output = 0.13,
                Letter = 'N',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1,
                },
                Output = 0.14,
                Letter = 'O',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    1, 0, 1,
                    1, 1, 1,
                    1, 0, 0,
                    1, 0, 0
                },
                Output = 0.15,
                Letter = 'P',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1,
                    0, 0, 1
                },
                Output = 0.16,
                Letter = 'Q',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    1, 0, 1,
                    1, 1, 1,
                    1, 1, 0,
                    1, 0, 1
                },
                Output = 0.17,
                Letter = 'R',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    1, 0, 0,
                    1, 1, 1,
                    0, 0, 1,
                    1, 1, 1
                },
                Output = 0.18,
                Letter = 'S',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    0, 1, 0,
                    0, 1, 0,
                    0, 1, 0,
                    0, 1, 0
                },
                Output = 0.19,
                Letter = 'T',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1
                },
                Output = 0.20,
                Letter = 'U',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    0, 1, 0,
                    0, 1, 0
                },
                Output = 0.21,
                Letter = 'V',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1,
                    1, 0, 1,
                },
                Output = 0.22,
                Letter = 'W',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 0, 1,
                    1, 0, 1,
                    0, 1, 0,
                    1, 0, 1,
                    1, 0, 1
                },
                Output = 0.23,
                Letter = 'X',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 0, 1,
                    1, 0, 1,
                    0, 1, 0,
                    0, 1, 0,
                    0, 1, 0
                },
                Output = 0.24,
                Letter = 'Y',
            },
            new Sample()
            {
                Input = new double[] 
                { 
                    1, 1, 1,
                    0, 0, 1,
                    0, 1, 0,
                    1, 0, 0,
                    1, 1, 1
                },
                Output = 0.25,
                Letter = 'Z',
            },
        };

        #region date definition
        /*
        double[][] testInput = new double[][]
            {
                new double[] 
                {
                    
                }, // B
                new double[] 
                {
                    
                }, // C
                new double[] 
                { 
                    
                }, // D
                new double[] 
                {
                    
                }, // E
                new double[]
                {
                    
                }, // F
                new double[]
                {
                    
                }, // G
                new double[]
                {
                    
                }, // H
                new double[]
                {
                    
                }, // I
                new double[]
                {
                    
                }, // J
                new double[]
                {
                    
                }, // K
                new double[]
                {
                    
                }, // L
                new double[]
                {
                    
                }, // M
                new double[]
                {
                    
                }, // N
                new double[]
                {
                    
                }, // O
                new double[]
                {
                    
                }, //  P
                new double[]
                {
                    
                }, // Q
                new double[]
                {
                    
                }, // R
                new double[]
                {
                   
                }, // S
                new double[]
                {

                }, // T
                new double[]
                {
                    
                }, // U
                new double[]
                {
                    
                }, // V
                new double[]
                {
                    
                }, // W
                new double[]
                {
                    
                }, // X
                new double[]
                {
                    
                }, // Y
                new double[]
                {
                    
                }, // Z
            };
        double[][] testOutput = new double[][]
            {
                new double[] { 0.01 }, // A
                new double[] { 0.02 }, // B
                new double[] { 0.03 }, // C
                new double[] { 0.04 }, // D
                new double[] { 0.05 }, // E
                new double[] { 0.06 }, // F
                new double[] { 0.07 }, // G
                new double[] { 0.08 }, // I
                new double[] { 0.09 }, // J
                new double[] { 0.10 }, // K
                new double[] { 0.11 }, // L
                new double[] { 0.12 }, // M
                new double[] { 0.13 }, // N
                new double[] { 0.14 }, // O
                new double[] { 0.15 }, // P
                new double[] { 0.16 }, // Q
                new double[] { 0.17 }, // R
                new double[] { 0.18 }, // S
                new double[] { 0.19 }, // T
                new double[] { 0.20 }, // U
                new double[] { 0.21 }, // V
                new double[] { 0.22 }, // W
                new double[] { 0.23 }, // X 
                new double[] { 0.24 }, // Y
                new double[] { 0.25 }, // Z 
                new double[] { 0.26 }, // H
                
                new double[] { 0. }, // 1
                new double[] { 0. }, // 2 
                new double[] { 0. }, // 3
                new double[] { 0. }, // 4
                new double[] { 0. }, // 5
                new double[] { 0. }, // 6
                new double[] { 0. }, // 7
                new double[] { 0. }, // 8
                new double[] { 0. }, // 9
                 
            };
         */
        #endregion
    }

    public class Sample
    {
        public double[] Input;
        public double Output;
        public char Letter;

        public Sample()
        {
        }
    }
}
