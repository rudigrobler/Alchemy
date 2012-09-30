using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace Alchemy.Controls
{
    public class MovableContentControl : ContentControl
    {
        public MovableContentControl()
        {
            this.DefaultStyleKey = typeof(MovableContentControl);
        }

        public event EventHandler MoveCompleted;

        private void BringToFront()
        {
            int zIndex = 1;
            var parent = VisualTreeHelper.GetParent(this) as Canvas;
            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is MovableContentControl)
                {
                    if (child != this)
                    {
                        Canvas.SetZIndex(child as MovableContentControl, 0);
                    }
                    else
                    {
                        Canvas.SetZIndex(child as MovableContentControl, 1);
                    }
                }
            }
        }

        public MovableContentControl[] IntersectWith()
        {
            var parent = VisualTreeHelper.GetParent(this) as Canvas;
            var boundingRect = new Rect(Canvas.GetLeft(this), Canvas.GetTop(this), Width, Height);
            return VisualTreeHelper.FindElementsInHostCoordinates(boundingRect, parent).Where(_ => _ is MovableContentControl).Select(_ => _ as MovableContentControl).ToArray();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var parent = VisualTreeHelper.GetParent(this) as Canvas;
            var thumb = GetTemplateChild("PART_THUMB") as Thumb;
            if (thumb != null)
            {
                thumb.DragStarted += (s, e) =>
                    {
                        BringToFront();
                    };
                thumb.DragDelta += (s, e) =>
                    {
                        double left = Canvas.GetLeft(this);
                        double top = Canvas.GetTop(this);

                        Canvas.SetLeft(this, left + e.HorizontalChange);
                        Canvas.SetTop(this, top + e.VerticalChange);
                    };
                thumb.DragCompleted += (s, e) =>
                    {
                        if (MoveCompleted != null)
                        {
                            MoveCompleted(this, EventArgs.Empty);
                        }
                    };
            }
        }
    }
}
