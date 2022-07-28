using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace CategoryControls.CustomControls;


[ContentProperty("Child")]
public class RoundedBorder : Control, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Determines whether the child of this control 
    /// </summary>
    public static DependencyProperty UseStrictModeProperty =
        DependencyProperty.Register(
            nameof(UseStrictMode),
            typeof(bool), typeof(RoundedBorder),
            new PropertyMetadata(
                true,
                UseStrictModePropertyChanged));
    
    /// <summary>
    /// Corner radius of the border (and content inside of it using Strict mode)
    /// </summary>
    public static DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(
            nameof(CornerRadius), 
            typeof(CornerRadius), 
            typeof(RoundedBorder), 
            new PropertyMetadata(
                new CornerRadius(0),
                CornerRadiusPropertyChanged));


    public static new DependencyProperty BackgroundProperty =
        DependencyProperty.Register(
            nameof(Background), 
            typeof(Brush), 
            typeof(RoundedBorder), 
            new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

    public static new DependencyProperty BorderBrushProperty =
        DependencyProperty.Register(
            nameof(BorderBrush), 
            typeof(Brush), 
            typeof(RoundedBorder), 
            new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

    public static new DependencyProperty BorderThicknessProperty =
        DependencyProperty.Register(
            nameof(BorderThickness), 
            typeof(Thickness), 
            typeof(RoundedBorder), 
            new PropertyMetadata(
                new Thickness(0),
                BorderThicknesPropertyChanged));

    public static DependencyProperty ChildProperty =
        DependencyProperty.Register(
            nameof(Child), 
            typeof(UIElement), 
            typeof(RoundedBorder), 
            new PropertyMetadata(
                null,
                ChildPropertyChanged));


    public bool UseStrictMode
    {
        get { return (bool)GetValue(UseStrictModeProperty); }
        set { SetValue(UseStrictModeProperty, value); }
    }

    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public new Brush Background
    {
        get { return (Brush)GetValue(BackgroundProperty); }
        set { SetValue(BackgroundProperty, value); }
    }

    public new Brush BorderBrush
    {
        get { return (Brush)GetValue(BorderBrushProperty); }
        set { SetValue(BorderBrushProperty, value); }
    }

    public new Thickness BorderThickness
    {
        get { return (Thickness)GetValue(BorderThicknessProperty); }
        set { SetValue(BorderThicknessProperty, value); }
    }

    public UIElement Child
    {
        get { return (UIElement)GetValue(ChildProperty); }
        set { SetValue(ChildProperty, value); }
    }

    private double _calculatedContentWidth;
    public double CalculatedContentWidth 
    {
        get { return _calculatedContentWidth; }
        set 
        { 
            if (_calculatedContentWidth != value)
            {
                _calculatedContentWidth = value;
                NotifyPropertyChanged(nameof(CalculatedContentWidth));
                
            }
        }
    }

    private double _calculatedContentHeight;
    public double CalculatedContentHeight 
    {
        get { return _calculatedContentHeight; }
        set
        {
            if (value != _calculatedContentHeight)
            {
                _calculatedContentHeight = value;
                NotifyPropertyChanged(nameof(CalculatedContentHeight));

            }
        }
    }

    private Geometry _contentClip;
    public Geometry ContentClip 
    { 
        get { return _contentClip; }
        set
        {
            if (value != _contentClip)
            {
                _contentClip = value;
                NotifyPropertyChanged(nameof(ContentClip));
            }
        }
    }

    static RoundedBorder()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(RoundedBorder),
            new FrameworkPropertyMetadata(typeof(RoundedBorder)));
    }

    public RoundedBorder() : base()
    {

    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        CalculateContentValues();
        if (Child != null)
        {
            Child.ClipToBounds = true;
        }

        SizeChanged += RoundedBorder_OnSizeChanged;
    }


    private static void UseStrictModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RoundedBorder roundedBorder)
        {
            roundedBorder.UseStrictMode = (bool)e.NewValue;
            roundedBorder.CalculateContentValues();
        }
    }

    private static void CornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if(d is RoundedBorder roundedBorder)
        {
            roundedBorder.CornerRadius = (CornerRadius)e.NewValue;
            roundedBorder.CalculateContentValues();
        }
    }

    private static void BorderThicknesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if(d is RoundedBorder roundedBorder)
        {
            roundedBorder.BorderThickness = (Thickness)e.NewValue;
            roundedBorder.CalculateContentValues();
        }
    }

    private static void ChildPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RoundedBorder roundedBorder)
        {
            roundedBorder.Child = (UIElement)e.NewValue;
            roundedBorder.Child.ClipToBounds = roundedBorder.UseStrictMode;
            roundedBorder.CalculateContentValues();
        }
    }

    private void RoundedBorder_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        CalculateContentValues();
    }

    public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

    public void CalculateContentValues()
    {
        if (!UseStrictMode)
        {
            CalculatedContentHeight = Height;
            CalculatedContentWidth = Width;
            ContentClip = Geometry.Empty;
        }
        else
        {
            CalculatedContentHeight = Height - (BorderThickness.Top + BorderThickness.Bottom);
            CalculatedContentWidth = Width - (BorderThickness.Left + BorderThickness.Right);

            Geometry clip;
            try
            {
                clip = new RectangleGeometry(
                new Rect(0, 0, CalculatedContentWidth, CalculatedContentHeight),
                CornerRadius.TopLeft,
                CornerRadius.BottomRight);
            } catch (ArgumentException)
            {
                clip = RectangleGeometry.Empty;
            }
            
            clip.Freeze();
            

            ContentClip = clip;
            if (Child != null && Child is FrameworkElement fe)
            {
                fe.Margin = new Thickness(
                    BorderThickness.Left - BorderThickness.Right,
                    BorderThickness.Top - BorderThickness.Bottom,
                    0, 0);
            }
        }
    }
}
