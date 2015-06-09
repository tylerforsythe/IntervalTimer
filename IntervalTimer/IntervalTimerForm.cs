using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
            btnSaveBreak.Visible = false;

            string workMinutesStr = ConfigurationManager.AppSettings["WORK_MINUTES"] ?? "20";
            string restMinutesStr = ConfigurationManager.AppSettings["REST_MINUTES"] ?? "5";
            string restMinutesLongStr = ConfigurationManager.AppSettings["REST_MINUTES_LONG"] ?? "15";

            int workMinutes = 20, restMinutes = 5, restMinutesLong = 15;
            int.TryParse(workMinutesStr, out workMinutes);
            int.TryParse(restMinutesStr, out restMinutes);
            int.TryParse(restMinutesLongStr, out restMinutesLong);

            if (1 == 1) {
                _workSpan = new TimeSpan(0, workMinutes, 0);
                _restSpan = new TimeSpan(0, restMinutes, 0);
                _restLongSpan = new TimeSpan(0, restMinutesLong, 0);
            }
            else {
                _workSpan = new TimeSpan(0, 0, 5);
                _restSpan = new TimeSpan(0, 0, 3);
                _restLongSpan = new TimeSpan(0, 0, 8);
            }
        }

        private Timer _timer = null;
        private TimeSpan _currentRemainingDuration;
        private TimeSpan _oneSecondSpan = new TimeSpan(0, 0, 1);
        private TimerState _state = TimerState.None;

        private int _workChunksComplete = 0;
        private TimeSpan _workSpan;
        private TimeSpan _restSpan;
        private TimeSpan _restLongSpan;
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
                _currentRemainingDuration = _workSpan;
                UpdateTimerDisplay();
                _state = TimerState.Work;
                btnCancel.Visible = true;
                btnSaveBreak.Visible = false;
                _timer.Start();
                PlayAudio("work.mp3");
            }
            else if (_state == TimerState.Work || _state == TimerState.Rest) {
                _timer.Enabled = !_timer.Enabled;
            }
        }
        
        private void TimerOnTick(object sender, EventArgs eventArgs) {
            _currentRemainingDuration = _currentRemainingDuration.Subtract(_oneSecondSpan);
            UpdateTimerDisplay();
            if (_currentRemainingDuration.TotalSeconds <= 0) {
                //  Transition from work to rest
                if (_state == TimerState.Work) {
                    ++_workChunksComplete;
                    _state = TimerState.Rest;
                    btnSaveBreak.Visible = true;
                    //every fourth chunk, they get a longer break
                    _restMinutesAccumulation = _restMinutesAccumulation.Add(
                        _workChunksComplete % 4 == 0 ? _restLongSpan : _restSpan
                    );
                    _currentRemainingDuration = _restMinutesAccumulation;
                    PlayAudio("break.mp3");
                }
                //  Transition from rest to work
                else if (_state == TimerState.Rest) {
                    _restMinutesAccumulation = _currentRemainingDuration;
                    _state = TimerState.Work;
                    btnSaveBreak.Visible = false;
                    _currentRemainingDuration = _workSpan;
                    PlayAudio("work.mp3");
                }
            }
        }

        private void PlayAudio(string soundFileName) {
            var wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = "audio/" + soundFileName;
            wplayer.controls.play();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            var confirmResult = MessageBox.Show("Are you sure you want to cancel?",
                                     "Confirm Cancel",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
                return;

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
            var confirmResult = MessageBox.Show("Are you sure you want to exit?",
                                     "Confirm Exit",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes) {
                Application.Exit();
            }
        }

        private void btnSaveBreak_Click(object sender, EventArgs e) {
            _restMinutesAccumulation = _currentRemainingDuration;
            _state = TimerState.Work;
            _currentRemainingDuration = _workSpan;
            btnSaveBreak.Visible = false;
            PlayAudio("work.mp3");
        }
    }
}
