using System;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using Timer = System.Windows.Forms.Timer;

namespace PidController.Visualizer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        // Create a PID controller
        PIDController controller = new PIDController(1.0, 0.5, 0.1);

        // Create a timer to update the visualization at regular intervals
        Timer timer = new Timer();

        // Create a gauge to display the output
        Gauge gauge = new Gauge();

        // Set the setpoint and process variable
        double setpoint = 10.0;
        double processVariable = 5.0;

        // Set the time step
        double dt = 1.0;

        public MainForm()
        {
            // Set the timer interval (in milliseconds)
            timer.Interval = 100;

            // Set the timer tick event handler
            timer.Tick += Timer_Tick;

            // Start the timer
            timer.Start();

            // Set up the gauge
            gauge.From = 0;
            gauge.To = 100;
            gauge.Value = 0;

            // Add the gauge to the form
            Controls.Add(gauge);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the PID controller and get the output
            double output = controller.Update(setpoint, processVariable, dt);

            // Update the gauge value
            gauge.Value = output;
        }
    }

}