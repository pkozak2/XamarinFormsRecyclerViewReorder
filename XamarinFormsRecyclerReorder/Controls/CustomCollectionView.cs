using Xamarin.Forms;

namespace XamarinFormsRecyclerReorder.Controls
{

    public class CustomCollectionView : CollectionView
    {
        public static readonly BindableProperty ReorderEnabledProperty = BindableProperty.Create(nameof(ReorderEnabled), typeof(bool), typeof(CustomCollectionView), false, BindingMode.OneWay);
        public bool ReorderEnabled
        {
            get => (bool)GetValue(ReorderEnabledProperty);
            set => SetValue(ReorderEnabledProperty, value);
        }

    }
}
