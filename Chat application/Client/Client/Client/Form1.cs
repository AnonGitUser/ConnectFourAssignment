using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    public partial class ClientForm : Form
    {
        delegate void AppendTextDelegate(string s);
        AppendTextDelegate textAppender;
        Socket serverSocket;
        public ClientForm()
        {
            InitializeComponent();
        }

        //gets local IP address and fills into textbox
        private void ClientForm_Load(object sender, EventArgs e)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            textAppender = new AppendTextDelegate(AppendText);

            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress defaultAddress = null;
            foreach (IPAddress addr in hostEntry.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    defaultAddress = addr;
                    break;
                }
            }

            if (defaultAddress == null) defaultAddress = IPAddress.Loopback;
            tbIP.Text = defaultAddress.ToString();
        }

        //connects to server if possible, otherwise error
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            int port;
            if (serverSocket == null) serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try { port = Int32.Parse(tbPort.Text); }
            catch
            {
                MessageBox.Show("Port number entered incorrectly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbPort.Focus();
                tbPort.SelectAll();
                return;
            }

            if (serverSocket.Connected)
                MessageBox.Show("Already connected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (port < 0 || port > 65535)
            {
                MessageBox.Show("Port number entered incorrectly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbPort.Focus();
                tbPort.SelectAll();
            }
            else if (tbUsername.Text == "")
                MessageBox.Show("Please enter a username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try { serverSocket.Connect(tbIP.Text, port); }
                catch (SocketException ex)
                {
                    MessageBox.Show("Connection failed.\nerror content: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                byte[] byteData = Encoding.UTF8.GetBytes(tbUsername.Text + '\x01' + '\x02');
                serverSocket.Send(byteData);
                tbIP.ReadOnly = true; tbPort.ReadOnly = true; tbUsername.ReadOnly = true;

                AppendText("Connected with server.");
                AsyncObject asyncObject = new AsyncObject(4096, serverSocket);
                serverSocket.BeginReceive(asyncObject.Buffer, 0, asyncObject.BufferSize, 0, ReceiveData, asyncObject);
            }
        }

        //displays data from server
        private void ReceiveData(IAsyncResult asyncResult)
        {
            AsyncObject asyncObject = asyncResult.AsyncState as AsyncObject;
            try { asyncObject.WorkingSocket.EndReceive(asyncResult); }
            catch
            {
                asyncObject.WorkingSocket.Close();
                return;
            }

            string text = Encoding.UTF8.GetString(asyncObject.Buffer);
            string[] tokens = text.Split('\x01');
            try
            {
                if (tokens[1][0] == '\x02') AppendText(tokens[0] + " has joined.");
                else if (tokens[1][0] == '\x03') AppendText(tokens[0] + " has left.");
                else if (tokens[1][0] == '\x04')
                {
                    try
                    {
                        AppendText("The server has been disconnected from the server shutdown.");
                        serverSocket.Close();
                        serverSocket = null;
                        tbIP.ReadOnly = false; tbPort.ReadOnly = false; tbUsername.ReadOnly = false;
                    }
                    catch { }
                    return;
                }
                else AppendText("[recieved] " + tokens[0] + ": " + tokens[1]);
            }
            catch { }

            asyncObject.ClearBuffer();
            try { asyncObject.WorkingSocket.BeginReceive(asyncObject.Buffer, 0, 4096, 0, ReceiveData, asyncObject); }
            catch { asyncObject.WorkingSocket.Close(); }
        }

        //sends data
        private void SendText(string message)
        {
            tbMessage.Clear();
            if (!serverSocket.Connected)
                MessageBox.Show("The server is not running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("No text entered.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbMessage.Focus();
            }
            else
            {
                string address = (serverSocket.LocalEndPoint as IPEndPoint).Address.ToString();
                byte[] byteData = Encoding.UTF8.GetBytes(tbUsername.Text + '\x01' + message);
                serverSocket.Send(byteData);

                //Thread.Sleep(1000); //for async testing
                AppendText("[sent] " + tbUsername.Text + ": " + message);
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            SendText(tbMessage.Text.Trim());
        }

        private void BtnSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendText(tbMessage.Text.Trim());
        }

        private void TbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendText(tbMessage.Text.Trim());
        }

        //terminate connection
        private void Disconnect()
        {
            if (serverSocket != null && serverSocket.Connected)
            {
                byte[] byteData = Encoding.UTF8.GetBytes(tbUsername.Text + "\x01\x03");
                serverSocket.Send(byteData);

                AppendText("Disconnected from the server.");
                serverSocket.Close();
                serverSocket = null;
            }
            tbIP.ReadOnly = false; tbPort.ReadOnly = false; tbUsername.ReadOnly = false;
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void AppendText(string message)
        {
            if (tbChat.InvokeRequired) tbChat.Invoke(textAppender, message);
            else tbChat.Text += "\r\n" + message;
        }

        
    }

    public class AsyncObject
    {
        public byte[] Buffer;
        public Socket WorkingSocket;
        public readonly int BufferSize;

        public AsyncObject(int bufferSize)
        {
            BufferSize = bufferSize;
            Buffer = new byte[BufferSize];
        }

        public AsyncObject(int buffersize, Socket tempSocket)
            : this(buffersize) { WorkingSocket = tempSocket; }

        public void ClearBuffer() { Array.Clear(Buffer, 0, BufferSize); }
    }

}
