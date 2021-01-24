using System;
using Android.Content;
using Android.Support.V7.Widget.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinFormsRecyclerReorder.Controls;
using XamarinFormsRecyclerReorder.Droid.CollectionViewRenderers;

[assembly: ExportRenderer(typeof(CustomCollectionView), typeof(ReorderCollectionViewRenderer))]

namespace XamarinFormsRecyclerReorder.Droid.CollectionViewRenderers
{
    public class ReorderCollectionViewRenderer : GroupableItemsViewRenderer<GroupableItemsView, ReorderGroupableItemsViewAdapter<GroupableItemsView, IGroupableItemsViewSource>, IGroupableItemsViewSource>
    {
        private SimpleItemTouchHelperCallback itemHelperCallback;
        private ItemTouchHelper mItemTouchHelper;

        public ReorderCollectionViewRenderer(Context context) : base(context)
        {
            //ItemsViewAdapter.

        }

        protected override void OnElementChanged(ElementChangedEventArgs<ItemsView> elementChangedEvent)
        {
            base.OnElementChanged(elementChangedEvent);

        }

        protected override ReorderGroupableItemsViewAdapter<GroupableItemsView, IGroupableItemsViewSource> CreateAdapter()
        {
            return new ReorderGroupableItemsViewAdapter<GroupableItemsView, IGroupableItemsViewSource>(ItemsView);
        }

        protected override void UpdateAdapter()
        {
            base.UpdateAdapter();

            if (ItemsViewAdapter != null)
            {
                itemHelperCallback = new SimpleItemTouchHelperCallback(ItemsViewAdapter);

                ItemTouchHelper.Callback callback = itemHelperCallback;
                mItemTouchHelper = new ItemTouchHelper(callback);

                mItemTouchHelper.AttachToRecyclerView(this);
            }
        }
    }
}
