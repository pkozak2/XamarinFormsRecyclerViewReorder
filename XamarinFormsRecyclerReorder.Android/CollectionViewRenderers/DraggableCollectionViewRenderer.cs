using System;
using System.ComponentModel;
using Android.Content;
using Android.Support.V7.Widget.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinFormsRecyclerReorder.Controls;
using XamarinFormsRecyclerReorder.Droid.CollectionViewRenderers;

[assembly: ExportRenderer(typeof(DraggableCollectionView), typeof(DraggableCollectionViewRenderer))]

namespace XamarinFormsRecyclerReorder.Droid.CollectionViewRenderers
{
    public class DraggableCollectionViewRenderer : SelectableItemsViewRenderer<SelectableItemsView, DraggableItemsViewAdapter<SelectableItemsView, IItemsViewSource>, IItemsViewSource>, IOnStartDragListener
    {
        private SimpleItemTouchHelperCallback itemHelperCallback;
        private ItemTouchHelper mItemTouchHelper;

        public DraggableCollectionViewRenderer(Context context) : base(context)
        {
            itemHelperCallback = new SimpleItemTouchHelperCallback();

            ItemTouchHelper.Callback callback = itemHelperCallback;
            mItemTouchHelper = new ItemTouchHelper(callback);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ItemsView> elementChangedEvent)
        {
            base.OnElementChanged(elementChangedEvent);

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs changedProperty)
        {
            base.OnElementPropertyChanged(sender, changedProperty);
            if (changedProperty.PropertyName == DraggableCollectionView.DragEnabledProperty.PropertyName)
            {
                UpdateReorder();
            }
        }

        protected override DraggableItemsViewAdapter<SelectableItemsView, IItemsViewSource> CreateAdapter()
        {
            return new DraggableItemsViewAdapter<SelectableItemsView, IItemsViewSource>(ItemsView, this);
        }

        protected override void UpdateAdapter()
        {
            mItemTouchHelper.AttachToRecyclerView(null);

            base.UpdateAdapter();

            if (ItemsViewAdapter != null)
            {
                itemHelperCallback.SetAdapter(ItemsViewAdapter);

                var list = Element as DraggableCollectionView;

                itemHelperCallback.SetLongPressDragEnabled(list.DragEnabled);

                mItemTouchHelper.AttachToRecyclerView(this);
            }
        }
        protected void UpdateReorder()
        {
            if (Element == null || ItemsViewAdapter == null || itemHelperCallback == null)
            {
                return;
            }
            var list = Element as DraggableCollectionView;

            itemHelperCallback.SetLongPressDragEnabled(list.DragEnabled);
        }

        public void OnStartDrag(ViewHolder viewHolder)
        {

        }

        public void OnItemMove(int fromPosition, int toPosition)
        {
            var list = Element as DraggableCollectionView;

            list.OnItemMoved(fromPosition, toPosition);
        }
    }
}