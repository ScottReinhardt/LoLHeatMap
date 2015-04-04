using System.Drawing;
using System.Windows.Forms;
using LoLJson;
using SharpMap.Layers;

namespace LoLHeatMap
{
    public partial class Main : Form
    {
        public const double AbsoluteMinX = -570;
        public const double AbsoluteMaxX = 15220;
        public const double AbsoluteMinY = -420;
        public const double AbsoluteMaxY = 14980;

        public const double MinX = 0;
        public const double MaxX = AbsoluteMaxX + (0 - AbsoluteMinX);
        public const double MinY = 0;
        public const double MaxY = AbsoluteMaxY + (0 - AbsoluteMinY);

        public const double MapMinX = 0;
        public const double MapMinY = 0;
        public const double MapMaxX = 512;
        public const double MapMaxY = 512;

        public Main()
        {
            InitializeComponent();
        }

        public SharpMap.Map InitializeMap(string fileName)
        {
            var fdt = GetRealFeatureDataTable();
            FillRealDataTable(fdt, fileName);

            var p = new SharpMap.Data.Providers.GeometryFeatureProvider(fdt);


            var m = new SharpMap.Map();
            Image image = Image.FromFile("Rift.png");
            var backgroundLayer = new GdiImageLayer("Background Image", image);

            var l = new HeatLayer(p, "Data", .5F) { LayerName = "HEAT" };
            m.BackgroundLayer.Add(backgroundLayer);
            m.Layers.Add(l);
            l.ZoomMin = 0;
            l.ZoomMax = m.GetExtents().Width;
            l.OpacityMax = 0.5f;
            l.OpacityMin = 0.9f;
            
            l.HeatColorBlend = HeatLayer.Fire;
            m.ZoomToBox(backgroundLayer.Envelope);
            return m;
        }

        private static SharpMap.Data.FeatureDataTable GetRealFeatureDataTable()
        {
            var res = new SharpMap.Data.FeatureDataTable();
            res.Columns.Add("Oid", typeof(uint));
            res.Columns.Add("Data", typeof(int));

            return res;
        }

        private static void FillRealDataTable(SharpMap.Data.FeatureDataTable table, string fileName)
        {
            table.BeginLoadData();
            var factory = GeoAPI.GeometryServiceProvider.Instance.CreateGeometryFactory(4326);

            uint id = 0;
            foreach (var data in PointData(fileName))
            {
                var row = (SharpMap.Data.FeatureDataRow)table.LoadDataRow(new object[] { id++, data.PlayerCount }, System.Data.LoadOption.OverwriteChanges);
                row.Geometry = factory.CreatePoint(new GeoAPI.Geometries.Coordinate(TranslateX(data.X), TranslateY(data.Y)));
                
            }
            table.EndLoadData();
        }

        private static double TranslateX(int x)
        {
            return (x - AbsoluteMinX) / (MaxX - MinX) * (MapMaxX - MapMinX);
        }

        private static double TranslateY(int y)
        {
            return (y - AbsoluteMinY) / (MaxY - MinY) * (MapMaxY - MapMinY);
        }

        private static System.Collections.Generic.IEnumerable<LoLJson.Point> PointData(string fileName)
        {
            return Parse.GetPoints(fileName);
        }

        private void BtnLoadData_Click(object sender, System.EventArgs e)
        {
            string fileName = "";
            mapBox1.Map = InitializeMap(fileName);
            mapBox1.ZoomToPointer = false;
            mapBox1.EnableShiftButtonDragRectangleZoom = true;
            mapBox1.Refresh();
        }
    }
}
