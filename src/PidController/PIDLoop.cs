using System.Timers;
using Timer = System.Timers.Timer;

namespace PidController;

public class PIDLoop
{
    // The PIDController instance
    private readonly PIDController controller;

    // The time step
    private readonly double dt;
    private double processVariable;

    // The setpoint and process variable
    private double setpoint;

    // The timer that will be used to call the Update method at regular intervals
    private readonly Timer timer;

    public PIDLoop(double dt)
    {
        // Save the time step
        this.dt = dt;

        // Create a new PIDController instance with some arbitrary constants
        controller = new PIDController(1.0, 0.5, 0.1);

        // Set up the timer
        timer = new Timer(dt);
        timer.Elapsed += OnTimerElapsed;
        timer.Start();
    }

    // The event handler for the timer's Elapsed event
    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        // Call the Update method and save the result
        var output = controller.Update(setpoint, processVariable, dt);

        // Use the output to adjust the process variable (e.g. by applying a correction)
        processVariable += output;
    }
}