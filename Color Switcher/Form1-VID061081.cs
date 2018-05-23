using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace Color_Switcher {
    public partial class MainForm : Form {
        /*
         * functions for randomizing lists
         */

        private struct randomSortItem<T> {
            public T value;
            public double randomValue;

            public randomSortItem(T valueToSort) {
                value = valueToSort;
                Random rng = new Random(Guid.NewGuid().GetHashCode());
                randomValue = rng.NextDouble();
            }
        }

        private T[] randomize<T>(T[] input) {
            List<randomSortItem<T>> sortList = new List<randomSortItem<T>>();

            for (int i = 0; i < input.Length; i++) {
                sortList.Add(new randomSortItem<T>(input[i]));
            }

            sortList.Sort((a, b) => a.randomValue.CompareTo(b.randomValue));

            T[] output = new T[sortList.Count()];

            for (int i = 0; i < sortList.Count(); i++) {
                output[i] = sortList[i].value;
            }

            return output;
        }

        /*
         * variables for tracking color boxes
         */

        private struct colorBox {
            public Color color;
            public bool movable;
            public int initialPosition;

            public colorBox(Color c, bool m, int i) {
                color = c;
                movable = m;
                initialPosition = i;
            }
        }

        private int targetWidth = 360;
        private int targetHeight = 360;

        private int rows = 7;
        private int cols = 7;
        private int selectedBox = -1;


        private Point mouseCoords = new Point(0, 0);
        private bool mouseDown = false;

        private static Color[] colorList =  {
            Color.FromArgb(0xFF, 0x00, 0x00),
            Color.FromArgb(0x00, 0xFF, 0xFF),
            Color.FromArgb(0xFF, 0xFF, 0x00),
            Color.FromArgb(0x00, 0xFF, 0x00)
        };

        private Color colorTopLeft = colorList[0];
        private Color colorTopRight = colorList[1];
        private Color colorBottomLeft = colorList[2];
        private Color colorBottomRight = colorList[3];


        private colorBox[] colorBoxes;

        private void swapColorBoxes(int index1, int index2) {
            // avoid unnecessary work
            if (index1 == index2)
                return;

            // make sure indices are valid
            if (index1 < 0 || index2 < 0 || index1 > colorBoxes.Length || index2 > colorBoxes.Length)
                return;

            // avoid swapping non-movable boxes
            if (!colorBoxes[index1].movable || !colorBoxes[index2].movable)
                return;

            // swap that shit
            colorBox c = colorBoxes[index1];
            colorBoxes[index1] = colorBoxes[index2];
            colorBoxes[index2] = c;
        }

        private void initializeColorBoxes() {
            selectedBox = -1;

            colorBoxes = new colorBox[rows * cols];


            for (int x = 0; x < cols; x++) {
                for (int y = 0; y < rows; y++) {
                    int r1 = (cols - x - 1) * colorTopLeft.R / (cols - 1) + x * colorTopRight.R / (cols - 1);
                    int r2 = (cols - x - 1) * colorBottomLeft.R / (cols - 1) + x * colorBottomRight.R / (cols - 1);
                    int r = (rows - y - 1) * r1 / (rows - 1) + (y * r2) / (rows - 1);

                    int g1 = (cols - x - 1) * colorTopLeft.G / (cols - 1) + x * colorTopRight.G / (cols - 1);
                    int g2 = (cols - x - 1) * colorBottomLeft.G / (cols - 1) + x * colorBottomRight.G / (cols - 1);
                    int g = (rows - y - 1) * g1 / (rows - 1) + (y * g2) / (rows - 1);

                    int b1 = (cols - x - 1) * colorTopLeft.B / (cols - 1) + x * colorTopRight.B / (cols - 1);
                    int b2 = (cols - x - 1) * colorBottomLeft.B / (cols - 1) + x * colorBottomRight.B / (cols - 1);
                    int b = (rows - y - 1) * b1 / (rows - 1) + (y * b2) / (rows - 1);

                    if (y * cols + x == selectedBox) {
                        r = b = g = 0;
                    }

                    int index = y * cols + x;
                    bool movable = (x != 0 && x != cols - 1) || (y != 0 && y != rows - 1);

                    colorBoxes[index] = new colorBox(Color.FromArgb(r, g, b), movable, index);
                }
            }

        }

        private void ShuffleColorBoxes() {
            selectedBox = -1;

            List<colorBox> movableColorBoxes = new List<colorBox>();
            for (int i = 0; i < colorBoxes.Length; i++) {
                if (colorBoxes[i].movable) {
                    movableColorBoxes.Add(colorBoxes[i]);
                }
            }

            colorBox[] randomizedColorBoxes = randomize<colorBox>(movableColorBoxes.ToArray());

            int j = 0;
            for (int i = 0; i < colorBoxes.Length; i++) {
                if (colorBoxes[i].movable) {
                    colorBoxes[i] = randomizedColorBoxes[j++];
                }
            }

            Invalidate();
        }

        private bool isColorBoxPuzzleSolved() {
            for (var i=0; i<colorBoxes.Length; i++) {
                if (colorBoxes[i].initialPosition != i) {
                    return false;
                }
            }

            return true;
        }

        private void newGame() {
            initializeColorBoxes();
            do {
                ShuffleColorBoxes();
            } while (isColorBoxPuzzleSolved());
        }


        /*
         * functions for the form
         */

        public MainForm() {
            InitializeComponent();
        }

        private void Form1_SetClientSize(int NewWidth, int NewHeight) {
            Width = NewWidth + Width - ClientSize.Width;
            Height = NewHeight + Height - ClientSize.Height + menuStrip.Height;
        }

        private void Form1_Load(object sender, EventArgs e) {
            // set color of menu items
            for (int i=0; i< fileToolStripMenuItem.DropDownItems.Count; i++) {
                fileToolStripMenuItem.DropDownItems[i].ForeColor = fileToolStripMenuItem.ForeColor;
                fileToolStripMenuItem.DropDownItems[i].BackColor = fileToolStripMenuItem.BackColor;
            }


            // set size of form

            int colorBoxWidth = targetWidth / cols;
            int colorBoxHeight = targetHeight / rows;

            int formWidth = targetWidth - targetWidth % colorBoxWidth;
            int formHeight = targetHeight - targetHeight % colorBoxHeight;

            while (formWidth < targetWidth)
                formWidth += cols;

            while (formHeight < targetHeight)
                formHeight += rows;

            Form1_SetClientSize(formWidth, formHeight);
            newGame();
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            Graphics formGraphics = e.Graphics;
            SolidBrush myBrush;
            colorBox currentColorBox;

            int w = ClientSize.Width / cols;
            int h = (ClientSize.Height - menuStrip.Height) / rows;

            int r = Math.Min(w, h) / 5;

            for (int x = 0; x < cols; x++) {
                for (int y = 0; y < rows; y++) {

                    int index = y * cols + x;
                    currentColorBox = colorBoxes[index];

                    myBrush = new SolidBrush(currentColorBox.color);
                    formGraphics.FillRectangle(myBrush, new Rectangle(x * w, y * h + menuStrip.Height, w, h));
                    myBrush.Dispose();

                    if (!currentColorBox.movable) {
                        myBrush = new SolidBrush(Color.Black);
                        formGraphics.FillEllipse(myBrush, new Rectangle(x * w + w / 2 - r / 2, y * h + h / 2 - r / 2 + menuStrip.Height, w / 5, h / 5));
                        myBrush.Dispose();
                    }
                }
            }

            if (mouseDown && selectedBox >= 0) {
                currentColorBox = colorBoxes[selectedBox];

                int x = selectedBox % cols;
                int y = selectedBox / cols;

                float scale = 1.3F;

                myBrush = new SolidBrush(Color.Black);
                formGraphics.FillRectangle(myBrush, new Rectangle(x*w, y* h + menuStrip.Height, w, h));
                myBrush.Dispose();


                myBrush = new SolidBrush(currentColorBox.color);
                formGraphics.FillRectangle(myBrush, new Rectangle(mouseCoords.X - (int)(w/2), mouseCoords.Y - (int)(h/2), w, h));
                myBrush.Dispose();
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e) {
            mouseCoords.X = e.X;
            mouseCoords.Y = e.Y;

            if (e.Button != MouseButtons.Left)
                return;

            mouseDown = true;

            int w = ClientSize.Width / cols;
            int h = (ClientSize.Height - menuStrip.Height) / rows;

            var x = mouseCoords.X / w;
            var y = (mouseCoords.Y- menuStrip.Height) / h;

            int index = y * cols + x;

            if (!colorBoxes[index].movable) {
                selectedBox = -1;
            }
            else {
                selectedBox = index;
            }

            Invalidate(); // force repaint
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button==MouseButtons.Left)
                mouseDown = false;

            mouseCoords.X = e.X;
            mouseCoords.Y = e.Y;

            if (e.Button != MouseButtons.Left)
                return;

            mouseDown = true;

            int w = ClientSize.Width / cols;
            int h = (ClientSize.Height - menuStrip.Height) / rows;

            var x = mouseCoords.X / w;
            var y = (mouseCoords.Y - menuStrip.Height) / h;

            int index = y * cols + x;

            if (!colorBoxes[index].movable) {
                selectedBox = -1;
            }
            else {
                if (selectedBox >= 0) {
                    swapColorBoxes(selectedBox, index);
                    selectedBox = -1;
                }
            }

            if (isColorBoxPuzzleSolved()) {
                Invalidate(); // force repaint

                MessageBox.Show("Congratulations!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                do {
                    ShuffleColorBoxes();
                } while (isColorBoxPuzzleSolved());
            }

            Invalidate(); // force repaint
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e) {
            
            mouseCoords.X = e.X;
            mouseCoords.Y = e.Y;

            Invalidate(); // force repaint
        }

        private void MainForm_MouseEnter(object sender, EventArgs e) {
            mouseDown = (mouseDown && Control.MouseButtons == MouseButtons.Left);

            if (!mouseDown)
                selectedBox = -1;

            Invalidate(); // force repaint
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            newGame();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e) {
            optionsDialog options = new optionsDialog();
            DialogResult result = options.ShowDialog();

            if (result == DialogResult.OK) {

            }

            options.Dispose();
        }
    }
}
