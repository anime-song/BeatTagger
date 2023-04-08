using BeatTagger.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeatTagger.WPF.Views
{
    /// <summary>
    /// MeasureView.xaml の相互作用ロジック
    /// </summary>
    public partial class MeasureView : UserControl
    {
        public MeasureView()
        {
            InitializeComponent();
        }

        private ItemsControl FindItemsControl(DependencyObject child)
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent == null)
            {
                return null;
            }

            if (parent is ItemsControl itemsControl)
            {
                return itemsControl;
            }

            return FindItemsControl(parent);
        }

        private bool isDraggingBeatRectangle;
        private double dragOffsetX;

        private void BeatBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border beatBorder && beatBorder.DataContext is Beat)
            {
                isDraggingBeatRectangle = true;
                beatBorder.CaptureMouse();

                dragOffsetX = e.GetPosition(beatBorder).X;
            }
        }

        private void BeatBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDraggingBeatRectangle)
            {
                return;
            }

            if (sender is Border beatBorder)
            {
                var beat = (Beat)beatBorder.DataContext;
                var itemsControl = FindItemsControl(beatBorder);
                var contentPresenter = itemsControl.ItemContainerGenerator.ContainerFromItem(beat) as ContentPresenter;
                var canvas = VisualTreeHelper.GetParent(contentPresenter) as Canvas;
                var measure = (Measure)canvas.DataContext;

                double leftX = e.GetPosition(canvas).X - (dragOffsetX - beatBorder.ActualWidth / 2.0);
                double width = canvas.ActualWidth;
                double newPosition = Math.Max(0, Math.Min(leftX / width, 1));

                var previousBeat = measure.GetPreviousBeat(beat);
                if (previousBeat is not null)
                {
                    newPosition = Math.Max(newPosition, previousBeat.Position.Value + (beatBorder.ActualWidth / 2.0 / width));
                }
                var nextBeat = measure.GetNextBeat(beat);
                if (nextBeat is not null)
                {
                    newPosition = Math.Min(newPosition, nextBeat.Position.Value - (beatBorder.ActualWidth / 2.0 / width));
                }

                beat.Position.Value = (float)newPosition;
            }
        }

        private void BeatBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border beatBorder && beatBorder.DataContext is Beat)
            {
                isDraggingBeatRectangle = false;
                beatBorder.ReleaseMouseCapture();
            }
        }
    }
}