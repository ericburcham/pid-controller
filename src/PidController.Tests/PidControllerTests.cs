namespace PidController.Tests;

public class PidControllerTests
{
    // The PIDController instance that will be tested
    private PIDController controller;

    // Set up the test fixture
    [SetUp]
    public void SetUp()
    {
        // Create a new PIDController instance with some arbitrary constants
        controller = new PIDController(1.0, 0.5, 0.1);
    }

    // Test that the Update method produces the correct output
    [Test]
    public void TestUpdate()
    {
        // Set the setpoint and process variable
        var setpoint = 10.0;
        var processVariable = 5.0;

        // Set the time step
        var dt = 1.0;

        // Call the Update method and save the result
        var output = controller.Update(setpoint, processVariable, dt);

        // Verify that the output is correct
        Assert.AreEqual(7.5, output, 0.001);
    }

    [Test]
    public void TestIntegral()
    {
        // Set the setpoint and process variable
        var setpoint = 10.0;
        var processVariable = 5.0;

        // Set the time step
        var dt = 1.0;

        // Call the Update method multiple times to update the integral term
        controller.Update(setpoint, processVariable, dt);
        controller.Update(setpoint, processVariable, dt);
        controller.Update(setpoint, processVariable, dt);

        // Get the current value of the integral term
        var integral = controller.integral;

        // Verify that the integral term is correct
        Assert.AreEqual(3.0, integral, 0.001);
    }

    [Test]
    public void TestDerivative()
    {
        // Set the setpoint and process variable
        var setpoint = 10.0;
        var processVariable = 5.0;

        // Set the time step
        var dt = 1.0;

        // Call the Update method to update the derivative term
        controller.Update(setpoint, processVariable, dt);

        // Get the current value of the derivative term
        var derivative = controller.derivative;

        // Verify that the derivative term is correct
        Assert.AreEqual(-5.0, derivative, 0.001);
    }

    [Test]
    public void TestLargeError()
    {
        // Set the setpoint and process variable
        var setpoint = 100.0;
        var processVariable = 5.0;

        // Set the time step
        var dt = 1.0;

        // Call the Update method and save the result
        var output = controller.Update(setpoint, processVariable, dt);

        // Verify that the output is correct
        Assert.AreEqual(95.0, output, 0.001);
    }
}