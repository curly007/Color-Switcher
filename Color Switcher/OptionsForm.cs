using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Color_Switcher {
    public partial class OptionsDialog : Form {
        private int difficulty = 0;

        public OptionsDialog() {
            InitializeComponent();
        }

        public void setDifficulty(int difficulty) {
            this.difficulty = difficulty;
        }

        public int getDifficulty() {
            return difficulty;
        }

        private void optionsDialog_Load(object sender, EventArgs e) {
            switch (difficulty) {
                case 0:
                    difficulty1.Checked = true;
                    break;
                case 1:
                    difficulty2.Checked = true;
                    break;
                case 2:
                    difficulty3.Checked = true;
                    break;
                default:
                    difficulty1.Checked = true;
                    break;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            if (difficulty1.Checked)
                difficulty = 0;

            if (difficulty2.Checked)
                difficulty = 1;

            if (difficulty3.Checked)
                difficulty = 2;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
