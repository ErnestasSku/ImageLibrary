using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace CategoryControls.CustomControls;


[ContentProperty("Child")]
public class RoundedBorder : Control
{
    public static DependencyProperty BorderOffsetProperty =
        DependencyProperty.Register(
            nameof(BorderOffset), 
            typeof(double), 
            typeof(RoundedBorder), 
            new PropertyMetadata(0.0));

    public static DependencyProperty UseStrictModeProperty =
        DependencyProperty.Register(
            nameof(UseStrictMode), 
            typeof(bool), typeof(RoundedBorder), 
            new PropertyMetadata(false));

    public static DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(
            nameof(CornerRadius), 
            typeof(CornerRadius), 
            typeof(RoundedBorder), 
            new PropertyMetadata(new CornerRadius(0)));

    public static DependencyProperty BackgroundProperty =
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

    public static DependencyProperty BorderThicknesProperty =
        DependencyProperty.Register(
            nameof(BorderThickness), 
            typeof(Thickness), 
            typeof(RoundedBorder), 
            new PropertyMetadata(new Thickness(0)));

    public static DependencyProperty ChildProperty =
        DependencyProperty.Register(
            nameof(Child), 
            typeof(UIElement), 
            typeof(RoundedBorder), 
            new PropertyMetadata(null));


    public double BorderOffset
    {
        get { return (double)GetValue(BorderOffsetProperty); }
        set { SetValue(BorderOffsetProperty, value); }
    }

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

    public Brush Background
    {
        get { return (Brush)GetValue(BackgroundProperty); }
        set { SetValue(BackgroundProperty, value); }
    }

    public Brush BorderBrush
    {
        get { return (Brush)GetValue(BorderBrushProperty); }
        set { SetValue(BorderBrushProperty, value); }
    }

    public Thickness BorderThickness
    {
        get { return (Thickness)GetValue(BorderThicknesProperty); }
        set { SetValue(BorderThicknesProperty, value); }
    }

    public UIElement Child
    {
        get { return (UIElement)GetValue(ChildProperty); }
        set { SetValue(ChildProperty, value); }
    }

    public double calculatedContentWidth { get; set; }

    public double calculatedContentHeight { get; set; }

    public Geometry contentClip { get; set; }

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
        if (UseStrictMode)
        {
            CalculateContentValues();
        }
        if (Child != null)
        {
            Child.ClipToBounds = true;
        }

        SizeChanged += RoundedBorder_OnSizeChanged;
    }

    private void RoundedBorder_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (UseStrictMode)
        {
            CalculateContentValues();
        }
    }

    private void CalculateContentValues()
    {

        calculatedContentHeight = Height - (BorderThickness.Top + BorderThickness.Bottom);
        calculatedContentWidth = Width - (BorderThickness.Left + BorderThickness.Right);
        var clip = new RectangleGeometry(
            new Rect(0, 0, calculatedContentWidth, calculatedContentHeight), 
            CornerRadius.TopLeft,
            CornerRadius.BottomRight);
        clip.Freeze();
        contentClip = clip;
        if (Child != null && Child is FrameworkElement fe)
        {
            fe.Margin = new Thickness(
                BorderThickness.Left - BorderThickness.Right, 
                BorderThickness.Top - BorderThickness.Bottom, 
                0, 0);
        }
    }
}
