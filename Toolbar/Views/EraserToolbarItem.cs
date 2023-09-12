﻿using System.Windows;
using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    public class EraserToolbarItem : AnnotationToolbarItem
    {
        static EraserToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EraserToolbarItem), new FrameworkPropertyMetadata(typeof(EraserToolbarItem)));
        }

        public EraserToolbarItem()
        {
            DataContext = new EraserToolbarItemViewModel();
        }
    }
}
