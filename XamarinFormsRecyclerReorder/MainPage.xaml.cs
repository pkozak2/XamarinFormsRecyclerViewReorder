using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsRecyclerReorder
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Collection.ReorderEnabled = e.Value;
        }

        private void Collection_ItemMoved(object sender, Controls.ItemMovedEventArgs e)
        {
            LastMove.Text = $"Moved from {e.FromPosition} to: {e.ToPosition}";
        }
    }
}
