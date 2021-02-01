using Android.Support.V7.Widget.Helper;
using Android.Support.V7.Widget;

namespace XamarinFormsRecyclerReorder.Droid.CollectionViewRenderers
{
    /// <summary>
    /// An implementation of ItemTouchHelper.Callback that enables basic drag & drop and
    /// swipe-to-dismiss. Drag events are automatically started by an item long-press.
    /// Expects the RecyclerView.Adapter to listen for 
    /// ItemTouchHelperAdapter callbacks and the RecyclerView.ViewHolder to implement
    /// ItemTouchHelperViewHolder.
    /// </summary>
    public class SimpleItemTouchHelperCallback : ItemTouchHelper.Callback
    {
        /// <summary>
        /// Reorderable adapter
        /// </summary>
        private IItemTouchHelperAdapter _adapter;

        /// <summary>
        /// Private Reorder Enabled property
        /// </summary>
        private bool _dragEnabled = false;

        /// <summary>
        /// Getter of IsDragEnabled
        /// </summary>
        public override bool IsLongPressDragEnabled
        {
            get
            {
                return _dragEnabled;
            }
        }

        /// <summary>
        /// Setter for drag Enabled
        /// </summary>
        /// <param name="dragEnabled">New value</param>
        public void SetLongPressDragEnabled(bool dragEnabled)
        {
            _dragEnabled = dragEnabled;
        }

        /// <summary>
        /// Not used
        /// </summary>
        private bool _swipeEnabled = false;

        /// <summary>
        /// Not Used
        /// </summary>
        public override bool IsItemViewSwipeEnabled
        {
            get
            {
                return _swipeEnabled;
            }
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="itemViewSwipeEnabled"></param>
        public void SetItemViewSwipeEnabled(bool itemViewSwipeEnabled)
        {
            _swipeEnabled = itemViewSwipeEnabled;
        }

        public SimpleItemTouchHelperCallback()
        {
        }

        public void SetAdapter(IItemTouchHelperAdapter adapter)
        {
            _adapter = adapter;
        }

        public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
        {
            int dragFlags = 0;
            int swipeFlags = 0;
            // Set movement flags based on the layout manager
            if (recyclerView.GetLayoutManager() is GridLayoutManager)
            {
                dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down | ItemTouchHelper.Left | ItemTouchHelper.Right;
            }
            else
            {
                //if (IsItemViewSwipeEnabled)
                //{
                //    swipeFlags = ItemTouchHelper.Left | ItemTouchHelper.Right;
                //}
                //else
                if (IsLongPressDragEnabled)
                {
                    dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down;
                    swipeFlags = ItemTouchHelper.Start | ItemTouchHelper.End;
                }
            }
            return MakeMovementFlags(dragFlags, swipeFlags);
        }

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder source, RecyclerView.ViewHolder target)
        {
            if (source.ItemViewType != target.ItemViewType)
            {
                return false;
            }
            if (_adapter == null) return false;
            // Notify the adapter of the move
            return _adapter.OnItemMove(source.AdapterPosition, target.AdapterPosition);
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="viewHolder"></param>
        /// <param name="direction"></param>
        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {

        }
    }
}
