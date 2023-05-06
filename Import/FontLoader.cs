using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Import
{
    class FontLoader
    {
        public static void AddFont(string fontAtlasLocation, string FNTFileLocation)
        {
            int atlasTexture = ImageLoader.LoadTexture(fontAtlasLocation);

            string[] lines = File.ReadAllLines(FNTFileLocation);
            string fontFace = RetrieveFontFace(lines);
            if (fontFace == "")
                return;
            string atlasWidth = RetrieveAtlasWidth(lines);
            int atlasWidthParsed = int.Parse(atlasWidth);
            if (atlasWidth == "")
                return;
            string atlasHeight = RetrieveAtlasHeight(lines);
            int atlasHeightParsed = int.Parse(atlasHeight);
            if (atlasHeight == "")
                return;
            Console.WriteLine($"[FontLoader]: Font face added: \"{fontFace}\" | Font atlas size: ({atlasWidth}, {atlasHeight})");

            CharLocation[] charLocations = new CharLocation[128];
            for(int i = 0; i < lines.Length; i++)
            {
                string charIndex = RetrieveSandwiched(lines[i], "char id=", " ");
                if (charIndex != "")
                {
                    int charIndexParsed = int.Parse(charIndex);
                    string xStart = RetrieveSandwiched(lines[i], "x=", " ");
                    int xStartParsed = int.Parse(xStart);
                    string xEnd = RetrieveSandwiched(lines[i], "width=", " ");
                    int xEndParsed = int.Parse(xEnd) + xStartParsed;
                    string yStart = RetrieveSandwiched(lines[i], "y=", " ");
                    int yStartParsed = int.Parse(yStart);
                    string yEnd = RetrieveSandwiched(lines[i], "height=", " ");
                    int yEndParsed = int.Parse(yEnd) + yStartParsed;
                    string xOffset = RetrieveSandwiched(lines[i], "xoffset=", " ");
                    int xOffsetParsed = int.Parse(xOffset);
                    string yOffset = RetrieveSandwiched(lines[i], "yoffset=", " ");
                    int yOffsetParsed = int.Parse(yOffset);
                    string xAdvance = RetrieveSandwiched(lines[i], "xadvance=", " ");
                    int xAdvanceParsed = int.Parse(xAdvance);
                    charLocations[charIndexParsed] = 
                        new CharLocation(xStartParsed / (float)atlasWidthParsed, 
                        xEndParsed / (float)atlasHeightParsed, 
                        yStartParsed / (float)atlasWidthParsed, 
                        yEndParsed / (float)atlasHeightParsed,
                        xOffsetParsed / (float)atlasWidthParsed,
                        yOffsetParsed / (float)atlasHeightParsed,
                        xAdvanceParsed / (float)atlasWidthParsed);
                }
            }
            Font newFont = new Font(atlasTexture, charLocations);
            FontLibrary.fonts.Add(fontFace, newFont);
        }

        private static string RetrieveFontFace(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string height = RetrieveSandwiched(lines[i], "info face=\"", "\"");
                if (height != "")
                    return height;
            }
            Program.ThrowError("Error occured while trying to retrive font face!\nTerminating process");
            return "";
        }
        private static string RetrieveAtlasWidth(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string height = RetrieveSandwiched(lines[i], "scaleW=", " ");
                if (height != "")
                    return height;
            }
            Program.ThrowError("Error occured while trying to retrive font width!\nTerminating process");
            return "";
        }
        private static string RetrieveAtlasHeight(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string height = RetrieveSandwiched(lines[i], "scaleH=", " ");
                if(height != "")
                    return height;
            }
            Program.ThrowError("Error occured while trying to retrive font height!\nTerminating process");
            return "";
        }
        private static string RetrieveSandwiched(string line, string prefix, string suffix)
        {
            int fontFaceStart = Substring(line, prefix, 0);
            if (fontFaceStart >= 0)
            {
                int start = fontFaceStart + prefix.Length;
                int end = Substring(line, suffix, start);
                return line.Substring(start, end - start);
            }
            return "";
        }
        private static int Substring(string str, string searchFor, int startingIndex)
        {
            for(int i = startingIndex; i <= str.Length - searchFor.Length; i++)
            {
                for (int j = 0; j < searchFor.Length; j++)
                {
                    if (str[i + j] != searchFor[j])
                        break;

                    //Succesfully found string!
                    if (j == searchFor.Length - 1)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }
}
