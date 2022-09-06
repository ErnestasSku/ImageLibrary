using CategoryControls.Datatypes;
using CategoryControls.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
    /// Animation repeat registration.
    /// </summary>
    public static DependencyProperty AnimationRepeatProperty =
        DependencyProperty.Register(
            nameof(AnimationRepeat),
            typeof(int),
            typeof(ValidationBox),
            new FrameworkPropertyMetadata(1));

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
    public static DependencyProperty ValidThicknessProperty =
        DependencyProperty.Register(
            nameof(ValidThickness),
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

    /// <summary>
    /// Border Thickness property registration for when the text is evaulated to Valid.
    /// </summary>
    public static DependencyProperty ValidBorderColorProperty =
        DependencyProperty.Register(
            nameof(ValidBorderColor),
            typeof(Brush),
            typeof(ValidationBox),
            new PropertyMetadata(new SolidColorBrush(Colors.Green)));

    /// <summary>
    /// Border Color property registration for when the text is evaulated to Invalid.
    /// </summary>
    public static DependencyProperty InvalidBorderColorProperty =
        DependencyProperty.Register(
            nameof(InvalidBorderColor),
            typeof(Brush),
            typeof(ValidationBox),
            new PropertyMetadata(new SolidColorBrush (Colors.Red)));
    /// <summary>
    /// Border Color property registration for when the text is evaulated to Normal.
    /// </summary>
    public static DependencyProperty NormalBorderColorProperty =
        DependencyProperty.Register(
            nameof(NormalBorderColor),
            typeof(Brush),
            typeof(ValidationBox),
            new PropertyMetadata(new SolidColorBrush(Colors.Gray)));
    
    /// <summary>
    /// Border Color property registration for when the text is evaulated to Incomplete.
    /// </summary>
    public static DependencyProperty IncompleteBorderColorProperty =
        DependencyProperty.Register(
            nameof(IncompleteBorderColor),
            typeof(Brush),
            typeof(ValidationBox),
            new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

    /// <summary>
    /// Text Color property registration for when the text is evaulated to Valid.
    /// </summary>
    public static DependencyProperty ValidTextColorProperty =
        DependencyProperty.Register(
             nameof(ValidTextColor),
             typeof(Brush),
             typeof(ValidationBox),
             new PropertyMetadata(new SolidColorBrush(Colors.Green)));

    /// <summary>
    /// Text Color property registration for when the text is evaulated to Invalid.
    /// </summary>
    public static DependencyProperty InvalidTextColorProperty =
        DependencyProperty.Register(
             nameof(InvalidTextColor),
             typeof(Brush),
             typeof(ValidationBox),
             new PropertyMetadata(new SolidColorBrush(Colors.Red)));

    /// <summary>
    /// Text Color property registration for when the text is evaulated to Normal.
    /// </summary>
    public static DependencyProperty NormalTextColorProperty =
        DependencyProperty.Register(
             nameof(NormalTextColor),
             typeof(Brush),
             typeof(ValidationBox),
             new PropertyMetadata(new SolidColorBrush(Colors.Black)));

    /// <summary>
    /// Text Color property registration for when the text is evaulated to Incomplete.
    /// </summary>
    public static DependencyProperty IncompleteTextColorProperty =
        DependencyProperty.Register(
             nameof(IncompleteTextColor),
             typeof(Brush),
             typeof(ValidationBox),
             new PropertyMetadata(new SolidColorBrush(Colors.Black)));

    /// <summary>
    /// Background Color Property registration for when the state is evaluated to Valid.
    /// </summary>
    public static DependencyProperty ValidBackgroundColorProperty =
        DependencyProperty.Register(
            nameof(ValidBackgroundColor),
            typeof(Brush),
            typeof(ValidationBox),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));

    /// <summary>
    /// Background Color Property registration for when the state is evaluated to Invalid.
    /// </summary>
    public static DependencyProperty InvalidBackgroundColorProperty =
        DependencyProperty.Register(
            nameof(InvalidBackgroundColor),
            typeof(Brush),
            typeof(ValidationBox),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));

    /// <summary>
    /// Background Color Property registration for when the state is evaluated to Normal.
    /// </summary>
    public static DependencyProperty NormalBackgroundColorProperty =
        DependencyProperty.Register(
            nameof(NormalBackgroundColor),
            typeof(Brush),
            typeof(ValidationBox),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));

    /// <summary>
    /// Background Color Property registration for when the state is evaluated to Incomplete.
    /// </summary>
    public static DependencyProperty IncompleteBackgroundColorProperty =
        DependencyProperty.Register(
            nameof(IncompleteBackgroundColor),
            typeof(Brush),
            typeof(ValidationBox),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));

    /// <summary>
    /// Boolean property registration for Valid state which decides whether the 
    /// HighlightWithoutFocus is going to keep a color after it's no longer highlighted.
    /// </summary>
    public static DependencyProperty ValidHighlightWithoutFocusProperty =
        DependencyProperty.Register(
            nameof(ValidHighlightWithoutFocus),
            typeof(bool),
            typeof(ValidationBox),
            new PropertyMetadata(false));

    /// <summary>
    /// Boolean property registration for Invalid state which decides whether the 
    /// HighlightWithoutFocus is going to keep a color after it's no longer highlighted.
    /// </summary>
    public static DependencyProperty InvalidHighlightWithoutFocusProperty =
        DependencyProperty.Register(
            nameof(InvalidHighlightWithoutFocus),
            typeof(bool),
            typeof(ValidationBox),
            new PropertyMetadata(false));

    /// <summary>
    /// Boolean property registration for Normal state which decides whether the 
    /// HighlightWithoutFocus is going to keep a color after it's no longer highlighted.
    /// </summary>
    public static DependencyProperty NormalHighlightWithoutFocusProperty =
        DependencyProperty.Register(
            nameof(NormalHighlightWithoutFocus),
            typeof(bool),
            typeof(ValidationBox),
            new PropertyMetadata(false));

    /// <summary>
    /// Boolean property registration for Incomplete state which decides whether the 
    /// HighlightWithoutFocus is going to keep a color after it's no longer highlighted.
    /// </summary>
    public static DependencyProperty IncompleteHighlightWithoutFocusProperty =
        DependencyProperty.Register(
            nameof(IncompleteHighlightWithoutFocus),
            typeof(bool),
            typeof(ValidationBox),
            new PropertyMetadata(false));

    /// <summary>
    /// TODO: 
    /// </summary>
    public static DependencyProperty StopInputAfterInvalidProperty =
        DependencyProperty.Register(
            nameof(StopInputAfterInvalid),
            typeof(bool),
            typeof(ValidationBox),
            new PropertyMetadata(false));
    
    /// <summary>
    /// TODO:
    /// </summary>
    public static DependencyProperty DisableAfterValidProperty =
        DependencyProperty.Register(
            nameof(DisableAfterValid),
            typeof(bool),
            typeof(ValidationBox),
            new PropertyMetadata(false));

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

    //NOTE: May not be needed. The current animation is in the storyboard, so only the animation repeat property is useful.
    /// <summary>
    /// Determines how long should an animation be.
    /// </summary>
    //public Duration AnimationLength;

    /// <summary>
    /// Determines how many times an animation should repeat. 
    /// </summary>
    public int AnimationRepeat;

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
    public Thickness ValidThickness
    {
        get { return (Thickness)GetValue(ValidThicknessProperty); }
        set { SetValue(ValidThicknessProperty, value); }
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

    /// <summary>
    /// Color of box when state is evaluated to Valid.
    /// </summary>
    public Brush ValidBorderColor
    {
        get { return (Brush)GetValue(ValidBorderColorProperty); }
        set { SetValue(ValidBorderColorProperty, value); }
    }

    /// <summary>
    /// Color of box when state is evaluated to Invalid.
    /// </summary>
    public Brush InvalidBorderColor
    {
        get { return (Brush)GetValue(InvalidBorderColorProperty); }
        set { SetValue(InvalidBorderColorProperty, value); }
    }

    /// <summary>
    /// Color of box when state is evaluated to Normal.
    /// </summary>
    public Brush NormalBorderColor
    {
        get { return (Brush)GetValue(NormalBorderColorProperty); }
        set { SetValue(NormalBorderColorProperty, value); }
    }

    /// <summary>
    /// Color of box when state is evaluated to Inomplete.
    /// </summary>
    public Brush IncompleteBorderColor
    {
        get { return (Brush)GetValue(IncompleteBorderColorProperty); }
        set { SetValue(IncompleteBorderColorProperty, value); }
    }

    /// <summary>
    /// Color of the text in the textvox when state is evaluated to Valid.
    /// </summary>
    public Brush ValidTextColor
    {
        get { return (Brush)GetValue(ValidTextColorProperty); }
        set { SetValue(ValidTextColorProperty, value); }
    }

    /// <summary>
    /// Color of the text in the textvox when state is evaluated to Incomplete.
    /// </summary>
    public Brush InvalidTextColor
    {
        get { return (Brush)GetValue(InvalidTextColorProperty); }
        set { SetValue(InvalidTextColorProperty, value); }
    }

    /// <summary>
    /// Color of the text in the textvox when state is evaluated to Normal.
    /// </summary>
    public Brush NormalTextColor
    {
        get { return (Brush)GetValue(NormalTextColorProperty); }
        set { SetValue(NormalTextColorProperty, value); }
    }

    /// <summary>
    /// Color of the text in the textvox when state is evaluated to Incomplete.
    /// </summary>
    public Brush IncompleteTextColor
    {
        get { return (Brush)GetValue(IncompleteTextColorProperty); }
        set { SetValue(IncompleteTextColorProperty, value); }
    }

    /// <summary>
    /// Background color for when the state is evaluated to Valid.
    /// </summary>
    public Brush ValidBackgroundColor
    {
        get { return (Brush)GetValue(ValidBackgroundColorProperty); }
        set { SetValue(ValidBackgroundColorProperty, value); }
    }

    /// <summary>
    /// Background color for when the state is evaluated to Invalid.
    /// </summary>
    public Brush InvalidBackgroundColor
    {
        get { return (Brush)GetValue(InvalidBackgroundColorProperty); }
        set { SetValue(InvalidBackgroundColorProperty, value); }
    }

    /// <summary>
    /// Background color for when the state is evaluated to Normal.
    /// </summary>
    public Brush NormalBackgroundColor
    {
        get { return (Brush)GetValue(NormalBackgroundColorProperty); }
        set { SetValue(NormalBackgroundColorProperty, value); }
    }

    /// <summary>
    /// Background color for when the state is evaluated to Incomplete.
    /// </summary>
    public Brush IncompleteBackgroundColor
    {
        get { return (Brush)GetValue(IncompleteBackgroundColorProperty); }
        set { SetValue(IncompleteBackgroundColorProperty, value); }
    }

    /// <summary>
    /// Determines whether validation box should colored/highlighted when it is no longer focued.
    /// </summary>
    public bool ValidHighlightWithoutFocus
    {
        get { return (bool)GetValue(ValidHighlightWithoutFocusProperty); }
        set { SetValue(ValidHighlightWithoutFocusProperty, value); }
    }

    /// <summary>
    /// Determines whether validation box should colored/highlighted when it is no longer focued
    /// </summary>
    public bool InvalidHighlightWithoutFocus
    {
        get { return (bool)GetValue(InvalidHighlightWithoutFocusProperty); }
        set { SetValue(InvalidHighlightWithoutFocusProperty, value); }
    }

    /// <summary>
    /// Determines whether validation box should colored/highlighted when it is no longer focued
    /// </summary>
    public bool NormalHighlightWithoutFocus
    {
        get { return (bool)GetValue(NormalHighlightWithoutFocusProperty); }
        set { SetValue(NormalHighlightWithoutFocusProperty, value); }
    }

    /// <summary>
    /// Determines whether validation box should colored/highlighted when it is no longer focued
    /// </summary>
    public bool IncompleteHighlightWithoutFocus
    {
        get { return (bool)GetValue(IncompleteHighlightWithoutFocusProperty); }
        set { SetValue(IncompleteHighlightWithoutFocusProperty, value); }
    }


    /// <summary>
    /// TODO:
    /// </summary>
    public bool StopInputAfterInvalid
    {
        get { return (bool)GetValue(StopInputAfterInvalidProperty); }
        set { SetValue(StopInputAfterInvalidProperty, value); }
    }

    /// <summary>
    /// TODO:
    /// </summary>
    public bool DisableAfterValid
    {
        get { return (bool)GetValue(DisableAfterValidProperty); }
        set { SetValue(DisableAfterValidProperty, value); }
    }

    /// <summary>
    /// Determines whether an icon should be shown when state is evaluated to Valid.
    /// </summary>
    public bool ShowValidIcon;

    /// <summary>
    /// Determines whether an icon should be shown when state is evaluated to Invalid. 
    /// </summary>
    public bool ShowInvalidIcon;

    //TODO: add these properties later.
    //public Image ValidIcon;
    //public Image InvalidIcon;

    #endregion

    #region Routed Events
    public static readonly RoutedEvent ValidationStateChangedEvent =
        EventManager.RegisterRoutedEvent(
            nameof(ValidationStateChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<ValidationBoxState>), 
            typeof(ValidationBox));

    public event RoutedPropertyChangedEventHandler<ValidationBoxState> ValidationStateChanged
    {
        add
        {
            AddHandler(ValidationStateChangedEvent, value);  
        }
        remove
        { 
            RemoveHandler(ValidationStateChangedEvent, value); 
        }
    }

    #endregion

    private ValidationBoxState _state;

    public ValidationBoxState State
    {
        get
        {
            return _state;
        }

        private set
        {
            if (value != _state)
            {
                RaiseEvent(new RoutedPropertyChangedEventArgs<ValidationBoxState>(_state, value, ValidationStateChangedEvent));
                _state = value;
                ChangeAppearance();
            }
        }
    }

    private Storyboard _horizontalShakeStoryboard;
    private Storyboard _verticalShakeStoryboard;

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
        
        _horizontalShakeStoryboard = (Storyboard)FindResource("HorizontalShake");
        _verticalShakeStoryboard = (Storyboard)FindResource("VerticalShake");
        base.OnApplyTemplate();
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        State = TextValidationMethod(Text);
        base.OnTextChanged(e);
    }



    /// <summary>
    /// Change state manager.
    /// </summary>
    private void ChangeAppearance()
    {
        switch (State)
        {
            case ValidationBoxState.Valid:
                ChangeToValid();
                break;
            case ValidationBoxState.Invalid:
                ChangeToInvalid();
                break;
            case ValidationBoxState.Normal:
                ChangeToNormal();
                break;
            case ValidationBoxState.Incomplete:
                ChangeToIncomplete();
                break;
            default:
                break;
        }
    }

    private void ChangeToValid()
    {
        BorderBrush = ValidBorderColor;
        BorderThickness = ValidThickness;
        Background = ValidBackgroundColor;
        Foreground = ValidTextColor;

    }

    private void ChangeToInvalid()
    {
        BorderBrush = InvalidBorderColor;
        BorderThickness = InvalidThickness;
        Background = InvalidBackgroundColor;
        Foreground = InvalidTextColor;
    }

    private void ChangeToNormal()
    {
        BorderBrush = NormalBorderColor;
        BorderThickness = NormalThickness;
        Background = NormalBackgroundColor;
        Foreground = NormalTextColor;
    }

    private void ChangeToIncomplete()
    {
        BorderBrush = IncompleteBorderColor;
        BorderThickness = IncompleteThickness;
        Background = IncompleteBackgroundColor;
        Foreground = IncompleteTextColor;
    
}
