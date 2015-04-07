using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoLJson;
using SharpMap;
using SharpMap.Data;
using SharpMap.Data.Providers;
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

        public async Task<Map> InitializeMap(string fileName)
        {
            var fdt = GetRealFeatureDataTable();

            //await Task.Run(() => FillRealDataTable(fdt, fileName));
            await FillRealDataTable(fdt, fileName);
            var p = new GeometryFeatureProvider(fdt);


            var m = new Map();
            Image image = Image.FromFile(@"Resources\Rift.png");
            var backgroundLayer = new GdiImageLayer("Background Image", image);

            var l = new HeatLayer(p, "Data") { LayerName = "HEAT" };
            m.BackgroundLayer.Add(backgroundLayer);

            var ctfac = new ProjNet.CoordinateSystems.Transformations.CoordinateTransformationFactory();
            l.CoordinateTransformation =
                ctfac.CreateFromCoordinateSystems(ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84,
                                                  ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WebMercator);

            m.Layers.Add(l);
            l.ZoomMin = 0;
            l.ZoomMax = m.GetExtents().Width;
            l.OpacityMax = 0.5f;
            l.OpacityMin = 0.9f;
            
            l.HeatColorBlend = HeatLayer.Classic;
            m.ZoomToBox(backgroundLayer.Envelope);
            return m;
        }

        private static FeatureDataTable GetRealFeatureDataTable()
        {
            var res = new FeatureDataTable();
            res.Columns.Add("Oid", typeof(uint));
            res.Columns.Add("Data", typeof(int));

            return res;
        }

        private static async Task FillRealDataTable(FeatureDataTable table, string fileName)
        {
            table.BeginLoadData();
            var factory = GeoAPI.GeometryServiceProvider.Instance.CreateGeometryFactory();

            uint id = 0;
            var points = await PointData(fileName);
            foreach (var data in points)
            {
                var row = (FeatureDataRow)table.LoadDataRow(new object[] { id++, data.PlayerCount }, System.Data.LoadOption.OverwriteChanges);
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

        private static async Task<IEnumerable<LoLJson.Point>> PointData(string fileName)
        {
            return await Parse.GetPoints(fileName);
        }

        private async void BtnLoadData_Click(object sender, System.EventArgs e)
        {
            string fileName = "data.lol";
            mapBox1.Map = await InitializeMap(fileName);
            mapBox1.ZoomToPointer = false;
            mapBox1.EnableShiftButtonDragRectangleZoom = true;
            mapBox1.Refresh();
        }
    }
}
