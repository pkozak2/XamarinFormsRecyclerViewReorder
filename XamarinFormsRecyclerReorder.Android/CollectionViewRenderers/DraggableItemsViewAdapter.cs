using System;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Views.View;

namespace XamarinFormsRecyclerReorder.Droid.CollectionViewRenderers
{
    /// <summary>
    /// Interface to listen for a move or dismissal event from a ItemTouchHelper.Callback.
    /// </summary>
    public interface IItemTouchHelperAdapter
    {
        /// <summary>
        /// Called when an item has been dragged far enough to trigger a move. This is called every time
        /// an item is shifted, and <strong>not</strong> at the end of a "drop" event.<br/>
        /// <br/>
        /// Implementations should call {@link RecyclerView.Adapter#notifyItemMoved(int, int)} after
        /// adjusting the underlying data to reflect this move.
        /// </summary>
        /// <returns>True if the item was moved to the new adapter position.</returns>
        /// <param name="fromPosition">The start position of the moved item.</param>
        /// <param name="toPosition">Then resolved position of the moved item.</param>
        bool OnItemMove(int fromPosition, int toPosition);
    }

    public class DraggableItemsViewAdapter<TItemsView, TItemsViewSource> : SelectableItemsViewAdapter<TItemsView, TItemsViewSource>, IItemTouchHelperAdapter
        where TItemsView : SelectableItemsView
        where TItemsViewSource : IItemsViewSource
    {
        private IOnStartDragListener mDragStartListener;

        public DraggableItemsViewAdapter(TItemsView groupableItemsView, IOnStartDragListener listener,
            Func<Xamarin.Forms.View, Context, ItemContentView> createView = null) : base(groupableItemsView, createView)
        {
            mDragStartListener = listener;
        }

        public bool OnItemMove(int fromPosition, int toPosition)
        {
            mDragStartListener.OnItemMove(fromPosition, toPosition);
            NotifyItemMoved(fromPosition, toPosition);
            return true;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);
            holder.ItemView.SetOnTouchListener(new TouchListenerHelper(holder, mDragStartListener));
        }
    }
}
