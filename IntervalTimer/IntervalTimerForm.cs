using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntervalTimer
{
    public partial class IntervalTimerForm : Form
    {
        public IntervalTimerForm() {
            InitializeComponent();

            //Hidden panel over the entire thing to be draggable
            panel1.MouseDown += panel1_MouseDown;
            btnCancel.Visible = false;
        }

        private Timer _timer = null;
        private TimeSpan _currentRemainingDuration;
        private TimeSpan _oneSecondSpan = new TimeSpan(0, 0, 1);
        private TimerState _state = TimerState.None;

        private int _workMinutes = 20;
        private int _workChunksComplete = 0;
        private int _restMinutes = 5;
        private int _restLongMinutes = 15;
        private TimeSpan _restMinutesAccumulation = new TimeSpan(0);

        private enum TimerState {
            None,
            Work,
            Rest
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void btnTimerAction_Click(object sender, EventArgs e) {
            if (_state == TimerState.None) {
                _timer = new Timer();
                _timer.Tick += TimerOnTick;
                _timer.Interval = 1000;
                _currentRemainingDuration = new TimeSpan(0, _workMinutes, 0);
                UpdateTimerDisplay();
                _state = TimerState.Work;
                btnCancel.Visible = true;
                _timer.Start();
            }
            else if (_state == TimerState.Work || _state == TimerState.Rest) {
                _timer.Enabled = !_timer.Enabled;
            }
        }
        
        private void TimerOnTick(object sender, EventArgs eventArgs) {
            _currentRemainingDuration = _currentRemainingDuration.Subtract(_oneSecondSpan);
            UpdateTimerDisplay();
            if (_currentRemainingDuration.TotalSeconds <= 0) {
                if (_state == TimerState.Work) {
                    ++_workChunksComplete;
                    _state = TimerState.Rest;
                    //every fourth chunk, they get a longer break
                    _restMinutesAccumulation.Add(
                        new TimeSpan(0, (_workChunksComplete % 4 == 0 ? _restLongMinutes : _restMinutes), 0)
                    );
                    _currentRemainingDuration = _restMinutesAccumulation;
                }
                else if (_state == TimerState.Rest) {
                    _state = TimerState.Work;
                    _restMinutesAccumulation.Add(new TimeSpan(0, _workMinutes, 0));
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            _timer.Stop();
            lblTime.Text = "----";
            _workChunksComplete = 0;
            _restMinutesAccumulation = new TimeSpan(0);
            _state = TimerState.None;
        }

        private void UpdateTimerDisplay() {
            lblTime.Text = _currentRemainingDuration.ToString("g");
        }

        private void btnExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}
