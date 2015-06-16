using Nova.NovaCare.MapMarker;
using Nova.NovaCare.TileMap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nova.Monitoring.Control
{
    /// <summary>
    /// TileMapView.xaml 的交互逻辑
    /// </summary>
    public partial class TileMapView : Window
    {
        private Nova.NovaCare.TileMap.Core.MapMarker _currentMarker;



        public TileMapView()
        {
            InitializeComponent();

            tileMap.Position = new PointLatLng(34.210170, 108.869360);//34.210170, 108.869360
            tileMap.MapType = MapType.GoogleMapChina;
            tileMap.Manager.Mode = AccessMode.ServerAndCache;
            tileMap.Zoom = 3;
            CurrentPosition = tileMap.Position;
            _currentMarker = new Nova.NovaCare.TileMap.Core.MapMarker(tileMap.Position);
            {
                _currentMarker.Shape = new PositionMarker(_currentMarker);
                _currentMarker.Offset = new System.Windows.Point(-10.5, -55.5);
                _currentMarker.ZIndex = int.MaxValue;
                tileMap.Markers.Add(_currentMarker);
            }
        }

        public PointLatLng CurrentPosition { get; set; }

        private void tileMap_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(tileMap);
            _currentMarker.Position = tileMap.FromLocalToLatLng((int)p.X, (int)p.Y);
            CurrentPosition = _currentMarker.Position;
        }



        private void DragLeft_Click(object sender, RoutedEventArgs e)
        {
            tileMap.MoveMap(50, 0);
        }

        private void DragRight_Click(object sender, RoutedEventArgs e)
        {
            tileMap.MoveMap(-50, 0);
        }

        private void DragTop_Click(object sender, RoutedEventArgs e)
        {
            tileMap.MoveMap(0, 50);
        }

        private void DragBottom_Click(object sender, RoutedEventArgs e)
        {
            tileMap.MoveMap(0, -50);
        }

        private void DragHand_Click(object sender, RoutedEventArgs e)
        {
            base.Cursor = Cursors.Hand;
        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            //Messenger.Default.Send<PointLatLng>(_currentMarker.Position, "Position");
            DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
