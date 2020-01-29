

namespace HardCopy
{
    using System;
    using System.Windows.Forms;
    public partial class U_Margin : UserControl
    {
        public U_Margin()
        {
            InitializeComponent();
        }
        MyMargin _value = new MyMargin();
        public int Margin_TOP
        {
            get
            {
                return _value.top;
            }
            set
            {
                numericUpDown2.Value = value;
                _value.top = value;
            }
        }
        public int Margin_BOTTOM
        {
            get
            {
                return _value.bottom;
            }
            set
            {
                numericUpDown4.Value = value;
                _value.bottom = value;
            }
        }
       
        public int Margin_LEFT
        {
            get
            {
                return _value.left;
            }
            set
            {
                numericUpDown3.Value = value;
                _value.left = value;
            }
        }
        public int Margin_RIGHT
        {
            get
            {
                return _value.right;

            }
            set
            {
                numericUpDown1.Value = value;
                _value.right = value;
            }
        }
        public event EventHandler value_changed = null;

      

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            _value.top = (int)numericUpDown2.Value;
            if (value_changed != null)
            {
                value_changed(new MyMargin(Margin_TOP, Margin_BOTTOM, Margin_LEFT, Margin_RIGHT), null);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _value.right = (int)numericUpDown1.Value;
            if (value_changed != null)
            {
                value_changed(new MyMargin(Margin_TOP, Margin_BOTTOM, Margin_LEFT, Margin_RIGHT), null);
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            _value.bottom = (int)numericUpDown4.Value;
            if (value_changed != null)
            {
                value_changed(new MyMargin(Margin_TOP, Margin_BOTTOM, Margin_LEFT, Margin_RIGHT), null);
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            _value.left = (int)numericUpDown3.Value;
            if (value_changed != null)
            {
                value_changed(new MyMargin(Margin_TOP, Margin_BOTTOM, Margin_LEFT, Margin_RIGHT), null);
            }
        }
    }
    class MyMargin
    {
        public int top;
        public int bottom;
        public int left;
        public int right;
        public MyMargin()
        {

        }
        public MyMargin(int top, int bottom, int left, int right)
        {
            this.top = top;
            this.left = left;
            this.right = right;
            this.bottom = bottom;
        }
    }
}
