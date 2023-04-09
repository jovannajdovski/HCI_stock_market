//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Controls.Primitives;
//using System.Windows.Input;

//namespace stockMarket
//{
//    class AutocompleteSelectBox : TextBox
//    {
//        Popup Popup { get { return this.Template.FindName("PART_Popup", this) as Popup; } }
//        ListBox ItemList
//        {
//            get
//            {
//                return
//             this.Template.FindName("PART_ItemList", this) as ListBox;
//            }
//        }
//        ScrollViewer Host
//        {
//            get
//            {
//                return
//             this.Template.FindName("PART_ContentHost", this) as ScrollViewer;
//            }
//        }
//        UIElement TextBoxView
//        {
//            get
//            {
//                foreach (object o in LogicalTreeHelper.GetChildren(Host))
//                    return o as UIElement; return null;
//            }
//        }
//        protected override void OnTextChanged(TextChangedEventArgs e)
//        {
//            if (_loaded)
//            {
//                try
//                {
//                    if (lastPath != Path.GetDirectoryName(this.Text))
//                    {
//                        lastPath = Path.GetDirectoryName(this.Text);
//                        string[] paths = Lookup(this.Text); //Get subdirectory of current

//                        ItemList.Items.Clear();
//                        foreach (string path in paths)
//                            if (!(String.Equals(path, this.Text,
//                              StringComparison.CurrentCultureIgnoreCase)))
//                                ItemList.Items.Add(path);
//                    }

//                    Popup.IsOpen = true;

//                    //I added a Filter so Directory polling is only called once 
//                    //per directory, thus improve speed
//                    ItemList.Items.Filter = p =>
//                    {
//                        string path = p as string;
//                        return path.StartsWith(this.Text, StringComparison.CurrentCultureIgnoreCase) &&
//                         !(String.Equals(path, this.Text, StringComparison.CurrentCultureIgnoreCase));
//                    };
//                }
//                catch
//                {
//                }
//            }
//        }
//        public override void OnApplyTemplate()
//        {
//            base.OnApplyTemplate();
//            _loaded = true;
//            this.KeyDown += new KeyEventHandler(AutoCompleteTextBox_KeyDown);
//            this.PreviewKeyDown += new KeyEventHandler(AutoCompleteTextBox_PreviewKeyDown);
//            ItemList.PreviewMouseDown +=
//                new MouseButtonEventHandler(ItemList_PreviewMouseDown);
//            ItemList.KeyDown += new KeyEventHandler(ItemList_KeyDown);
//        }
//        void AutoCompleteTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.Key == Key.Down && ItemList.Items.Count > 0
//             && !(e.OriginalSource is ListBoxItem))
//            {
//                ItemList.Focus();
//                ItemList.SelectedIndex = 0;
//                ListBoxItem lbi = ItemList.
//                 ItemContainerGenerator.ContainerFromIndex(ItemList.SelectedIndex) as ListBoxItem;
//                lbi.Focus();
//                e.Handled = true;
//            }
//        }
//        void AutoCompleteTextBox_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.Key == Key.Enter)
//            {
//                Popup.IsOpen = false;
//                updateSource();
//            }
//        }
//        void ItemList_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.OriginalSource is ListBoxItem)
//            {
//                ListBoxItem tb = e.OriginalSource as ListBoxItem;
//                Text = (tb.Content as string);
//                if (e.Key == Key.Enter)
//                {
//                    Popup.IsOpen = false;
//                    updateSource();
//                }

//            }
//        }

//        void ItemList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
//        {
//            if (e.LeftButton == MouseButtonState.Pressed)
//            {
//                {
//                    TextBlock tb = e.OriginalSource as TextBlock;
//                    if (tb != null)
//                    {
//                        Text = tb.Text;
//                        updateSource();
//                        Popup.IsOpen = false;
//                        e.Handled = true;
//                    }
//                }
//            }
//        }
//        void updateSource()
//        {
//            if (this.GetBindingExpression(TextBox.TextProperty) != null)
//                this.GetBindingExpression(TextBox.TextProperty).UpdateSource();
//        }
//    }

//}