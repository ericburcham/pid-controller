namespace PidController;

public class PIDController
{
    private readonly double Kd;

    private readonly double Ki;

    // Proportional, integral, and derivative constants
    private readonly double Kp;
    public double derivative;

    // Integral and derivative terms
    public double integral;

    // The previous error value, used to calculate the derivative
    public double previousError;

    public PIDController(double Kp, double Ki, double Kd)
    {
        this.Kp = Kp;
        this.Ki = Ki;
        this.Kd = Kd;
    }

    public double Update(double setpoint, double processVariable, double dt)
    {
        // Calculate the error
        var error = setpoint - processVariable;

        // Update the integral term
        integral += error * dt;

        // Calculate the derivative
        derivative = (error - previousError) / dt;

        // Save the error for the next iteration
        previousError = error;

        // Calculate the output
        var output = Kp * error + Ki * integral + Kd * derivative;

        return output;
    }
}