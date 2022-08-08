using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace ImageLibrary.Utilities;

public struct HSV
{
    public double Hue;
    public double Saturation;
    public double Value;
}

public struct RGB
{
    public int Red;
    public int Green;
    public int Blue;
}

public static class ColorUtilities
{
    public static HSV RGBtoHSV(int red, int green, int blue)
    {
        double redT = red / 255.0;
        double greenT = green / 255.0;
        double blueT = blue / 255.0;

        double colorMax = new[] { redT, greenT, blueT }.Max();
        double colorMin = new[] { redT, greenT, blueT }.Min();
        double delta = colorMax - colorMin;

        double hue, saturation, value;

        if (colorMax == colorMin)
        {
            hue = 0;
        } 
        else if (colorMax == redT)
        {
            hue = (60 * ((greenT - blueT) / delta) + 360) % 360;
        }
        else if (colorMax == greenT)
        {
            hue = (60 * ((blueT - redT) / delta) + 120) % 360;
        }
        else if (colorMax == blueT)
        {
            hue = (60 * ((redT - greenT) / delta) + 240) % 360;
        } 
        else
        {
            hue = 0;
        }
        
        if (colorMax == 0)
        {
            saturation = 0;
        }
        else
        {
            saturation = (delta / colorMax);
        }

        value = colorMax;
        return new HSV() { Hue = hue, Saturation = saturation, Value = value };

    }


    public static HSV RGBtoHSV(RGB values)
    {
        return RGBtoHSV(values.Red, values.Green, values.Blue);
    }

    public static RGB HSVtoRGB(double hue, double saturation, double value)
    {

        double color = value * saturation;
        double X = color * (1 - Math.Abs(hue / 60 % 2 - 1)) % 360;
        double m = value - color;

        var (rT, gT, bT) = calculateRGBPrime(color, X, hue);

        RGB rgb = new RGB()
        {
            Red = (int)Math.Round(((rT + m) * 255.0)),
            Green = (int)Math.Round((gT + m) * 255.0),
            Blue = (int)Math.Round((bT + m) * 255.0)
        };
        return rgb;
    }

    public static RGB HSVtoRGB(HSV values)
    {
        return HSVtoRGB(values.Hue, values.Saturation, values.Value);
    }

    private static (double, double, double) calculateRGBPrime(double color, double x, double hue)
    {
        if (hue >= 0 && hue < 60)
        {
            return (color, x, 0);
        }
        else if (hue >= 60 && hue < 120)
        {
            return (x, color, 0);
        }
        else if (hue >= 120 && hue < 180)
        {
            return (0, color, x);
        }
        else if (hue >= 180 && hue < 240)
        {
            return (0, x, color);
        }
        else if (hue >= 240 && hue < 300)
        {
            return (x, 0, color);
        }
        else if (hue >= 300 && hue < 360)
        {
            return (color, 0, x);
        }
        else
        {
            throw new ArgumentException(message:"Hue should always be in the range specified above");
        }
    }

    public static RGB HEXtoRGB(string hex)
    {
        return new RGB()
        {
            Red = Convert.ToInt32(Regex.Matches(hex, @"[0-9a-fA-F]{2}")[0].Value, 16),
            Green = Convert.ToInt32(Regex.Matches(hex, @"[0-9a-fA-F]{2}")[1].Value, 16),
            Blue = Convert.ToInt32(Regex.Matches(hex, @"[0-9a-fA-F]{2}")[2].Value, 16)
        };
    }

    public static string RGBtoHEX(RGB value)
    {
        return RGBtoHEX(value.Red, value.Green, value.Blue);
    }

    public static string RGBtoHEX(int red, int green, int blue)
    {
        return Convert.ToString(red, 16).PadLeft(2, '0') +
               Convert.ToString(green, 16).PadLeft(2, '0') +
               Convert.ToString(blue, 16).PadLeft(2, '0');
    }

    public static Color MakeBackground(Color color, double coeficient)
    {
        var hsv = RGBtoHSV(color.R, color.G, color.B);
        
        if (hsv.Value - coeficient >= 0.0)
            hsv.Value -= coeficient;
        else if (hsv.Value + coeficient <= 1.0)
            hsv.Value += coeficient;

        var rgb = HSVtoRGB(hsv);
        return Color.FromRgb((byte)rgb.Red, (byte)rgb.Green, (byte)rgb.Blue);
    }
}
