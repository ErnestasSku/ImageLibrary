﻿using CategoryControls.Datatypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CategoryControls.Interfaces;

interface IValidationBox
{
    public AnimationType AnimationType { get; set; }
    


    

    //public int MyProperty
    //{
    //    get { return (int)GetValue(MyPropertyProperty); }
    //    set { SetValue(MyPropertyProperty, value); }
    //}

    //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    //public static readonly DependencyProperty MyPropertyProperty =
    //    DependencyProperty.Register("MyProperty", typeof(int), typeof(ownerclass), new PropertyMetadata(0));


}
