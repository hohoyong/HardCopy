

namespace HardCopy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Threading;
    public class C_CurrentTime
    {
        ToolStripStatusLabel current_time;
        Form main;
        Thread tr_loop;
        bool is_run = true;
        public C_CurrentTime(Form main, ToolStripStatusLabel current_time)
        {
            this.main = main;
            this.current_time = current_time;
            tr_loop = new Thread(new ThreadStart(() =>
            {
                Action act = new Action(delegate ()
                {
                    if (current_time.IsDisposed == false)
                    {
                        current_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                });
                while (is_run)
                {
                    try
                    {
                        if (main.IsDisposed == false)
                        {
                            main.Invoke(act);
                        }
                    }
                    catch (System.ObjectDisposedException)
                    {

                    }

                    Thread.Sleep(200);
                }
               
                act = null;

            }));
            tr_loop.IsBackground = true;
            tr_loop.Start();
        }

        public void Close()
        {
            is_run = false;
        }

    }
}
