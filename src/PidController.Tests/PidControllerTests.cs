namespace PidController.Tests;

public class PidControllerTests
{
    private const double Kp = 1.0;
    private const double Ki = 0.5;
    private const double Kd = 0.1;

    // The PIDController instance that will be tested
    private PIDController controller;

    // Set up the test fixture
    [SetUp]
    public void SetUp()
    {
        // Create a new PIDController instance with some arbitrary constants
        controller = new PIDController(Kp, Ki, Kd);
    }

    // Test that the Update method produces the correct output
    [Test]
    public void TestUpdate()
    {
        // Set the setpoint and process variable
        double setpoint = 10.0;
        double processVariable = 5.0;

        // Set the time step
        double dt = 1.0;

        // Call the Update method and save the result
        double output = controller.Update(setpoint, processVariable, dt);

        // Calculate the error
        double error = setpoint - processVariable;

        // Calculate the expected output
        double expectedOutput = Kp * error + Ki * controller.integral + Kd * controller.derivative;

        // Verify that the output is correct
        Assert.AreEqual(expectedOutput, output, 0.001);
    }

    [Test]
    public void TestIntegral()
    {
        // Set the setpoint and process variable
        double setpoint = 10.0;
        double processVariable = 5.0;

        // Set the time step
        double dt = 1.0;

        // Call the Update method multiple times to update the integral term
        controller.Update(setpoint, processVariable, dt);
        controller.Update(setpoint, processVariable, dt);
        controller.Update(setpoint, processVariable, dt);

        // Get the current value of the integral term
        double integral = controller.integral;

        // Calculate the error
        double error = setpoint - processVariable;

        // Calculate the expected output
        double expectedOutput = Kp * error + Ki * integral + Kd * controller.derivative;

        // Verify that the integral term is correct
        Assert.AreEqual(expectedOutput, integral, 0.001);
    }

    [Test]
    public void TestDerivative()
    {
        // Set the setpoint and process variable
        double setpoint = 10.0;
        double processVariable = 5.0;

        // Set the time step
        double dt = 1.0;

        // Call the Update method to update the derivative term
        controller.Update(setpoint, processVariable, dt);

        // Get the current value of the derivative term
        double derivative = controller.derivative;

        // Calculate the error
        double error = setpoint - processVariable;

        // Calculate the expected derivative value
        double expectedDerivative = (error - controller.previousError) / dt;

        // Verify that the derivative term is correct
        Assert.AreEqual(expectedDerivative, derivative, 0.001);
    }


    [Test]
    public void TestLargeError()
    {
        // Set the setpoint and process variable
        double setpoint = 100.0;
        double processVariable = 5.0;

        // Set the time step
        double dt = 1.0;

        // Call the Update method and save the result
        double output = controller.Update(setpoint, processVariable, dt);

        // Calculate the error
        double error = setpoint - processVariable;

        // Calculate the expected output
        double expectedOutput = Kp * error + Ki * controller.integral + Kd * controller.derivative;

        // Verify that the output is correct
        Assert.AreEqual(expectedOutput, output, 0.001);
    }
}