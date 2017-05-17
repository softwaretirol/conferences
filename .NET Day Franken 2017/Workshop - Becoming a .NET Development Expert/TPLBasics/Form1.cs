using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPLBasics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //ThreadPool
        //    // STA / MTA - COM Programmierung
        //    //ThreadPool.QueueUserWorkItem(DoWork);

        //    //Thread t = new Thread(DoWork);
        //    //t.IsBackground = true;
        //    //t.Start();

        //    Task<int> t = new Task<int>(DoWork);
        //    t.ContinueWith(previousTask =>
        //    {
        //        label1.Text = previousTask.Result.ToString();
        //    }, TaskScheduler.FromCurrentSynchronizationContext());
        //    t.Start();
        //}


        // async / await -> Task / Task<TResult>
        private async void button1_Click(object sender, EventArgs e)
        {
            //Task<int> t = new Task<int>(DoWork);
            //t.Start();

            //await t;
            //label1.Text = t.Result.ToString();

            var t1= ABC();
            var t2= ABC();
            var t3= ABC();

            // TPL - PLINQ
            //new[] {1, 2, 3, 4}.Where(x => x > 3).AsParallel().ToList();
            //System.Collections.Concurrent.
            // TPL

            await TaskEx.WhenAll(t1, t2, t3);
            using (var conn = new SqlConnection())
            {
                await conn.OpenAsync();
            }
            // WEB: CPU / IO
            // IIS <--- HTTP Request
        }

        private Task ABC()
        {
            //Task.Factory.StartNew()
            TaskEx.Run(() => //.NET 4.5
            {

            });
            return TaskEx.FromResult(42);
        }


        private int DoWork()
        {
            Thread.Sleep(10000);
            int result = 42;
            return result;
            //label1.Invoke(new Action(() => {
            //    label1.Text = result.ToString();
            //}));
        }
    }
}
