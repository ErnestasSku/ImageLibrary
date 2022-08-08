using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ImageLibrary.Utilities;


namespace ImageLibrary.ViewModels.CategoryWindow;

/// <summary>
/// Interaction logic for TagControl.xaml
/// </summary>
public partial class TagControl : UserControl
{
    private const double BackgroundColorValueCoeficient = 0.2;

    #region Dependency properties
    /// <summary>
    /// Default Background color property
    /// </summary>
    public static readonly DependencyProperty BackgroundColorProperty =
        DependencyProperty.Register(
            nameof(BackgroundColor), 
            typeof(Brush), 
            typeof(TagControl), 
            new PropertyMetadata(new SolidColorBrush(Colors.White), BackgroundColorPropertyChanged));


    private static DependencyPropertyKey BackgroundPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(Background),
            typeof(Brush),
            typeof(TagControl),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));

    /// <summary>
    /// Hide base Background.
    /// </summary>
    public static new DependencyProperty BackgroundProperty =
        BackgroundPropertyKey.DependencyProperty;

    /// <summary>
    /// Hide base border bursh property.
    /// </summary>
    public static new readonly DependencyProperty BorderBrushProperty =
        DependencyProperty.Register(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(TagControl),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

    /// <summary>
    /// Hide base foreground property.
    /// </summary>
    public static new readonly DependencyProperty ForegroundProperty =
        DependencyProperty.Register(
            nameof(Foreground),
            typeof(Brush),
            typeof(TagControl),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

    /// <summary>
    /// Hide base border thickness property.
    /// </summary>
    public static new readonly DependencyProperty BorderThicknessProperty =
        DependencyProperty.Register(
            nameof(BorderThickness),
            typeof(Thickness),
            typeof(TagControl),
            new PropertyMetadata(new Thickness(1)));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(TagControl),
            new PropertyMetadata(new CornerRadius(10)));

    public static readonly DependencyProperty EditModeProperty =
        DependencyProperty.Register(
            nameof(EditMode),
            typeof(bool),
            typeof(TagControl),
            new PropertyMetadata(false));

    public static DependencyProperty TagTextProperty =
        DependencyProperty.Register(
            nameof(TagText),
            typeof(string),
            typeof(TagControl),
            new PropertyMetadata(string.Empty));

    #endregion

    #region Properties
    public Brush BackgroundColor
    {
        get { return (Brush)GetValue(BackgroundProperty); }
        set { SetValue(BackgroundProperty, value); }
    }

    public new Brush Background
    {
        get { return (Brush)GetValue(BackgroundProperty); }
        private set { SetValue(BackgroundPropertyKey, value); }
    }

    public new Brush BorderBrush
    {
        get { return (Brush)GetValue(BorderBrushProperty); }
        set { SetValue(BorderBrushProperty, value); }
    }

    public new Brush Foreground
    {
        get { return (Brush)GetValue(ForegroundProperty); }
        set { SetValue(ForegroundProperty, value);  }
    }

    public new Thickness BorderThickness
    {
        get { return (Thickness)GetValue(BorderThicknessProperty); }
        set { SetValue(BorderThicknessProperty, value); }
    }

    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public bool EditMode
    {
        get { return (bool)GetValue(EditModeProperty); }
        set { SetValue(EditModeProperty, value); }
    }
    
    public string TagText
    {
        get { return (string)GetValue(TagTextProperty); }
        set { SetValue(TagTextProperty, value); }
    }

    #endregion
    
    private Brush backgroundColor;
    private Brush backGroundHoverColor;

    public TagControl()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Overide OnApplyTemplate.
    /// Applies backgroundColor brush to background if not null.
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        Background = backgroundColor ?? Background;
    }

    /// <summary>
    /// Calculates the min size of the control.
    /// </summary>
    private void CalculateMinSize()
    {
        MinHeight = FontSize + 8;
        MinWidth = TextBlock.RenderSize.Width + xImage.RenderSize.Width + 15;
    
    }

    
    /// <summary>
    /// Handles the logic for hovering over the control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Background = backGroundHoverColor;
        if (EditMode)
        {
            xImage.Visibility = Visibility.Visible;
        }
    }

    /// <summary>
    /// Restores control back to its default after mouse leaves the control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UserControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Background = backgroundColor;
        xImage.Visibility = Visibility.Hidden;
    }

    /// <summary>
    /// TODO: this should raise a RoutedEvent? or something similar indicating that the tag was pressed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UserControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        //TODO:
    }

    /// <summary>
    /// Background property changed callback.
    /// Sets a new default background color and calculates a new background hover color.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    private static void BackgroundColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TagControl tagControl)
        {
            SolidColorBrush newValue = (SolidColorBrush)e.NewValue;
            tagControl.backgroundColor = new SolidColorBrush(
                newValue.Color);
            tagControl.backGroundHoverColor =
                new SolidColorBrush(ColorUtilities.MakeBackground(
                    newValue.Color,
                    BackgroundColorValueCoeficient));
        }
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        CalculateMinSize();
    }

}
