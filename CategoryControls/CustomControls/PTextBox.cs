using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CategoryControls.CustomControls;

/// <summary>
/// Placeholder Text Box | PTextBox
/// This text box acts as a regular textbox, but instead it also has a placeholder text.
/// Similar to Xamarin.forms Entry control.
/// </summary>
public class PTextBox : TextBox
{
    public static DependencyProperty BorderHoverColorProperty =
        DependencyProperty.Register(
            nameof(BorderHoverColor), 
            typeof(Brush), 
            typeof(PTextBox), 
            new PropertyMetadata(new SolidColorBrush(Colors.CadetBlue)));

    public static DependencyProperty BorderFocusColorProperty =
        DependencyProperty.Register(
            nameof(BorderFocusColor), 
            typeof(Brush), 
            typeof(PTextBox), 
            new PropertyMetadata(new SolidColorBrush(Colors.Blue)));

    public static DependencyProperty PlaceholderTextProperty =
        DependencyProperty.Register(
            nameof(PlaceholderText), 
            typeof(string), 
            typeof(PTextBox), 
            new PropertyMetadata(string.Empty));

    public static DependencyProperty PlaceholderTextColorProperty =
        DependencyProperty.Register(
            nameof(PlaceholderTextColor), 
            typeof(Brush), 
            typeof(PTextBox));

    private static DependencyPropertyKey HasTextPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(HasText), 
            typeof(bool), 
            typeof(PTextBox), 
            new PropertyMetadata(false));

    public static DependencyProperty HasTextProperty = 
        HasTextPropertyKey.DependencyProperty;

    public static DependencyProperty HidePlaceholderOnFocusProperty =
        DependencyProperty.Register(
            nameof(HidePlaceholderOnFocus), 
            typeof(bool), 
            typeof(PTextBox), 
            new PropertyMetadata(true));

    public Brush BorderHoverColor
    {
        get { return (Brush)GetValue(BorderHoverColorProperty); }
        set { SetValue(BorderHoverColorProperty, value); }
    }

    public Brush BorderFocusColor
    {
        get { return (Brush)GetValue(BorderFocusColorProperty); }
        set { SetValue(BorderFocusColorProperty, value); }
    }

    public string PlaceholderText
    {
        get { return (string)GetValue(PlaceholderTextProperty); }
        set { SetValue(PlaceholderTextProperty, value); }
    }

    public Brush PlaceholderTextColor
    {
        get { return (Brush)GetValue(PlaceholderTextColorProperty); }
        set { SetValue(PlaceholderTextColorProperty, value); }
    }

    public bool HasText
    {
        get { return (bool)GetValue(HasTextProperty); }
        private set { SetValue(HasTextPropertyKey, value); }
    }

    public bool HidePlaceholderOnFocus
    {
        get { return (bool)GetValue(HidePlaceholderOnFocusProperty); }
        set { SetValue(HidePlaceholderOnFocusProperty, value); }
    }

    private string _displayPlaceholderText { get; set; } = string.Empty;

    private Brush _defaultBorderColor { get; set; } = new SolidColorBrush(Colors.Black);

    static PTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(PTextBox), 
            new FrameworkPropertyMetadata(typeof(PTextBox)));
    }

    public PTextBox() : base()
    {

    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _displayPlaceholderText = PlaceholderText;
        _defaultBorderColor = BorderBrush;
    }


    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);
        if (!HasText)
        {
            PlaceholderText = _displayPlaceholderText;
        }
    }

    protected override void OnGotFocus(RoutedEventArgs e)
    {
        base.OnGotFocus(e);
        if (!HasText && HidePlaceholderOnFocus)
        {
            PlaceholderText = "";
        }
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        base.OnTextChanged(e);
        HasText = Text.Length != 0;
    }

    protected override void OnMouseEnter(MouseEventArgs e)
    {
        base.OnMouseEnter(e);
        BorderBrush = BorderHoverColor;
        
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        base.OnMouseLeave(e);
        BorderBrush = _defaultBorderColor; 
    }
}
