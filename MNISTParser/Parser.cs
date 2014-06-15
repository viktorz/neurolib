using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNISTParser
{
    public class Parser
    {
        const string trainLabelsFileName = @"\train-labels.idx1-ubyte";
        const string trainImagesFileName = @"\train-images.idx3-ubyte";
        const string testLabelsFileName = @"\t10k-labels.idx1-ubyte";
        const string testImagesFileName = @"\t10k-images.idx3-ubyte";

        public Image[] ParseTrainingData(string path)
        {
            return ParseImages(path + trainImagesFileName, path + trainLabelsFileName);
        }

        public Image[] ParseTestData(string path)
        {
            return ParseImages(path + testImagesFileName, path + testLabelsFileName);
        }

        private Image[] ParseImages(string imageFilePath, string labelFilePath)
        {
            Image[] result;

            using (FileStream labelReader = new FileStream(labelFilePath, FileMode.Open))
            using (FileStream imageReader = new FileStream(imageFilePath, FileMode.Open))
            {
                byte[] tmp = new byte[4];
                labelReader.Read(tmp, 0, 4);
                int labelFileHeader = BitConverter.ToInt32(FixEndian(tmp), 0);
                labelReader.Read(tmp, 0, 4);
                int numberOfLabels = BitConverter.ToInt32(FixEndian(tmp), 0);

                imageReader.Read(tmp, 0, 4);
                int imageFileHeader = BitConverter.ToInt32(FixEndian(tmp), 0);
                imageReader.Read(tmp, 0, 4);
                int numberOfImages = BitConverter.ToInt32(FixEndian(tmp), 0);
                imageReader.Read(tmp, 0, 4);
                int numberOfRows = BitConverter.ToInt32(FixEndian(tmp), 0);
                imageReader.Read(tmp, 0, 4);
                int numberOfColumns = BitConverter.ToInt32(FixEndian(tmp), 0);

                result = new Image[numberOfLabels];
                for (int imageIndex = 0; imageIndex < numberOfLabels; imageIndex++)
                {
                    Image image = new Image();
                    image.label = (byte)labelReader.ReadByte();
                    int pixelCount = numberOfRows * numberOfColumns;
                    image.pixelData = new byte[pixelCount];

                    for (int ri = 0; ri < pixelCount; ri++)
                    {
                        image.pixelData[ri] = (byte)imageReader.ReadByte();
                    }

                    result[imageIndex] = image;
                }
            }

            return result;
        }

        private byte[] FixEndian(byte[] data)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(data);

            return data;
        }
    }
}