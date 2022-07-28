using ImageLibrary.Utilities;

namespace UnitTest;

[TestClass]
public class ColorUtilitiesTest
{
    [TestMethod]
    [TestCategory("RGB to HSV")]
    public void RGBtoHSV1()
    {
        RGB value = new()
        {
            Red = 255,
            Green = 255,
            Blue = 255
        };
        Assert.AreEqual(
            value,
            ColorUtilities.HSVtoRGB(
                ColorUtilities.RGBtoHSV(value)));
    }

    [TestMethod]
    public void RGBtoHSV2()
    {
        RGB value = new()
        {
            Red = 0,
            Green = 0,
            Blue = 0
        };
        Assert.AreEqual(
            value,
            ColorUtilities.HSVtoRGB(
                ColorUtilities.RGBtoHSV(value)));
    }

    [TestMethod]
    [TestCategory("RGB to HSV")]
    public void RGBtoHSV3()
    {
        RGB value = new()
        {
            Red = 255,
            Green = 0,
            Blue = 0
        };
        Assert.AreEqual(
            value,
            ColorUtilities.HSVtoRGB(
                ColorUtilities.RGBtoHSV(value)));
    }

    [TestMethod]
    [TestCategory("RGB to HSV")]
    public void RGBtoHS4()
    {
        RGB value = new()
        {
            Red = 0,
            Green = 255,
            Blue = 0
        };
        Assert.AreEqual(
            value,
            ColorUtilities.HSVtoRGB(
                ColorUtilities.RGBtoHSV(value)));
    }

    [TestMethod]
    [TestCategory("RGB to HSV")]
    public void RGBtoHSV5()
    {
        RGB value = new()
        {
            Red = 0,
            Green = 0,
            Blue = 255
        };
        Assert.AreEqual(
            value,
            ColorUtilities.HSVtoRGB(
                ColorUtilities.RGBtoHSV(value)));
    }

    [TestMethod]
    [TestCategory("RGB to HSV")]
    public void RGBtoHSV6()
    {
        RGB value = new()
        {
            Red = 255,
            Green = 255,
            Blue = 0
        };
        Assert.AreEqual(
            value,
            ColorUtilities.HSVtoRGB(
                ColorUtilities.RGBtoHSV(value)));
    }

    [TestMethod]
    [TestCategory("RGB to HSV")]
    public void RGBtoHSV7()
    {
        RGB value = new()
        {
            Red = 255,
            Green = 0,
            Blue = 255
        };
        Assert.AreEqual(
            value,
            ColorUtilities.HSVtoRGB(
                ColorUtilities.RGBtoHSV(value)));
    }

    [TestMethod]
    [TestCategory("RGB to HSV")]
    public void RGBtoHSV8()
    {
        RGB value = new()
        {
            Red = 172,
            Green = 172,
            Blue = 172
        };
        Assert.AreEqual(
            value,
            ColorUtilities.HSVtoRGB(
                ColorUtilities.RGBtoHSV(value)));
    }

    [TestMethod]
    [TestCategory("RGB to HSV")]
    public void RGBtoHSV9()
    {
        RGB value = new()
        {
            Red = 120,
            Green = 234,
            Blue = 42
        };
        Assert.AreEqual(
            value,
            ColorUtilities.HSVtoRGB(
                ColorUtilities.RGBtoHSV(value)));
    }

    [TestMethod]
    [TestCategory("RGB to HSV")]
    public void RGBtoHSV10()
    {
        RGB value = new()
        {
            Red = 0,
            Green = 140,
            Blue = 120
        };
        Assert.AreEqual(
            value,
            ColorUtilities.HSVtoRGB(
                ColorUtilities.RGBtoHSV(value)));
    }

    [TestMethod]
    [TestCategory("RGB to HEX")]
    public void RGBtoHEX1()
    {
        RGB value = new()
        {
            Red = 0,
            Green = 140,
            Blue = 120
        };
        Assert.AreEqual(
            value,
            ColorUtilities.HEXtoRGB(
                ColorUtilities.RGBtoHEX(value)));
    }
}
