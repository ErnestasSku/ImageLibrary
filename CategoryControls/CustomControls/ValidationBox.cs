using CategoryControls.Datatypes;
using CategoryControls.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CategoryControls.CustomControls;

//[TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
public class ValidationBox : TextBox, IValidationBox
{

    #region Dependency Properties
    /// <summary>
    /// Animation enum type registration.
    /// </summary>
    public static DependencyProperty AnimationTypeProperty =
        DependencyProperty.Register(
            nameof(AnimationType),
            typeof(AnimationType),
            typeof(ValidationBox),
            new PropertyMetadata(AnimationType.None));

    /// <summary>
    /// A function delegate holding the text validation function registration.
    /// </summary>
    public static DependencyProperty TextValidationMethodProperty =
        DependencyProperty.Register(
            nameof(TextValidationMethod),
            typeof(Func<string, ValidationBoxState>),
            typeof(ValidationBox),
            new FrameworkPropertyMetadata((string s) => ValidationBoxState.Normal));

    /// <summary>
    /// Border Thickness property registration for when the text is evaulated to Valid.
    /// </summary>
    public static DependencyProperty ValidThicknesProperty =
        DependencyProperty.Register(
            nameof(ValidThicknes),
            typeof(Thickness),
            typeof(ValidationBox),
            new PropertyMetadata(new Thickness(1)));

    /// <summary>
    /// Border Thickness property registration for when the text is evaulated to Invalid.
    /// </summary>
    public static DependencyProperty InvalidThicknessProperty =
        DependencyProperty.Register(
            nameof(InvalidThickness),
            typeof(Thickness),
            typeof (ValidationBox),
            new PropertyMetadata(new Thickness(1)));

    /// <summary>
    /// Border Thickness property registration for when the text is evaulated to Normal.
    /// </summary>
    public static DependencyProperty NormalThicknessProperty =
        DependencyProperty.Register(
            nameof(NormalThickness),
            typeof(Thickness),
            typeof(ValidationBox),
            new PropertyMetadata(new Thickness(1)));

    /// <summary>
    /// Border Thickness property registration for when the text is evaulated to Incomplete.
    /// </summary>
    public static DependencyProperty IncompleteThicknessProperty =
        DependencyProperty.Register(
            nameof(IncompleteThickness),
            typeof(Thickness),
            typeof(ValidationBox),
            new PropertyMetadata(new Thickness(1)));

    #endregion

    #region public Properties

    /// <summary>
    /// Animation type enum;
    /// </summary>
    public AnimationType AnimationType
    {
        get { return (AnimationType)GetValue(AnimationTypeProperty); }
        set { SetValue(AnimationTypeProperty, value); }
    }

    /// <summary>
    /// Text validation Function delegate.
    /// </summary>
    public Func<string, ValidationBoxState> TextValidationMethod
    {
        get { return (Func<string, ValidationBoxState>)GetValue(TextValidationMethodProperty); }
        set { SetValue(TextValidationMethodProperty, value); }
    }

    /// <summary>
    /// Thickness Property for when the state is evaluated to Valid.
    /// </summary>
    public Thickness ValidThicknes
    {
        get { return (Thickness)GetValue(ValidThicknesProperty); }
        set { SetValue(ValidThicknesProperty, value); }
    }

    /// <summary>
    /// Thickness Property for when the state is evaluated to Invalid.
    /// </summary>
    public Thickness InvalidThickness
    {
        get { return (Thickness)GetValue(InvalidThicknessProperty); }
        set { SetValue(InvalidThicknessProperty, value); }
    }

    /// <summary>
    /// Thickness Property for when the state is evaluated to Normal.
    /// </summary>
    public Thickness NormalThickness
    {
        get { return (Thickness)GetValue(NormalThicknessProperty); }
        set { SetValue(NormalThicknessProperty, value); }
    }

    /// <summary>
    /// Thickness Property for when the state is evaluated to Incomplete.
    /// </summary>
    public Thickness IncompleteThickness
    {
        get { return (Thickness)GetValue(IncompleteThicknessProperty); }
        set { SetValue(IncompleteThicknessProperty, value); }
    }


    public Brush ValidBorderColor;
    public Brush InvalidBorderColor;
    public Brush NormalBorderColor;
    public Brush IncompleteBorderColor;

    public Brush ValidTextColor;
    public Brush InvalidTextColor;
    public Brush NormalTextColor;
    public Brush IncompleteTextColor;

    public Brush ValidBackgroundColor;
    public Brush InvalidBackgroundColor;
    public Brush NormalBackgroundColor;
    public Brush IncompleteBackgroundColor;

    public bool ValidHighlightWithoutFocus;
    public bool InvalidHighlightWithoutFocus;
    public bool NormalHighlightWithoutFocus;
    public bool IncompleteHighlightWithoutFocus;


    public bool ShowValidIcon;
    public bool ShowInvalidIcon;

    /*
     * Animation Length
     * Animation Repeat
     */

    #endregion


    private ValidationBoxState _state;

    static ValidationBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ValidationBox),
            new FrameworkPropertyMetadata(typeof(ValidationBox)));
    }

    public ValidationBox() : base()
    {

    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        _state = TextValidationMethod(Text);
        UpdateControl();
        base.OnTextChanged(e);
    }


    //TODO: write a proper changing function
    private void UpdateControl()
    {
        if(_state == ValidationBoxState.Incomplete)
        {
            BorderBrush = new SolidColorBrush(Colors.Gray);
        } 
        else if (_state == ValidationBoxState.Valid)
        {
            BorderBrush = new SolidColorBrush(Colors.Green);

        }
        else if (_state == ValidationBoxState.Invalid)
        {
            BorderBrush = new SolidColorBrush(Colors.Red);

        }
        else
        {
            throw new Exception();
        }
    }
}
