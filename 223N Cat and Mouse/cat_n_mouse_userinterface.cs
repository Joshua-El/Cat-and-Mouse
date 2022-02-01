//Author: Joshua Elmer
//Mail: joshuaelmer@csu.fullerton.edu
//Course: CPSC223N
//Assignment number: Final
//Title: Final


using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class finaluserinterface: Form
{private Label welcome = new Label();
 private Label coordinates = new Label();
 private TextBox x_text = new TextBox();
 private TextBox y_text = new TextBox();
 private TextBox attempts_text = new TextBox();
 private TextBox successes_text = new TextBox();
 private Label attempts = new Label();
 private Label successes = new Label();
 private Button startbutton = new Button();
 private Button pausebutton = new Button();
 private Button quitbutton = new Button();
 private Panel headerpanel = new Panel();
 private Panel controlpanel = new Panel();
 private Size maximumFinalInterfaceSize = new Size(800,700);
 private Size minimumFinalInterfaceSize = new Size(800,700);

 private const double animationClockSpeed = 45.7;
 private const double animationClockInterval = 1000.0/animationClockSpeed;
 int animationClockSpeedInteger = (int)System.Math.Round(animationClockInterval);
 private const double refreshClockSpeed = 24.0;
 private const double refreshClockInterval = 1000.0/refreshClockSpeed;
 int refreshClockSpeedInteger = (int)System.Math.Round(refreshClockInterval);

 private const double radius = 8;
 private double ball_speed_sec;
 private double ball_speed_tics;
 private double delta_x;
 private double delta_y;
 private double direction;
 private double x;
 private double y;
 private double cursor_x;
 private double cursor_y;
 private int attempts_counter = 0;
 private int successes_counter = 0;
 Random generator = new Random();
 private double collision_distance;

 public finaluserinterface()  //Constructor
   {//Set the size of the user interface box.
    MaximumSize = maximumFinalInterfaceSize;
    MinimumSize = minimumFinalInterfaceSize;
    //Initialize text strings
    Text = "Final";
    welcome.Text = "Cat and Mouse by Joshua Elmer";
    attempts.Text = "# Attempts";
    coordinates.Text = "Coordinates of center of ball";
    successes.Text = "# Successes";
    startbutton.Text = "Start";
    pausebutton.Text = "Pause";
    quitbutton.Text = "Quit";

    //Set sizes
    welcome.Size = new Size(800,50);
    successes.Size = new Size(100,30);
    attempts.Size = new Size(100,30);
    coordinates.Size = new Size(240,30);
    x_text.Size = new Size(50,30);
    y_text.Size = new Size(50,30);
    attempts_text.Size = new Size(80,50);
    successes_text.Size = new Size(80,50);
    startbutton.Size = new Size(80,30);
    pausebutton.Size = new Size(80,30);
    quitbutton.Size = new Size(80,30);
    headerpanel.Size = new Size(800,70);
    controlpanel.Size = new Size(800,150);

    //Set colors
    headerpanel.BackColor = Color.LightGreen;
    controlpanel.BackColor = Color.Gold;
    startbutton.BackColor = Color.Red;
    pausebutton.BackColor = Color.Red;
    quitbutton.BackColor = Color.Red;
    BackColor = Color.LightBlue;

    //Set fonts
    welcome.Font = new Font("Times New Roman",26,FontStyle.Bold);
    successes.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    coordinates.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    x_text.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    y_text.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    attempts.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    attempts_text.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    successes_text.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    startbutton.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    pausebutton.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    quitbutton.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    successes.TextAlign = ContentAlignment.MiddleCenter;
    attempts.TextAlign = ContentAlignment.MiddleCenter;

    //Set locations
    welcome.Location = new Point(200,20);
    successes.Location = new Point(450,30);
    successes_text.Location = new Point(550,30);
    attempts.Location = new Point(200,30);
    attempts_text.Location = new Point(300,30);
    coordinates.Location = new Point(200,90);
    x_text.Location = new Point(450,90);
    y_text.Location = new Point(510,90);
    startbutton.Location = new Point(20,30);
    pausebutton.Location = new Point(20,90);
    quitbutton.Location = new Point(700,90);
    headerpanel.Location = new Point(0,0);
    controlpanel.Location = new Point(0,550);

    //Associate the start button with the Enter key of the keyboard
    AcceptButton = startbutton;

    //Add controls to the form
    Controls.Add(headerpanel);
    headerpanel.Controls.Add(welcome);
    Controls.Add(controlpanel);
    controlpanel.Controls.Add(startbutton);
    controlpanel.Controls.Add(pausebutton);
    controlpanel.Controls.Add(quitbutton);
    controlpanel.Controls.Add(successes);
    controlpanel.Controls.Add(attempts);
    controlpanel.Controls.Add(coordinates);
    controlpanel.Controls.Add(x_text);
    controlpanel.Controls.Add(y_text);
    controlpanel.Controls.Add(attempts_text);
    controlpanel.Controls.Add(successes_text);

    startbutton.Click += new EventHandler(start);
    pausebutton.Click += new EventHandler(pause);
    quitbutton.Click += new EventHandler(stoprun);

    //Prepare the clocks
    refreshClock.Enabled = false;
    refreshClock.Interval = refreshClockSpeedInteger;
    refreshClock.Elapsed += new ElapsedEventHandler(Refresh_user_interface);
    finalClock.Enabled = false;
    finalClock.Interval = animationClockSpeedInteger;
    finalClock.Elapsed += new ElapsedEventHandler(Update_final_coordinates);

    //Initialize the ball at the starting point: subtract ball's radius so that (x,y) is the upper corner of the ball.
    x = (double)400-radius;
    y = (double)275+radius;

    //Open this user interface window in the center of the display.
    CenterToScreen();

  }//End of constructor trafficUserinterfadoublece

  private static System.Timers.Timer refreshClock = new System.Timers.Timer();
  private static System.Timers.Timer finalClock = new System.Timers.Timer();

 protected override void OnPaint(PaintEventArgs ee) {
   Graphics graph = ee.Graphics;

   graph.FillEllipse (Brushes.Yellow,
                           (int)System.Math.Round(x),
                           (int)System.Math.Round(y),
                           (int)System.Math.Round(2.0*radius),
                           (int)System.Math.Round(2.0*radius));

   base.OnPaint(ee);
 } // End of Onpaint

 protected override void OnMouseDown(MouseEventArgs me){
   cursor_x = me.X;
   cursor_y = me.Y;
   attempts_counter ++;
   attempts_text.Text = String.Format("{00}",attempts_counter);
   successes_text.Text = String.Format("{00}",successes_counter);
   if(attempts_counter >= 10){
     refreshClock.Enabled = false;
     finalClock.Enabled = false;
   }
   delta_x = delta_x * 1.25;
   delta_y = delta_y * 1.25;
   collision_distance = Math.Sqrt(Math.Pow((x - cursor_x),2) + Math.Pow((y - cursor_y),2));
    if((int)System.Math.Round(collision_distance - radius) <= 0){
      successes_counter ++;
    }
   base.OnMouseDown(me);
 }

 protected void start(Object sender, EventArgs events) {
  System.Console.WriteLine("The animation has started.");
  ball_speed_sec = 80;
  direction = (generator.NextDouble() * 360);
  ball_speed_tics = ball_speed_sec / animationClockSpeed;
  delta_x = ball_speed_tics * Math.Cos(direction);
  delta_y = ball_speed_tics * Math.Sin(direction);
  refreshClock.Enabled = true;
  finalClock.Enabled = true;
  x_text.Text = String.Format("{000}",x);
  y_text.Text = String.Format("{000}",y);
  Invalidate();
}

 // pause the ball location
 protected void pause(Object sender, EventArgs events) {
   System.Console.WriteLine("The animation has been paused.");
   refreshClock.Enabled = false;
   finalClock.Enabled = false;
   Invalidate();
 }

 // updates time accumulated
 protected void Refresh_user_interface(System.Object sender, ElapsedEventArgs even)  //See Footnote #2
 {x_text.Text = String.Format("{000}",x);
  y_text.Text = String.Format("{000}",y);
  Invalidate();
 }//End of Refresh_user_interface

 protected void Update_final_coordinates(System.Object sender, ElapsedEventArgs even) {
   x += delta_x;
   y -= delta_y;
   if ((int)System.Math.Round(x + radius) >= (790 - radius)){
     delta_x = -delta_x;
   }
   if ((int)System.Math.Round(x - radius) <= (-10 + radius)){
     delta_x = -delta_x;
   }
   if ((int)System.Math.Round(y + radius) <= (65 + radius)){
     delta_y = -delta_y;
   }
   if ((int)System.Math.Round(y + radius) >= (550 - radius)){
     delta_y = -delta_y;
   }
 }
// end of Update_final_coordinates

 // closes the program
 protected void stoprun(Object sender, EventArgs events) {
   Close();
 }//End of stoprun

}//End of class finaluserinterface
