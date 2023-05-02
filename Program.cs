using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

namespace NotifyIconExample
{
  /// <summary>
  /// Summary description for NotifyIconForm.
  /// </summary>
  public class NotifyIconForm : System.Windows.Forms.Form
  {
    private System.Windows.Forms.NotifyIcon notifyIcon1;
    private System.Windows.Forms.Button button1;
    private System.ComponentModel.IContainer components;

    public NotifyIconForm()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();

      //
      // TODO: Add any constructor code after InitializeComponent call
      //
      this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();

      this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
      this.button1 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // notifyIcon1
      // 
      this.notifyIcon1.Icon = new Icon("test.ico");
      this.notifyIcon1.Text = "Hello from NotifyIconExample";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(109, 122);
      this.button1.Name = "button1";
      this.button1.TabIndex = 0;
      this.button1.Text = "Hide in tray";
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // NotifyIconForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(292, 266);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                      this.button1});
      this.Name = "NotifyIconForm";
      this.Text = "NotifyIconForm";
      this.ResumeLayout(false);

    }
    #endregion

    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")]
    public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    [DllImport("user32.dll")]
    public static extern int ChangeDisplaySettingsEx(string lpszDeviceName, ref DEVMODE lpDevMode, IntPtr hwnd, int dwflags, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    public struct DEVMODE
    {
      public const int CCHDEVICENAME = 32;
      public const int DM_PELSWIDTH = 0x80000;
      public const int DM_PELSHEIGHT = 0x100000;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
      public string dmDeviceName;
      public short dmSpecVersion;
      public short dmDriverVersion;
      public short dmSize;
      public short dmDriverExtra;
      public int dmFields;
      public int dmPositionX;
      public int dmPositionY;
      public int dmDisplayOrientation;
      public int dmDisplayFixedOutput;
      public short dmColor;
      public short dmDuplex;
      public short dmYResolution;
      public short dmTTOption;
      public short dmCollate;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
      public string dmFormName;
      public short dmLogPixels;
      public int dmBitsPerPel;
      public int dmPelsWidth;
      public int dmPelsHeight;
      public int dmDisplayFlags;
      public int dmDisplayFrequency;
      public int dmICMMethod;
      public int dmICMIntent;
      public int dmMediaType;
      public int dmDitherType;
      public int dmReserved1;
      public int dmReserved2;
      public int dmPanningWidth;
      public int dmPanningHeight;
    }


    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      // Get the primary monitor's device context
      IntPtr hdc = GetDC(IntPtr.Zero);

      // Retrieve the current display settings
      int screenWidth = GetDeviceCaps(hdc, 0x00);
      int screenHeight = GetDeviceCaps(hdc, 0x01);

      // Release the device context
      ReleaseDC(IntPtr.Zero, hdc);

      // Change the resolution to 1920x1080
      DEVMODE devMode = new DEVMODE();
      devMode.dmSize = (short)Marshal.SizeOf(devMode);
      devMode.dmPelsWidth = 1920;
      devMode.dmPelsHeight = 1080;
      devMode.dmFields = DEVMODE.DM_PELSWIDTH | DEVMODE.DM_PELSHEIGHT;

      // Apply the new display settings
      int result = ChangeDisplaySettingsEx(null, ref devMode, IntPtr.Zero, 0x00000004 | 0x00000001, IntPtr.Zero);
      if (result == 0)
      {
        Console.WriteLine("Display settings changed successfully.");
      }
      else
      {
        Console.WriteLine("Failed to change display settings. Error code: " + result);
      }
      Application.Run(new NotifyIconForm());
    }

    private void button1_Click(object sender, System.EventArgs e)
    {
      notifyIcon1.Visible = true;
      this.Visible = false;
    }

    private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
    {
      notifyIcon1.Visible = false;
      this.Visible = true;
    }

  }
}
