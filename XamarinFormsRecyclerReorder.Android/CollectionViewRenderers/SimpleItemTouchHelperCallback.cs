using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Support.V7.Widget;
using Android.Graphics;

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
        private bool dragEnabled = true;

        private bool swipeEnabled = false;

        public static float AlphaFull = 1.0f;

        private IItemTouchHelperAdapter mAdapter;

        public SimpleItemTouchHelperCallback(IItemTouchHelperAdapter adapter)
        {
            mAdapter = adapter;
        }

        public override bool IsLongPressDragEnabled
        {
            get
            {
                return dragEnabled;
            }
        }

        public void SetLongPressDragEnabled(bool longPressDragEnabled)
        {
            dragEnabled = longPressDragEnabled;
        }

        public override bool IsItemViewSwipeEnabled
        {
            get
            {
                return swipeEnabled;
            }
        }

        public void SetItemViewSwipeEnabled(bool itemViewSwipeEnabled)
        {
            swipeEnabled = itemViewSwipeEnabled;
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
                if (IsItemViewSwipeEnabled)
                {
                    swipeFlags = ItemTouchHelper.Left | ItemTouchHelper.Right;
                }
                else if (IsLongPressDragEnabled)
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

            // Notify the adapter of the move
            return mAdapter.OnItemMove(source.AdapterPosition, target.AdapterPosition);
        }

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {

        }
    }
}
