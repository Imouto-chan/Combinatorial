namespace Combinatorial
{
    public partial class CombinatorialForm : Form
    {
        Algorithm algo = new Algorithm();
        Build solution;

        public CombinatorialForm()
        {
            InitializeComponent();

            solution = algo.RunWangAlgorithm(algo.pieces.ToList<Algorithm.StockPiece>(),
                                Algorithm.SheetWidth, Algorithm.SheetLength, Algorithm.Beta, Algorithm.SheetArea);
        }

        private void DrawRectangle(PaintEventArgs e, int x, int y, int width, int height, Brush brush)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rc = new Rectangle(x, y, width, height);
            gp.AddRectangle(rc);
            System.Drawing.Region r = new System.Drawing.Region(gp);
            Graphics gr = e.Graphics;
            gr.FillRegion(brush, r);
        }

        private void CombinatorialForm_Shown(object sender, EventArgs e)
        {
            if (solution != null)
            {
                double totalTrimLoss = (Algorithm.SheetArea - solution.PiecesArea) + solution.InternalTrimLoss;
                logBox.Text += string.Format("Build Found (with beta = {0:F2}):\r\n", Algorithm.Beta);
                logBox.Text += string.Format("Build dimensions: {0} x {1}\r\n", solution.Width, solution.Length);
                logBox.Text += string.Format("Internal Trim Loss: {0}\r\n", solution.InternalTrimLoss);
                logBox.Text += string.Format("Total Trim Loss: {0}\r\n", totalTrimLoss);
                logBox.Text += string.Format("Base StockPiece usage:\r\n");

                for (int i = 0; i < algo.pieces.Count(); i++)
                {
                    foreach (var kv in solution.BaseUsage)
                    {
                        if (algo.pieces[i].Area == kv.Key)
                        logBox.Text += string.Format("  StockPiece {0}: Dimensions {1} x {2}, Count used: {3}\r\n",
                            i, algo.pieces[i].Length, algo.pieces[i].Width, kv.Value);
                    }
                }
                //foreach (var kv in solution.BaseUsage)
                //{
                //    for (int i = 0; i < algo.pieces.Count(); i++)
                //    {
                //        if (algo.pieces[i].Area == kv.Key)
                //            logBox.Text += string.Format("  StockPiece {0}: Dimensions {1} x {2}, Count used: {3}\r\n",
                //                i, algo.pieces[i].Length, algo.pieces[i].Width, kv.Value);
                //    }
                //}
            }
            else
            {
                logBox.Text += string.Format("No solution found under the given constraints.");
            }
        }
    }
}
