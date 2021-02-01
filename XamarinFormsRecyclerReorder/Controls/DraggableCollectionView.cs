using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinFormsRecyclerReorder.Controls
{
    public class ItemMovedEventArgs : EventArgs
    {
        public int FromPosition { get; set; }
        public int ToPosition { get; set; }
        public ItemMovedEventArgs() { }
        public ItemMovedEventArgs(int fromPosition, int toPosition) : this()
        {
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }
    }

    public class DraggableCollectionView : SelectableItemsView
    {
        public static readonly BindableProperty DragEnabledProperty = BindableProperty.Create(nameof(DragEnabled), typeof(bool), typeof(DraggableCollectionView), false, BindingMode.OneWay);
        public bool DragEnabled
        {
            get => (bool)GetValue(DragEnabledProperty);
            set => SetValue(DragEnabledProperty, value);
        }

        public static readonly BindableProperty ItemMovedCommandProperty = BindableProperty.Create(nameof(ItemMovedCommand), typeof(Command<ItemMovedEventArgs>), typeof(DraggableCollectionView));
        public Command<ItemMovedEventArgs> ItemMovedCommand
        {
            get => (Command<ItemMovedEventArgs>)GetValue(ItemMovedCommandProperty);
            set => SetValue(ItemMovedCommandProperty, value);
        }

        public event EventHandler<ItemMovedEventArgs> ItemMoved;

        /// <inheritdoc />
        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnItemMoved(int fromPosition, int toPosition)
        {
            var args = new ItemMovedEventArgs(fromPosition, toPosition);
            ItemMoved?.Invoke(this, args);
            ItemMovedCommand?.Execute(args);
        }
    }
}
