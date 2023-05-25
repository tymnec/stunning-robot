using Bluetooth_Controller_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO.Ports;
using Bluetooth_Controller_Application;

namespace Bluetooth_Controller_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public SerialPortProgram SerialPortProgramApp;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            SerialPortProgramApp = new SerialPortProgram();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult ButtonClick(string buttonName)
        {
            SerialPortProgramApp.Execute(buttonName);
            return Ok();
        }
    }

    public class SerialPortProgram
    {
        // Create the serial port with basic settings
        private SerialPort port;
        private bool isConnected;

        public bool IsConnected => isConnected;

        public SerialPortProgram()
        {
            port = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
            isConnected = false;
        }

        public void Execute(string option)
        {
            port.Open();
            Thread.Sleep(500);
            port.Write(option);
            Console.WriteLine(option);
            Thread.Sleep(1000);
            port.Close();
        }
    }
}
