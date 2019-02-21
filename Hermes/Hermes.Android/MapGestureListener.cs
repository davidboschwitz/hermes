using Android.Views;
namespace Hermes.Droid
{
    class MapGestureListener : GestureDetector.SimpleOnGestureListener
    {
        public override void OnLongPress(MotionEvent e)
        {
            base.OnLongPress(e);
        }

        public override bool OnDoubleTap(MotionEvent e)
        {
            return base.OnDoubleTap(e);
        }

        public override bool OnDoubleTapEvent(MotionEvent e)
        {
            return base.OnDoubleTapEvent(e);
        }

        public override bool OnSingleTapUp(MotionEvent e)
        {
            return base.OnSingleTapUp(e);
        }

        public override bool OnDown(MotionEvent e)
        {
            return base.OnDown(e);
        }

        public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            return base.OnFling(e1, e2, velocityX, velocityY);
        }

        public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            return base.OnScroll(e1, e2, distanceX, distanceY);
        }

        public override void OnShowPress(MotionEvent e)
        {
            base.OnShowPress(e);
        }

        public override bool OnSingleTapConfirmed(MotionEvent e)
        {
            return base.OnSingleTapConfirmed(e);
        }
    }
}