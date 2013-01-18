using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace WindowsFormsApplication1
{
    public partial class Plot : Form
    {
        private PointPairList listQ1;
        private PointPairList listQ1E;
        private PointPairList listQ2;
        private PointPairList listQ2E;
        private PointPairList listQ3;
        private PointPairList listQ3E;


        private static float Q1 { get; set; }
        private static float Q2 { get; set; }
        private static float Q3 { get; set; }
        private static float Q1E { get; set; }
        private static float Q2E { get; set; }
        private static float Q3E { get; set; }

        public float[] add_graph
        {
            get
            {
                return new float[] { Q1, Q1E, Q2, Q2E, Q3, Q3E };
            }
            set
            {
                if (value.Length != 6) throw new Exception("Array must be of length 6.");
                Q1 = value[0]; Q1E = value[1]; Q2 = value[2]; Q2E = value[3]; Q3 = value[4]; Q3E = value[5];
            }
        }
        ZedGraphControl zgc1 = new ZedGraphControl();
        ZedGraphControl zgc2 = new ZedGraphControl();
        ZedGraphControl zgc3 = new ZedGraphControl();
        //ZedGraphControl zgc4 = new ZedGraphControl ();
        private int index;
        private int firsttime = 1;
        public Plot()
        {
            InitializeComponent();
            index = 0;
            listQ1 = new PointPairList();
            listQ1E = new PointPairList();
            listQ2 = new PointPairList();
            listQ2E = new PointPairList();
            listQ3 = new PointPairList();
            listQ3E = new PointPairList();
            this.Location = new Point(400, 100);
            Plot_Load(null, null);
        }

        private void Plot_Load(object sender, EventArgs e)
        {
            CreateGraph(zedGraphControl1, zedGraphControl2, zedGraphControl3);//zedGraphControl4
            timer1.Enabled = true;
            timer1.Start();
        }

        private void CreateGraph(ZedGraphControl zgc1, ZedGraphControl zgc2, ZedGraphControl zgc3)//ZedGraphControl zgc4
        {
            // get a reference to the GraphPane

            GraphPane myPane1 = zgc1.GraphPane;
            GraphPane myPane2 = zgc2.GraphPane;
            GraphPane myPane3 = zgc3.GraphPane;
            //  GraphPane myPane4 = zgc4.GraphPane;

            // Set the Titles
            myPane1.Title.Text = "X Axis";
            myPane1.XAxis.Title.Text = "Times(ms)";
            myPane1.YAxis.Title.Text = "X ( degree)";

            LineItem myCurve1 = myPane1.AddCurve("IMUX",
            listQ1, Color.Red, SymbolType.None);

            //  myCurve = myPane1.AddCurve ( "ENCODER",
            // listQ_1, Color.Blue, SymbolType.None);

            LineItem myCurve1E = myPane1.AddCurve("ENCODERX",
            listQ1E, Color.Blue, SymbolType.None);

            myPane1.XAxis.Scale.Min = 0;
            myPane1.XAxis.Scale.Max = 1000;
            myPane1.XAxis.Scale.MinorStep = 10;
            myPane1.XAxis.Scale.MajorStep = 50;
            myCurve1.IsY2Axis = true;
            zgc1.AxisChange();

            // Set the Titles
            myPane2.Title.Text = "Y Axis";
            myPane2.XAxis.Title.Text = "Times(ms)";
            myPane2.YAxis.Title.Text = "Y ( degree)";

            LineItem myCurve2 = myPane2.AddCurve("IMUY",
                  listQ2, Color.Red, SymbolType.None);
            LineItem myCurve2E = myPane2.AddCurve("ENCODERY",
                  listQ2E, Color.Blue, SymbolType.None);

            myPane2.XAxis.Scale.Min = 0;
            myPane2.XAxis.Scale.Max = 1000;
            myPane2.XAxis.Scale.MinorStep = 10;
            myPane2.XAxis.Scale.MajorStep = 50;

            zgc2.AxisChange();

            // Set the Titles
            myPane3.Title.Text = "Z Axis";
            myPane3.XAxis.Title.Text = "Times (ms)";
            myPane3.YAxis.Title.Text = "Z(degree)";

            LineItem myCurve3 = myPane3.AddCurve("IMUZ",
                  listQ3, Color.Red, SymbolType.None);
            LineItem myCurve3E = myPane3.AddCurve("ENCODERZ",
                  listQ3E, Color.Blue, SymbolType.None);
            myPane3.XAxis.Scale.Min = 0;
            myPane3.XAxis.Scale.Max = 1000;
            myPane3.XAxis.Scale.MinorStep = 10;
            myPane3.XAxis.Scale.MajorStep = 50;

            zgc3.AxisChange();

            // Set the Titles
            /*   myPane4.Title.Text = "Motor Speed";
               myPane4.XAxis.Title.Text = "Times(ms)";
               myPane4.YAxis.Title.Text = "Veclocity ( rpm)";

               LineItem myCurve4 = myPane4.AddCurve("Speed",
                     listQ1, Color.Red, SymbolType.None);
               myPane4.XAxis.Scale.Min = 0;
               myPane4.XAxis.Scale.Max = 1000;
               myPane4.XAxis.Scale.MinorStep = 10;
               myPane4.XAxis.Scale.MajorStep = 50;

               zgc4.AxisChange();*/
        }

        private void AddDataToGraph()
        {


            index++;

            //First Quaternion Redraw

            // Get the first CurveItem in the graph
            LineItem curve1 = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
            LineItem curve1E = zedGraphControl1.GraphPane.CurveList[1] as LineItem;
            // Get the PointPairList
            IPointListEdit list1 = curve1.Points as IPointListEdit;
            IPointListEdit list1E = curve1E.Points as IPointListEdit;

            //if (flag)
            //    list.RemoveAt(0);
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            if (list1 == null)
                return;
            if (list1E == null)
                return;

            list1.Add((double)index, (double)Q1);
            list1E.Add((double)index, (double)Q1E);
            // add new data points to the graph


            //list_2.Add ( index, 0.001 );
            // Keep the X scale at a rolling 30 second interval, with one
            // major step between the max X value and the end of the axis
            Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
            if (index > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = index + xScale.MajorStep;
                xScale.Min = xScale.Max - 1000;
            }

            // force redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();


            //Second Quaternion Redraw

            // Get the first CurveItem in the graph
            LineItem curve2 = zedGraphControl2.GraphPane.CurveList[0] as LineItem;
            LineItem curve2E = zedGraphControl2.GraphPane.CurveList[1] as LineItem;

            // Get the PointPairList
            IPointListEdit list2 = curve2.Points as IPointListEdit;
            IPointListEdit list2E = curve2E.Points as IPointListEdit;
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            if (list2 == null)
                return;
            if (list2E == null)
                return;
            // add new data points to the graph

            list2.Add(index, Q2);
            list2E.Add(index, Q2E);
            Scale xScale2 = zedGraphControl2.GraphPane.XAxis.Scale;
            if (index > xScale2.Max - xScale2.MajorStep)
            {
                xScale2.Max = index + xScale2.MajorStep;
                xScale2.Min = xScale2.Max - 1000;
            }

            // force redraw
            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();


            //Third Quaternion Redraw

            // Get the first CurveItem in the graph
            LineItem curve3 = zedGraphControl3.GraphPane.CurveList[0] as LineItem;
            LineItem curve3E = zedGraphControl3.GraphPane.CurveList[1] as LineItem;
            // Get the PointPairList
            IPointListEdit list3 = curve3.Points as IPointListEdit;
            IPointListEdit list3E = curve3E.Points as IPointListEdit;
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            if (list3 == null)
                return;
            if (list3E == null)
                return;
            // add new data points to the graph


            list3.Add(index, Q3);
            list3E.Add(index, Q3E);
            Scale xScale3 = zedGraphControl3.GraphPane.XAxis.Scale;
            if (index > xScale3.Max - xScale3.MajorStep)
            {
                xScale3.Max = index + xScale3.MajorStep;
                xScale3.Min = xScale3.Max - 1000;
            }
            //list3.Add ( index, 0.01 );

            // force redraw
            zedGraphControl3.AxisChange();
            zedGraphControl3.Invalidate();


            //Fourth Quaternion Redraw

            // Get the first CurveItem in the graph
            //   LineItem curve4 = zedGraphControl4.GraphPane.CurveList[0] as LineItem;

            // Get the PointPairList
            //   IPointListEdit list4 = curve4.Points as IPointListEdit;
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            //   if (list4 == null)
            //      return;
            // add new data points to the graph

            //       list4.Add(index, Q3);
            // list4.Add ( index, -0.01);
            //   Scale xScale4 = zedGraphControl4.GraphPane.XAxis.Scale;
            //   if (index > xScale4.Max - xScale4.MajorStep)
            //  {
            //     xScale4.Max = index + xScale4.MajorStep;
            //    xScale4.Min = xScale4.Max - 1000;
            // }

            // force redraw
            //  zedGraphControl4.AxisChange ();
            // zedGraphControl4.Invalidate();

        }

        private void timer1_Tick( object sender, EventArgs e )
        {
           AddDataToGraph();
        }

        

    }
}
