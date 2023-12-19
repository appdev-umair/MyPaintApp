using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaintApp
{
    public enum DRAWTOOLS
    {
        SELECT, LINE, RECTANGLE, ELLIPSE, TRIANGLE, RHOMBUS, ICE_CREAM_CONE
    }
    public partial class MainForm : Form
    {

        int X1 = 0, Y1 = 0;
        int X2 = 0, Y2 = 0;
        private ColorDialog colorDialog = new ColorDialog();

        bool IsDrawing = false;

        private bool isResizing = false;
        private int resizeHandleSize = 8; // Size of resizing handles
        private int resizeStartX, resizeStartY;
        private Shape selectedShape;

        DRAWTOOLS SelectedTool = DRAWTOOLS.SELECT;

        List<Shape> ShapeList = new List<Shape>();
        private Color shapeOutlineColor = Color.Black;
        private Color shapeFillColor = Color.Black;

        public MainForm()
        {
            InitializeComponent();

        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Check if mouse is over a resizing handle
                selectedShape = GetSelectedShape(e.X, e.Y);

                
                if (selectedShape != null)
                {
                    isResizing = true;
                    resizeStartX = e.X;
                    resizeStartY = e.Y;
                }
                else
                {
                    // Your existing code for drawing shapes
                    X1 = e.X;
                    Y1 = e.Y;
                    IsDrawing = true;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedTool = DRAWTOOLS.LINE;
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedTool = DRAWTOOLS.RECTANGLE;
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedTool = DRAWTOOLS.ELLIPSE;
        }


        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedTool = DRAWTOOLS.TRIANGLE;

        }
        private void rhombusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedTool = DRAWTOOLS.RHOMBUS;

        }

        private void DropToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedTool = DRAWTOOLS.ICE_CREAM_CONE;
        }

        private void selectOutlineColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the selected color to the currently selected shape (assuming one shape can be selected at a time)
                if (selectedShape != null)
                {
                    selectedShape.OutLineColor = colorDialog.Color;
                    selectedShape.FillColor = colorDialog.Color; // Set the fill color

                    Invalidate();
                }

                shapeOutlineColor = colorDialog.Color;
                tsbOutlineColor.BackColor = colorDialog.Color;
                shapeFillColor = colorDialog.Color;
            }
        }

        private void selectColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the selected color to the currently selected shape (assuming one shape can be selected at a time)
                if (selectedShape != null)
                {
                    selectedShape.FillColor = colorDialog.Color; // Set the fill color

                    Invalidate();
                }

                tsbFillColor.BackColor = colorDialog.Color;
                shapeFillColor = colorDialog.Color;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDrawing)
            {
                // Your existing code for drawing shapes
                X2 = e.X;
                Y2 = e.Y;
                Invalidate();
            }
            else if (isResizing && selectedShape != null)
            {
                // Resize the selected shape
                int deltaX = e.X - resizeStartX;
                int deltaY = e.Y - resizeStartY;

                if (selectedShape is Rectangle resizableRect)
                {
                    resizableRect.Width += deltaX;
                    resizableRect.Height += deltaY;
                }
                // Add similar resizing logic for other shapes (Ellipse, Triangle, etc.) if needed

                resizeStartX = e.X;
                resizeStartY = e.Y;
                Invalidate();
            }
        }


        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsDrawing)
            {
                X2 = e.X;
                Y2 = e.Y;
                Invalidate();
                IsDrawing = false;


                switch (SelectedTool)
                {
                    case DRAWTOOLS.SELECT:
                        break;
                    case DRAWTOOLS.LINE:
                        Line line = new Line();
                        line.X = X1;
                        line.Y = Y1;
                        line.X2 = X2;
                        line.Y2 = Y2;
                        line.OutLineColor = shapeOutlineColor;
                        line.OutLineWidth = 2;
                        ShapeList.Add(line);
                        break;
                    case DRAWTOOLS.RECTANGLE:
                        ShapeList.Add(new Rectangle(X1, Y1, X2 - X1, Y2 - Y1, shapeOutlineColor, 2, shapeFillColor));
                        break;
                    case DRAWTOOLS.ELLIPSE:
                        ShapeList.Add(new Ellipse(X1, Y1, X2 - X1, Y2 - Y1, shapeOutlineColor, 2, shapeFillColor));
                        break;
                    case DRAWTOOLS.TRIANGLE:
                        ShapeList.Add(new Triangle(X1, Y1, X2, Y1, X1 + (X2 - X1) / 2, Y2, shapeOutlineColor, 2, shapeFillColor));
                        break;
                    case DRAWTOOLS.RHOMBUS:
                        ShapeList.Add(new Rhombus(X1, Y1, X2 - X1, Y2 - Y1, shapeOutlineColor, 2, shapeFillColor));
                        break;
                    case DRAWTOOLS.ICE_CREAM_CONE:
                        ShapeList.Add(new IceCreamCone(X1, Y1, X2 - X1, Y2 - Y1, Color.Blue, 2));
                        break;




                }

                tssInfo.Text = "Object Count = " + ShapeList.Count;
            }
            else if (isResizing)
            {
                isResizing = false;
                resizeStartX = 0;
                resizeStartY = 0;
            }

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape s in ShapeList)
            {
                s.Draw(e.Graphics);
            }

            if (isResizing && selectedShape != null)
            {
                DrawResizeHandles(e.Graphics, selectedShape);
            }
            Pen pen = new Pen(shapeOutlineColor, 2);

            switch (SelectedTool)
            {
                case DRAWTOOLS.LINE:
                    e.Graphics.DrawLine(pen, X1, Y1, X2, Y2);
                    break;
                case DRAWTOOLS.RECTANGLE:
                    Rectangle rectangle = new Rectangle(X1, Y1, X2 - X1, Y2 - Y1, shapeOutlineColor, 2, shapeFillColor);
                    rectangle.Draw(e.Graphics);
                    break;

                case DRAWTOOLS.ELLIPSE:
                    Ellipse ellipse = new Ellipse(X1, Y1, X2 - X1, Y2 - Y1, shapeOutlineColor, 2, shapeFillColor);
                    ellipse.Draw(e.Graphics);
                    break;

                case DRAWTOOLS.TRIANGLE:
                    Triangle triangle = new Triangle(X1, Y1, X2, Y1, X1 + (X2 - X1) / 2, Y2, shapeOutlineColor, 2, shapeFillColor);
                    triangle.Draw(e.Graphics);
                    break;

                case DRAWTOOLS.RHOMBUS:
                    Rhombus rhombus = new Rhombus(X1, Y1, X2 - X1, Y2 - Y1, shapeOutlineColor, 2, shapeFillColor);
                    rhombus.Draw(e.Graphics);
                    break;




            }


        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Text Files (*.txt)|*.txt";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveToFile(saveDialog.FileName);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Text Files (*.txt)|*.txt";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadFromFile(openDialog.FileName);
                }
            }
        }

        private void SaveToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (Shape shape in ShapeList)
                    {
                        // Serialize the shape information to save in the file
                        string serializedShape = $"{shape.GetType().Name},{shape.X},{shape.Y}," +
                                                 $"{shape.OutLineColor.ToArgb()},{shape.FillColor.ToArgb()}," +
                                                 $"{shape.OutLineWidth}";

                        if (shape is Rectangle rect)
                        {
                            serializedShape += $",{rect.Width},{rect.Height}";
                        }
                        else if (shape is Ellipse ellipse)
                        {
                            serializedShape += $",{ellipse.HDiameter},{ellipse.VDiameter}";
                        }

                        writer.WriteLine(serializedShape);
                    }
                }

                MessageBox.Show("Drawing saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving drawing: {ex.Message}");
            }
        }

        private void LoadFromFile(string filePath)
        {
            try
            {
                // Clear existing shapes before loading new ones
                ShapeList.Clear();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Deserialize the shape information from the file
                        string[] parts = line.Split(',');

                        if (Enum.TryParse(parts[0], out DRAWTOOLS tool) &&
                            int.TryParse(parts[1], out int x) &&
                            int.TryParse(parts[2], out int y) &&
                            int.TryParse(parts[3], out int colorArgb) &&
                            int.TryParse(parts[4], out int fillColorArgb) &&
                            int.TryParse(parts[5], out int outlineWidth))
                        {
                            Color shapeColor = Color.FromArgb(colorArgb);
                            Color shapeFillColor = Color.FromArgb(fillColorArgb);

                            switch (tool)
                            {
                                case DRAWTOOLS.RECTANGLE:
                                    if (int.TryParse(parts[6], out int width) && int.TryParse(parts[7], out int height))
                                    {
                                        ShapeList.Add(new Rectangle(x, y, width, height, shapeColor, outlineWidth, shapeFillColor));
                                    }
                                    break;

/*                                case DRAWTOOLS.ELLIPSE:
                                    if (int.TryParse(parts[6], out int hdia) && int.TryParse(parts[7], out int vdia))
                                    {
                                        ShapeList.Add(new Ellipse(x, y, hdia, vdia, shapeColor, outlineWidth));
                                    }
                                    break;*/

                            }
                        }
                    }
                }

                // Redraw the shapes on the form
                Invalidate();

                MessageBox.Show("Drawing loaded successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading drawing: {ex.Message}");
            }
        }


        private void DrawResizeHandles(Graphics g, Shape shape)
        {
            if (shape is Rectangle resizableRect)
            {
                // Draw resizing handles for a rectangle
                DrawResizeHandle(g, resizableRect.X, resizableRect.Y);                                  // Top-left
                DrawResizeHandle(g, resizableRect.X + resizableRect.Width - resizeHandleSize, resizableRect.Y);    // Top-right
                DrawResizeHandle(g, resizableRect.X + resizableRect.Width - resizeHandleSize, resizableRect.Y + resizableRect.Height - resizeHandleSize);    // Bottom-right
                DrawResizeHandle(g, resizableRect.X, resizableRect.Y + resizableRect.Height - resizeHandleSize);    // Bottom-left
            }
            // Add similar logic for other shapes (Ellipse, Triangle, etc.) if needed
        }
        private void DrawResizeHandle(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.Black, x, y, resizeHandleSize, resizeHandleSize);
        }
        private Shape GetSelectedShape(int x, int y)
        {
            // Check if the mouse is over a resizing handle of any shape
           

            return null;
        }
    }


}
