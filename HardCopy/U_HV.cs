namespace HardCopy
{
    using System;
    using System.Windows.Forms;
    public partial class U_HV : UserControl
    {
        public U_HV()
        {
            InitializeComponent();
        }
        public int HORIZONTAL
        {
            get
            {
                return (int)numericUpDown1.Value;
            }
            set
            {
                numericUpDown1.Value = value;
            }
        }
        public int VERTICAL
        {
            get
            {
                return (int)numericUpDown2.Value;
            }
            set
            {
                numericUpDown2.Value = value;
            }
        }
        int _left = 0, _right = 0, _top = 0, _bottom = 0;

        public int Margin_Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
                CAL();
            }
        }
        public int Margin_Right
        {
            get
            {
                return _right;
            }
            set
            {
                _right = value;
                CAL();
            }
        }
        public int Margin_TOP
        {
            get
            {
                return _top;
            }
            set
            {
                _top = value;
                CAL();
            }
        }
        public int Margin_BOTTOM
        {
            get
            {
                return _bottom;
            }
            set
            {
                _bottom = value;
                CAL();
            }
        }
        int _width = 1188;
        int _height = 299;
        private void CAL()
        {
            _width = (int)HORIZONTAL - Margin_Left - Margin_Right;
            _height = (int)VERTICAL - Margin_TOP - Margin_BOTTOM;

            label6.Text = _width.ToString();
            label5.Text = _height.ToString();
        }



        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            CAL();
        }
    }
}
